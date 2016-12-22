using System;
using System.Collections.Generic;
using System.IO;

namespace ORM.DBUtility
{
    public class NLog
    {
        /// <summary>
        /// 日志队列
        /// </summary>
        private Queue<LogInfo> _Ilog = new Queue<LogInfo>();
        /// <summary>
        /// 定时器：保存数据
        /// </summary>
        private System.Timers.Timer SaveTimer = new System.Timers.Timer(60000);
        /// <summary>
        /// 文件对象
        /// </summary>
        private FileInfo LogFile = null;
        /// <summary>
        /// 日志路径
        /// </summary>
        private string LogFilePath = "";

        public NLog(string _LogFilePath)
        {
            LogFilePath = _LogFilePath;
            CheckDir(LogFilePath);
            SaveTimer.Elapsed += new System.Timers.ElapsedEventHandler(SaveTimer_Elapsed);
            SaveTimer.AutoReset = true;
            SaveTimer.Start();
        }
        /// <summary>
        /// 定时保存日志数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                CheckFile();
                using (StreamWriter swt = LogFile.AppendText())
                {
                    swt.AutoFlush = true;
                    int i = _Ilog.Count;
                    while (i > 0)
                    {
                        LogInfo li = _Ilog.Dequeue();
                        swt.WriteLine("[@Log]:" + li.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + li.Content);
                        i--;
                    }
                    swt.Close();
                    swt.Dispose();
                }
            }
            catch { }
        }
        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        private void CheckFile()
        {
            string FileDir = LogFilePath + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            CheckDir(FileDir);
            string FileLocation = FileDir + "\\DBU." + DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(":", "-") + ".log";
            if (LogFile == null || LogFile.FullName != FileLocation)
            {
                LogFile = new FileInfo(FileLocation);
            }
        }

        private void CheckDir(string DirName)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(DirName);
            if (dirinfo.Exists)
            {
                return;
            }
            if (dirinfo.Parent != null)
            {
                CheckDir(dirinfo.Parent.FullName);
            }
            if (!DirName.Contains(":") && !dirinfo.Exists)
            {
                dirinfo.Create();
            }
        }

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="BlockCode"></param>
        public void Info(string message)
        {
            _Ilog.Enqueue(new LogInfo() { Content = message, Time = DateTime.Now });
        }
        /// <summary>
        /// 日志对象
        /// </summary>
        public class LogInfo
        {
            /// <summary>
            /// 事件时间
            /// </summary>
            public DateTime Time { get; set; }
            /// <summary>
            /// 时间内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 请求批次号码
            /// </summary>
            public string BlockCode { get; set; }
        }
        public void FinalSave()
        {
            try
            {
                CheckFile();
                using (StreamWriter swt = LogFile.AppendText())
                {
                    swt.AutoFlush = true;
                    int i = _Ilog.Count;
                    while (i > 0)
                    {
                        LogInfo li = _Ilog.Dequeue();
                        swt.WriteLine("[@Log]:" + li.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + li.Content);
                        i--;
                    }
                    swt.Close();
                    swt.Dispose();
                }
            }
            catch { }
        }
    }
}
