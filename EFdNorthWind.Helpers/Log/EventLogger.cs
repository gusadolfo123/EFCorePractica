namespace EFdNorthWind.Helpers.Log
{
    using System;
    using System.Text;
    using EFdNorthWind.Services;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class EventLogger : LogBase
    {
        public override void Log(List<string> messages)
        {
            foreach (var msg in messages)
            {
                EventLog.WriteEntry("EFNorthwind", msg);
            }
        }
    }
}
