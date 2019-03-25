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
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(messages);
                    break;
                case LogTarget.DataBase:
                    logger = new DBLogger();
                    logger.Log(messages);
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(messages);
                    break;
                default:
                    return;
            }
        }
    }
}
