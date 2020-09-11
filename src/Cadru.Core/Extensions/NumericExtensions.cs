//------------------------------------------------------------------------------
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
using System.Runtime.CompilerServices;

using Cadru.Resources;
using Cadru.Text;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common numeric manipulation.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this byte expression, byte min, byte max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any byte value.</param>
        /// <param name="min">The minimum byte value.</param>
        /// <param name="max">The maximum byte value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this byte expression, byte min, byte max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this decimal expression, decimal min, decimal max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <param name="max">The maximum decimal value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this decimal expression, decimal min, decimal max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this double expression, double min, double max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any double value.</param>
        /// <param name="min">The minimum double value.</param>
        /// <param name="max">The maximum double value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this double expression, double min, double max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this short expression, short min, short max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any short value.</param>
        /// <param name="min">The minimum short value.</param>
        /// <param name="max">The maximum short value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this short expression, short min, short max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this int expression, int min, int max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any integer value.</param>
        /// <param name="min">The minimum integer value.</param>
        /// <param name="max">The maximum integer value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this int expression, int min, int max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this long expression, long min, long max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any long value.</param>
        /// <param name="min">The minimum long value.</param>
        /// <param name="max">The maximum long value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this long expression, long min, long max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this float expression, float min, float max)
        {
            return expression > min && expression < max;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is between the minimum and maximum indicated.
        /// </summary>
        /// <param name="expression">Any single value.</param>
        /// <param name="min">The minimum single value.</param>
        /// <param name="max">The maximum single value.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// Between returns <see langword="true"/> if
        /// <paramref name="expression"/> is greater than the minimum value but
        /// less than the maximum value; otherwise it returns <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(this float expression, float min, float max, NumericComparisonOptions options)
        {
            return options switch
            {
                NumericComparisonOptions.IncludeBoth => expression >= min && expression <= max,
                NumericComparisonOptions.IncludeMinimum => expression >= min && expression < max,
                NumericComparisonOptions.IncludeMaximum => expression > min && expression <= max,
                _ => expression > min && expression < max,
            };
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(this byte value, byte min, byte max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal Clamp(this decimal value, decimal min, decimal max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double value, double min, double max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Clamp(this short value, short min, short max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int value, int min, int max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(this long value, long min, long max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Clamp(this sbyte value, sbyte min, sbyte max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float value, float min, float max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Clamp(this ushort value, ushort min, ushort max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(this uint value, uint min, uint max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        /// <para><paramref name="value"/> if <paramref name="min"/> &#x2264; value &#x2265; max.</para>
        /// <para>-or-</para>
        /// <para><paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/></para>
        /// <para>-or-</para>
        /// <para><paramref name="max"/> if <paramref name="max"/> &lt; <paramref name="value"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Clamp(this ulong value, ulong min, ulong max)
        {
            if (min > max)
            {
                ThrowMinMaxException(min, max);
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

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
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this byte expression, byte min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this decimal expression, decimal min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this double expression, double min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this short expression, short min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see clangwordref="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this int expression, int min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this long expression, long min)
        {
            return expression > min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this float expression, float min)
        {
            return expression > min;
        }

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
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this byte expression, byte min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this decimal expression, decimal min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this double expression, double min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this short expression, short min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this int expression, int min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this long expression, long min)
        {
            return expression >= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is greater
        /// than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrEqualTo(this float expression, float min)
        {
            return expression >= min;
        }

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
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this int expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this short expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this long expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this decimal expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this double expression)
        {
            return (expression % 2) == 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an even number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an even
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this float expression)
        {
            return (expression % 2) == 0;
        }

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
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this int expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this short expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this long expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this decimal expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this double expression)
        {
            return (expression % 2) != 0;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is an odd number.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is an odd
        /// number; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this float expression)
        {
            return (expression % 2) != 0;
        }

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
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this byte expression, byte min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this decimal expression, decimal min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this double expression, double min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this short expression, short min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this int expression, int min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this long expression, long min)
        {
            return expression < min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this float expression, float min)
        {
            return expression < min;
        }

        /// <overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this byte expression, byte min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">Any decimal value.</param>
        /// <param name="min">The minimum decimal value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this decimal expression, decimal min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this double expression, double min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this short expression, short min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this int expression, int min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this long expression, long min)
        {
            return expression <= min;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether
        /// <paramref name="expression"/> is less than or equal to the minimum indicated.
        /// </summary>
        /// <param name="expression">The value to test.</param>
        /// <param name="min">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="expression"/> is less than
        /// or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrEqualTo(this float expression, float min)
        {
            return expression <= min;
        }

        internal static T Clamp<T>(this T value, T min, T max) where T : unmanaged, IComparable<T>
        {
            if (min.CompareTo(max) > 0)
            {
                ThrowMinMaxException(min, max);
            }

            if (value.CompareTo(min) < 0)
            {
                return min;
            }
            else if (value.CompareTo(max) > 0)
            {
                return max;
            }

            return value;
        }

        private static void ThrowMinMaxException<T>(T min, T max)
        {
            throw new ArgumentException(String.Format(Strings.Argument_MinMaxValue, min, max));
        }
    }
}