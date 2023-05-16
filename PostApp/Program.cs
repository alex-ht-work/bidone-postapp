using System.IO.Abstractions;
using PostApp.Endpoints;
using PostApp.Services;
using PostApp.Services.FileRepository;

namespace PostApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args, Action<IServiceCollection>? addTestServices = null)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables(); // docker compose will inject parameters via environment variables
            builder.Services.Configure<FileRepositoryConfig>(builder.Configuration.GetSection("Repo"))
                            .AddSingleton<IFileSystem, FileSystem>()
                            .AddSingleton<IFileNameProvider, FileNameProvider>()
                            .AddSingleton<IRepository, FileRepository>();
            
            addTestServices?.Invoke(builder.Services);

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapFallbackToFile("index.html");
            app.MapSaveParticipantEndpoint();
            return app;
        }
    }
}