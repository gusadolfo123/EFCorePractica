namespace EFdNorthWind.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class LogBase
    {
        public abstract void Log(List<string> messages);
    }
}
