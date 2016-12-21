using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text;

namespace ORM.Base
{
    public class Loger:IDisposable
    {
        private static readonly string DBLogEnable = System.Configuration.ConfigurationManager.AppSettings["DBLog_Enable"] ?? "false";
        private static readonly string DBLogPath = System.Configuration.ConfigurationManager.AppSettings["DBLog_Path"] ?? "";
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="CommandText"></param>
        public void Save(Exception e,System.Data.SqlClient.SqlCommand ExCmd)
        {
            if (DBLogEnable.ToUpper() == "TRUE" && !string.IsNullOrEmpty(DBLogPath))
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
                FileLog.WriteLine(LogText.ToString());
            }
        }

        public void CheckDirs(string DirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(DirPath);
            if (!dir.Parent.Exists)
            {
                CheckDirs(dir.Parent.FullName);
            }
            dir.Create();
        }

        private StreamWriter FileLog
        {
            get
            {
                DirectoryInfo dir = new DirectoryInfo(DBLogPath);
                CheckDirs(DBLogPath);
                string FileName = DateTime.Now.ToString("yyyy-MM-dd HH") + ".log";
                string FileDir = DBLogPath;
                string FileFullPath = FileDir + "\\" + FileName;
                if (File.Exists(FileFullPath))
                {
                    if (_swt == null)
                    {
                        _swt = new StreamWriter(FileFullPath, true, System.Text.Encoding.UTF8);
                        _swt.AutoFlush = true;
                    }
                    return _swt;
                }
                else
                {
                    if (_swt != null)
                    {
                        _swt.Flush();
                        _swt.Close();
                        _swt.Dispose();
                    }
                    _swt = File.CreateText(FileFullPath);
                    _swt.AutoFlush = true;
                    return _swt;
                }
            }
        }

        private StreamWriter _swt;

        public void Dispose()
        {
            FileLog.Flush();
            FileLog.Close();
            FileLog.Dispose();
        }
    }
}