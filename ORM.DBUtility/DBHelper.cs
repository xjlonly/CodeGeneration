using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;

namespace ORM.DBUtility
{
    /// <summary>
    /// 数据通用访问类
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// 数据库类型,1代表SQLSERVER,2代表ORACLE,0代表OLEDB
        /// </summary>
        private int DBTypeMARK = 1;
        private bool isReadonly = false;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBHelper()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IsReadOnly">是否连接只读数据库</param>
        public DBHelper(bool IsReadOnly)
        {
            this.isReadonly = IsReadOnly;
        }
        #endregion

        #region 执行SQL语句,返回第一行第一列数据
        /// <summary>
        /// 执行SQL语句,返回第一行第一列数据
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns></returns>
        public object ExecTextScalar(string SQLText)
        {
            object obj = null;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar(SQLText);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        /// <summary>
        /// 执行SQL语句,返回第一行第一列数据
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public object ExecTextScalar(string SQLText, params object[] cmdParams)
        {
            object obj = null;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar(SQLText, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        #endregion

        #region 执行存储过程,返回第一行第一列数据
        /// <summary>
        /// 执行存储过程,返回第一行第一列数据
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <returns></returns>
        public object ExecProcScalar(string ProcName)
        {
            object obj = null;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar_Proc(ProcName);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        /// <summary>
        /// 执行存储过程,返回第一行第一列数据
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public object ExecProcScalar(string ProcName, params object[] cmdParams)
        {
            object obj = null;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar_Proc(ProcName, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        #endregion

        #region 执行SQL语句,返回受影响行数
        /// <summary>
        /// 执行SQL语句,返回受影响行数
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns></returns>
        public int ExecTextNonQuery(string SQLText)
        {
            int obj = 0;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecNonquery(SQLText);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        /// <summary>
        /// 执行SQL语句,返回受影响行数
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public int ExecTextNonQuery(string SQLText, params object[] cmdParams)
        {
            int obj = 0;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecNonquery(SQLText, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        #endregion

        #region 执行存储过程,返回受影响行数
        /// <summary>
        /// 执行存储过程,返回受影响行数
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <returns></returns>
        public int ExecProcNonQuery(string ProcName)
        {
            int obj = 0;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecNonquery_Proc(ProcName);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        /// <summary>
        /// 执行存储过程,返回受影响行数
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public int ExecProcNonQuery(string ProcName, params object[] cmdParams)
        {
            int obj = 0;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecNonquery_Proc(ProcName, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        #endregion

        #region 执行SQL语句,返回DataSet
        /// <summary>
        /// 执行SQL语句,返回DataSet
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns></returns>
        public DataSet ExecTextDataSet(string SQLText)
        {
            DataSet DS = null;
            switch (DBTypeMARK)
            {
                case 1:
                    DS = new SQLHelper(this.isReadonly).ExecDataSet(SQLText);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return DS;
        }
        /// <summary>
        /// 执行SQL语句,返回DataSet
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public DataSet ExecTextDataSet(string SQLText, params object[] cmdParams)
        {
            DataSet DS = null;
            switch (DBTypeMARK)
            {
                case 1:
                    DS = new SQLHelper(this.isReadonly).ExecDataSet(SQLText, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return DS;
        }
        #endregion

        #region 执行存储过程,返回DataSet
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <returns></returns>
        public DataSet ExecProcDataSet(string ProcName)
        {
            DataSet DS = null;
            switch (DBTypeMARK)
            {
                case 1:
                    DS = new SQLHelper(this.isReadonly).ExecDataSet_Proc(ProcName);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return DS;
        }
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public DataSet ExecProcDataSet(string ProcName, params object[] cmdParams)
        {
            DataSet DS = null;
            switch (DBTypeMARK)
            {
                case 1:
                    DS = new SQLHelper(this.isReadonly).ExecDataSet_Proc(ProcName, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return DS;
        }
        #endregion

        #region 执行SQL语句,返回DataTable
        /// <summary>
        /// 执行SQL语句,返回DataTable
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <returns></returns>
        public DataTable ExecTextDataTable(string SQLText)
        {
            DataTable dt = null;
            switch (DBTypeMARK)
            {
                case 1:
                    dt = new SQLHelper(this.isReadonly).ExecDataTable(SQLText);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return dt;
        }
        /// <summary>
        /// 执行SQL语句,返回DataTable
        /// </summary>
        /// <param name="SQLText">SQL语句</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public DataTable ExecTextDataTable(string SQLText, params object[] cmdParams)
        {
            DataTable dt = null;
            switch (DBTypeMARK)
            {
                case 1:
                    dt = new SQLHelper(this.isReadonly).ExecDataTable(SQLText, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return dt;
        }
        #endregion

        #region 执行存储过程,返回DataTable
        /// <summary>
        /// 执行存储过程,返回DataTable
        /// </summary>
        /// <param name="ProcName">存储过程名称</param>
        /// <returns></returns>
        public DataTable ExecProcDataTable(string ProcName)
        {
            DataTable dt = null;
            switch (DBTypeMARK)
            {
                case 1:
                    dt = new SQLHelper(this.isReadonly).ExecDataTable_Proc(ProcName);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return dt;
        }
        /// <summary>
        /// 执行存储过程,返回DataTable
        /// </summary>
        /// <param name="ProcName">存储过程</param>
        /// <param name="cmdParams">参数数组</param>
        /// <returns></returns>
        public DataTable ExecProcDataTable(string ProcName, params object[] cmdParams)
        {
            DataTable dt = null;
            switch (DBTypeMARK)
            {
                case 1:
                    dt = new SQLHelper(this.isReadonly).ExecDataTable_Proc(ProcName, (SqlParameter[])cmdParams);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return dt;
        }
        #endregion

        #region 执行SQL命令,返回第一行第一列数据
        /// <summary>
        /// 执行SQL命令,返回第一行第一列数据
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <returns></returns>
        public object ExecScalar(DbCommand cmd)
        {
            object obj = null;
            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar((SqlCommand)cmd);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        public object ExecScalar(string cmdText)
        {
            object obj = null;

            switch (DBTypeMARK)
            {
                case 1:
                    obj = new SQLHelper(this.isReadonly).ExecScalar(cmdText);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return obj;
        }
        #endregion

        #region 执行SQL命令,返回受影响行数
        /// <summary>
        /// 执行SQL命令,返回受影响行数
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <returns></returns>
        public int ExecNonQuery(DbCommand cmd)
        {
            int query = 0;
            switch (DBTypeMARK)
            {
                case 1:
                    query = new SQLHelper(this.isReadonly).ExecNonquery((SqlCommand)cmd);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return query;
        }
        #endregion

        #region 执行SQL命令,返回DataTable
        /// <summary>
        /// 执行SQL命令,返回DataTable
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <returns></returns>
        public DataTable ExecDataTable(DbCommand cmd)
        {
            DataTable dt = new DataTable();
            switch (DBTypeMARK)
            {
                case 1:
                    dt = new SQLHelper(this.isReadonly).ExecDataTable((SqlCommand)cmd);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return dt;
        }
        #endregion

        #region 执行SQL命令,返回DataSet
        /// <summary>
        /// 执行SQL命令,返回DataSet
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <returns></returns>
        public DataSet ExecDataSet(DbCommand cmd)
        {
            DataSet ds = new DataSet();
            switch (DBTypeMARK)
            {
                case 1:
                    ds = new SQLHelper(this.isReadonly).ExecDataSet((SqlCommand)cmd);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return ds;
        }
        #endregion

        #region 事务执行多个SQL语句
        /// <summary>
        /// 事务提交多个数据库操作,返回每一次执行影响的行数列表
        /// </summary>
        /// <param name="cmdTextList">SQL语句组</param>
        /// <returns></returns>
        public List<int> ExecTransaction(List<string> cmdTextList)
        {
            List<int> ExecQueryList = new List<int>();
                    ExecQueryList = new SQLHelper(this.isReadonly).ExecTransaction(cmdTextList);
            return ExecQueryList;
        }
        /// <summary>
        /// 事务提交多个数据库操作,返回每一次执行影响的行数列表
        /// </summary>
        /// <param name="cmdTextList">SQL语句组</param>
        /// <returns></returns>
        public List<int> ExecTransaction(List<string> cmdTextList,List<List<SqlParameter>> cmdParametersList)
        {
            List<int> ExecQueryList = new List<int>();
            switch (DBTypeMARK)
            {
                case 1:
                    ExecQueryList = new SQLHelper(this.isReadonly).ExecTransaction(cmdTextList, cmdParametersList);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
            }
            return ExecQueryList;
        }
        #endregion
    }
}