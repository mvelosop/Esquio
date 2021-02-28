using Esquio;
using Esquio.Abstractions;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace GettingStarted.AspNetCore.ClientEndpoint.Toggles
{
    [DesignType(FriendlyName = "FromToggle", Description = "This is the from toggle description")]
    [DesignTypeParameter(ParameterName = "From", ParameterDescription = "The from parameter", ParameterType = EsquioConstants.DATE_PARAMETER_TYPE)]
    public class FromToggle
        : IToggle
    {
        internal const string DEFAULT_FORMAT_DATE = "yyyy-MM-dd HH:mm:ss";
        internal const string SINGLE_DIGIT_FORMAT_DATE = "yyyy-M-d H:m:s";

        internal const string From = nameof(From);

        public ValueTask<bool> IsActiveAsync(ToggleExecutionContext context, CancellationToken cancellationToken = default)
        {
            var parseExactFormats = new string[] { DEFAULT_FORMAT_DATE, SINGLE_DIGIT_FORMAT_DATE };

            var fromDate = DateTime.ParseExact(
                context.Data[From].ToString(),
                parseExactFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal);

            if (fromDate < DateTime.UtcNow)
            {
                return new ValueTask<bool>(true);
            }

            return new ValueTask<bool>(false);
        }
    }
}
