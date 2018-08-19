using AutoMapper;
using ProductShop.App.DTOs.Export;
using ProductShop.App.DTOs.Import;
using ProductShop.Models;

namespace ProductShop.App.MapperProfiles
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<ImportUserDto, User>()
                .ReverseMap();

            CreateMap<ProductDto, Product>()
                .ReverseMap();

            CreateMap<CategoryDto, Category>()
                .ReverseMap();

            CreateMap<Product, ProductInRangeDto>()
                .ForMember(dest => dest.BuyerFullName,
                    from => from.MapFrom(src => $"{src.Buyer.FirstName} {src.Buyer.LastName}"));
        }
    }
}
