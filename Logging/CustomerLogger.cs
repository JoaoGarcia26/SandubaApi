namespace SandubaApi.Logging
{
    public class CustomerLogger : ILogger
    {
        private readonly string loggerName;
        private readonly CustomLoggingProviderConfiguration loggerConfig;
        private string name;

        public CustomerLogger(string name, CustomLoggingProviderConfiguration loggerConfig)
        {
            this.loggerName = name;
            this.loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
            EscreverTextoNoArquivo(mensagem);
        }

        private void EscreverTextoNoArquivo(string mensagem)
        {
            string path = @"C:\Users\João Victor\Documents\GitHub\SandubaApi\Assets\Logs\log_SandubaApi.txt";
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                try
                {
                    stream.WriteLine(mensagem);
                    stream.Close();
                } catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
