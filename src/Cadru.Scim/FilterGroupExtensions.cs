namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Extension methods for working with <see cref="IFilterGroup"></see> instances.
    /// </summary>
    public static class FilterGroupExtensions
    {
        /// <summary>
        /// Add a new <see cref="IFilterExpression"></see> to the <see
        /// cref="IFilterGroup"></see>.
        /// </summary>
        /// <param name="filterGroup">A <see cref="IFilterGroup"></see> to
        /// modify.</param>
        /// <param name="filter">An <see cref="IFilterExpression"></see> to
        /// add.</param>
        /// <returns>A reference to the <paramref name="filterGroup"/> after the
        /// operation has completed. </returns>
        public static IFilterGroup AddExpression(this IFilterGroup filterGroup, IFilterExpression filter)
        {
            filterGroup.Filters.Add(filter);
            return filterGroup;
        }

        /// <summary>
        /// Add a new <see cref="IFilterGroup"></see> to the <see cref="IFilterGroup"></see>.
        /// </summary>
        /// <param name="filterGroup">A <see cref="IFilterGroup"></see> to modify.</param>
        /// <param name="group">An <see cref="IFilterGroup"></see> to
        /// add.</param>
        /// <returns>A reference to the <paramref name="filterGroup"/> after the
        /// operation has completed. </returns>
        public static IFilterGroup AddGroup(this IFilterGroup filterGroup, IFilterGroup group)
        {
            filterGroup.Filters.Add(group);
            return filterGroup;
        }
    }
}
