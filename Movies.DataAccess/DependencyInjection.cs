using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.DataAccess.Services;
using Movies.DataAccess.Services.Interfaces;

namespace Movies.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            // Database
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<MoviesDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
            );

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(MappingProfile));

            // Services
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IWatchlistService, WatchlistService>();

            return services;
        }
    }
}
