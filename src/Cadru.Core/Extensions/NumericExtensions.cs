﻿//------------------------------------------------------------------------------
// <copyright file="NumericExtensions.cs"
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

using Cadru.Text;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common numeric manipulation.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this byte expression, byte min, byte max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this byte expression, byte min, byte max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<byte>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this decimal expression, decimal min, decimal max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this decimal expression, decimal min, decimal max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<decimal>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this double expression, double min, double max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this double expression, double min, double max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<double>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this short expression, short min, short max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this short expression, short min, short max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<short>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this int expression, int min, int max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this int expression, int min, int max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<int>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this long expression, long min, long max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this long expression, long min, long max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<long>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this float expression, float min, float max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether <paramref name="expression" />
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <param name="options">A bitwise combination of enumeration values
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression" /> is greater than
        /// the minimum value but less than the maximum value; otherwise it
        /// returns <see langword="false" />.</returns>
        public static bool Between(this float expression, float min, float max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<float>.Default);
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this byte expression, byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<byte>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this decimal expression, decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<decimal>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this double expression, double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<double>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this short expression, short min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<short>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see clangwordref="false" />.
        /// </returns>
        public static bool GreaterThan(this int expression, int min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<int>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this long expression, long min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<long>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThan(this float expression, float min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<float>.Default);
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this byte expression, byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<byte>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this decimal expression, decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<decimal>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this double expression, double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<double>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this short expression, short min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<short>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this int expression, int min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<int>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this long expression, long min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<long>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is greater than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool GreaterThanOrEqualTo(this float expression, float min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<float>.Default);
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this int expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this short expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this long expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this decimal expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this double expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an even number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEven(this float expression)
        {
            return (expression % 2) == 0;
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this int expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this short expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this long expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this decimal expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this double expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is an odd number; otherwise <see langword="false" />.
        /// </returns>
        public static bool IsOdd(this float expression)
        {
            return (expression % 2) != 0;
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this byte expression, byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<byte>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this decimal expression, decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<decimal>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this double expression, double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<double>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this short expression, short min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<short>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this int expression, int min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<int>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this long expression, long min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<long>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than the minimum indicated; otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThan(this float expression, float min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<float>.Default);
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this byte expression, byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<byte>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this decimal expression, decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<decimal>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this double expression, double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<double>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this short expression, short min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<short>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this int expression, int min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<int>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this long expression, long min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<long>.Default);
        }

        /// <summary>
        /// Returns a <see cref="Boolean" /> expression indicating whether
        /// <paramref name="expression" /> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="expression" />
        /// is less than or equal to the minimum indicated;
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool LessThanOrEqualTo(this float expression, float min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<float>.Default);
        }

        private static bool Between<T>(T expression, T minimum, T maximum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            var success = options switch
            {
                NumericComparisonOptions.IncludeBoth => (comparer.Compare(expression, minimum) >= 0) && (comparer.Compare(expression, maximum) <= 0),
                NumericComparisonOptions.IncludeMinimum => (comparer.Compare(expression, minimum) >= 0) && (comparer.Compare(expression, maximum) < 0),
                NumericComparisonOptions.IncludeMaximum => (comparer.Compare(expression, minimum) > 0) && (comparer.Compare(expression, maximum) <= 0),
                _ => (comparer.Compare(expression, minimum) > 0) && (comparer.Compare(expression, maximum) < 0),
            };
            return success;
        }

        private static bool GreaterThan<T>(T expression, T minimum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            bool success;
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

        private static bool LessThan<T>(T expression, T maximum, NumericComparisonOptions options, IComparer<T> comparer) where T : IComparable
        {
            bool success;
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
    }
}