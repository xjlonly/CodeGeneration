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
	/// ���ݿ����:FreeLink
	/// </summary>
	public class FreeLink
	{
		#region ��ѯ
		#region ��ѯ���������ݱ�
		/// <summary>
		/// ��ѯ���������ݱ�
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">������ date desc,id asc</param>
		/// <param name="ParamsList">����ֵ�б��� {"123",1}</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere = "", string sqlSort = "", string sqlCols = "*", object[] ParamsList = null)
		{
			DataTable table = new DataTable();
			string txtCols = sqlCols;
			if (!string.IsNullOrEmpty(sqlCols))
			{
				txtCols = "*";
			}
			StringBuilder sql = new StringBuilder("SELECT " + sqlCols + " FROM FreeLink(NOLOCK)");
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
		#region ��ѯ�����ض����б�
		/// <summary>
		/// ��ѯ�����ض����б�
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">������ date desc,id asc</param>
		/// <param name="ParamsList">�����б�</param>
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
		#region ��ѯ�����ض����б�
		/// <summary>
		/// ��ѯ�����ض����б�
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and usertype=@usertype</param>
		/// <param name="sqlSort">������ date desc,id asc</param>
		/// <param name="ParamsList">�����б�</param>
		/// <returns></returns>
		public static List<ORM.Test.FreeLink> GetList(string sqlWhere = "", string sqlSort = "", object[] ParamsList = null)
		{
			DataTable table = GetTable(sqlWhere, sqlSort, "*", ParamsList);
			List<ORM.Test.FreeLink> rtnObjList = new List<ORM.Test.FreeLink>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.FreeLink(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region ��ҳ��ѯ
		/// <summary>
		/// ��ҳ��ѯ
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <param name="sqlSort">��������,��username desc</param>
		/// <param name="sqlCols">���ݿ��ֶ�����,�ö��ŷָ�,����:username,userpwd,userid</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <param name="pageSize">ҳ��С</param>
		/// <param name="recordCount">��¼����</param>
		/// <returns></returns>
		public static List<ORM.Test.FreeLink> GetList(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out int recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt32(LRecordCount);
			List<ORM.Test.FreeLink> rtnObjList = new List<ORM.Test.FreeLink>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.FreeLink(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region ��ҳ��ѯ
		/// <summary>
		/// ��ҳ��ѯ
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <param name="sqlSort">��������,��username desc</param>
		/// <param name="sqlCols">���ݿ��ֶ�����,�ö��ŷָ�,����:username,userpwd,userid</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <param name="pageSize">ҳ��С</param>
		/// <param name="recordCount">��¼����</param>
		/// <returns></returns>
		public static List<ORM.Test.FreeLink> GetList(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out long recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt64(LRecordCount);
			List<ORM.Test.FreeLink> rtnObjList = new List<ORM.Test.FreeLink>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					rtnObjList.Add(new ORM.Test.FreeLink(dr));
				}
			}
			return rtnObjList;
		}
		#endregion
		#region ��������:ID ��ѯ
		/// <summary>
		/// ��������:ID ��ѯ
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static DataTable GetTableByID(Int32 _ID)
		{
			DataTable table = new DataTable();
			string sql = "SELECT * FROM FreeLink(NOLOCK) WHERE [ID]=@ID";
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
		#region ��������:ID ��ѯ�����ض����б�
		/// <summary>
		/// ��������:ID ��ѯ�����ض����б�
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static List<ORM.Test.FreeLink> GetListByID(Int32 _ID)
		{
			DataTable table = GetTableByID(_ID);
			List<ORM.Test.FreeLink> objList = new List<ORM.Test.FreeLink>();
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow dr in table.Rows)
				{
					objList.Add(new ORM.Test.FreeLink(dr));
				}
			}
			return objList;
		}
		#endregion
		#region ��ѯĳ�ֶ����ݵ�һ�е�һ������
		/// <summary>
		/// ������ѯ,��ѯĳ�ֶ����ݵ�һ�е�һ������
		/// </summary>
		/// <param name="sqlCol">���ݿ��ֶ���,Ҳ����ΪCOUNT(),TOP 1 ����</param>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123' and usertype=@usertype</param>
		/// <param name="sqlSort">������ date desc,id asc</param>
		/// <param name="ParamsList">�����б�</param>
		/// <returns></returns>
		public static object GetSingle(string sqlCol, string sqlWhere = "", string sqlSort = "", object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("SELECT TOP 1 " + sqlCol + " FROM FreeLink(NOLOCK)");
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
		#region ������ѯ,��ѯ��¼����
		/// <summary>
		/// ��ѯ��¼����
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123',���ɴ���������</param>
		/// <returns></returns>
		public static long CountOutLong(string sqlWhere, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("SELECT COUNT(1) FROM FreeLink(NOLOCK) WHERE 1=1");
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
		#region ������ѯ,��ѯ��¼����
		/// <summary>
		/// ������ѯ,��ѯ��¼����
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123',���ɴ���������</param>
		/// <returns></returns>
		public static int Count(string sqlWhere, object[] ParamsList = null)
		{
			long rtnInt= CountOutLong(sqlWhere, ParamsList);
			return Convert.ToInt32(rtnInt);
		}
		#endregion
		#region ��ҳ��ѯ
		/// <summary>
		/// ��ҳ��ѯ
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <param name="sqlSort">��������,��username desc</param>
		/// <param name="sqlCols">���ݿ��ֶ�����,�ö��ŷָ�,����:username,userpwd,userid</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <param name="pageSize">ҳ��С</param>
		/// <param name="recordCount">��¼����</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out int recordCount, object[] ParamsList = null)
		{
			long LRecordCount = 0;
			DataTable table = GetTable(sqlWhere, sqlSort, sqlCols, pageIndex, pageSize, out LRecordCount, ParamsList);
			recordCount = Convert.ToInt32(LRecordCount);
			return table;
		}
		#endregion
		#region ��ҳ��ѯ
		/// <summary>
		/// ��ҳ��ѯ
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <param name="sqlSort">��������,��username desc</param>
		/// <param name="sqlCols">���ݿ��ֶ�����,�ö��ŷָ�,����:username,userpwd,userid</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <param name="pageSize">ҳ��С</param>
		/// <param name="recordCount">��¼����</param>
		/// <returns></returns>
		public static DataTable GetTable(string sqlWhere, string sqlSort, string sqlCols, int pageIndex, int pageSize, out long recordCount, object[] ParamsList = null)
		{
			int SI = pageIndex * pageSize - pageSize + 1;
			int EI = pageIndex * pageSize;
			DataSet ds = new DataSet();
			if (string.IsNullOrEmpty(sqlCols))
			{
				sqlCols = "ID,LinkTitle,LinkContent,LinkDescription,LinkMark";
			}
			StringBuilder sql = new StringBuilder("WITH PST(RN," + sqlCols + ") AS");
			sql.Append("(");
			sql.Append("SELECT ROW_NUMBER() OVER(ORDER BY ").Append(sqlSort).Append(") RN,").Append(sqlCols).Append(" ");
			sql.Append("FROM FreeLink(NOLOCK)");
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
				sql.Append(";SELECT COUNT(*) FROM FreeLink(NOLOCK) WHERE ").Append(sqlWhere);
			}
			else
			{
				sql.Append(";SELECT COUNT(*) FROM FreeLink(NOLOCK)");
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
		#region ��ѯ����������,���ص�һ������
		/// <summary>
		/// ��ѯ����������,���ص�һ������
		/// </summary>
		/// <param name="sqlWhere">��ѯ����</param>
		/// <param name="sqlSort">��������</param>
		/// <returns></returns>
		public static ORM.Test.FreeLink Get(string sqlWhere, string sqlSort = "", object[] ParamsList = null)
		{
			List<ORM.Test.FreeLink> objList = GetList(sqlWhere, sqlSort, ParamsList);
			if(objList != null && objList.Count > 0)
			{
				return objList[0];
			}
			else
			{
				return new ORM.Test.FreeLink();
			}
		}
		#endregion
		#endregion

		#region ɾ��
		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <returns></returns>
		public static int Delete(string sqlWhere, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("DELETE FROM FreeLink WHERE ");
			sql.Append(sqlWhere);
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			return new ORM.DBUtility.DBHelper(false).ExecNonQuery(cmd);
		}
		#endregion

		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="sqlWhere">��ѯ��������username='123' and password='123'</param>
		/// <param name="sqlSet">�������ã���username='123',password='123'</param>
		/// <returns></returns>
		public static int Update(string sqlWhere, string sqlSet, object[] ParamsList = null)
		{
			StringBuilder sql = new StringBuilder("UPDATE FreeLink SET ");
			sql.Append(sqlSet).Append(" WHERE ").Append(sqlWhere);
			SqlCommand cmd = BuildCommand(sql.ToString(), ParamsList);
			return new ORM.DBUtility.DBHelper(false).ExecNonQuery(cmd);
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="FreeLink_obj"></param>
		/// <returns></returns>
		public static int Update(ORM.Test.FreeLink FreeLink_obj)
		{
			StringBuilder sqlSet = new StringBuilder();
			StringBuilder sqlWhere = new StringBuilder();
			string sql = "UPDATE FreeLink SET {0} WHERE {1}";
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.AddWithValue("@ID", FreeLink_obj.ID);
			cmd.Parameters["@ID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlWhere.Length == 0)
			{
				sqlWhere.Append("[ID]=@ID");
			}
			else
			{
				sqlWhere.Append(" AND [ID]=@ID");
			}
			if (FreeLink_obj.LinkTitle != null)
			{
				cmd.Parameters.AddWithValue("@LinkTitle", FreeLink_obj.LinkTitle);
				cmd.Parameters["@LinkTitle"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkTitle]=@LinkTitle");
				}
				else
				{
					sqlSet.Append(",[LinkTitle]=@LinkTitle");
				}
			}
			if (FreeLink_obj.LinkContent != null)
			{
				cmd.Parameters.AddWithValue("@LinkContent", FreeLink_obj.LinkContent);
				cmd.Parameters["@LinkContent"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkContent]=@LinkContent");
				}
				else
				{
					sqlSet.Append(",[LinkContent]=@LinkContent");
				}
			}
			if (FreeLink_obj.LinkDescription != null)
			{
				cmd.Parameters.AddWithValue("@LinkDescription", FreeLink_obj.LinkDescription);
				cmd.Parameters["@LinkDescription"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkDescription]=@LinkDescription");
				}
				else
				{
					sqlSet.Append(",[LinkDescription]=@LinkDescription");
				}
			}
			if (FreeLink_obj.LinkMark != null)
			{
				cmd.Parameters.AddWithValue("@LinkMark", FreeLink_obj.LinkMark);
				cmd.Parameters["@LinkMark"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkMark]=@LinkMark");
				}
				else
				{
					sqlSet.Append(",[LinkMark]=@LinkMark");
				}
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
		/// ��������
		/// </summary>
		/// <param name="FreeLink_obj"></param>
		/// <param name="IsRowLock">�Ƿ�����</param>
		/// <returns></returns>
		public static int Update(ORM.Test.FreeLink FreeLink_obj, bool IsRowLock)
		{
			StringBuilder sqlSet = new StringBuilder();
			StringBuilder sqlWhere = new StringBuilder();
			string sql = string.Empty;
			if (IsRowLock)
			{
				sql = "UPDATE FreeLink WITH(ROWLOCK) SET {0} WHERE {1}";
			}
			else
			{
				sql = "UPDATE FreeLink SET {0} WHERE {1}";
			}
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.AddWithValue("@ID", FreeLink_obj.ID);
			cmd.Parameters["@ID"].SqlDbType = System.Data.SqlDbType.Int;
			if (sqlWhere.Length == 0)
			{
				sqlWhere.Append("[ID]=@ID");
			}
			else
			{
				sqlWhere.Append(" AND [ID]=@ID");
			}
			if (FreeLink_obj.LinkTitle != null)
			{
				cmd.Parameters.AddWithValue("@LinkTitle", FreeLink_obj.LinkTitle);
				cmd.Parameters["@LinkTitle"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkTitle]=@LinkTitle");
				}
				else
				{
					sqlSet.Append(",[LinkTitle]=@LinkTitle");
				}
			}
			if (FreeLink_obj.LinkContent != null)
			{
				cmd.Parameters.AddWithValue("@LinkContent", FreeLink_obj.LinkContent);
				cmd.Parameters["@LinkContent"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkContent]=@LinkContent");
				}
				else
				{
					sqlSet.Append(",[LinkContent]=@LinkContent");
				}
			}
			if (FreeLink_obj.LinkDescription != null)
			{
				cmd.Parameters.AddWithValue("@LinkDescription", FreeLink_obj.LinkDescription);
				cmd.Parameters["@LinkDescription"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkDescription]=@LinkDescription");
				}
				else
				{
					sqlSet.Append(",[LinkDescription]=@LinkDescription");
				}
			}
			if (FreeLink_obj.LinkMark != null)
			{
				cmd.Parameters.AddWithValue("@LinkMark", FreeLink_obj.LinkMark);
				cmd.Parameters["@LinkMark"].SqlDbType = System.Data.SqlDbType.NVarChar;
				if (sqlSet.Length == 0)
				{
					sqlSet.Append("[LinkMark]=@LinkMark");
				}
				else
				{
					sqlSet.Append(",[LinkMark]=@LinkMark");
				}
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

		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		/// <returns></returns>
		public static bool insert( ORM.Test.FreeLink FreeLink_obj)
		{
			StringBuilder cols = new StringBuilder();
			StringBuilder parameters = new StringBuilder();
			string sql = "INSERT INTO FreeLink({0}) values({1})";
			if (cols.Length > 0)
			{
				cols.Append(",[LinkTitle]");
				parameters.Append(",@LinkTitle");
			}
			else
			{
				cols.Append("[LinkTitle]");
				parameters.Append("@LinkTitle");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkContent]");
				parameters.Append(",@LinkContent");
			}
			else
			{
				cols.Append("[LinkContent]");
				parameters.Append("@LinkContent");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkDescription]");
				parameters.Append(",@LinkDescription");
			}
			else
			{
				cols.Append("[LinkDescription]");
				parameters.Append("@LinkDescription");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkMark]");
				parameters.Append(",@LinkMark");
			}
			else
			{
				cols.Append("[LinkMark]");
				parameters.Append("@LinkMark");
			}
			sql = string.Format(sql, cols.ToString(), parameters.ToString());
			SqlCommand cmd = new SqlCommand(sql);
			cmd = ParameterElse(cmd, FreeLink_obj);
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
		/// ��������,����������ID
		/// </summary>
		/// <returns></returns>
		public static bool Add( ORM.Test.FreeLink FreeLink_obj, out Int32 ID)
		{
			ID = 0;
			StringBuilder cols = new StringBuilder();
			StringBuilder parameters = new StringBuilder();
			string sql = "INSERT INTO FreeLink({0}) values({1});SELECT @@IDENTITY;";
			if (cols.Length > 0)
			{
				cols.Append(",[LinkTitle]");
				parameters.Append(",@LinkTitle");
			}
			else
			{
				cols.Append("[LinkTitle]");
				parameters.Append("@LinkTitle");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkContent]");
				parameters.Append(",@LinkContent");
			}
			else
			{
				cols.Append("[LinkContent]");
				parameters.Append("@LinkContent");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkDescription]");
				parameters.Append(",@LinkDescription");
			}
			else
			{
				cols.Append("[LinkDescription]");
				parameters.Append("@LinkDescription");
			}
			if (cols.Length > 0)
			{
				cols.Append(",[LinkMark]");
				parameters.Append(",@LinkMark");
			}
			else
			{
				cols.Append("[LinkMark]");
				parameters.Append("@LinkMark");
			}
			sql = string.Format(sql, cols.ToString(), parameters.ToString());
			SqlCommand cmd = new SqlCommand(sql);
			cmd = ParameterElse(cmd, FreeLink_obj);
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

		#region ��ѯִ��������
		/// <summary>
		/// ��ѯִ��������
		/// </summary>
		/// <param name="sql">����SQL���</param>
		/// <param name="ParamsList">��ѡ�����б�</param>
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
		#region ����׼��
		/// <summary>
		/// ����׼��
		/// </summary>
		/// <returns></returns>
		public static SqlCommand ParameterElse(SqlCommand cmd, ORM.Test.FreeLink FreeLink_obj)
		{
			if (FreeLink_obj.LinkTitle != null)
			{
				cmd.Parameters.AddWithValue("@LinkTitle", FreeLink_obj.LinkTitle);
				cmd.Parameters["@LinkTitle"].SqlDbType = System.Data.SqlDbType.NVarChar;
			}
			if (FreeLink_obj.LinkContent != null)
			{
				cmd.Parameters.AddWithValue("@LinkContent", FreeLink_obj.LinkContent);
				cmd.Parameters["@LinkContent"].SqlDbType = System.Data.SqlDbType.NVarChar;
			}
			if (FreeLink_obj.LinkDescription != null)
			{
				cmd.Parameters.AddWithValue("@LinkDescription", FreeLink_obj.LinkDescription);
				cmd.Parameters["@LinkDescription"].SqlDbType = System.Data.SqlDbType.NVarChar;
			}
			if (FreeLink_obj.LinkMark != null)
			{
				cmd.Parameters.AddWithValue("@LinkMark", FreeLink_obj.LinkMark);
				cmd.Parameters["@LinkMark"].SqlDbType = System.Data.SqlDbType.NVarChar;
			}
			return cmd;
		}
		#endregion

	}
}
