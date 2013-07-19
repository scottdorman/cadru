//------------------------------------------------------------------------------
// <copyright file="NumericExtensions.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
namespace Cadru
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Cadru.Text;

    /// <summary>
    /// Provides basic routines for common numeric manipulation.
    /// </summary>
    public static class NumericExtensions
    {
        #region Between

        #region Between(Byte expression, Byte min, Byte max)

        #region Between(Byte expression, Byte min, Byte max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Byte expression, Byte min, Byte max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Byte expression, Byte min, Byte max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Byte expression, Byte min, Byte max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Byte>.Default);
        }
        #endregion

        #endregion

        #region Between(Decimal expression, Decimal min, decimal max)

        #region Between(Decimal expression, Decimal min, Decimal max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Decimal expression, Decimal min, Decimal max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Decimal expression, Decimal min, Decimal max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Decimal expression, Decimal min, Decimal max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Decimal>.Default);
        }
        #endregion

        #endregion

        #region Between(Double expression, Double min, Double max)

        #region Between(Double expression, Double min, Double max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Double expression, Double min, Double max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Double expression, Double min, Double max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Double expression, Double min, Double max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Double>.Default);
        }
        #endregion

        #endregion

        #region Between(Int16 expression, Int16 min, Int16 max)

        #region Between(Int16 expression, Int16 min, Int16 max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int16 value.</param>
        /// <param name="min">The minimum Int16 value.</param>
        /// <param name="max">The maximum Int16 value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int16 expression, Int16 min, Int16 max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Int16 expression, Int16 min, Int16 max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int16 value.</param>
        /// <param name="min">The minimum Int16 value.</param>
        /// <param name="max">The maximum Int16 value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int16 expression, Int16 min, Int16 max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Int16>.Default);
        }
        #endregion

        #endregion

        #region Between(Int32 expression, Int32 min, Int32 max)

        #region Between(Int32 expression, Int32 min, Int32 max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int32 value.</param>
        /// <param name="min">The minimum Int32 value.</param>
        /// <param name="max">The maximum Int32 value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int32 expression, Int32 min, Int32 max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Int32 expression, Int32 min, Int32 max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int32 value.</param>
        /// <param name="min">The minimum Int32 value.</param>
        /// <param name="max">The maximum Int32 value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int32 expression, Int32 min, Int32 max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Int32>.Default);
        }
        #endregion

        #endregion

        #region Between(Int64 expression, Int64 min, Int64 max)

        #region Between(Int64 expression, Int64 min, Int64 max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int64 value.</param>
        /// <param name="min">The minimum Int64 value.</param>
        /// <param name="max">The maximum Int64 value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int64 expression, Int64 min, Int64 max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Int64 expression, Int64 min, Int64 max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any Int64 value.</param>
        /// <param name="min">The minimum Int64 value.</param>
        /// <param name="max">The maximum Int64 value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Int64 expression, Int64 min, Int64 max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Int64>.Default);
        }
        #endregion

        #endregion

        #region Between(Single expression, Single min, Single max)

        #region Between(Single expression, Single min, Single max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Single expression, Single min, Single max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(Single expression, Single min, Single max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this Single expression, Single min, Single max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<Single>.Default);
        }
        #endregion

        #endregion

        #endregion

        #region GreaterThan

        #region GreaterThan(Byte expression, Byte min)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Byte expression, Byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Byte>.Default);
        }
        #endregion

        #region GreaterThan(Decimal expression, Decimal min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Decimal expression, Decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Decimal>.Default);
        }
        #endregion

        #region GreaterThan(Double expression, Double min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Double expression, Double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Double>.Default);
        }
        #endregion

        #region GreaterThan(Int16 expression, Int16 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Int16 expression, Int16 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Int16>.Default);
        }
        #endregion

        #region GreaterThan(Int32 expression, Int32 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see clangwordref="false"/>.
        /// </returns>
        public static bool GreaterThan(this Int32 expression, Int32 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Int32>.Default);
        }
        #endregion

        #region GreaterThan(this Int64 expression, Int64 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Int64 expression, Int64 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Int64>.Default);
        }
        #endregion

        #region GreaterThan(Single expression, Single min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThan(this Single expression, Single min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<Single>.Default);
        }
        #endregion

        #endregion

        #region GreaterThanOrEqualTo

        #region GreaterThanOrEqualTo(Byte expression, Byte min)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Byte expression, Byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Byte>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Decimal expression, Decimal min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Decimal expression, Decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Decimal>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Double expression, Double min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Double expression, Double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Double>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Int16 expression, Int16 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Int16 expression, Int16 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int16>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Int32 expression, Int32 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Int32 expression, Int32 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int32>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Int64 expression, Int64 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Int64 expression, Int64 min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int64>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(Single expression, Single min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this Single expression, Single min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Single>.Default);
        }
        #endregion

        #endregion

        #region LessThan

        #region LessThan(Byte expression, Byte min)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Byte expression, Byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Byte>.Default);
        }
        #endregion

        #region LessThan(Decimal expression, Decimal min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Decimal expression, Decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Decimal>.Default);
        }
        #endregion

        #region LessThan(Double expression, Double min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Double expression, Double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Double>.Default);
        }
        #endregion

        #region LessThan(Int16 expression, Int16 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Int16 expression, Int16 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Int16>.Default);
        }
        #endregion

        #region LessThan(Int32 expression, Int32 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Int32 expression, Int32 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Int32>.Default);
        }
        #endregion

        #region LessThan(this Int64 expression, Int64 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Int64 expression, Int64 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Int64>.Default);
        }
        #endregion

        #region LessThan(Single expression, Single min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThan(this Single expression, Single min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<Single>.Default);
        }
        #endregion

        #endregion

        #region LessThanOrEqualTo

        #region LessThanOrEqualTo(Byte expression, Byte min)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Byte expression, Byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Byte>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Decimal expression, Decimal min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Decimal expression, Decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Decimal>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Double expression, Double min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Double expression, Double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Double>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Int16 expression, Int16 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Int16 expression, Int16 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int16>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Int32 expression, Int32 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Int32 expression, Int32 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int32>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Int64 expression, Int64 min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Int64 expression, Int64 min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Int64>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(Single expression, Single min)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool LessThanOrEqualTo(this Single expression, Single min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<Single>.Default);
        }
        #endregion

        #endregion

        #region Between<T>(T expression, T minimum, T maximum, MinMaxCompareOptions options, IComparer<T> comparer)
        private static bool Between<T>(T expression, T minimum, T maximum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            bool success = false;

            switch (options)
            {
                case NumericComparisonOptions.IncludeBoth:
                    success = (comparer.Compare(expression, minimum) >= 0) && (comparer.Compare(expression, maximum) <= 0);
                    break;

                case NumericComparisonOptions.IncludeMinimum:
                    success = (comparer.Compare(expression, minimum) >= 0) && (comparer.Compare(expression, maximum) < 0);
                    break;

                case NumericComparisonOptions.IncludeMaximum:
                    success = (comparer.Compare(expression, minimum) > 0) && (comparer.Compare(expression, maximum) <= 0);
                    break;

                default:
                    success = (comparer.Compare(expression, minimum) > 0) && (comparer.Compare(expression, maximum) < 0);
                    break;
            }

            return success;
        }
        #endregion

        #region GreaterThan<T>(T expression, T minimum, MinMaxCompareOptions options, IComparer<T> comparer)
        private static bool GreaterThan<T>(T expression, T minimum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            bool success = false;

            switch (options)
            {
                case NumericComparisonOptions.IncludeBoth:
                case NumericComparisonOptions.IncludeMinimum:
                    success = comparer.Compare(expression, minimum) >= 0;
                    break;

                default:
                    success = comparer.Compare(expression, minimum) > 0;
                    break;
            }

            return success;
        }
        #endregion

        #region LessThan<T>(T expression, T maximum, MinMaxCompareOptions options, IComparer<T> comparer)
        private static bool LessThan<T>(T expression, T maximum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            bool success = false;

            switch (options)
            {
                case NumericComparisonOptions.IncludeBoth:
                case NumericComparisonOptions.IncludeMaximum:
                    success = comparer.Compare(expression, maximum) <= 0;
                    break;

                default:
                    success = comparer.Compare(expression, maximum) < 0;
                    break;
            }

            return success;
        }
        #endregion
    }
}
