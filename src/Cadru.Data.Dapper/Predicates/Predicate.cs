//------------------------------------------------------------------------------
// <copyright file="Predicate.cs"
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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Cadru.Contracts;
using Cadru.Data.Dapper.Internal;
using Cadru.Data.Dapper.Predicates.Internal;

namespace Cadru.Data.Dapper.Predicates
{
    /// <summary>
    /// Provides factory methods for creating <see cref="IPredicate"/> instances.
    /// </summary>
    public static class Predicate
    {
        /// <summary>
        /// Creates a predicate group whose predicates are joined using an AND operator.
        /// </summary>
        /// <returns>An <see cref="IPredicateGroup"/> instance representing the predicate.</returns>
        public static IPredicateGroup And()
        {
            var group = new PredicateGroup
            {
                Operator = GroupOperator.And,
            };

            return group;
        }

        /// <summary>
        /// Creates a predicate group whose predicates are joined using an AND operator.
        /// </summary>
        /// <param name="predicates">A collection of predicates to add the group.</param>
        /// <returns>An <see cref="IPredicateGroup"/> instance representing the predicate.</returns>
        public static IPredicateGroup And(IList<IPredicate> predicates)
        {
            var group = (PredicateGroup)And();
            group.AddRange(predicates);
            return group;
        }

        /// <summary>
        /// Represents an empty predicate.
        /// </summary>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate Empty() => new EmptyPredicate();

        /// <summary>
        /// Creates a predicate which represents an EXISTS clause.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate Exists(IPredicate predicate, bool not = false)
        {
            return new ExistsPredicate
            {
                Not = not,
                Predicate = predicate
            };
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName] [Operator] [Value].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity.</typeparam>
        /// <typeparam name="TFieldType">The data type of the property.</typeparam>
        /// <param name="expression">An expression that returns the [FieldName] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="value">The value for the predicate.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate FieldComparison<TModel, TFieldType>(Expression<Func<TModel, TFieldType>> expression, Operator op, TFieldType value, bool not = false)
            where TModel : class
        {
            Requires.NotNull(expression, nameof(expression));

            var predicate = new FieldPredicate<TModel, TFieldType>
            {
                Not = not,
                PropertyName = expression!.GetProperty()?.Name!,
                Operator = op,
                Value = value,
            };

            return predicate;
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName] [Operator] [Value].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity.</typeparam>
        /// <typeparam name="TFieldType">The data type of the property.</typeparam>
        /// <param name="expression">An expression that returns the [FieldName] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="value">The value for the predicate.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate FieldComparison<TModel, TFieldType>(Expression<Func<TModel, TFieldType>> expression, Operator op, IEnumerable<TFieldType> value, bool not = false)
            where TModel : class
        {
            var predicate = new FieldPredicate<TModel, TFieldType>
            {
                Not = not,
                PropertyName = expression?.GetProperty()?.Name,
                Operator = op,
                Value = value,
            };

            return predicate;
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName] [Operator] [Value].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity.</typeparam>
        /// <typeparam name="TFieldType">The data type of the property.</typeparam>
        /// <param name="fieldName">The [FieldName] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="value">The value for the predicate.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate FieldComparison<TModel, TFieldType>(string fieldName, Operator op, TFieldType value, bool not = false)
            where TModel : class
        {
            var predicate = new FieldPredicate<TModel, TFieldType>
            {
                Not = not,
                PropertyName = fieldName,
                Operator = op,
                Value = value,
            };

            return predicate;
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName] [Operator] [Value].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity.</typeparam>
        /// <typeparam name="TFieldType">The data type of the property.</typeparam>
        /// <param name="fieldName">The [FieldName] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="value">The value for the predicate.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate FieldComparison<TModel, TFieldType>(string fieldName, Operator op, IEnumerable<TFieldType> value, bool not = false)
            where TModel : class
        {
            var predicate = new FieldPredicate<TModel, TFieldType>
            {
                Not = not,
                PropertyName = fieldName,
                Operator = op,
                Value = value,
            };

            return predicate;
        }

        /// <summary>
        /// Creates a predicate group whose predicates are joined using an OR operator.
        /// </summary>
        /// <returns>An <see cref="IPredicateGroup"/> instance representing the predicate.</returns>
        public static IPredicateGroup Or()
        {
            var group = new PredicateGroup
            {
                Operator = GroupOperator.Or,
            };

            return group;
        }

        /// <summary>
        /// Creates a predicate group whose predicates are joined using an OR operator.
        /// </summary>
        /// <param name="predicates">A collection of predicates to add the group.</param>
        /// <returns>An <see cref="IPredicateGroup"/> instance representing the predicate.</returns>
        public static IPredicateGroup Or(IList<IPredicate> predicates)
        {
            var group = (PredicateGroup)Or();
            group.AddRange(predicates);
            return group;
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName1] [Operator] [FieldName2].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity for the left operand.</typeparam>
        /// <typeparam name="TModel2">The type of the entity for the right operand.</typeparam>
        /// <typeparam name="TFieldType">The data type of the property.</typeparam>
        /// <param name="left">An expression that returns the [FieldName1] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="right">An expression that returns the [FieldName2] operand.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate PropertyComparison<TModel, TModel2, TFieldType>(Expression<Func<TModel, TFieldType>> left, Operator op, Expression<Func<TModel2, TFieldType>> right, bool not = false)
            where TModel : class
            where TModel2 : class
        {
            var predicate = new PropertyPredicate<TModel, TModel2>
            {
                Not = not,
                PropertyName = left?.GetProperty()?.Name,
                Operator = op,
                PropertyName2 = right?.GetProperty()?.Name
            };

            return predicate;
        }

        /// <summary>
        /// Creates a predicate which represents a standard comparison clause,
        /// of the form [FieldName1] [Operator] [FieldName2].
        /// </summary>
        /// <typeparam name="TModel">The type of the entity for the left operand.</typeparam>
        /// <typeparam name="TModel2">The type of the entity for the right operand.</typeparam>
        /// <param name="left">The [FieldName1] operand.</param>
        /// <param name="op">One of the <see cref="Operator"/> values.</param>
        /// <param name="right">The [FieldName2] operand.</param>
        /// <param name="not"><see langword="true"/> to invert the comparison operator.</param>
        /// <returns>An <see cref="IPredicate"/> instance representing the predicate.</returns>
        public static IPredicate PropertyComparison<TModel, TModel2>(string left, Operator op, string right, bool not = false)
            where TModel : class
            where TModel2 : class
        {
            var predicate = new PropertyPredicate<TModel, TModel2>
            {
                Not = not,
                PropertyName = left,
                Operator = op,
                PropertyName2 = right
            };

            return predicate;
        }
    }
}