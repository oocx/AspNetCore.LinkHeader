using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Oocx.AspNetCore.LinkHeader.Tests
{
    [TestClass]
    public class LinkOptionsIntegrationTests
    {
        [TestMethod]
        public async Task Should_add_link_header()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseLinkHeader(o =>
                    {
                        o.Preload("foo.js")
                         .Prefetch("bar.css")
                         .Preconnect("service");
                    });
                    app.Use(next =>
                    {
                        return context =>
                        {
                            context.Response.StatusCode = 999;
                            context.Response.ContentType = "text/html";                            
                            return Task.CompletedTask;
                        };
                    });
                });
            var server = new TestServer(builder);

            // Act
            var response = await server.CreateClient().GetAsync("/index.html");

            // Assert
            response.StatusCode.Should().Be(999, "the next middleware should be called");

            var linkHeader = response.Headers.SingleOrDefault(h => h.Key == "Link");
            linkHeader.Should().NotBeNull("it should add a Link header");
                           
            var expected = "</foo.js>; rel=preload; as=script, </bar.css>; rel=prefetch; as=style, </service>; rel=preconnect; as=script";
            linkHeader.Value.Single().Should().Be(expected);

        }
    }
}
