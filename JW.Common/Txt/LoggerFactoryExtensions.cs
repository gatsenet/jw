
using Microsoft.Extensions.Logging;

namespace JW.Common
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory)
        {
            factory.AddProvider(new FileLoggerProvider());
            return factory;
        }
    }
}
