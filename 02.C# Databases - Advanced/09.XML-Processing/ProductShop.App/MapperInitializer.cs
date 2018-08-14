using AutoMapper;
using ProductShop.App.MapperProfiles;

namespace ProductShop.App
{
    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new ProductShopProfile()));
        }
    }
}
