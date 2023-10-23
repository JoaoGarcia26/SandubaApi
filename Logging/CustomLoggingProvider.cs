using System.Collections.Concurrent;

namespace SandubaApi.Logging
{
    public class CustomLoggingProvider : ILoggerProvider
    {
        private readonly CustomLoggingProviderConfiguration loggerConfig;
        private readonly ConcurrentDictionary<string, CustomerLogger> logs = new ConcurrentDictionary<string, CustomerLogger>();
        public CustomLoggingProvider(CustomLoggingProviderConfiguration config)
        {
            loggerConfig = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return logs.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            logs.Clear();
        }
    }
}
