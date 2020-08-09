namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Indicates the grouping character used by an <see
    /// cref="IFilterGroup"></see>.
    /// </summary>
    public enum GroupingCharacter
    {
        /// <summary>
        /// No grouping characters will be used.
        /// </summary>
        None,

        /// <summary>
        /// Boolean expressions may be grouped using parentheses to change the
        /// standard order of operations.
        /// </summary>
        Parentheses,

        /// <summary>
        /// Service providers may support complex filters where expressions must
        /// be applied to the same value of a parent attribute. The expression
        /// with square brackets must be a valid filter expression based upon
        /// sub-attributes of the parent attribute. Nested expressions may be
        /// used.
        /// </summary>
        SquareBracket
    }
}
