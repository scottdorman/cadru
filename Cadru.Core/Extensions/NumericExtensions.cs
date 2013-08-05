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

namespace Cadru.Extensions
{
    using System;
    using System.Collections.Generic;
    using Cadru.Text;

    /// <summary>
    /// Provides basic routines for common numeric manipulation.
    /// </summary>
    public static class NumericExtensions
    {
        #region fields
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region Between

        #region Between(byte expression, byte min, byte max)

        #region Between(byte expression, byte min, byte max)
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
        public static bool Between(this byte expression, byte min, byte max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(byte expression, byte min, byte max, MinMaxCompareOptions options)
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
        public static bool Between(this byte expression, byte min, byte max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<byte>.Default);
        }
        #endregion

        #endregion

        #region Between(decimal expression, decimal min, decimal max)

        #region Between(decimal expression, decimal min, decimal max)
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
        public static bool Between(this decimal expression, decimal min, decimal max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(decimal expression, decimal min, decimal max, MinMaxCompareOptions options)
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
        public static bool Between(this decimal expression, decimal min, decimal max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<decimal>.Default);
        }
        #endregion

        #endregion

        #region Between(double expression, double min, double max)

        #region Between(double expression, double min, double max)
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
        public static bool Between(this double expression, double min, double max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(double expression, double min, double max, MinMaxCompareOptions options)
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
        public static bool Between(this double expression, double min, double max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<double>.Default);
        }
        #endregion

        #endregion

        #region Between(short expression, short min, short max)

        #region Between(short expression, short min, short max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this short expression, short min, short max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(short expression, short min, short max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this short expression, short min, short max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<short>.Default);
        }
        #endregion

        #endregion

        #region Between(int expression, int min, int max)

        #region Between(int expression, int min, int max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this int expression, int min, int max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(int expression, int min, int max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this int expression, int min, int max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<int>.Default);
        }
        #endregion

        #endregion

        #region Between(long expression, long min, long max)

        #region Between(long expression, long min, long max)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this long expression, long min, long max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(long expression, long min, long max, MinMaxCompareOptions options)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <param name="options">A bitwise combination of enumeration values 
        /// that defines whether the comparison is inclusive.</param>
        /// <returns>Between returns <see langword="true" /> if <paramref name="expression"/> is greater than
        /// the minimum value but less than the maximum value; otherwise it 
        /// returns <see langword="false" />.</returns>
        public static bool Between(this long expression, long min, long max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<long>.Default);
        }
        #endregion

        #endregion

        #region Between(float expression, float min, float max)

        #region Between(float expression, float min, float max)
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
        public static bool Between(this float expression, float min, float max)
        {
            return Between(expression, min, max, NumericComparisonOptions.IncludeBoth);
        }
        #endregion

        #region Between(float expression, float min, float max, MinMaxCompareOptions options)
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
        public static bool Between(this float expression, float min, float max, NumericComparisonOptions options)
        {
            return Between(expression, min, max, options, Comparer<float>.Default);
        }
        #endregion

        #endregion

        #endregion

        #region GreaterThan

        #region GreaterThan(byte expression, byte min)
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
        public static bool GreaterThan(this byte expression, byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<byte>.Default);
        }
        #endregion

        #region GreaterThan(decimal expression, decimal min)
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
        public static bool GreaterThan(this decimal expression, decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<decimal>.Default);
        }
        #endregion

        #region GreaterThan(double expression, double min)
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
        public static bool GreaterThan(this double expression, double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<double>.Default);
        }
        #endregion

        #region GreaterThan(short expression, short min)
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
        public static bool GreaterThan(this short expression, short min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<short>.Default);
        }
        #endregion

        #region GreaterThan(int expression, int min)
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
        public static bool GreaterThan(this int expression, int min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<int>.Default);
        }
        #endregion

        #region GreaterThan(this long expression, long min)
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
        public static bool GreaterThan(this long expression, long min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<long>.Default);
        }
        #endregion

        #region GreaterThan(float expression, float min)
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
        public static bool GreaterThan(this float expression, float min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.None, Comparer<float>.Default);
        }
        #endregion

        #endregion

        #region GreaterThanOrEqualTo

        #region GreaterThanOrEqualTo(byte expression, byte min)
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
        public static bool GreaterThanOrEqualTo(this byte expression, byte min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<byte>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(decimal expression, decimal min)
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
        public static bool GreaterThanOrEqualTo(this decimal expression, decimal min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<decimal>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(double expression, double min)
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
        public static bool GreaterThanOrEqualTo(this double expression, double min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<double>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(short expression, short min)
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
        public static bool GreaterThanOrEqualTo(this short expression, short min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<short>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(int expression, int min)
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
        public static bool GreaterThanOrEqualTo(this int expression, int min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<int>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(long expression, long min)
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
        public static bool GreaterThanOrEqualTo(this long expression, long min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<long>.Default);
        }
        #endregion

        #region GreaterThanOrEqualTo(float expression, float min)
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
        public static bool GreaterThanOrEqualTo(this float expression, float min)
        {
            return GreaterThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<float>.Default);
        }
        #endregion

        #endregion

        #region IsEven

        #region IsEven(this int expression)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this int expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #region IsEven(this short expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this short expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #region IsEven(this long expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this long expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #region IsEven(this decimal expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this decimal expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #region IsEven(this double expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this double expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #region IsEven(this float expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an even number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsEven(this float expression)
        {
            return (expression % 2) == 0;
        }
        #endregion

        #endregion

        #region IsOdd

        #region IsOdd(this int expression)
        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this int expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #region IsOdd(this short expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this short expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #region IsOdd(this long expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this long expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #region IsOdd(this decimal expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this decimal expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #region IsOdd(this double expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this double expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #region IsOdd(this float expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether 
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/>
        /// is an odd number; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsOdd(this float expression)
        {
            return (expression % 2) != 0;
        }
        #endregion

        #endregion

        #region LessThan

        #region LessThan(byte expression, byte min)
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
        public static bool LessThan(this byte expression, byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<byte>.Default);
        }
        #endregion

        #region LessThan(decimal expression, decimal min)
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
        public static bool LessThan(this decimal expression, decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<decimal>.Default);
        }
        #endregion

        #region LessThan(double expression, double min)
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
        public static bool LessThan(this double expression, double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<double>.Default);
        }
        #endregion

        #region LessThan(short expression, short min)
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
        public static bool LessThan(this short expression, short min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<short>.Default);
        }
        #endregion

        #region LessThan(int expression, int min)
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
        public static bool LessThan(this int expression, int min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<int>.Default);
        }
        #endregion

        #region LessThan(this long expression, long min)
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
        public static bool LessThan(this long expression, long min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<long>.Default);
        }
        #endregion

        #region LessThan(float expression, float min)
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
        public static bool LessThan(this float expression, float min)
        {
            return LessThan(expression, min, NumericComparisonOptions.None, Comparer<float>.Default);
        }
        #endregion

        #endregion

        #region LessThanOrEqualTo

        #region LessThanOrEqualTo(byte expression, byte min)
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
        public static bool LessThanOrEqualTo(this byte expression, byte min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<byte>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(decimal expression, decimal min)
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
        public static bool LessThanOrEqualTo(this decimal expression, decimal min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<decimal>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(double expression, double min)
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
        public static bool LessThanOrEqualTo(this double expression, double min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<double>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(short expression, short min)
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
        public static bool LessThanOrEqualTo(this short expression, short min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<short>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(int expression, int min)
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
        public static bool LessThanOrEqualTo(this int expression, int min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<int>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(long expression, long min)
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
        public static bool LessThanOrEqualTo(this long expression, long min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<long>.Default);
        }
        #endregion

        #region LessThanOrEqualTo(float expression, float min)
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
        public static bool LessThanOrEqualTo(this float expression, float min)
        {
            return LessThan(expression, min, NumericComparisonOptions.IncludeBoth, Comparer<float>.Default);
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

        #endregion
    }
}
