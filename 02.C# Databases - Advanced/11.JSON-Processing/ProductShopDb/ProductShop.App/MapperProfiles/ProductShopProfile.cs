using AutoMapper;
using ProductShop.App.Dto.Import;
using ProductShop.Models;

namespace ProductShop.App.MapperProfiles
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<ImportUserDto, User>()
                .ReverseMap();

            CreateMap<ImportProductDto, Product>()
                .ReverseMap();

            CreateMap<ImportCategoryDto, Category>()
                .ReverseMap();
        }
    }
}
