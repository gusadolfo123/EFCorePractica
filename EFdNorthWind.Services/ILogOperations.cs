namespace EFdNorthWind.Services
{
    using EFdNorthWind.Entities;
    using System.Collections.Generic;

    public interface ILogOperations
    {
        List<Log> GetAll();
    }
}
