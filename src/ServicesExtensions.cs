using server.Services;

namespace server;

public static class ServicesExtensions
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
    }
}