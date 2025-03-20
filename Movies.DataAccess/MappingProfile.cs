using AutoMapper;
using Movies.DataAccess.Models;
using Movies.Shared.DTO.ModelDTOs;

namespace Movies.DataAccess
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>(MemberList.Destination).ReverseMap();
            CreateMap<Comment, CommentDto>(MemberList.Destination).ReverseMap();
            CreateMap<Content, ContentDto>(MemberList.Destination).ReverseMap();
            CreateMap<Rating, RatingDto>(MemberList.Destination).ReverseMap();
            CreateMap<Genre, GenreDto>(MemberList.Destination).ReverseMap();
            CreateMap<GroupQuiz, GroupQuizDto>(MemberList.Destination).ReverseMap();
            CreateMap<GroupQuizParticipant, GroupQuizParticipantDto>(MemberList.Destination).ReverseMap();
            CreateMap<QuizSession, QuizSessionDto>(MemberList.Destination).ReverseMap();
            CreateMap<QuizVote, QuizVoteDto>(MemberList.Destination).ReverseMap();

            CreateMap<Favorite, ContentDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Content.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Content.Description))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Content.ReleaseYear))
                .ForMember(dest => dest.IMDBRating, opt => opt.MapFrom(src => src.Content.IMDBRating))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.Content.TrailerUrl))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Content.PosterUrl))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Content.Ratings))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Content.Comments))
                .ForMember(dest => dest.ContentGenres, opt => opt.MapFrom(src => src.Content.ContentGenres));

            CreateMap<WatchlistItem, ContentDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Content.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Content.Description))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Content.ReleaseYear))
                .ForMember(dest => dest.IMDBRating, opt => opt.MapFrom(src => src.Content.IMDBRating))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.Content.TrailerUrl))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Content.PosterUrl))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Content.Type))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Content.Ratings))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Content.Comments))
                .ForMember(dest => dest.ContentGenres, opt => opt.MapFrom(src => src.Content.ContentGenres));

            CreateMap<ContentGenre, GenreDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Genre.Description));
        }
    }
}
