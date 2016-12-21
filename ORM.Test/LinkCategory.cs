using System;
using System.Data;
using System.Collections.Generic;

namespace ORM.Test
{
	/// <summary>
	/// 数据实体类:LinkCategory
	/// </summary>
	[Serializable]
	public class LinkCategory : ORM.Base.ModelBase
	{
		#region 表名
		private static string _tablename = "LinkCategory";
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName { get { return _tablename; } }
		#endregion

		#region 实体类型
		private static string _typeName = "ORM.Test.LinkCategory";
		#endregion

		#region 列名列表
		private static List<string> _cols = new List<string>() { "ID", "CATEGORY", "DESCRIPTION" };
		/// <summary>
		/// 列名列表
		/// </summary>
		public List<string> ColList { get { return _cols; } }
		#endregion

		#region 字段、属性
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
		public string Category
		{
			get{ return this._Category;}
			set{ this._Category = value;}
		}
		private string _Category = "";
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			get{ return this._Description;}
			set{ this._Description = value;}
		}
		private string _Description = "";
		#endregion

		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public LinkCategory()
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
		/// 构造函数,初始化一行数据
		/// </summary>
		public LinkCategory(DataRow dr) : this()
		{
			if (dr != null && dr.Table.Columns.Contains("ID") && !(dr["ID"] is DBNull))
			{
				this._ID = Convert.ToInt32(dr["ID"]);
			}
			if (dr != null && dr.Table.Columns.Contains("Category") && !(dr["Category"] is DBNull))
			{
				this._Category = Convert.ToString(dr["Category"]);
			}
			if (dr != null && dr.Table.Columns.Contains("Description") && !(dr["Description"] is DBNull))
			{
				this._Description = Convert.ToString(dr["Description"]);
			}
		}
		#endregion
	}
}
