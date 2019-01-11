namespace EFdNorthWind.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public enum LogType
    {
        DeleteCategory,
        DeleteProduct
    }

    public class Log
    {
        public int LogID { get; set; } 

        public DateTime DateTime { get; set; }

        public LogType Type { get; set; }

        public string Message { get; set; } 
    }
}
