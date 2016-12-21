using System;
using System.Data;
using System.Collections.Generic;

namespace ORM.Test
{
	/// <summary>
	/// ����ʵ����:LinkAndCategory
	/// </summary>
	[Serializable]
	public class LinkAndCategory : ORM.Base.ModelBase
	{
		#region ����
		private static string _tablename = "LinkAndCategory";
		/// <summary>
		/// ����
		/// </summary>
		public string TableName { get { return _tablename; } }
		#endregion

		#region ʵ������
		private static string _typeName = "ORM.Test.LinkAndCategory";
		#endregion

		#region �����б�
		private static List<string> _cols = new List<string>() { "ID", "CID", "LID" };
		/// <summary>
		/// �����б�
		/// </summary>
		public List<string> ColList { get { return _cols; } }
		#endregion

		#region �ֶΡ�����
		/// <summary>
		/// 
		/// </summary>
		public Int32 ID
		{
			get{ return this._ID;}
			set{ this._ID = value;}
		}
		private Int32 _ID;
		/// <summary>
		/// 
		/// </summary>
		public Int32 CID
		{
			get{ return this._CID;}
			set{ this._CID = value;}
		}
		private Int32 _CID;
		/// <summary>
		/// 
		/// </summary>
		public Int32 LID
		{
			get{ return this._LID;}
			set{ this._LID = value;}
		}
		private Int32 _LID;
		#endregion

		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public LinkAndCategory()
		{
			_ConnectionMark = "CNMSGS";
			_tabinfo = TableInfo;
			base.Init();
		}
		public static ORM.Base.TabInfo TableInfo = new Base.TabInfo()
		{
			TableName = _tablename,
			ColList = _cols,
			TypeName = _typeName,
			AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location
		};
		/// <summary>
		/// ���캯��,��ʼ��һ������
		/// </summary>
		public LinkAndCategory(DataRow dr) : this()
		{
			if (dr != null && dr.Table.Columns.Contains("ID") && !(dr["ID"] is DBNull))
			{
				this._ID = Convert.ToInt32(dr["ID"]);
			}
			if (dr != null && dr.Table.Columns.Contains("CID") && !(dr["CID"] is DBNull))
			{
				this._CID = Convert.ToInt32(dr["CID"]);
			}
			if (dr != null && dr.Table.Columns.Contains("LID") && !(dr["LID"] is DBNull))
			{
				this._LID = Convert.ToInt32(dr["LID"]);
			}
		}
		#endregion
	}
}
