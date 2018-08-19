using AutoMapper;
using CarDealer.App.DTOs.Import;
using CarDealer.Models;

namespace CarDealer.App.MapperProfiles
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>()
                .ReverseMap();

            CreateMap<PartDto, Part>()
                .ReverseMap();

            CreateMap<CarDto, Car>()
                .ReverseMap();

            CreateMap<CustomerDto, Customer>()
                .ReverseMap();
        }
    }
}
