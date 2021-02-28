using Microsoft.Extensions.Logging;
using System;

namespace Esquio.AspNetCore.Diagnostics
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class EsquioAspNetCoreDiagnostics
    {
        private readonly ILogger _logger;

        public EsquioAspNetCoreDiagnostics(ILoggerFactory loggerFactory)
        {
            _ = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger("Esquio.AspNetCore");
        }

        public void FeatureMatcherPolicyCanBeAppliedToEndpoint(string endpointDisplayName)
        {
            Log.FeatureMatcherPolicyCanBeAppliedToEndpoint(_logger, endpointDisplayName);
        }

        public void FeatureMatcherPolicyEvaluatingFeatures(string endpointDisplayName, string names)
        {
            Log.FeatureMatcherPolicyEvaluatingFeatures(_logger, endpointDisplayName, names);
        }
        public void FeatureMatcherPolicyEndpointIsNotValid(string endpointDisplayName)
        {
            Log.FeatureMatcherPolicyEndpointIsNotValid(_logger, endpointDisplayName);
        }

        public void FeatureMatcherPolicyEndpointIsValid(string endpointDisplayName)
        {
            Log.FeatureMatcherPolicyEndpointIsValid(_logger, endpointDisplayName);
        }

        public void FeatureMatcherPolicyExecutingFallbackEndpoint(string requestPath)
        {
            Log.FeatureMatcherPolicyExecutingFallbackEndpoint(_logger, requestPath);
        }

        public void FeatureTagHelperBegin(string featureName)
        {
            Log.FeatureTagHelperBegin(_logger, featureName);
        }

        public void FeatureTagHelperClearContent(string featureName)
        {
            Log.FeatureTagHelperClearContent(_logger, featureName);
        }

        public void EsquioClientMiddlewareThrow(string featureName, Exception exception)
        {
            Log.EsquioClientMiddlewareThrow(_logger, featureName, exception);
        }

        public void EsquioClientMiddlewareEvaluatingFeature(string featureName)
        {
            Log.EsquioClientMiddlewareEvaluatingFeature(_logger, featureName);
        }

        public void EsquioClientMiddlewareSuccess()
        {
            Log.EsquioClientMiddlewareSuccess(_logger);
        }

        public void EsquioDiscoverCustomToggleSucess()
        {
            Log.EsquioDiscoverCustomTogglesMiddlewareSuccess(_logger);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
