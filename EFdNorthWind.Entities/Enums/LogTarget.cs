using System;
using System.Collections.Generic;
using System.Linq;

namespace EFdNorthWind.Entities.Enums
{
    public sealed class LogTarget
    {
        public static readonly LogTarget File = new LogTarget(1, nameof(File));
        public static readonly LogTarget DataBase = new LogTarget(2, nameof(DataBase));
        public static readonly LogTarget EventLog = new LogTarget(3, nameof(EventLog));

        public readonly int Id;
        public readonly string Name;

        private LogTarget(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IReadOnlyCollection<LogTarget> GetLogTargets()
        {
            return new[] { File, DataBase, EventLog };
        }

        public static LogTarget FindBy(int id)
        {
            var target = GetLogTargets().SingleOrDefault(t => t.Id == id);
            if (target == null)
            {
                throw new ArgumentOutOfRangeException($"Invalid id {id}");
            }
            return target;
        }
    }
}
