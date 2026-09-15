using CSharpApp.Application.Categories;
using Microsoft.Extensions.Options;
using Polly;
namespace CSharpApp.Infrastructure.Configuration;

public static class HttpConfiguration
{
    public static IServiceCollection AddHttpConfiguration (
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(nameof(HttpClientSettings))
            .Get<HttpClientSettings>()
            ?? new HttpClientSettings();

        services
            .AddHttpClient<IProductsService, ProductsService>(
                (serviceProvider, client) =>
                {
                    var settings = serviceProvider
                        .GetRequiredService<IOptions<RestApiSettings>>()
                        .Value;

                    client.BaseAddress = new Uri(settings.BaseUrl);
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(settings.LifeTime))
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = settings.RetryCount;

                options.Retry.Delay = TimeSpan.FromMilliseconds(settings.SleepDuration);

                options.Retry.BackoffType = DelayBackoffType.Constant;
            });

        services
            .AddHttpClient<ICategoriesService, CategoriesService>(
                (serviceProvider, client) =>
                {
                    var settings = serviceProvider
                        .GetRequiredService<IOptions<RestApiSettings>>()
                        .Value;

                    client.BaseAddress = new Uri(settings.BaseUrl);
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(settings.LifeTime))
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = settings.RetryCount;

                options.Retry.Delay = TimeSpan.FromMilliseconds(settings.SleepDuration);

                options.Retry.BackoffType = DelayBackoffType.Constant;
            });

        return services;
    }
}