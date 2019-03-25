using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EFdNorthWind.DAL
{
    internal class UpdateDeleteLogger : ILogger
    {
        private readonly List<string> logMessages; 

        public UpdateDeleteLogger(List<string> logMessages)
        {
            this.logMessages = logMessages;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // si esta ejecutando un comando
            if (eventId == RelationalEventId.CommandExecuted.Id)
            {
                var Message = formatter(state, exception);
                var StartCommand = Math.Max(Message.IndexOf("UPDATE"), Message.IndexOf("DELETE")); // SE OBTIENE EL INDICE MAYOR
                if (StartCommand >= 0)
                {
                    int EndCommand = Message.IndexOf(";", StartCommand);
                    int CommandLength = EndCommand - StartCommand;
                    var SqlCommand = Message.Substring(StartCommand, CommandLength);
                    SqlCommand = SqlCommand.Replace("\r\n", " ");

                    string Pattern = @"@p\d+";
                    var Rgx = new Regex(Pattern);

                    var Parameters = Rgx.Matches(SqlCommand);

                    foreach (Match match in Parameters)
                    {
                        string ParamToSearch = $"{match}='";
                        int ParamStart = Message.IndexOf(ParamToSearch);
                        int ParamEnd = Message.IndexOf("'", ParamStart + ParamToSearch.Length);
                        int StartParamValue = ParamStart + ParamToSearch.Length;
                        var ParamValue = Message.Substring(StartParamValue, ParamEnd - StartParamValue);

                        if (ParamValue == "")
                        {
                            ParamValue = "NULL";
                        }
                        SqlCommand = SqlCommand.Replace(match.Value, $"'{ParamValue}'");
                    }
                    logMessages.Add(SqlCommand);
                }
            }
        }
    }
}