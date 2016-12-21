using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ORM.Base;
using ORM.Test;

namespace ORM.MT
{
    class Program
    {
        public static void Main(string[] args)
        {
            FreeLink fl = new FreeLink();
            long rc = 0;
            ORM.Base.ModelBase mb= fl.INNER_JOIN(LinkAndCategory.TableInfo, "lac", "SRCTAB.ID=lac.lid").Where("lac.cid=@cid", new object[] { 1 });
            List<ORM.Base.QueryResult> jr = mb.List("lac.id asc", out rc, 1, 2);
            Console.WriteLine(fl.SQLTEXT);
            Console.WriteLine();
            Console.WriteLine(((LinkAndCategory)jr[0]["LinkAndCategory"]).ID);
            Console.WriteLine((jr[0].Get<LinkAndCategory>()).ID);
            Console.WriteLine(((LinkAndCategory)jr[0][typeof(LinkAndCategory)]).ID);
            Console.WriteLine(((FreeLink)jr[0][typeof(FreeLink)]).LinkDescription);
            foreach (string tn in jr[0].Tables)
            {
                Console.WriteLine(tn);
            }
            Console.WriteLine(ORM.Data.DBOper.FreeLink.GetList<LinkCategory>()[0].ID);
            Console.ReadLine();
        }
    }
}