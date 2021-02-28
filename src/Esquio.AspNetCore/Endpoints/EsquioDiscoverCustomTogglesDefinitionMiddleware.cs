using Esquio.Abstractions;
using Esquio.AspNetCore.Diagnostics;
using Esquio.AspNetCore.Toggles;
using Esquio.Toggles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Esquio.AspNetCore.Endpoints
{
    internal class EsquioDiscoverCustomTogglesDefinitionMiddleware
    {
        const string DEFAULT_MIME_TYPE = MediaTypeNames.Application.Json;

        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        private readonly RequestDelegate _next;

        public EsquioDiscoverCustomTogglesDefinitionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, EsquioAspNetCoreDiagnostics diagnostics)
        {
            // get all toggle types on this app domain except all default toggles
            // defined on Esquio and Esquio.AspNetCore assemblies

            var customTogglesConfiguration = new DiscoverCustomTogglesConfiguration()
            {
                CustomToggles = new List<CustomToggleDefinition>()
            };
           
            var customToggles = AppDomain.CurrentDomain
                .GetAssemblies()
                .Except(new[] { typeof(FromToToggle).Assembly, typeof(HeaderValueToggle).Assembly })
                .SelectMany(a => a.GetTypes())
                .Where(type => typeof(IToggle).IsAssignableFrom(type) && type.IsClass)
                .ToList();

            foreach (var toggle in customToggles)
            {
                customTogglesConfiguration.CustomToggles
                    .Add(RevealToggle(toggle));
            }

            diagnostics.EsquioDiscoverCustomToggleSucess();
            await WriteResponse(context, customTogglesConfiguration);
        }

        private CustomToggleDefinition RevealToggle(Type toggle)
        {
            var designTypeAttribute = toggle.GetCustomAttribute<DesignTypeAttribute>(inherit: true);
            var parametersAttribute = toggle.GetCustomAttributes<DesignTypeParameterAttribute>(inherit: true);

            var toggleDefinition = new CustomToggleDefinition()
            {
                Type = $"{toggle.FullName},{toggle.Assembly.GetName().Name}",
                Assembly = toggle.Assembly.GetName(copiedName: false).Name,
                FriendlyName = designTypeAttribute != null ? designTypeAttribute.FriendlyName : toggle.Name,
                Description = designTypeAttribute != null ? designTypeAttribute.Description : "No description"
            };

            if (parametersAttribute != null && parametersAttribute.Any())
            {
                var parameters = parametersAttribute.Select(item =>
                {
                    return new CustomToggleParameterDefinition()
                    {
                        Name = item.ParameterName,
                        ClrType = item.ParameterType,
                        Description = item.ParameterDescription
                    };
                }).ToList();

                toggleDefinition.Parameters
                    .AddRange(parameters);
            }
            
            return toggleDefinition;
        }

        private async Task WriteResponse(HttpContext currentContext, DiscoverCustomTogglesConfiguration response)
        {
            await WriteAsync(
                currentContext,
                JsonSerializer.Serialize(response, options: _serializerOptions),
                DEFAULT_MIME_TYPE,
                StatusCodes.Status200OK);
        }

        private async Task WriteAsync(
           HttpContext context,
           string content,
           string contentType,
           int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(content);
        }

        private class DiscoverCustomTogglesConfiguration
        {
            public List<CustomToggleDefinition> CustomToggles { get; set; }
        }
        private class CustomToggleDefinition
        {
            public string Type { get; set; }
            public string Assembly { get; set; }
            public string FriendlyName { get; set; }
            public string Description { get; set; }
            public List<CustomToggleParameterDefinition> Parameters { get; set; } = new List<CustomToggleParameterDefinition>();
        }
        private class CustomToggleParameterDefinition
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string ClrType { get; set; }
        }
    }
}
