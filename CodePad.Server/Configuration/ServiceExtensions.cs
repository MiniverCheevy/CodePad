namespace CodePad.Server.Configuration;

using CodePad.Server.Services;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileManagementService, FileManagerService>();
        return services;
    }
}
