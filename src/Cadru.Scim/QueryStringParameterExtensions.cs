using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cadru.Scim.Filters;

namespace Cadru.Scim
{
    public static class QueryStringParameterExtensions
    {
        /// <summary>
        /// Converts the given filter expression to a string that can
        /// be used as a query string parameter value.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string ToQueryStringParameterExpresion(this IFilter filter)
        {
            return filter.ToFilterExpression(new FilterExpressionFormatOptions
            {
                IncludeFilterParameterName = false,
                IncludeQuerySeparator = false
            });
        }

        /// <summary>
        /// Converts the given filter expression to a string that can
        /// be used as a query string parameter value.
        /// </summary>
        /// <param name="filterGroup"></param>
        /// <param name="parentProperty"></param>
        /// <returns></returns>
        public static string ToQueryStringParameterExpresion(this FilterGroup filterGroup, string parentProperty)
        {
            // TODO: Temporary solution until Cadru.SCIM gets rewritten to support these types of filter expressions.
            List<FilterGroup> groups = new(filterGroup.Filters.Count);
            foreach (var filter in filterGroup.Filters.Cast<FilterExpression>())
            {
                groups.Add(new FilterGroup
                {
                    GroupingCharacter = GroupingCharacter.SquareBracket,
                    Filters =
                    {
                        { filter }
                    }
                });
            }

            IEnumerable<string>? filters = groups.Select(g => $"{parentProperty}{g.ToQueryStringParameterExpresion()}");
            return String.Join($" {filterGroup.LogicalOperator} ", filters);
        }
    }
}
