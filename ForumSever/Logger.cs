using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Log
{
    /// <summary>
    /// this class was taken from the internet and was modified by Vadi
    /// </summary>
    public class Logger
    {

        private static int _ALL = 100;
        private static int _INFO = 75;
        private static int _ERROR = 50;
        private static int _OFF = 0;
        public static String _LEVEL = "ALL" ; //ALL|INFO|ERROR|OFF        
        //private static String logDirectory = "../../../Furom_Log";
        private static String logDirectory = "C:/Users/Vadi/Desktop/furom/Furom_Log";
        public static int ALL
        {
            get { return _ALL; }
        }

        public static string LOGDIR
        {
            set { logDirectory  = value; }
        }

        public static int ERROR
        {
            get { return _ERROR; }
        }

        public static int INFO
        {
            get { return _INFO; }
        }

        public static int OFF
        {
            get { return _OFF; }
        }

        public static string LEVEL
        {
            set { _LEVEL = value; }
        }

        /// <summary>
        /// author:Vadi podolich (modified)
        /// this method append a massege to the logger if the massege LEVEL is lower or equal to the loggers  _LEVEL
        /// </summary>
        /// <param name="message"> the massege to log</param>
        /// <param name="level">the level of the massege</param>
        public static void append(String message, int level)
        {
            int logLevel = _OFF;
            String strLogLevel = _LEVEL;
            switch (strLogLevel)
            {
                case "ALL":
                    logLevel = _ALL;
                    break;
  
                case "ERROR":
                    logLevel = _ERROR;
                    break;

                case "INFO":
                    logLevel = _INFO;
                    break;

                default:
                    logLevel = _OFF;
                    break;
            }
            if (logLevel >= level)
            {

                DateTime dt = DateTime.Now;
                String filePath = logDirectory + dt.ToString("yyyyMMdd") + ".log";
                if (!File.Exists(filePath))
                {
                    FileStream fs = File.Create(filePath);
                    fs.Close();
                }

                try
                {
                    StreamWriter sw = File.AppendText(filePath);
                    sw.WriteLine(dt.ToString("hh:mm:ss") + "|" + message);
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
            }
        }
    }
}
