using AutoMapper;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;

namespace FastFood.App
{
	public class FastFoodProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public FastFoodProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Position,
                    opts => opts.Ignore())
                .ReverseMap();

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.Category,
                    opts => opts.Ignore())
                .ReverseMap();

            CreateMap<OrderItemDto, Item>()
                .ForMember(dest => dest.Name,
                    from => from.MapFrom(src => src.ItemName))
                .ReverseMap();
        }
	}
}
