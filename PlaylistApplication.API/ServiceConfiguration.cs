using PlaylistApplication.API.Repositories;
using PlaylistApplication.API.Repositories.Interfaces;
using PlaylistApplication.API.Services;
using PlaylistApplication.API.Services.Interfaces;
using PlaylistApplication.API.Validators;

namespace PlaylistApplication.API
{
    public static class ServiceConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IPlaylistService, PlaylistService>();
        }

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<ISongPlaylistRepository, SongPlaylistRepository>();
        }

        public static void AddCustomValidators(this IServiceCollection services)
        {
            services.AddScoped<SongValidator>();
            services.AddScoped<PlaylistValidator>();
        }
    }
}
