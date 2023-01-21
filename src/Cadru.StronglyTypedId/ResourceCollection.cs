using System.Collections.Generic;

namespace Cadru.StronglyTypedId
{
    internal class ResourceCollection
    {
        public string? RecordTemplate { get; init; }
        public string? StructTemplate { get; init; }
        public string? ClassTemplate { get; init; }
        public IDictionary<StronglyTypedIdConverter, string> Converters { get; init; } = new Dictionary<StronglyTypedIdConverter, string>();
        public IDictionary<string, string> Resources { get; init; } = new Dictionary<string, string>();
    }
}
