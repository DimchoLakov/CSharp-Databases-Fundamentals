namespace PhotoShare.Client.Core
{
    using AutoMapper;
    using Commands;
    using Models;
    using Dtos;

    public class PhotoShareProfile : Profile
    {
        public PhotoShareProfile()
        {
            CreateMap<Town, Town>();

            CreateMap<User, User>();

            CreateMap<Tag, Tag>();

            CreateMap<Album, Album>();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, RegisterUserDto>()
                .ForMember(dest => dest.RepeatPassword, from => from.MapFrom(p => p.Password))
                .ReverseMap();

            CreateMap<Town, TownDto>().ReverseMap();

            CreateMap<Album, AlbumDto>().ReverseMap();

            CreateMap<Tag, TagDto>().ReverseMap();

            CreateMap<AlbumRole, AlbumRoleDto>()
                    .ForMember(dest => dest.AlbumName, from => from.MapFrom(p => p.Album.Name))
                    .ForMember(dest => dest.Username, from => from.MapFrom(p => p.User.Username))
                    .ReverseMap();

            CreateMap<User, UserFriendsDto>()
                .ForMember(dest => dest.Friends,
                    from => from.MapFrom(u => u.FriendsAdded))
                .ReverseMap();

            CreateMap<Friendship, FriendDto>()
                .ForMember(dto => dto.Username,
                    opt => opt.MapFrom(f => f.Friend.Username))
                .ReverseMap();
        }
    }
}
