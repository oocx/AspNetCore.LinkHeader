using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;

namespace Oocx.AspNetCore.LinkHeader
{
    public class LinkOptions
    {

        public IFileProvider FileProvider { get; set; }


        public LinkOptions Preload(params string[] links)
        {

            Links.AddRange(links.Select(l => new Link(l)).ToArray());
            return this;
        }

        public LinkOptions Prefetch(params string[] links)
        {
            Links.AddRange(links.Select(l => new Link(l, "prefetch")).ToArray());
            return this;
        }

        public LinkOptions Preconnect(params string[] links)
        {
            Links.AddRange(links.Select(l => new Link(l, "preconnect")).ToArray());
            return this;
        }

        public Link[] GetLinks()
        {
            return Links.ToArray();
        }

        internal List<Link> Links { get; } = new List<Link>();

    }
}
