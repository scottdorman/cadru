namespace Cadru.Scim.Filters
{
    /// <inheritdoc/>
    public class FilterExpression : IFilterExpression
    {
        /// <inheritdoc/>
        public virtual string? Attribute { get; set; }

        /// <inheritdoc/>
        public virtual FilterExpressionOperator Operator { get; set; }

        /// <inheritdoc/>
        public string? Value { get; set; }

        /// <inheritdoc/>
        public string ToFilterExpression(bool prependQuerySeprator = true)
        {
            return $@"{(prependQuerySeprator ? "?" : "")}filter={ this }";
        }

        /// <summary>
        /// Returns a string that represents the current <see
        /// cref="FilterExpression"></see> as a valid query
        /// </summary>
        /// <returns>A string that represents the current <see
        /// cref="FilterExpression"></see>.</returns>
        public override string ToString()
        {
            string expression;
            if (this.Operator == FilterExpressionOperator.Present)
            {
                expression = $@"{ this.Attribute } { this.Operator }";
            }
            else
            {
                expression = $@"{ this.Attribute } { this.Operator } ""{ this.Value }""";
            }

            return expression;
        }
    }
}
