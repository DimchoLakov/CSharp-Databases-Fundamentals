using AutoMapper;
using CarDealer.App.DTOs.Import;
using CarDealer.Models;

namespace CarDealer.App.MapperProfiles
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDto, Supplier>()
                .ReverseMap();

            CreateMap<ImportPartDto, Part>()
                .ReverseMap();

            CreateMap<ImportCarDto, Car>()
                .ReverseMap();

            CreateMap<ImportCustomerDto, Customer>()
                .ReverseMap();
        }
    }
}
