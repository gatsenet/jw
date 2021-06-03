using Microsoft.Extensions.Logging;

namespace JW.Common
{
    public class UtilLogger<T>
    {
        private static ILogger iLog;
        public static ILogger Log
        {
            get
            {
                if (iLog != null) return iLog;

                ////第一种写法
                //ILoggerFactory loggerFactory = new LoggerFactory();
                //loggerFactory.AddFileLogger();
                //iLog = loggerFactory.CreateLogger<DbCommand>();

                //第二种写法
                iLog = new LoggerFactory().AddFileLogger().CreateLogger<T>();
                return iLog;
            }
            set => iLog = value;
        }
    }
}
