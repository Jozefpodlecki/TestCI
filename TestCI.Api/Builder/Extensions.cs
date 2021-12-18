using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestCI.Api.Builder
{
    public static class Extensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddHttpClient<IApiWrapper, ApiWrapper>((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://test.com");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "pl");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            return services;
        }
    }
}
