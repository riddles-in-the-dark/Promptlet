namespace Promptlet.Api.Client
{        using System;
using System.Net.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Http;
    using Polly;
using Polly.Extensions.Http;
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPromptletApiClient(this IServiceCollection services, ApiClientConfiguration apiClientConfiguration)
        {
            services.AddHttpClient<IPromptletApiClient, PromptletApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiClientConfiguration.ApiBaseUrl);
            })
           .SetHandlerLifetime(TimeSpan.FromMinutes(apiClientConfiguration.HandlerLifetimeInSeconds))
           .AddPolicyHandler(GetRetryPolicy());
            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }


}
