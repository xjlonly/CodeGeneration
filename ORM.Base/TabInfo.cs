using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM.Base
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TabInfo
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 数据库表字段列表
        /// </summary>
        public List<string> ColList { get; set; }
        /// <summary>
        /// 实体类型名
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 实体类所在程序集
        /// </summary>
        public string AssemblyName { get; set; }
    }
}