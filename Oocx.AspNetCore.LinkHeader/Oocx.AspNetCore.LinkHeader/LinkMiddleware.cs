using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oocx.AspNetCore.LinkHeader
{
    public class LinkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LinkOptions _options;

        public LinkMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, LinkOptions options)
        {
            this._next = next;
            this._options = options;
            if (options.FileProvider == null)
            {
                options.FileProvider = hostingEnv.ContentRootFileProvider;
            }
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var originalPath = httpContext.Request.Path.Value;

            httpContext.Response.OnStarting(() =>
            {
                if (!(httpContext.Response.ContentType == "text/html"))
                {
                    return Task.CompletedTask;
                }

                var links = ReplaceHashesInLinks(_options.Links);
                httpContext.Response.Headers.Add("Link", string.Join(", ", links));

                return Task.CompletedTask;
            });

            await _next(httpContext);
        }

        private IEnumerable<Link> ReplaceHashesInLinks(IEnumerable<Link> links)
        {
            foreach (var link in links)
            {
                if (TryReplaceHashInLinkHref(link, out Link? replacedLink))
                {
                    yield return replacedLink.Value;
                }
            }
        }

        private bool TryReplaceHashInLinkHref(Link link, out Link? replacedLink)
        {
            var idx = link.Href.IndexOf('#');
            if (idx == -1)
            {
                replacedLink = link;
                return true;
            }

            var allFiles = _options.FileProvider.GetDirectoryContents("/").Select(f => f.Name).ToArray();

            var left = link.Href.Substring(1, idx - 1);
            var right = link.Href.Substring(idx + 1);
            var replaced = allFiles.FirstOrDefault(f => f.StartsWith(left) && f.EndsWith(right));

            if (replaced == null)
            {
                replacedLink = null;
                return false;
            }

            replacedLink = new Link(replaced, link.As, link.Rel);
            return true;
        }

    }
}
