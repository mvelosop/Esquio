using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Seedwork;
using Xunit;

namespace UnitTests.Esquio.AspNetCore.Endpoint
{
    [Collection(nameof(AspNetCoreServer))]
    public class esquio_autodiscover_endpoint_middleware_should
    {
        private readonly ServerFixture _fixture;
        public esquio_autodiscover_endpoint_middleware_should(ServerFixture fixture)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        [Fact]
        public async Task response_custom_toggles_on_this_assembly()
        {
            var response = await _fixture.TestServer
                .CreateRequest($"esquio-autodiscover")
                .GetAsync();

            response.StatusCode
                .Should()
                .Be(StatusCodes.Status200OK);

            var result = await response.Content.ReadAs<DiscoverCustomTogglesConfiguration>();

            result.Should().NotBeNull();

            result.CustomToggles
                .Any()
                .Should().BeTrue();

            result.CustomToggles
                .Count
                .Should().Be(3);

            result.CustomToggles
                .Where(c=>c.Type == $"{typeof(AllwaysOnToggle).FullName},{typeof(AllwaysOnToggle).Assembly.GetName().Name}")
                .Any()
                .Should().BeTrue();

            result.CustomToggles
                .Where(c => c.Assembly == typeof(AllwaysOnToggle).Assembly.GetName(copiedName: false).Name)
                .Any()
                .Should().BeTrue();
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
