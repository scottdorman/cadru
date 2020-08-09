using System;
using System.Collections.Generic;

namespace Cadru.Scim.Filters
{
    /// <inheritdoc/>
    public class FilterGroup : IFilterGroup
    {
        /// <inheritdoc/>
        public IList<IFilter> Filters { get; } = new List<IFilter>();

        /// <inheritdoc/>
        public GroupingCharacter GroupingCharacter { get; set; } = GroupingCharacter.Parentheses;

        /// <inheritdoc/>
        public FilterLogicalOperator LogicalOperator { get; set; } = FilterLogicalOperator.And;

        /// <inheritdoc/>
        public string ToFilterExpression(bool prependQuerySeprator = true)
        {
            return $"?filter={ this }";
        }

        /// <summary>
        /// Returns a string that represents the current <see
        /// cref="FilterGroup"></see> as a valid query
        /// </summary>
        /// <returns>A string that represents the current <see
        /// cref="FilterGroup"></see>.</returns>
        public override string ToString()
        {
            string[] brackets = Array.Empty<string>();
            switch (this.GroupingCharacter)
            {
                case GroupingCharacter.Parentheses:
                    brackets = new string[]
                    {
                        "(", ")"
                    };
                    break;

                case GroupingCharacter.SquareBracket:
                    brackets = new string[]
                    {
                        "[", "]"
                    };
                    break;
            }

            return $"{ brackets[0] }{ String.Join($" { this.LogicalOperator } ", this.Filters) }{ brackets[1] }";
        }
    }
}
