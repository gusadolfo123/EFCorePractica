namespace EFdNorthWind.BLL
{
    using EFdNorthWind.DAL;
    using EFdNorthWind.Entities;
    using EFdNorthWind.Services;
    using System.Collections.Generic;

    public class LogOperations : ILogOperations
    {
        public List<Log> GetAll()
        {
            return NorthWindRepositoryFactory.GetNorthWindRepository().GetLogs();
        }
    }
}
