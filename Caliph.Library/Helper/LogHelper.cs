using NLog;
using System;
using System.Text;

namespace Caliph.Library.Helper
{
    public class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string LogFormat(string function, string functionParam, string exception, bool enableAPINotify = true)
        {
            StringBuilder logMsg = new StringBuilder();
            logMsg.AppendFormat("Function : {0} | Param : {1} | {2}",
                function,
                functionParam,
                exception == "" ? "" : "Exp: " + exception);

            return logMsg.ToString();
        }

        /// <summary>
        /// Logger Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            try
            {
                logger.Error(msg);
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        /// <summary>
        /// Logger Info
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            try
            {
                logger.Info(msg);
            }
            catch (Exception)
            {
                // do nothing
            }
        }
    }
}
