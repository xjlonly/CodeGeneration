using System;
using System.Data;
using System.Collections.Generic;

namespace ORM.Test
{
	/// <summary>
	/// ����ʵ����:FreeLink
	/// </summary>
	[Serializable]
	public class FreeLink : ORM.Base.ModelBase
	{
		#region ����
		private static string _tablename = "FreeLink";
		/// <summary>
		/// ����
		/// </summary>
		public string TableName { get { return _tablename; } }
		#endregion

		#region ʵ������
		private static string _typeName = "ORM.Test.FreeLink";
		#endregion

		#region �����б�
		private static List<string> _cols = new List<string>() { "ID", "LINKTITLE", "LINKCONTENT", "LINKDESCRIPTION", "LINKMARK" };
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
		public string LinkTitle
		{
			get{ return this._LinkTitle;}
			set{ this._LinkTitle = value;}
		}
		private string _LinkTitle = "";
		/// <summary>
		/// 
		/// </summary>
		public string LinkContent
		{
			get{ return this._LinkContent;}
			set{ this._LinkContent = value;}
		}
		private string _LinkContent = "";
		/// <summary>
		/// 
		/// </summary>
		public string LinkDescription
		{
			get{ return this._LinkDescription;}
			set{ this._LinkDescription = value;}
		}
		private string _LinkDescription = "";
		/// <summary>
		/// 
		/// </summary>
		public string LinkMark
		{
			get{ return this._LinkMark;}
			set{ this._LinkMark = value;}
		}
		private string _LinkMark = "";
		#endregion

		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public FreeLink()
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
		public FreeLink(DataRow dr) : this()
		{
			if (dr != null && dr.Table.Columns.Contains("ID") && !(dr["ID"] is DBNull))
			{
				this._ID = Convert.ToInt32(dr["ID"]);
			}
			if (dr != null && dr.Table.Columns.Contains("LinkTitle") && !(dr["LinkTitle"] is DBNull))
			{
				this._LinkTitle = Convert.ToString(dr["LinkTitle"]);
			}
			if (dr != null && dr.Table.Columns.Contains("LinkContent") && !(dr["LinkContent"] is DBNull))
			{
				this._LinkContent = Convert.ToString(dr["LinkContent"]);
			}
			if (dr != null && dr.Table.Columns.Contains("LinkDescription") && !(dr["LinkDescription"] is DBNull))
			{
				this._LinkDescription = Convert.ToString(dr["LinkDescription"]);
			}
			if (dr != null && dr.Table.Columns.Contains("LinkMark") && !(dr["LinkMark"] is DBNull))
			{
				this._LinkMark = Convert.ToString(dr["LinkMark"]);
			}
		}
		#endregion
	}
}
