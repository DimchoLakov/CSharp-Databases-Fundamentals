namespace Employees.App.Mapper
{
    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile(typeof(EmployeeProfile)));
        }
    }
}
