using Microsoft.Extensions.DependencyInjection;
using SlidoSapm.Http.Clients.Slido;

namespace SlidoSapm.Http.ServiceExtensions.SlidoExtensions;

public static class SlidoServiceExtensions
{
    internal const string SlidoHttpClient = "SlidoHttpClient";
    internal static IServiceCollection AddSlidoHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(SlidoHttpClient,
            client => { client.BaseAddress = new Uri("https://app.sli.do/eu1/api/v0.5/events/"); });

        services.AddSingleton<SlidoClient>();
        return services;
    }
}