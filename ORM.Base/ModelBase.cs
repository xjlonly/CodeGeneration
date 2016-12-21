using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ORM.Base
{
    [Serializable]
    public class ModelBase
    {
        private static Loger log = new Loger();

        #region 内部变量
        /// <summary>
        /// 输出字段构造
        /// </summary>
        [NonSerialized]
        private StringBuilder OutCols = new StringBuilder();
        /// <summary>
        /// 分页输出列表
        /// </summary>
        [NonSerialized]
        private StringBuilder PageCols = new StringBuilder();
        /// <summary>
        /// 连接文本构造
        /// </summary>
        [NonSerialized]
        private StringBuilder JoinTxt = new StringBuilder();
        /// <summary>
        /// 表-伪名字典
        /// </summary>
        [NonSerialized]
        private Dictionary<string, string> TabNameDic = new Dictionary<string, string>();
        /// <summary>
        /// 表-字段列表字典
        /// </summary>
        [NonSerialized]
        private Dictionary<string, List<string>> TabColDic = new Dictionary<string, List<string>>();
        /// <summary>
        /// 表-类字典
        /// </summary>
        [NonSerialized]
        private Dictionary<string, string> TabClassDic = new Dictionary<string, string>();
        /// <summary>
        /// 输出表伪名列表
        /// </summary>
        [NonSerialized]
        private List<string> OutTabList = new List<string>();
        /// <summary>
        /// 表-程序集字典
        /// </summary>
        [NonSerialized]
        private Dictionary<string, string> TabAssemblyDic = new Dictionary<string, string>();
        /// <summary>
        /// 数据库链接字符串标记
        /// </summary>
        protected string _ConnectionMark = string.Empty;
        #endregion

        public ModelBase()
        {
        }
        /// <summary>
        /// 通用初始化
        /// </summary>
        protected void Init()
        {
            JoinTxt.Append("[").Append(this._tabinfo.TableName).Append("] AS SRCTAB ");
            TabNameDic.Add("SRCTAB", this._tabinfo.TableName);
            TabColDic.Add("SRCTAB", this._tabinfo.ColList);
            TabClassDic.Add("SRCTAB", this._tabinfo.TypeName);
            TabAssemblyDic.Add("SRCTAB", this._tabinfo.AssemblyName);
            foreach (string colName in _tabinfo.ColList)
            {
                if (OutCols.Length == 0)
                {
                    OutCols.Append("SRCTAB.").Append("[").Append(colName.ToUpper()).Append("] AS SRCTAB_").Append(colName);
                }
                else
                {
                    OutCols.Append(",").Append("SRCTAB.").Append("[").Append(colName.ToUpper()).Append("] AS SRCTAB_").Append(colName);
                }
                if (PageCols.Length == 0)
                {
                    PageCols.Append("SRCTAB_").Append(colName);
                }
                else
                {
                    PageCols.Append(",SRCTAB_").Append(colName);
                }
            }
        }

        #region 最终SQL
        /// <summary>
        /// 最终SQL，连接查询全数据列
        /// </summary>
        [JsonIgnore]
        public string SQLTEXT
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ").Append(OutCols.ToString().TrimEnd(',')).Append(" FROM ")
                    .Append(JoinTxt.ToString());
                return sql.ToString();
            }
        }

        /// <summary>
        /// 最终SQL，记录总数
        /// </summary>
        [JsonIgnore]
        public string SQLTEXT_COUNT
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT COUNT(1) FROM ")
                    .Append(JoinTxt.ToString());
                return sql.ToString();
            }
        }

        private string SingleSql = "";
        /// <summary>
        /// 最终SQL，单项函数，如：sum(),count()等
        /// </summary>
        [JsonIgnore]
        public string SQLTEXT_SINGLE
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ").Append(SingleSql.TrimEnd(',')).Append(" FROM ")
                    .Append(JoinTxt.ToString());
                return sql.ToString();
            }
        }

        private string PageSort = "";
        /// <summary>
        /// 最终SQL，分页
        /// </summary>
        [JsonIgnore]
        public string SQLTEXT_PAGE
        {
            get
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("WITH PST(RN,").Append(PageCols.ToString().TrimEnd(',')).Append(")AS(");
                sql.Append("SELECT ROW_NUMBER() OVER(ORDER BY ").Append(PageSort).Append(") RN,").Append(OutCols.ToString().TrimEnd(',')).Append(" FROM ")
                    .Append(JoinTxt.ToString()).Append(")");
                sql.Append("SELECT RN,").Append(PageCols.ToString().TrimEnd(',')).Append(" FROM PST WHERE RN BETWEEN @SI AND @EI;");
                sql.Append("SELECT COUNT(1) FROM ").Append(JoinTxt.ToString());
                return sql.ToString();
            }
        }
        #endregion

        #region 查询命令
        /// <summary>
        /// 查询命令
        /// </summary>
        [NonSerialized]
        [JsonIgnore]
        private SqlCommand MyCmd = new SqlCommand();
        #endregion

        #region 添加输出字段
        /// <summary>
        /// 添加输出字段
        /// </summary>
        /// <param name="JoinTableColList"></param>
        /// <param name="SName"></param>
        private void AddOutCols(List<string> JoinTableColList, string SName)
        {
            foreach (string colName in JoinTableColList)
            {
                OutCols.Append(",").Append(SName).Append(".[").Append(colName).Append("]").Append(" AS ").Append(SName).Append("_").Append(colName);
                PageCols.Append(",").Append(SName).Append("_").Append(colName);
            }
        }
        #endregion

        #region 条件检查
        /// <summary>
        /// 条件检查
        /// </summary>
        /// <param name="JoinCondition"></param>
        private void CheckJoinCols(string JoinCondition, out string SafeCondition)
        {
            StringBuilder SafeConditionTxt = new StringBuilder(JoinCondition);
            SafeConditionTxt.Replace("[", "").Replace("]", "");
            Regex JoinArrayReg = new Regex(@"([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})\.[\[]?([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})[\]]?", RegexOptions.IgnoreCase);
            MatchCollection MC = JoinArrayReg.Matches(JoinCondition);
            if (MC != null && MC.Count > 0)
            {
                foreach (Match m in MC)
                {
                    string TabName = m.Groups[1].Value;
                    string ColName = m.Groups[2].Value;

                    if (!TabNameDic.ContainsKey(TabName))
                    {
                        throw new Exception(string.Concat(TabName, "表不存在"));
                    }

                    if (!TabColDic[TabName].Contains(ColName.ToUpper()))
                    {
                        throw new Exception(string.Concat(TabName, ":[", TabNameDic[TabName], "]", "表不存在字段", ColName));
                    }
                    SafeConditionTxt = SafeConditionTxt.Replace(string.Concat(TabName, ".", ColName), string.Concat(TabName, ".[", ColName, "]"));

                    if (!OutTabList.Contains(TabName))
                    {
                        OutTabList.Add(TabName);
                    }
                }
                SafeCondition = SafeConditionTxt.ToString();
            }
            else
            {
                SafeCondition = JoinCondition;
            }
        }
        /// <summary>
        /// 输出字段变换
        /// </summary>
        /// <param name="JoinCondition"></param>
        private void CheckOutCols(string OutColumns)
        {
            Regex JoinArrayReg = new Regex(@"([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30})\.[\[]?([a-zA-Z_]{1,30}[0-9a-zA-Z_]{0,30}|[\*]?)[\]]?", RegexOptions.IgnoreCase);
            MatchCollection MC = JoinArrayReg.Matches(OutColumns);
            StringBuilder OutputTxt = new StringBuilder();
            if (MC != null && MC.Count > 0)
            {
                foreach (Match m in MC)
                {
                    string TabName = m.Groups[1].Value;
                    string ColName = m.Groups[2].Value;

                    if (!TabNameDic.ContainsKey(TabName))
                    {
                        throw new Exception(string.Concat(TabName, "表不存在"));
                    }

                    if (ColName == "*")
                    {
                        foreach (string col in TabColDic[TabName])
                        {
                            if (OutputTxt.Length == 0)
                            {
                                OutputTxt.Append(string.Concat(TabName, ".", col, " AS ", TabName, "_", col));
                            }
                            else
                            {
                                OutputTxt.Append(",").Append(string.Concat(TabName, ".", col, " AS ", TabName, "_", col));
                            }
                        }
                    }
                    else
                    {
                        if (!TabColDic[TabName].Contains(ColName))
                        {
                            throw new Exception(string.Concat(TabName, ":[", TabNameDic[TabName], "]", "表不存在字段", ColName));
                        }

                        if (OutputTxt.Length == 0)
                        {
                            OutputTxt.Append(string.Concat(TabName, ".", ColName, " AS ", TabName, "_", ColName));
                        }
                        else
                        {
                            OutputTxt.Append(",").Append(string.Concat(TabName, ".", ColName, " AS ", TabName, "_", ColName));
                        }
                    }

                    if (!OutTabList.Contains(TabName))
                    {
                        OutTabList.Add(TabName);
                    }
                }
                this.OutCols.Clear();
                this.OutCols.Append(OutputTxt.ToString());
            }
        }
        #endregion

        #region 内连接
        /// <summary>
        /// 内连接,基础表伪名固定:SRCTAB
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="SName"></param>
        /// <returns></returns>
        public ModelBase INNER_JOIN(TabInfo tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            TabNameDic.Add(SName, tab.TableName);
            TabColDic.Add(SName, tab.ColList);
            TabClassDic.Add(SName, tab.TypeName);
            TabAssemblyDic.Add(SName, tab.AssemblyName);
            JoinTxt.Append(" INNER JOIN ").Append("[").Append(tab.TableName).Append("]").Append(" AS ").Append(SName);
            AddOutCols(tab.ColList, SName);
            string SafeCondition = "";
            CheckJoinCols(JoinCondition, out SafeCondition);
            JoinTxt.Append(" ON ").Append(SafeCondition);
            BuildCommand(string.Concat(" ON ", SafeCondition), ParamsList);
            return this;
        }
        #endregion

        #region 左连接
        /// <summary>
        /// 左连接
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public ModelBase LEFT_JOIN(TabInfo tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            TabNameDic.Add(SName, tab.TableName);
            TabColDic.Add(SName, tab.ColList);
            TabClassDic.Add(SName, tab.TypeName);
            TabAssemblyDic.Add(SName, tab.AssemblyName);
            JoinTxt.Append(" LEFT OUTER JOIN ").Append("[").Append(tab.TableName).Append("]").Append(" AS ").Append(SName);
            AddOutCols(tab.ColList, SName);
            string SafeCondition = "";
            CheckJoinCols(JoinCondition, out SafeCondition);
            JoinTxt.Append(" ON ").Append(SafeCondition);
            BuildCommand(string.Concat(" ON ", SafeCondition), ParamsList);
            return this;
        }
        #endregion

        #region 右连接
        /// <summary>
        /// 右连接
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public ModelBase RIGHT_JOIN(TabInfo tab, string SName, string JoinCondition, object[] ParamsList = null)
        {
            TabNameDic.Add(SName, tab.TableName);
            TabColDic.Add(SName, tab.ColList);
            TabClassDic.Add(SName, tab.TypeName);
            TabAssemblyDic.Add(SName, tab.AssemblyName);
            JoinTxt.Append(" RIGHT OUTER JOIN ").Append("[").Append(tab.TableName).Append("]").Append(" AS ").Append(SName);
            AddOutCols(tab.ColList, SName);
            string SafeCondition = "";
            CheckJoinCols(JoinCondition, out SafeCondition);
            JoinTxt.Append(" ON ").Append(SafeCondition);
            BuildCommand(string.Concat(" ON ", SafeCondition), ParamsList);
            return this;
        }
        #endregion

        #region 数据筛选
        /// <summary>
        /// 数据筛选
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="ParamsList"></param>
        /// <returns></returns>
        public ModelBase Where(string Condition, object[] ParamsList = null)
        {
            string SafeCondition = "";
            CheckJoinCols(Condition, out SafeCondition);
            JoinTxt.Append(" WHERE ").Append(SafeCondition);
            BuildCommand(SafeCondition, ParamsList);
            return this;
        }
        #endregion

        #region 指定列输出
        /// <summary>
        /// 指定列输出
        /// </summary>
        /// <param name="OutColumns"></param>
        /// <returns></returns>
        public ModelBase OUT(string OutColumns)
        {
            CheckOutCols(OutColumns);
            return this;
        }
        #endregion

        #region 输出结果
        /// <summary>
        /// 输出列表
        /// </summary>
        /// <param name="UseReadOnlyDataSource">是否使用只读数据源</param>
        /// <returns></returns>
        public List<QueryResult> List(bool UseReadOnlyDataSource = true)
        {
            string DataSourceRWMark = "true";
            if (UseReadOnlyDataSource)
            {
                DataSourceRWMark = "_READ";
            }
            else
            {
                DataSourceRWMark = "_WRITE";
            }
            DataTable DT = new DataTable();
            using (SqlConnection MyConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionMark + DataSourceRWMark].ConnectionString))
            {
                try
                {
                    this.MyCmd.Connection = MyConn;
                    this.MyCmd.CommandText = this.SQLTEXT;
                    SqlDataAdapter MyAdp = new SqlDataAdapter(MyCmd);
                    MyAdp.Fill(DT);
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Close();
                    }
                }
                catch (Exception e)
                {
                    log.Save(e, MyCmd);
                }
            }

            List<QueryResult> jlist = new List<QueryResult>();
            if (DT != null && DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    QueryResult jr = new QueryResult();
                    foreach (string tabSName in OutTabList)
                    {
                        Assembly Asm = LoadAssemblyFromFile(TabAssemblyDic[tabSName]);
                        Type t = Asm.GetType(TabClassDic[tabSName]);
                        object tobj = Asm.CreateInstance(TabClassDic[tabSName]);
                        System.Reflection.ConstructorInfo[] constructorInfoArray = t.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                        Type[] ParameterTypes = new Type[] { dr.GetType() };
                        ConstructorInfo CallConstructFun = t.GetConstructor(ParameterTypes);

                        DataTable TempDT = new DataTable();

                        foreach (DataColumn dc in DT.Columns)
                        {
                            if (dc.ColumnName.StartsWith(tabSName + "_"))
                            {
                                TempDT.Columns.Add(dc.ColumnName.Substring((tabSName + "_").Length));
                            }
                        }
                        DataRow TempDr = TempDT.NewRow();
                        foreach (DataColumn dc in TempDT.Columns)
                        {
                            TempDr[dc.ColumnName] = dr[tabSName + "_" + dc.ColumnName];
                        }
                        TempDT.Rows.Add(TempDr);

                        tobj = CallConstructFun.Invoke(new object[] { TempDr });
                        jr.RowResultList.Add(TabNameDic[tabSName], tobj);
                    }
                    jlist.Add(jr);
                }
            }

            return jlist;
        }
        /// <summary>
        /// 输出列表
        /// </summary>
        /// <param name="UseReadOnlyDataSource">是否使用只读数据源</param>
        /// <returns></returns>
        public List<QueryResult> List(string sqlSort, bool UseReadOnlyDataSource = true)
        {
            string DataSourceRWMark = "true";
            if (UseReadOnlyDataSource)
            {
                DataSourceRWMark = "_READ";
            }
            else
            {
                DataSourceRWMark = "_WRITE";
            }
            DataTable DT = new DataTable();
            using (SqlConnection MyConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionMark + DataSourceRWMark].ConnectionString))
            {
                try
                {
                    this.MyCmd.Connection = MyConn;
                    this.MyCmd.CommandText = this.SQLTEXT + " ORDER BY " + sqlSort;
                    SqlDataAdapter MyAdp = new SqlDataAdapter(MyCmd);
                    MyAdp.Fill(DT);
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Close();
                    }
                }
                catch (Exception e)
                {
                    log.Save(e, MyCmd);
                }
            }

            List<QueryResult> jlist = new List<QueryResult>();
            if (DT != null && DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    QueryResult jr = new QueryResult();
                    foreach (string tabSName in OutTabList)
                    {
                        Assembly Asm = LoadAssemblyFromFile(TabAssemblyDic[tabSName]);
                        Type t = Asm.GetType(TabClassDic[tabSName]);
                        object tobj = Asm.CreateInstance(TabClassDic[tabSName]);
                        System.Reflection.ConstructorInfo[] constructorInfoArray = t.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                        Type[] ParameterTypes = new Type[] { dr.GetType() };
                        ConstructorInfo CallConstructFun = t.GetConstructor(ParameterTypes);

                        DataTable TempDT = new DataTable();

                        foreach (DataColumn dc in DT.Columns)
                        {
                            if (dc.ColumnName.StartsWith(tabSName + "_"))
                            {
                                TempDT.Columns.Add(dc.ColumnName.Substring((tabSName + "_").Length));
                            }
                        }
                        DataRow TempDr = TempDT.NewRow();
                        foreach (DataColumn dc in TempDT.Columns)
                        {
                            TempDr[dc.ColumnName] = dr[tabSName + "_" + dc.ColumnName];
                        }
                        TempDT.Rows.Add(TempDr);

                        tobj = CallConstructFun.Invoke(new object[] { TempDr });
                        jr.RowResultList.Add(TabNameDic[tabSName], tobj);
                    }
                    jlist.Add(jr);
                }
            }

            return jlist;
        }

        /// <summary>
        /// 输出列表,执行成功返回记录总数，执行错误返回-1
        /// </summary>
        /// <param name="UseReadOnlyDataSource">是否使用只读数据源</param>
        /// <returns></returns>
        public long Count(bool UseReadOnlyDataSource = true)
        {
            string DataSourceRWMark = "true";
            if (UseReadOnlyDataSource)
            {
                DataSourceRWMark = "_READ";
            }
            else
            {
                DataSourceRWMark = "_WRITE";
            }
            object CountObject = null;
            using (SqlConnection MyConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionMark + DataSourceRWMark].ConnectionString))
            {
                try
                {
                    this.MyCmd.Connection = MyConn;
                    this.MyCmd.CommandText = this.SQLTEXT_COUNT;
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Open();
                    }
                    CountObject = MyCmd.ExecuteScalar();
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Close();
                    }
                }
                catch (Exception e)
                {
                    log.Save(e, MyCmd);
                }
            }
            if (CountObject != null && !string.IsNullOrEmpty(CountObject.ToString()))
            {
                long CountVal = 0;
                long.TryParse(CountObject.ToString(), out CountVal);
                return CountVal;
            }
            return -1;
        }

        /// <summary>
        /// 输出列表,输出自定义输出函数结果集,如：sum(price),sum(amount),执行错误返回Null
        /// </summary>
        /// <param name="FunctionList">自定义函数列表，如：sum(price),sum(amount)</param>
        /// <param name="UseReadOnlyDataSource">是否使用只读数据源</param>
        /// <returns></returns>
        public DataTable Function(string FunctionList, bool UseReadOnlyDataSource = true)
        {
            this.SingleSql = FunctionList;
            string DataSourceRWMark = "true";
            if (UseReadOnlyDataSource)
            {
                DataSourceRWMark = "_READ";
            }
            else
            {
                DataSourceRWMark = "_WRITE";
            }
            DataTable DT = new DataTable();
            using (SqlConnection MyConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionMark + DataSourceRWMark].ConnectionString))
            {
                try
                {
                    this.MyCmd.Connection = MyConn;
                    this.MyCmd.CommandText = this.SQLTEXT_SINGLE;
                    SqlDataAdapter MyAdp = new SqlDataAdapter(MyCmd);
                    MyAdp.Fill(DT);
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Close();
                    }
                }
                catch (Exception e)
                {
                    log.Save(e, MyCmd);
                }
            }
            return DT;
        }

        /// <summary>
        /// 输出列表，分页
        /// </summary>
        /// <param name="sqlSort">排序规则，如：SRCTAB.Create Desc</param>
        /// <param name="RecordCount">输出：记录总数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="UseReadOnlyDataSource">是否使用只读数据源</param>
        /// <returns></returns>
        public List<QueryResult> List(string sqlSort, out long RecordCount, int PageIndex = 1, int PageSize = 20, bool UseReadOnlyDataSource = true)
        {
            RecordCount = 0;
            string DataSourceRWMark = "true";
            if (UseReadOnlyDataSource)
            {
                DataSourceRWMark = "_READ";
            }
            else
            {
                DataSourceRWMark = "_WRITE";
            }

            DataSet DS = new DataSet();
            using (SqlConnection MyConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionMark + DataSourceRWMark].ConnectionString))
            {
                this.MyCmd.Connection = MyConn;
                this.PageSort = sqlSort;
                this.MyCmd.CommandText = this.SQLTEXT_PAGE;
                this.MyCmd.Parameters.Add("@SI", SqlDbType.Int).Value = PageIndex * PageSize - PageSize + 1;
                this.MyCmd.Parameters.Add("@EI", SqlDbType.Int).Value = PageIndex * PageSize;
                SqlDataAdapter MyAdp = new SqlDataAdapter(MyCmd);
                try
                {
                    MyAdp.Fill(DS);
                    if (MyConn.State == ConnectionState.Closed)
                    {
                        MyConn.Close();
                    }
                }
                catch (Exception e)
                {
                    log.Save(e, MyCmd);
                }
            }

            List<QueryResult> jlist = new List<QueryResult>();
            if (DS != null && DS.Tables.Count == 2)
            {
                DataTable DT = DS.Tables[0];
                try
                {
                    long.TryParse(DS.Tables[1].Rows[0][0].ToString(), out RecordCount);
                }
                catch { }
                if (DT != null && DT.Rows.Count > 0)
                {
                    foreach (DataRow dr in DT.Rows)
                    {
                        QueryResult jr = new QueryResult();
                        foreach (string tabSName in OutTabList)
                        {
                            Assembly Asm = LoadAssemblyFromFile(TabAssemblyDic[tabSName]);
                            Type t = Asm.GetType(TabClassDic[tabSName]);
                            object tobj = Asm.CreateInstance(TabClassDic[tabSName]);
                            System.Reflection.ConstructorInfo[] constructorInfoArray = t.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                            Type[] ParameterTypes = new Type[] { dr.GetType() };
                            ConstructorInfo CallConstructFun = t.GetConstructor(ParameterTypes);

                            DataTable TempDT = new DataTable();

                            foreach (DataColumn dc in DT.Columns)
                            {
                                if (dc.ColumnName.StartsWith(tabSName + "_"))
                                {
                                    TempDT.Columns.Add(dc.ColumnName.Substring((tabSName + "_").Length));
                                }
                            }
                            DataRow TempDr = TempDT.NewRow();
                            foreach (DataColumn dc in TempDT.Columns)
                            {
                                TempDr[dc.ColumnName] = dr[tabSName + "_" + dc.ColumnName];
                            }
                            TempDT.Rows.Add(TempDr);

                            tobj = CallConstructFun.Invoke(new object[] { TempDr });
                            jr.RowResultList.Add(TabNameDic[tabSName], tobj);
                        }
                        jlist.Add(jr);
                    }
                }
            }

            return jlist;
        }

        [NonSerialized]
        private Dictionary<string, Assembly> AsmDic = new Dictionary<string, Assembly>();

        private Assembly LoadAssemblyFromFile(string fileFullName)
        {
            if (!AsmDic.ContainsKey(fileFullName))
            {
                Assembly asm = Assembly.LoadFile(fileFullName);
                AsmDic.Add(fileFullName, asm);
                return asm;
            }
            else
            {
                return AsmDic[fileFullName];
            }
        }
        #endregion

        #region 查询命令构造
        /// <summary>
        /// 查询命令构造
        /// </summary>
        /// <param name="sqlPart">部分SQL语句</param>
        /// <param name="ParamsList">可选参数列表</param>
        /// <returns></returns>
        private void BuildCommand(string sqlPart, object[] ParamsList = null)
        {
            if (!string.IsNullOrEmpty(sqlPart))
            {
                List<string> ParameterList = new List<string>();
                Regex reg = new Regex("(@[0-9a-zA-Z_]{1,30})", RegexOptions.IgnoreCase);
                MatchCollection mc = reg.Matches(sqlPart);
                if (mc != null && mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        if (!ParameterList.Contains(m.Groups[1].Value))
                        {
                            ParameterList.Add(m.Groups[1].Value);
                        }
                    }
                }
                if (ParameterList.Count > 0)
                {
                    int i = 0;
                    foreach (string ParameterName in ParameterList)
                    {
                        if (!this.MyCmd.Parameters.Contains(ParameterName))
                        {
                            this.MyCmd.Parameters.AddWithValue(ParameterName, ParamsList[i]);
                        }
                        i++;
                    }
                }
            }
        }
        #endregion

        #region 表信息
        /// <summary>
        /// 表信息
        /// </summary>
        [NonSerialized]
        [JsonIgnore]
        protected TabInfo _tabinfo = new TabInfo();
        #endregion
    }
}