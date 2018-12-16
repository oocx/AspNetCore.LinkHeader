using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oocx.AspNetCore.LinkHeader;

namespace Oocx.AspNetCore.LinkHeader.Tests
{
    [TestClass]
    public class LinkOptionsUnitTests
    {
        [TestMethod]
        public void Should_generate_preload_links()
        {
            // Arrange
            var sut = new LinkOptions();

            // Act
            var result = sut.Preload("foo.js", "bar.css").GetLinks();

            // Assert
            result[0].Href.Should().Be("/foo.js");
            result[0].Rel.Should().Be("preload");
            result[0].As.Should().Be("script");

            result[1].Href.Should().Be("/bar.css");
            result[1].Rel.Should().Be("preload");
            result[1].As.Should().Be("style");
        }
    }
}
