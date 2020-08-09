namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents common features of an SCIM filter expression or filter group.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Returns a string that represents the current <see
        /// cref="FilterExpression"></see> as a valid query
        /// </summary>
        /// <param name="prependQuerySeprator">To prepend the "?" query string
        /// separator, <see langword="true"></see>; otherwise, <see
        /// langword="false"></see>.</param>
        /// <returns>A string that represents the current <see
        /// cref="FilterExpression"></see>.</returns>
        string ToFilterExpression(bool prependQuerySeprator = true);
    }
}