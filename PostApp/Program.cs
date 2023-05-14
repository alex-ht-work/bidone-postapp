using PostApp.Endpoints;

namespace PostApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables();
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapFallbackToFile("index.html");
            app.MapSaveParticipantEndpoint();
            return app;
        }
    }
}