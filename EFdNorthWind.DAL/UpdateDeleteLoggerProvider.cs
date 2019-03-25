namespace EFdNorthWind.DAL
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UpdateDeleteLoggerProvider : ILoggerProvider
    {
        readonly List<string> LogMessages;

        public UpdateDeleteLoggerProvider(List<string> logmessages)
        {
            this.LogMessages = logmessages;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new UpdateDeleteLogger(LogMessages);
        }

        public void Dispose()
        {
        }
    }
}
