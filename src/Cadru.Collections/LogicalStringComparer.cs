//------------------------------------------------------------------------------
// <copyright file="LogicalStringComparer.cs"
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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Cadru.Collections.Resources;
using Cadru.Internal;

namespace Cadru.Collections
{
    /// <summary>
    /// Compares two strings for equivalence, ignoring case, in natural numeric order.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Windows implements natural numeric sorting inside the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb759947(v=vs.85).aspx">StrCmpLogicalW</see>
    /// function in
    /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb759844(v=vs.85).aspx">Shell
    /// Lightweight Utility Functions</seealso>. This function is available on
    /// Windows XP or higher.
    /// </para>
    /// <para>
    /// This implementation is not 100% compatible with
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb759947(v=vs.85).aspx">StrCmpLogicalW</see>.
    /// It gives the same results for the numeric sort, with the exception of
    /// strings containing non-alphanumeric ASCII characters. The code relies on
    /// the current locale to find the order of the characters.
    /// </para>
    /// <para>
    /// The code here will order files that start with special characters based
    /// on the code table order. Windows Explorer uses another order.
    /// </para>
    /// <para><example>Windows Explorer: (1.txt, [1.txt, _1.txt, =1.txt</example></para>
    /// <para><example>this code: (1.txt, =1.txt, [1.txt, _1.txt</example></para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public sealed class LogicalStringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
    {
        private readonly CultureInfo cultureInfo;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="LogicalStringComparer"/> class using the
        /// <see cref="CultureInfo.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <remarks>
        /// When the <see cref="LogicalStringComparer"/> instance is created
        /// using this constructor, the <see cref="CultureInfo.CurrentCulture"/>
        /// of the current thread is saved. Comparison procedures use the saved
        /// culture to determine the sort order and casing rules; therefore,
        /// string comparisons might have different results depending on the
        /// culture. For more information on culture-specific comparisons, see
        /// the <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        public LogicalStringComparer()
            : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="LogicalStringComparer"/> class using the specified <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> to use for the new <see cref="LogicalStringComparer"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="culture"/> is a <see langword="null"/>.
        /// </exception>
        /// <rermarks>
        /// Comparison procedures use the specified <see cref="CultureInfo"/> to
        /// determine the sort order and casing rules. String comparisons might
        /// have different results depending on the culture. For more
        /// information on culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </rermarks>
        public LogicalStringComparer(CultureInfo culture)
        {
            Contracts.Requires.NotNull(culture, nameof(culture));

            this.cultureInfo = culture;
        }

        /// <summary>
        /// Represents an instance of <see cref="LogicalStringComparer"/> that
        /// is associated with the <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <value>The default <see cref="LogicalStringComparer"/></value>
        /// <remarks>
        /// Comparison procedures use the
        /// <see cref="CultureInfo.CurrentCulture"/> of the current thread to
        /// determine the sort order and casing rules. String comparisons might
        /// have different results depending on the culture. For more
        /// information on culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Cadru.Collections.LogicalStringComparer.#ctor", Justification = "This constructor call implicitly passes a culture.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer Default => new LogicalStringComparer();

        /// <summary>
        /// Represents an instance of <see cref="LogicalStringComparer"/> that
        /// is associated with the <see cref="CultureInfo.InvariantCulture"/>
        /// and that is always available.
        /// </summary>
        /// <value>
        /// An instance of <see cref="LogicalStringComparer"/> that is
        /// associated with <see cref="CultureInfo.InvariantCulture"/>.
        /// </value>
        /// <remarks>
        /// Comparison procedures use the
        /// <see cref="CultureInfo.InvariantCulture"/> to determine the sort
        /// order and casing rules. String comparisons might have different
        /// results depending on the culture. For more information on
        /// culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer DefaultInvariant => new LogicalStringComparer(CultureInfo.InvariantCulture);

        /// <inheritdoc/>
        public int Compare(object x, object y)
        {
            int result;

            if (x is string left && y is string right)
            {
                if (String.IsNullOrEmpty(left) && String.IsNullOrEmpty(right))
                {
                    result = 0;
                }
                else if (String.IsNullOrEmpty(left))
                {
                    result = -1;
                }
                else if (String.IsNullOrEmpty(right))
                {
                    result = 1;
                }
                else
                {
                    result = this.Compare(left, right);
                }
            }
            else
            {
                result = x switch
                {
                    null when y is null => 0,
                    null => -1,
                    _ => 1
                };
            }

            return result;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Reviewed.")]
        public int Compare(string x, string y)
        {
            if (String.IsNullOrEmpty(x) && String.IsNullOrEmpty(y))
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(x))
            {
                return -1;
            }
            else if (String.IsNullOrEmpty(y))
            {
                return 1;
            }
            else
            {
                var lengthOfX = x.Length;
                var lengthOfY = y.Length;

                var sp1 = Char.IsLetterOrDigit(x[0]);
                var sp2 = Char.IsLetterOrDigit(y[0]);

                if (sp1 && !sp2)
                {
                    return 1;
                }

                if (!sp1 && sp2)
                {
                    return -1;
                }

                char c1, c2;
                int i1 = 0, i2 = 0;
                bool letter1, letter2;

                while (true)
                {
                    c1 = x[i1];
                    c2 = y[i2];

                    sp1 = Char.IsDigit(c1);
                    sp2 = Char.IsDigit(c2);

                    int r;
                    if (!sp1 && !sp2)
                    {
                        if (c1 != c2)
                        {
                            letter1 = Char.IsLetter(c1);
                            letter2 = Char.IsLetter(c2);

                            if (letter1 && letter2)
                            {
                                c1 = this.cultureInfo.TextInfo.ToUpper(c1);
                                c2 = this.cultureInfo.TextInfo.ToUpper(c2);
                                r = c1 - c2;
                                if (0 != r)
                                {
                                    return r;
                                }
                            }
                            else if (!letter1 && !letter2)
                            {
                                r = c1 - c2;
                                if (0 != r)
                                {
                                    return r;
                                }
                            }
                            else if (letter1)
                            {
                                return 1;
                            }
                            else if (letter2)
                            {
                                return -1;
                            }
                        }
                    }
                    else if (sp1 && sp2)
                    {
                        r = CompareNumbers(x, lengthOfX, ref i1, y, lengthOfY, ref i2);
                        if (0 != r)
                        {
                            return r;
                        }
                    }
                    else if (sp1)
                    {
                        return -1;
                    }
                    else if (sp2)
                    {
                        return 1;
                    }

                    i1++;
                    i2++;

                    if (i1 >= lengthOfX)
                    {
                        if (i2 >= lengthOfY)
                        {
                            return 0;
                        }

                        return -1;
                    }
                    else if (i2 >= lengthOfY)
                    {
                        return 1;
                    }
                }
            }
        }

        /// <inheritdoc/>
        bool IEqualityComparer.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        /// <inheritdoc/>
        public bool Equals(string x, string y)
        {
            return String.Equals(x, y);
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
        {
            Contracts.Requires.NotNull(obj, nameof(obj));

            if (obj is string s1)
            {
                return s1.GetHashCode();
            }

            throw ExceptionBuilder.CreateArgumentException(nameof(obj), Strings.Argument_MustBeString);
        }

        /// <inheritdoc/>
        public int GetHashCode(string obj)
        {
            Contracts.Requires.NotNull(obj, nameof(obj));

            return obj.GetHashCode();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static int CompareNumbers(string s1, int s1Length, ref int i1, string s2, int s2Length, ref int i2)
        {
            int nzStart1 = i1, nzStart2 = i2;
            int end1 = i1, end2 = i2;

            ScanNumber(s1, s1Length, i1, ref nzStart1, ref end1);
            ScanNumber(s2, s2Length, i2, ref nzStart2, ref end2);

            var start1 = i1;
            i1 = end1 - 1;
            var start2 = i2;
            i2 = end2 - 1;

            var length1 = end2 - nzStart2;
            var length2 = end1 - nzStart1;

            if (length1 == length2)
            {
                int r;
                for (int j1 = nzStart1, j2 = nzStart2; j1 <= i1; j1++, j2++)
                {
                    r = s1[j1] - s2[j2];
                    if (0 != r)
                    {
                        return r;
                    }
                }

                length1 = end1 - start1;
                length2 = end2 - start2;

                if (length1 == length2)
                {
                    return 0;
                }
            }

            if (length1 > length2)
            {
                return -1;
            }

            return 1;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static void ScanNumber(string s, int length, int start, ref int nzStart, ref int end)
        {
            nzStart = start;
            end = start;

            var countZeros = true;
            var c = s[end];

            while (true)
            {
                if (countZeros)
                {
                    if ('0' == c)
                    {
                        nzStart++;
                    }
                    else
                    {
                        countZeros = false;
                    }
                }

                end++;

                if (end >= length)
                {
                    break;
                }

                c = s[end];

                if (!Char.IsDigit(c))
                {
                    break;
                }
            }
        }
    }
}