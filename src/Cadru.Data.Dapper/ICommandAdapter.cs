namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a way to create provider specific SQL statements.
    /// </summary>
    public interface ICommandAdapter
    {
        /// <summary>
        /// Gets a string used as the catalog separator.
        /// </summary>
        string CatalogSeparator { get; }

        /// <summary>
        /// Gets the starting character or characters to use when
        /// specifying SQL Server database objects, such as tables or columns,
        /// whose names contain characters such as spaces or reserved tokens.
        /// </summary>
        string IdentifierPrefix { get; }

        /// <summary>
        /// Gets the ending character or characters to use when
        /// specifying SQL Server database objects, such as tables or columns,
        /// whose names contain characters such as spaces or reserved tokens.
        /// </summary>
        string IdentifierSuffix { get; }

        /// <summary>
        /// Gets a string used as the name separator.
        /// </summary>
        string NameSeparator { get; }

        /// <summary>
        /// Gets the maximum parameter name length.
        /// </summary>
        int ParameterNameMaxLength { get; }

        /// <summary>
        /// Gets the pattern used to create parameter names.
        /// </summary>
        string ParameterNamePattern { get; }

        /// <summary>
        /// Gets a string used as the parameter prefix.
        /// </summary>
        string ParameterPrefix { get; }
        /// <summary>
        /// Gets the starting character or characters to use when
        /// specifying SQL Server database objects, such as tables or columns,
        /// whose names contain characters such as spaces or reserved tokens.
        /// </summary>
        string QuotePrefix { get; }

        /// <summary>
        /// Gets the ending character or characters to use when
        /// specifying SQL Server database objects, such as tables or columns,
        /// whose names contain characters such as spaces or reserved tokens.
        /// </summary>
        string QuoteSuffix { get; }

        /// <summary>
        /// Gets the character to be used for the separator between the
        /// schema identifier and any other identifiers.
        /// </summary>
        string SchemaSeparator { get; }

        /// <summary>
        /// Returns the full parameter name, given the partial parameter name.
        /// </summary>
        /// <param name="parameterName">The partial name of the parameter.</param>
        /// <returns>The full parameter name corresponding to the partial parameter name requested.</returns>
        string GetParameterName(string parameterName);

        /// <summary>
        /// Checks whether the specified identifier is valid.
        /// </summary>
        /// <param name="identifier">The identifier to check.</param>
        /// <returns><see langword="true"/> if <paramref name="identifier"/>
        /// represents a valid SQL identifier; otherwise, <see
        /// langword="false"/>.</returns>
        /// <remarks>
        /// <list type="number">
        /// <item>
        /// <description>
        /// The first character must be one of the following:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// A letter as defined by the Unicode Standard 3.2. The Unicode
        /// definition of letters includes Latin characters from a through z,
        /// from A through Z, and also letter characters from other languages.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <para>The underscore (_), at sign (@), or number sign (#).</para>
        /// <para>
        /// Certain symbols at the beginning of an identifier have special
        /// meaning in SQL Server. A regular identifier that starts with the at
        /// sign always denotes a local variable or parameter and cannot be used
        /// as the name of any other type of object. An identifier that starts
        /// with a number sign denotes a temporary table or procedure. An
        /// identifier that starts with double number signs (##) denotes a
        /// global temporary object. Although the number sign or double number
        /// sign characters can be used to begin the names of other types of
        /// objects, we do not recommend this practice.
        /// </para>
        /// <para>
        /// Some Transact-SQL functions have names that start with double at
        /// signs (@@). To avoid confusion with these functions, you should not
        /// use names that start with @@.
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Subsequent characters can include the following:
        /// <list type="bullet">
        /// <item>
        /// <description>Letters as defined in the Unicode Standard
        /// 3.2.</description>
        /// </item>
        /// <item>
        /// <description>Decimal numbers from either Basic Latin or other
        /// national scripts.</description>
        /// </item>
        /// <item>
        /// <description>The at sign, dollar sign ($), number sign, or
        /// underscore.</description>
        /// </item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>Embedded spaces or special characters are not
        /// allowed.</description>
        /// </item>
        /// <item>
        /// <description>Supplementary characters are not allowed.</description>
        /// </item>
        /// </list>
        /// </remarks>
        bool IsValidIdentifier(string identifier);

        /// <summary>
        /// Given an unquoted identifier in the correct catalog case, returns
        /// the correct quoted form of that identifier, including properly
        /// escaping any embedded quotes in the identifier.
        /// </summary>
        /// <param name="identifier">The original unquoted identifier.</param>
        /// <returns>The quoted version of the identifier. Embedded quotes
        /// within the identifier are properly escaped.</returns>
        /// <remarks>
        /// This uses the <see cref="IdentifierPrefix"/> and <see
        /// cref="IdentifierSuffix"/> values.
        /// </remarks>
        string QuoteIdentifier(string identifier);

        /// <summary>
        /// Returns the correct quoted form of the value, including properly escaping any embedded quotes.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <remarks>
        /// This uses the <see cref="QuotePrefix"/> and <see
        /// cref="QuoteSuffix"/> values.
        /// </remarks>
        /// <returns>The quoted version of the value. Embedded quotes
        /// within the value are properly escaped.</returns>
        string QuoteStringLiteral(string value);

        /// <summary>
        /// Given a quoted identifier, returns the correct unquoted form of that
        /// identifier, including properly un-escaping any embedded quotes in
        /// the identifier.
        /// </summary>
        /// <param name="identifier">The identifier that will have its embedded
        /// quotes removed.</param>
        /// <returns>The unquoted identifier, with embedded quotes properly
        /// un-escaped.</returns>
        /// <remarks>
        /// This uses the <see cref="IdentifierPrefix"/> and <see
        /// cref="IdentifierSuffix"/> values.
        /// </remarks>
        string UnquoteIdentifier(string identifier);
    }
}