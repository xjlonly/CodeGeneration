using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Lixx.DBS
{
    /// <summary>
    /// Model实体生成类
    /// </summary>
    public class DBS_CCClass
    {
        private string _namespaceHead = string.Empty;
        private string _folderHead = string.Empty;
        private string _tableName = string.Empty;
        private DataTable DT = null;
        private string _ConnectionMark = string.Empty;

        public DBS_CCClass(DataTable dt, string NameSpaceHead, string FolderHead, string TableName, string ConnectionMark)
        {
            this.DT = dt;
            this._namespaceHead = NameSpaceHead;
            this._folderHead = FolderHead;
            this._tableName = TableName;
            this._ConnectionMark = ConnectionMark;
        }

        public string CreateFileContent()
        {
            StringBuilder txt = new StringBuilder();
            txt.AppendLine("using System;");
            txt.AppendLine("using System.Data;");
            txt.AppendLine("using System.Collections.Generic;");
            txt.AppendLine("using Newtonsoft.Json;");
            txt.AppendLine("using System.ComponentModel;");
            txt.AppendLine("");
            txt.AppendLine("namespace " + this._namespaceHead.TrimEnd(new char[] { '.' }));
            txt.AppendLine("{");
            txt.AppendLine("\t/// <summary>");
            txt.AppendLine("\t/// 数据实体类:" + this._tableName);
            txt.AppendLine("\t/// </summary>");
            txt.AppendLine("\t[Serializable]");
            txt.AppendLine("\tpublic class " + this._tableName + " : ORM.Base.ModelBase");
            txt.AppendLine("\t{");

            txt.AppendLine("\t\t#region 表名");
            txt.AppendLine("\t\tprivate static string _tablename = \"" + this._tableName + "\";");
            txt.AppendLine("\t\t/// <summary>");
            txt.AppendLine("\t\t/// 表名");
            txt.AppendLine("\t\t/// </summary>");
            txt.AppendLine("\t\t[JsonIgnore]");
            txt.AppendLine("\t\tpublic string TableName { get { return _tablename; } }");
            txt.AppendLine("\t\t#endregion");
            txt.AppendLine("");

            txt.AppendLine("\t\t#region 实体类型");
            txt.AppendLine("\t\tprivate static string _typeName = \"" + this._namespaceHead + this._tableName + "\";");
            txt.AppendLine("\t\t#endregion");
            txt.AppendLine("");

            DBS_TypeMap TM = new DBS_TypeMap();

            txt.AppendLine("\t\t#region 列名列表");
            StringBuilder ColListTxt = new StringBuilder();
            foreach (DataRow dr in DT.Rows)
            {
                if (TM[dr["Xtype_Name"].ToString()].DBType == "uniqueidentifier")
                {
                    continue;
                }
                if (TM[dr["Xtype_Name"].ToString()].DBType == "timestamp")
                {
                    continue;
                }
                if (ColListTxt.Length == 0)
                {
                    ColListTxt.Append(string.Concat("\"", dr["name"].ToString().ToUpper(), "\""));
                }
                else
                {
                    ColListTxt.Append(string.Concat(", \"", dr["name"].ToString().ToUpper(), "\""));
                }
            }
            txt.AppendLine("\t\tprivate static List<string> _cols = new List<string>() { " + ColListTxt.ToString() + " };");
            txt.AppendLine("\t\t/// <summary>");
            txt.AppendLine("\t\t/// 列名列表");
            txt.AppendLine("\t\t/// </summary>");
            txt.AppendLine("\t\t[JsonIgnore]");
            txt.AppendLine("\t\tpublic List<string> ColList { get { return _cols; } }");
            txt.AppendLine("\t\t#endregion");
            txt.AppendLine("");

            txt.AppendLine("\t\t#region 字段、属性");
            foreach (DataRow dr in DT.Rows)
            {
                if (TM[dr["Xtype_Name"].ToString()].DBType == "uniqueidentifier")
                {
                    continue;
                }
                if (TM[dr["Xtype_Name"].ToString()].DBType == "timestamp")
                {
                    continue;
                }
                txt.AppendLine("\t\t/// <summary>");
                txt.AppendLine("\t\t/// " + dr["Description"].ToString().Replace("\r", "").Replace("\n", ""));
                txt.AppendLine("\t\t/// </summary>");
                txt.AppendLine("\t\t[Description(\"" + dr["Description"].ToString().Replace("\r", "").Replace("\n", "") + "\")]");
                if (dr["name"].ToString().ToUpper() == "ISDEL")
                {
                    txt.AppendLine("\t\t[JsonIgnore]");
                }
                if (TM.IsKeyWord(dr["name"].ToString()))
                {
                    string nameStr = dr["name"].ToString();
                    txt.AppendLine("\t\tpublic " + TM[dr["Xtype_Name"].ToString()].CodeType + " " + nameStr.ToUpper());
                }
                else
                {
                    txt.AppendLine("\t\tpublic " + TM[dr["Xtype_Name"].ToString()].CodeType + " " + dr["name"].ToString());
                }
                txt.AppendLine("\t\t{");
                txt.AppendLine("\t\t\tget{ return this._" + dr["name"].ToString() + ";}");
                txt.AppendLine("\t\t\tset{ this._" + dr["name"].ToString() + " = value;}");
                txt.AppendLine("\t\t}");

                if (TM[dr["Xtype_Name"].ToString()].CodeType == "string")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = \"\";");
                    }
                    else
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = \"" + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + "\";");
                    }
                }
                else if (TM[dr["Xtype_Name"].ToString()].CodeType == "decimal")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = 0M;");
                    }
                    else
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = " + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + "M;");
                    }
                }
                else if (TM[dr["Xtype_Name"].ToString()].CodeType == "double")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = 0;");
                    }
                    else
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = " + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + ";");
                    }
                }
                else if (TM[dr["Xtype_Name"].ToString()].CodeType == "int" || TM[dr["Xtype_Name"].ToString()].CodeType == "Int32")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = 0;");
                    }
                    else
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = " + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + ";");
                    }
                }
                else if (TM[dr["Xtype_Name"].ToString()].CodeType == "Int64")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = 0L;");
                    }
                    else
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = " + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + "L;");
                    }
                }
                else if (TM[dr["Xtype_Name"].ToString()].CodeType == "DateTime")
                {
                    if (string.IsNullOrEmpty(dr["dval"].ToString()))
                    {
                        txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + ";");
                    }
                    else
                    {
                        if (dr["dval"].ToString().ToUpper().Contains("GETDATE()"))
                        {
                            txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = DateTime.Now;");
                        }
                        else
                        {
                            txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + " = DateTime.Parse(\"" + dr["dval"].ToString().Replace("(", "").Replace(")", "").Replace("'", "") + "\");");
                        }
                    }
                }
                else
                {
                    txt.AppendLine("\t\tprivate " + TM[dr["Xtype_Name"].ToString()].CodeType + " _" + dr["name"].ToString() + ";");
                }
            }
            txt.AppendLine("\t\t#endregion");
            txt.AppendLine("");

            txt.AppendLine("\t\t#region 构造函数");
            txt.AppendLine("\t\t/// <summary>");
            txt.AppendLine("\t\t/// 构造函数");
            txt.AppendLine("\t\t/// </summary>");
            txt.AppendLine("\t\tpublic " + this._tableName + "()");
            txt.AppendLine("\t\t{");
            txt.AppendLine("\t\t\t_ConnectionMark = \"" + this._ConnectionMark + "\";");
            txt.AppendLine("\t\t\t_tabinfo = TableInfo;");
            txt.AppendLine("\t\t\tbase.Init();");
            txt.AppendLine("\t\t}");

            txt.AppendLine("\t\tpublic static ORM.Base.TabInfo TableInfo = new ORM.Base.TabInfo()");
            txt.AppendLine("\t\t{");
            txt.AppendLine("\t\t\tTableName = _tablename,");
            txt.AppendLine("\t\t\tColList = _cols,");
            txt.AppendLine("\t\t\tTypeName = _typeName,");
            txt.AppendLine("\t\t\tAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location");
            txt.AppendLine("\t\t};");

            txt.AppendLine("\t\t/// <summary>");
            txt.AppendLine("\t\t/// 构造函数,初始化一行数据");
            txt.AppendLine("\t\t/// </summary>");
            txt.AppendLine("\t\tpublic " + this._tableName + "(DataRow dr) : this()");
            txt.AppendLine("\t\t{");

            foreach (DataRow dr in DT.Rows)
            {
                if (TM[dr["Xtype_Name"].ToString()].DBType == "uniqueidentifier")
                {
                    continue;
                }
                if (TM[dr["Xtype_Name"].ToString()].DBType == "timestamp")
                {
                    continue;
                }
                txt.AppendLine("\t\t\tif (dr != null && dr.Table.Columns.Contains(\"" + dr["name"].ToString() + "\") && !(dr[\"" + dr["name"].ToString() + "\"] is DBNull))");
                txt.AppendLine("\t\t\t{");
                txt.AppendLine("\t\t\t\tthis._" + dr["name"].ToString() + " = " + TM[dr["Xtype_Name"].ToString()].ConvertType + "(dr[\"" + dr["name"].ToString() + "\"]);");
                txt.AppendLine("\t\t\t}");
            }
            txt.AppendLine("\t\t}");
            txt.AppendLine("\t\t#endregion");
            txt.AppendLine("\t}");
            txt.Append("}");

            return txt.ToString();
        }
    }
}