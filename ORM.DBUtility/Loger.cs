using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.DBUtility
{
    public class Loger : IDisposable
    {
        private string DBLogEnable = System.Configuration.ConfigurationManager.AppSettings["DBLog_Enable"] ?? "false";
        private string DBLogPath = System.Configuration.ConfigurationManager.AppSettings["DBLog_Path"] ?? "";
        private string DBLog_Throw = System.Configuration.ConfigurationManager.AppSettings["DBLog_Throw"] ?? "false";
        private NLog MyLoger = null;

        public Loger()
        {
            if (!string.IsNullOrEmpty(DBLogPath))
            {
                MyLoger = new NLog(DBLogPath);
            }
        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="CommandText"></param>
        public void Save(Exception e, System.Data.SqlClient.SqlCommand ExCmd)
        {
            try
            {
                if (DBLogEnable.ToUpper() == "TRUE" && !string.IsNullOrEmpty(DBLogPath) && MyLoger != null)
                {
                    StringBuilder LogText = new System.Text.StringBuilder();
                    LogText.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    LogText.Append("Message").AppendLine(e.Message).Append("StackTrace:").AppendLine(e.StackTrace);
                    LogText.Append("CommandText:").AppendLine(ExCmd.CommandText);
                    LogText.AppendLine("SqlParameters:");
                    foreach (System.Data.SqlClient.SqlParameter sqlParam in ExCmd.Parameters)
                    {
                        LogText.Append("\tNAME:").Append(sqlParam.ParameterName).Append(",").Append("VALUE:").AppendLine(sqlParam.SqlValue.ToString());
                    }
                    LogText.AppendLine("-----------------------------------------------------------------------------").Append("\r\n");
                    MyLoger.Info(LogText.ToString());
                }
            }
            catch { }

            if (DBLog_Throw.ToUpper() == "TRUE")
            {
                throw e;
            }
        }

        public void Dispose()
        {
            MyLoger.FinalSave();
        }
    }
}
