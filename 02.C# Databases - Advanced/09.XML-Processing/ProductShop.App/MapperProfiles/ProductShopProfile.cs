using System;
using AutoMapper;
using ProductShop.App.DTOs;
using ProductShop.Models;

namespace ProductShop.App.MapperProfiles
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
