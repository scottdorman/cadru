using System;
using System.Collections.Generic;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents a group of <see cref="IFilter"></see> instances where the
    /// filter is a match if the expression evaluates to <see
    /// langword="false"></see>.
    /// </summary>
    public class NotFilterGroup : IFilterGroup
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
        /// cref="NotFilterGroup"></see> as a valid query
        /// </summary>
        /// <returns>A string that represents the current <see
        /// cref="NotFilterGroup"></see>.</returns>
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

            return $"not { brackets[0] }{ String.Join($" { this.LogicalOperator } ", this.Filters) }{ brackets[1] }";
        }
    }
}
