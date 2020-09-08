namespace Cadru.Data.Dapper
{
    public interface ICommandAdapter
    {
        string CatalogSeparator { get; }
        string NameSeparator { get; }
        int ParameterNameMaxLength { get; }
        string ParameterNamePattern { get; }
        string ParameterPrefix { get; }
        string QuotePrefix { get; }
        string QuoteSuffix { get; }
        string SchemaSeparator { get; }
        string GetParameterName(string parameterName);

        /// <summary>
        /// Escapes a string as a SQL literal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string QouteStringLiteral(string value);

        /// <summary>
        /// Escapes the identifier.
        /// </summary>
        /// <param name="identifier">The identifier name, in unescaped form.</param>
        /// <returns>The escaped identifier.</returns>
        /// <remarks>
        /// This uses the <see cref="IdentifierPrefix"/> and <see
        /// cref="IdentifierSuffix"/> values.
        /// </remarks>
        string QuoteIdentifier(string identifier);

        /// <summary>
        /// Unescapes the identifier.
        /// </summary>
        /// <param name="identifier">The identifier name</param>
        /// <returns>The unescaped identifier.</returns>
        /// <remarks>
        /// This uses the <see cref="IdentifierPrefix"/> and <see
        /// cref="IdentifierSuffix"/> values.
        /// </remarks>
        string UnquoteIdentifier(string identifier);
    }
}