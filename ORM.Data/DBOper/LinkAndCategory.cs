using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ORM.Test;
using ORM.DBUtility;
namespace ORM.Data.DBOper
{
	/// <summary>
	/// 数据库操作:LinkAndCategory
	/// </summary>
	public class LinkAndCategory
	{
		#region 查询
		#region 查询：返回数据表
		/// <summary>
		/// 查询：返回数据表
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">排序，如 date desc,id asc</param>
		/// <param name="ParamsList">参数值列表，如 {"123",1}</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere = "", string sqlSort = "", string sqlCols = "*", object[] ParamsList = null)
		{
			DataTable table = new DataTable();
			string txtCols = sqlCols;
			if (!string.IsNullOrEmpty(sqlCols))
			{
				txtCols = "*";
			}
			StringBuilder sql = new StringBuilder("SELECT " + sqlCols + " FROM LinkAndCategory(NOLOCK)");
			if (!string.IsNullOrEmpty(sqlWhere))
			{
				sql.Append(" WHERE ").Append(sqlWhere);
			}
			if (!string.IsNullOrEmpty(sqlSort))
			{
				sql.Append(" ORDER BY ").Append(sqlSort);
			}
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			table = new ORM.DBUtility.DBHelper(true).ExecDataTable(cmd);
			if (table == null)
			{
				table = new DataTable();
			}
			table.TableName = "table";
			return table;
		}
		#endregion
		#region 查询：返回对象列表
		/// <summary>
		/// 查询：返回对象列表
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">排序，如 date desc,id asc</param>
		/// <param name="ParamsList">参数列表</param>
		/// <returns></returns>
		public static List<T> GetList<T>(string sqlWhere = "", string sqlSort = "", string sqlCols = "*", object[] ParamsList = null)
		{
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, ParamsList);
			List<T> rtnObjList = new List<T>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
                    rtnObjList.Add((T)System.Activator.CreateInstance(typeof(T), dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region 查询：返回对象列表
		/// <summary>
		/// 查询：返回对象列表
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">排序，如 date desc,id asc</param>
		/// <param name="ParamsList">参数列表</param>
		/// <returns></returns>
		public static List<ORM.Test.LinkAndCategory> GetList(string sqlWhere = "", string sqlSort = "", object[] ParamsList = null)
		{
			DataTable table = GetTable(sqlWhere, sqlSort, "*", ParamsList);
			List<ORM.Test.LinkAndCategory> rtnObjList = new List<ORM.Test.LinkAndCategory>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.LinkAndCategory(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region 分页查询
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <param name="sqlSort">排序条件,如username desc</param>
		/// <param name="sqlCols">数据库字段名组,用逗号分割,例如:username,userpwd,userid</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">页大小</param>
		/// <param name="recordCount">记录行数</param>
		/// <returns></returns>
		public static List<ORM.Test.LinkAndCategory> GetList(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out int recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt32(LRecordCount);
			List<ORM.Test.LinkAndCategory> rtnObjList = new List<ORM.Test.LinkAndCategory>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.LinkAndCategory(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region 分页查询
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <param name="sqlSort">排序条件,如username desc</param>
		/// <param name="sqlCols">数据库字段名组,用逗号分割,例如:username,userpwd,userid</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">页大小</param>
		/// <param name="recordCount">记录行数</param>
		/// <returns></returns>
		public static List<ORM.Test.LinkAndCategory> GetList(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out long recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt64(LRecordCount);
			List<ORM.Test.LinkAndCategory> rtnObjList = new List<ORM.Test.LinkAndCategory>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.LinkAndCategory(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region 根据主键:ID 查询
		/// <summary>
		/// 根据主键:ID 查询
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static DataTable GetTableByID(Int32 _ID)
		{
			DataTable table = new DataTable();
			string sql = "SELECT * FROM LinkAndCategory(NOLOCK) WHERE [ID]=@ID";
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = sql;
			cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = _ID;
			table = new ORM.DBUtility.DBHelper(true).ExecDataTable(cmd);
			if (table == null)
			{
				table = new DataTable();
			}
			table.TableName = "table";
			return table;
		}
		#endregion
		#region 根据主键:ID 查询，返回对象列表
		/// <summary>
		/// 根据主键:ID 查询，返回对象列表
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static List<ORM.Test.LinkAndCategory> GetListByID(Int32 _ID)
		{
			DataTable table = GetTableByID(_ID);
			List<ORM.Test.LinkAndCategory> objList = new List<ORM.Test.LinkAndCategory>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					objList.Add(new ORM.Test.LinkAndCategory(dr));
				}
			}
			return objList;
		}
		#endregion
		#region 查询某字段数据第一行第一列数据
		/// <summary>
		/// 条件查询,查询某字段数据第一行第一列数据
		/// </summary>
		/// <param name="sqlCol">数据库字段名,也可以为COUNT(),TOP 1 列名</param>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123' and usertype=@usertype</param>
		/// <param name="sqlSort">排序，如 date desc,id asc</param>
		/// <param name="ParamsList">参数列表</param>
		/// <returns></returns>
		public static object GetSingle(string sqlCol, string sqlWhere = "", string sqlSort = "", object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("SELECT TOP 1 " + sqlCol + " FROM LinkAndCategory(NOLOCK)");
			if (!string.IsNullOrEmpty(sqlWhere))
			{
				sql.Append(" WHERE ").Append(sqlWhere);
			}
			if (!string.IsNullOrEmpty(sqlSort))
			{
				sql.Append(" ORDER BY ").Append(sqlSort);
			}
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			return new ORM.DBUtility.DBHelper(true).ExecScalar(cmd);
		}
		#endregion
		#region 条件查询,查询记录总数
		/// <summary>
		/// 查询记录总数
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123',并可带排序条件</param>
		/// <returns></returns>
		public static long CountOutLong(string sqlWhere, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("SELECT COUNT(1) FROM LinkAndCategory(NOLOCK) WHERE 1=1");
			if (!string.IsNullOrEmpty(sqlWhere))
			{
				sql.Append(sqlWhere);
			}
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			object CountObj = new ORM.DBUtility.DBHelper(true).ExecScalar(cmd);
			long RtnCount = 0;
			if (CountObj != null && CountObj != DBNull.Value && !string.IsNullOrEmpty(CountObj.ToString()))
			{
				long.TryParse(CountObj.ToString(), out RtnCount);
			}
			return RtnCount;
		}
		#endregion
		#region 条件查询,查询记录总数
		/// <summary>
		/// 条件查询,查询记录总数
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123',并可带排序条件</param>
		/// <returns></returns>
		public static int Count(string sqlWhere, object[] ParamsList = null)
		{
			long rtnInt= CountOutLong(sqlWhere, ParamsList);
			return Convert.ToInt32(rtnInt);
		}
		#endregion
		#region 分页查询
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <param name="sqlSort">排序条件,如username desc</param>
		/// <param name="sqlCols">数据库字段名组,用逗号分割,例如:username,userpwd,userid</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">页大小</param>
		/// <param name="recordCount">记录行数</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out int recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt32(LRecordCount);
			return table;
		}
		#endregion
		#region 分页查询
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <param name="sqlSort">排序条件,如username desc</param>
		/// <param name="sqlCols">数据库字段名组,用逗号分割,例如:username,userpwd,userid</param>
		/// <param name="pageIndex">页码</param>
		/// <param name="pageSize">页大小</param>
		/// <param name="recordCount">记录行数</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out long recordCount, object[] ParamsList = null)
		{
			int SI = pageIndex * pageSize - pageSize + 1;
			int EI = pageIndex * pageSize;
			DataSet ds = new DataSet();
			if (string.IsNullOrEmpty(sqlCols))
			{
				sqlCols = "ID,CID,LID";
			}
			StringBuilder sql = new StringBuilder("WITH PST(RN," + sqlCols + ") AS");
			sql.Append("(");
			sql.Append("SELECT ROW_NUMBER() OVER(ORDER BY ").Append(sqlSort).Append(") RN,").Append(sqlCols).Append(" ");
			sql.Append("FROM LinkAndCategory(NOLOCK)");
			if (!string.IsNullOrEmpty(sqlWhere))
			{
				sql.Append(" WHERE ").Append(sqlWhere);
			}
			sql.Append(")");
			sql.Append("SELECT RN,").Append(sqlCols).Append(" FROM PST WHERE RN BETWEEN @SI AND @EI");
			object[] NewParamsList;
			if (ParamsList != null)
			{
				NewParamsList = new object[ParamsList.Length + 2];
				for (int i = 0;i < ParamsList.Length; i++)
				{
					NewParamsList[i] = ParamsList[i];
				}
				NewParamsList[ParamsList.Length] = SI;
				NewParamsList[ParamsList.Length + 1] = EI;
			}
			else
			{
				NewParamsList = new object[2];
				NewParamsList[0] = SI;
				NewParamsList[1] = EI;
			}
			if (!string.IsNullOrEmpty(sqlWhere))
			{
				sql.Append(";SELECT COUNT(*) FROM LinkAndCategory(NOLOCK) WHERE ").Append(sqlWhere);
			}
			else
			{
				sql.Append(";SELECT COUNT(*) FROM LinkAndCategory(NOLOCK)");
			}
			SqlCommand cmd = BuildCommand(sql.ToString(), NewParamsList);
			ds = new ORM.DBUtility.DBHelper(true).ExecDataSet(cmd);
			if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count >0)
			{
				recordCount = Convert.ToInt64(ds.Tables[1].Rows[0][0]);
			}
			else
			{
				recordCount = 0;
			}
			if (ds != null && ds.Tables.Count >1)
			{
				return ds.Tables[0];
			}
			else
			{
				return new DataTable();
			}
		}
		#endregion
		#region 查询并构建对象,返回第一个对象
		/// <summary>
		/// 查询并构建对象,返回第一个对象
		/// </summary>
		/// <param name="sqlWhere">查询条件</param>
		/// <param name="sqlSort">排序条件</param>
		/// <returns></returns>
		public static ORM.Test.LinkAndCategory Get(string sqlWhere, string sqlSort = "", object[] ParamsList = null)
		{
			List<ORM.Test.LinkAndCategory> objList = GetList(sqlWhere, sqlSort, ParamsList);
			if(objList != null && objList.Count > 0)
			{
				return objList[0];
			}
			else
			{
				return new ORM.Test.LinkAndCategory();
			}
		}
		#endregion
		#endregion

		#region 删除
		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <returns></returns>
		public static int Delete(string sqlWhere, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("DELETE FROM LinkAndCategory WHERE ");
			sql.Append(sqlWhere);
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			return new ORM.DBUtility.DBHelper(false).ExecNonQuery(cmd);
		}
		#endregion

		#region 更新
		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="sqlWhere">查询条件，如username='123' and password='123'</param>
		/// <param name="sqlSet">数据设置，如username='123',password='123'</param>
		/// <returns></returns>
		public static int Update(string sqlWhere, string sqlSet, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("UPDATE LinkAndCategory SET ");
			sql.Append(sqlSet).Append(" WHERE ").Append(sqlWhere);
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			return new ORM.DBUtility.DBHelper(false).ExecNonQuery(cmd);
		}
		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="LinkAndCategory_obj"></param>
		/// <returns></returns>
		public static int Update(ORM.Test.LinkAndCategory LinkAndCategory_obj)
		{
			StringBuilder sqlSet = new StringBuilder();
			StringBuilder sqlWhere = new StringBuilder();
			string sql = "UPDATE LinkAndCategory SET {0} WHERE {1}";
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.AddWithValue("@ID", LinkAndCategory_obj.ID);
			cmd.Parameters["@ID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlWhere.Length == 0)
			{
				sqlWhere.Append("[ID]=@ID");
			}
			else
			{
				sqlWhere.Append(" AND [ID]=@ID");
			}
			cmd.Parameters.AddWithValue("@CID", LinkAndCategory_obj.CID);
			cmd.Parameters["@CID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlSet.Length == 0)
			{
				sqlSet.Append("[CID]=@CID");
			}
			else
			{
				sqlSet.Append(",[CID]=@CID");
			}
			cmd.Parameters.AddWithValue("@LID", LinkAndCategory_obj.LID);
			cmd.Parameters["@LID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlSet.Length == 0)
			{
				sqlSet.Append("[LID]=@LID");
			}
			else
			{
				sqlSet.Append(",[LID]=@LID");
			}
			sql = string.Format(sql, sqlSet.ToString(), sqlWhere.ToString());
			cmd.CommandText = sql;
			try
			{
				return new ORM.DBUtility.DBHelper(true).ExecNonQuery(cmd);
			}
			catch { return -1; }
		}
		/// <summary>
		/// 更新数据
		/// </summary>
		/// <param name="LinkAndCategory_obj"></param>
		/// <param name="IsRowLock">是否锁行</param>
		/// <returns></returns>
		public static int Update(ORM.Test.LinkAndCategory LinkAndCategory_obj, bool IsRowLock)
		{
			StringBuilder sqlSet = new StringBuilder();
			StringBuilder sqlWhere = new StringBuilder();
			string sql = string.Empty;
			if (IsRowLock)
			{
				sql = "UPDATE LinkAndCategory WITH(ROWLOCK) SET {0} WHERE {1}";
			}
			else
			{
				sql = "UPDATE LinkAndCategory SET {0} WHERE {1}";
			}
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.AddWithValue("@ID", LinkAndCategory_obj.ID);
			cmd.Parameters["@ID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlWhere.Length == 0)
			{
				sqlWhere.Append("[ID]=@ID");
			}
			else
			{
				sqlWhere.Append(" AND [ID]=@ID");
			}
			cmd.Parameters.AddWithValue("@CID", LinkAndCategory_obj.CID);
			cmd.Parameters["@CID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlSet.Length == 0)
			{
				sqlSet.Append("[CID=@CID");
			}
			else
			{
				sqlSet.Append(",[CID=@CID");
			}
			cmd.Parameters.AddWithValue("@LID", LinkAndCategory_obj.LID);
			cmd.Parameters["@LID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlSet.Length == 0)
			{
				sqlSet.Append("[LID=@LID");
			}
			else
			{
				sqlSet.Append(",[LID=@LID");
			}
			sql = string.Format(sql, sqlSet.ToString(), sqlWhere.ToString());
			cmd.CommandText = sql;
			try
			{
				return new ORM.DBUtility.DBHelper(true).ExecNonQuery(cmd);
			}
			catch { return -1; }
		}
		#endregion

		#region 插入
		/// <summary>
		/// 插入数据
		/// </summary>
		/// <returns></returns>
		public static bool insert( ORM.Test.LinkAndCategory LinkAndCategory_obj)
		{
			StringBuilder cols = new StringBuilder();
			StringBuilder parameters = new StringBuilder();
			string sql = "INSERT INTO LinkAndCategory({0}) values({1})";
			if (cols.Length > 0)
			{
				cols.Append(",[CID]");
				parameters.Append(",@CID");
			}
			else
			{
				cols.Append("[CID]");
				parameters.Append("@CID");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LID]");
				parameters.Append(",@LID");
			}
			else
			{
				cols.Append("[LID]");
				parameters.Append("@LID");
			}
			sql = string.Format(sql, cols.ToString(), parameters.ToString());
			SqlCommand cmd = new SqlCommand(sql);
			cmd = ParameterElse(cmd, LinkAndCategory_obj);
			bool b = true;
			try
			{
				int QueryCount = new ORM.DBUtility.DBHelper(true).ExecNonQuery(cmd);
				if (QueryCount != 1)
				{
					b = false;
				}
			}
			catch { b = false; }
			return b;
		}
		/// <summary>
		/// 插入数据,返回自增列ID
		/// </summary>
		/// <returns></returns>
		public static bool Add( ORM.Test.LinkAndCategory LinkAndCategory_obj, out Int32 ID)
		{
			ID = 0;
			StringBuilder cols = new StringBuilder();
			StringBuilder parameters = new StringBuilder();
			string sql = "INSERT INTO LinkAndCategory({0}) values({1});SELECT @@IDENTITY;";
			if (cols.Length > 0)
			{
				cols.Append(",[CID]");
				parameters.Append(",@CID");
			}
			else
			{
				cols.Append("[CID]");
				parameters.Append("@CID");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LID]");
				parameters.Append(",@LID");
			}
			else
			{
				cols.Append("[LID]");
				parameters.Append("@LID");
			}
			sql = string.Format(sql, cols.ToString(), parameters.ToString());
			SqlCommand cmd = new SqlCommand(sql);
			cmd = ParameterElse(cmd, LinkAndCategory_obj);
			bool b = true;
			try
			{
				object idobj = new ORM.DBUtility.DBHelper(true).ExecScalar(cmd);
				ID = Convert.ToInt32(idobj);
				if (ID == 0)
				{
					b = false;
				}
			}
			catch { b = false; }
			return b;
		}
		#endregion

		#region 查询执行器构造
		/// <summary>
		/// 查询执行器构造
		/// </summary>
		/// <param name="sql">完整SQL语句</param>
		/// <param name="ParamsList">可选参数列表</param>
		/// <returns></returns>
		private static SqlCommand BuildCommand(string sql, object[] ParamsList = null)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = sql;
			if (!string.IsNullOrEmpty(sql))
			{
				List<string> ParameterList = new List<string>();
				Regex reg = new Regex("(@[0-9a-zA-Z_]{1,30})", RegexOptions.IgnoreCase);
				MatchCollection mc = reg.Matches(sql);
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
						cmd.Parameters.AddWithValue(ParameterName, ParamsList[i]);
						i++;
					}
				}
			}
			return cmd;
		}
		#endregion
		#region 参数准备
		/// <summary>
		/// 参数准备
		/// </summary>
		/// <returns></returns>
		public static SqlCommand ParameterElse(SqlCommand cmd, ORM.Test.LinkAndCategory LinkAndCategory_obj)
		{
			cmd.Parameters.AddWithValue("@CID", LinkAndCategory_obj.CID);
			cmd.Parameters["@CID"].SqlDbType = System.Data.SqlDbType.Int;
			cmd.Parameters.AddWithValue("@LID", LinkAndCategory_obj.LID);
			cmd.Parameters["@LID"].SqlDbType = System.Data.SqlDbType.Int;
			return cmd;
		}
		#endregion

	}
}
