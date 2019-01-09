namespace EFdNorthWind.DAL
{
    using EFdNorthWind.Services;

    public static class NorthWindRepositoryFactory
    {
        public static INorthWindRepository GetNorthWindRepository(bool isUnitOfWork = false)
        {
            // Factory Method no se especifica la clase exacta que se implementa
            return new NorthWindRepository(isUnitOfWork);
        }
    }
}
