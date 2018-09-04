using SoftJail.Data.Models;
using SoftJail.DataProcessor.ImportDto;

namespace SoftJail
{
    using AutoMapper;


    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            CreateMap<DepartmentDto, Department>()
                .ReverseMap();

            CreateMap<CellDto, Cell>()
                .ReverseMap();

            CreateMap<PrisonerDto, Prisoner>()
                .ReverseMap();

            CreateMap<MailDto, Mail>()
                .ReverseMap();

            CreateMap<OfficerDto, Officer>()
                .ForMember(dest => dest.Salary,
                    from => from.MapFrom(src => src.Money))
                .ForMember(dest => dest.FullName,
                    from => from.MapFrom(src => src.Name))
                .ForMember(dest => dest.Position,
                    from => from.MapFrom(src => src.Position))
                .ForMember(dest => dest.Weapon,
                    from => from.MapFrom(src => src.Weapon))
                .ReverseMap();
        }
    }
}
