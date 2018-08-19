using AutoMapper;
using ProductShop.App.MapperProfiles;

namespace ProductShop.App
{
    public class MapperInitializer
    {
        public MapperInitializer()
        {

        }

        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
        }
    }
}
