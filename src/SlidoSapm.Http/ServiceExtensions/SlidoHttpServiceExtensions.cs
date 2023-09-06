using Microsoft.Extensions.DependencyInjection;
using SlidoSapm.Http.ServiceExtensions.SlidoExtensions;

namespace SlidoSapm.Http.ServiceExtensions;

public static class SlidoHttpServiceExtensions
{
    public static IServiceCollection AddSlidoHttp(this IServiceCollection services)
    {
        services.AddSlidoHttpClient();
        return services;
    }
}