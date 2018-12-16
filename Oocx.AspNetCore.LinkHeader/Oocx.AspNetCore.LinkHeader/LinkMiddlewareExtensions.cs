using Microsoft.AspNetCore.Builder;
using System;

namespace Oocx.AspNetCore.LinkHeader
{
    public static class LinkMiddlewareExtensions
    {
        public static IApplicationBuilder UseLinkHeader(this IApplicationBuilder builder, Action<LinkOptions> configureOptions)
        {
            var options = new LinkOptions();
            configureOptions(options);

            return builder.UseMiddleware<LinkMiddleware>(options);
        }
    }
}
