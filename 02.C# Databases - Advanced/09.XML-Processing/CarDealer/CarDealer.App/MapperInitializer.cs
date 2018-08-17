using AutoMapper;
using CarDealer.App.MapperProfiles;

namespace CarDealer.App
{
    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));
        }
    }
}
