using Microsoft.Extensions.Logging;
using System;

namespace Esquio.AspNetCore.Diagnostics
{
    static class Log
    {
        public static void FeatureMatcherPolicyCanBeAppliedToEndpoint(ILogger logger, string endpointDisplayName)
        {
            _featureMatcherPolicyEndpointCanBeApplied(logger, endpointDisplayName, null);
        }
        public static void FeatureMatcherPolicyEvaluatingFeatures(ILogger logger, string endpointDisplayName, string names)
        {
            _featureMathcherPolicyValidatingMetadata(logger, endpointDisplayName, names, null);
        }
        public static void FeatureMatcherPolicyEndpointIsNotValid(ILogger logger, string endpointDisplayName)
        {
            _featureMatcherPolicyEndpointIsNotValid(logger, endpointDisplayName, null);
        }
        public static void FeatureMatcherPolicyEndpointIsValid(ILogger logger, string endpointDisplayName)
        {
            _featureMatcherPolicyEndpointIsValid(logger, endpointDisplayName, null);
        }
        public static void FeatureMatcherPolicyExecutingFallbackEndpoint(ILogger logger, string requestPath)
        {
            _featureMatcherPolicyExecutingFallbackEndpoint(logger, requestPath, null);
        }
        public static void FeatureTagHelperBegin(ILogger logger, string featureName)
        {
            _featureTagHelperBegin(logger, featureName, null);
        }
        public static void FeatureTagHelperClearContent(ILogger logger, string featureName)
        {
            _featureTagHelperClearContent(logger, featureName, null);
        }
        public static void EsquioClientMiddlewareThrow(ILogger logger, string featureName, Exception exception)
        {
            _esquioClientMiddlewareThrow(logger, featureName, exception);
        }
        public static void EsquioClientMiddlewareEvaluatingFeature(ILogger logger, string featureName)
        {
            _esquioClientMiddlewareEvaluateFeature(logger, featureName, null);
        }
        public static void EsquioClientMiddlewareSuccess(ILogger logger)
        {
            _esquioClientMiddlewareSuccess(logger, null);
        }
        public static void EsquioDiscoverCustomTogglesMiddlewareSuccess(ILogger logger)
        {
            _esquioDiscoverCustomToggleMiddlewareSuccess(logger, null);
        }

        private static readonly Action<ILogger, string, Exception> _featureMatcherPolicyEndpointCanBeApplied = LoggerMessage.Define<string>(
            LogLevel.Debug,
            EventIds.FeatureEndpointMatcherCanBeAppliedToEndpoint,
            "FeatureMatcherPolicy can be applied to endpoint {endpointDisplayName}.");
        private static readonly Action<ILogger, string, string, Exception> _featureMathcherPolicyValidatingMetadata = LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            EventIds.FeatureEndpointMatcherValidatingFeatures,
            "FeatureMatcherPolicy is validating features for endpoint {endpointDisplayName} for features {names}.");
        private static readonly Action<ILogger, string, Exception> _featureMatcherPolicyEndpointIsNotValid = LoggerMessage.Define<string>(
           LogLevel.Debug,
           EventIds.FeatureEndpointMatcherEndPointNotValid,
           "FeatureMatcherPolicy set validity FALSE to endpoint {endpointDisplayName}.");
        private static readonly Action<ILogger, string, Exception> _featureMatcherPolicyEndpointIsValid = LoggerMessage.Define<string>(
           LogLevel.Debug,
           EventIds.FeatureEndpointMatcherEndPointValid,
           "FeatureMatcherPolicy set validity TRUE to endpoint {endpointDisplayName}.");
        private static readonly Action<ILogger, string, Exception> _featureMatcherPolicyExecutingFallbackEndpoint = LoggerMessage.Define<string>(
          LogLevel.Debug,
          EventIds.FeatureEndpointMatcherUsingFallbackEndPoint,
          "FeatureMatcherPolicy use fallback enpoint configured service because the endpoint for request {requestPath} does not have valid candidates to use.");
        private static readonly Action<ILogger, string, Exception> _featureTagHelperBegin = LoggerMessage.Define<string>(
            LogLevel.Debug,
            EventIds.FeatureTagHelperBeginProcess,
            "FeatureTagHelper begin check if {featureName} is enabled.");
        private static readonly Action<ILogger, string, Exception> _featureTagHelperClearContent = LoggerMessage.Define<string>(
            LogLevel.Debug,
            EventIds.FeatureTagHelperClearContent,
            "FeatureTagHelper is clearing inner content because {featureName} is not enabled.");
        private static readonly Action<ILogger, string, Exception> _esquioClientMiddlewareThrow = LoggerMessage.Define<string>(
            LogLevel.Error,
            EventIds.EsquioClientMiddlewareThrow,
            "Esquio Client middleware throw exception when evaluating {featureName}.");
        private static readonly Action<ILogger, string, Exception> _esquioClientMiddlewareEvaluateFeature = LoggerMessage.Define<string>(
            LogLevel.Debug,
            EventIds.EsquioClientMiddlewareEvaluateFeature,
            "Evaluating {featureName} for product on Esquio Client middleware.");
        private static readonly Action<ILogger, Exception> _esquioClientMiddlewareSuccess = LoggerMessage.Define(
            LogLevel.Debug,
            EventIds.EsquioClientMiddlewareSuccess,
            "Esquio Client middleware perform feature evaluation succesfully.");
        private static readonly Action<ILogger, Exception> _esquioDiscoverCustomToggleMiddlewareSuccess = LoggerMessage.Define(
           LogLevel.Debug,
           EventIds.EsquioDiscoverCustomTogglesMiddlewareSuccess,
           "Esquio discover custom toggles middleware success.");


    }
}

