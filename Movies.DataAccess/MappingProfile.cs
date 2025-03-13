using AutoMapper;
using Movies.DataAccess.Models;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.DataAccess
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>(MemberList.Destination);
            CreateMap<Comment, CommentDto>(MemberList.Destination);
            CreateMap<Content, ContentDto>(MemberList.Destination);
            CreateMap<Rating, RatingDto>(MemberList.Destination);
            CreateMap<Genre, GenreDto>(MemberList.Destination);
        }
    }
}
