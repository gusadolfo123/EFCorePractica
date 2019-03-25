namespace EFdNorthWind.Helpers.Log
{
    using System;
    using System.Text;
    using EFdNorthWind.Services;
    using System.Collections.Generic;
    using EFdNorthWind.Entities.Enums;

    public static class LogHelper
    {
        private static LogBase logger = null;

        public static void Log(LogTarget target, List<string> messages)
        {
            if(target == LogTarget.File)
            {
                logger = new FileLogger();
                logger.Log(messages);
            }
            else if (target == LogTarget.DataBase)
            {
                logger = new DBLogger();
                logger.Log(messages);
            }
            else if (target == LogTarget.EventLog)
            {
                logger = new EventLogger();
                logger.Log(messages);
            }
        }
    }
}
