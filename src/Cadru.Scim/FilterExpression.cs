//------------------------------------------------------------------------------
// <copyright file="FilterExpression.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

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
