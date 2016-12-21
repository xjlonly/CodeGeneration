using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM.Base
{
    /// <summary>
    /// 查询结果，对应单行记录
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// 查询结果，对应单行记录
        /// </summary>
        public Dictionary<string, object> RowResultList = new Dictionary<string, object>();
        /// <summary>
        /// 按照表名索引
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public object this[string TypeName]
        {
            get { return RowResultList[TypeName]; }
        }
        /// <summary>
        /// 结果集中表名列表
        /// </summary>
        public List<string> Tables
        {
            get
            {
                return this.RowResultList.Keys.ToList<string>();
            }
        }
        /// <summary>
        /// 返回指定类型的对象
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public object this[Type T]
        {
            get
            {
                foreach (object obj in RowResultList.Values)
                {
                    if (obj.GetType() == T)
                    {
                        return obj;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 返回类型对应指定表对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public T Get<T>(string TypeName)
        {
            return (T)RowResultList[TypeName];
        }
        /// <summary>
        /// 返回类型对应默认表对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            T to = default(T);
            foreach (object obj in RowResultList.Values)
            {
                if (obj is T)
                {
                    to = (T)obj;
                }
            }
            return to;
        }
    }
}