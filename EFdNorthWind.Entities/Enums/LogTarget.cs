namespace EFdNorthWind.Entities.Enums
{
    public sealed class LogTarget
    {
        public static readonly LogTarget File = new LogTarget(1, nameof(File));
        public static readonly LogTarget DataBase = new LogTarget(2, nameof(DataBase));
        public static readonly LogTarget EventLog = new LogTarget(3, nameof(EventLog));

        public readonly int Id;
        public readonly string Name;

        public LogTarget(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
