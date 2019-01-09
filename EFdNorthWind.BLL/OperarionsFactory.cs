namespace EFdNorthWind.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EFdNorthWind.Services;

    public static class OperarionsFactory
    {
        public static ICategoryOperations GetCategoryOperations()
        {
            return new CategoryOperations();
        }

        public static ILogOperations GetLogOperations()
        {
            return new LogOperations();
        }
    }
}
