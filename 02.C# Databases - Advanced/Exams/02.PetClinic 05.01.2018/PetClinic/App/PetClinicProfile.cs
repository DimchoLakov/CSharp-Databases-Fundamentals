using PetClinic.DTOs.Import;
using PetClinic.Models;

namespace PetClinic.App
{
    using AutoMapper;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<AnimalAidDto, AnimalAid>()
                .ReverseMap();

            CreateMap<AnimalDto, Animal>()
                .ReverseMap();

            CreateMap<PassportDto, Passport>()
                .ReverseMap();

            CreateMap<VetDto, Vet>()
                .ReverseMap();
        }
    }
}
