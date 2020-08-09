using System.Collections.Generic;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents a group of <see cref="IFilter"></see> instances.
    /// </summary>
    public interface IFilterGroup : IFilter
    {
        /// <summary>
        /// The collection of <see cref="IFilter"></see> instances to be
        /// grouped.
        /// </summary>
        /// <remarks>
        /// An <see cref="IFilterGroup"></see> may contain either <see
        /// cref="IFilterExpression"></see> instances or <see
        /// cref="IFilterGroup"></see> instances.
        /// </remarks>
        IList<IFilter> Filters { get; }

        /// <summary>
        /// The grouping character used.
        /// </summary>
        GroupingCharacter GroupingCharacter { get; set; }

        /// <summary>
        /// The logical grouping operator.
        /// </summary>
        FilterLogicalOperator LogicalOperator { get; set; }
    }
}