namespace Oocx.AspNetCore.LinkHeader
{

    public struct Link
    {
        public static Link HashReplacementFailed = new Link(null, null, null);

        public Link(string href, string rel = "preload")
        {
            Href = "/" + href;
            Rel = rel;

            if (href.EndsWith(".css"))
            {
                As = "style";
            }
            else
            {
                As = "script";
            }
        }

        public Link(string href, string @as, string rel = "preload")
        {
            Href = href;
            Rel = rel;
            As = @as;
        }

        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string As { get; private set; }

        public override string ToString()
        {
            return $"<{Href}>; rel={Rel}; as={As}";
        }
    }
}
