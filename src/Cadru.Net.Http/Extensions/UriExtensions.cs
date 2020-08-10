using System;

namespace Cadru.Net.Http.Extensions
{
    public static class UriExtensions
    {
        public static string GetUriSegment(this Uri uri, int index)
        {
            var segment = String.Empty;
            var segments = uri.Segments;
            if (index <= segments.Length || index < 0)
            {
                segment = segments[index];
            }

            return segment;
        }
    }
}
