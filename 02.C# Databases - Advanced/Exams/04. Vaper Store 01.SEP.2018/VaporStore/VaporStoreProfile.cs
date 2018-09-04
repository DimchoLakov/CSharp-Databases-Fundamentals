using VaporStore.Data.Models;
using VaporStore.DataProcessor.ImportDto;

namespace VaporStore
{
	using AutoMapper;

	public class VaporStoreProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public VaporStoreProfile()
		{
		    CreateMap<GameDto, Game>()
		        .ForPath(dest => dest.Developer.Name,
		            mapFrom => mapFrom.MapFrom(src => src.DeveloperName))
		        .ForPath(dest => dest.Genre.Name,
		            mapFrom => mapFrom.MapFrom(src => src.GenreName))
		        .ReverseMap();
		}
	}
}