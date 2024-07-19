using server.Services;
using server.UseCases.Auth;

namespace server;

public static class ServicesExtensions
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<CreateUser>();
        services.AddTransient<LoginUser>();
        services.AddTransient<ConfirmEmail>();
        services.AddTransient<ForgotPassword>();
        services.AddTransient<ResetPassword>();
    }
}