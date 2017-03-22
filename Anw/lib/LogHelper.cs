using System;
using log4net;

namespace AnwConnector.Util
{
    public class LogHelper
    {
        public static readonly ILog Loginfo = LogManager.GetLogger("loginfo");
        public static readonly ILog Logerror = LogManager.GetLogger("logerror");
        public static void WriteLog(string info)
        {
            if (Loginfo.IsInfoEnabled)
            {
                Loginfo.Info(info);
            }
        }
        public static void WriteLog(string info, Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            if (Logerror.IsErrorEnabled)
            {
                Logerror.Error(info, exception);
            }
        }
    }
}