﻿namespace SandubaApi.Logging
{
    public class CustomLoggingProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
