using System;
using System.Collections.Generic;
using System.Text;

namespace Cadru.StronglyTypedId
{
    internal static class Constants
    {
        private const string RuntimeAttributesKey = "Cadru.StronglyTypedId";
        private const string AttributeName = "StronglyTypedId";

        public const string TemplatesKey = $"{RuntimeAttributesKey }.Templates";
        public const string AttributeFullName = $"{RuntimeAttributesKey }.{ AttributeName }Attribute";
    }
}
