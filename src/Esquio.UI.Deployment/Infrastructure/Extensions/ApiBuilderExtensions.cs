﻿using Esquio.UI.Deployment.Infrastructure.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApiBuilderExtensions
    {
        public static IApplicationBuilder AddClientBlazorConfiguration(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<BlazorClientConfigurationMiddleware>();
        }
    }
}
