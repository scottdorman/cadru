using System.ComponentModel.DataAnnotations;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents an SCIM filter expression.
    /// </summary>
    /// <remarks>
    /// Each expression MUST contain an attribute name followed by an attribute
    /// operator and optional value. Multiple expressions MAY be combined using
    /// an <see cref="IFilterGroup"></see>.
    /// </remarks>
    public interface IFilterExpression : IFilter
    {
        /// <summary>
        /// The attribute name.
        /// </summary>
        [Required]
        string? Attribute { get; }

        /// <summary>
        /// The attribute operator.
        /// </summary>
        [Required]
        FilterExpressionOperator Operator { get; }

        /// <summary>
        /// The value.
        /// </summary>
        string? Value { get; }
    }
}