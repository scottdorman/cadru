//------------------------------------------------------------------------------
// <copyright file="Comb.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    using System.Runtime.InteropServices;
    using System.Text;

    using Cadru.Core.Resources;
    using Cadru.Extensions;
    using Cadru.Internal;

    /// <summary>
    /// Represents a combined globally unique identifier (GUID) and time stamp.
    /// </summary>
    /// <remarks>A COMB is a 128-bit integer (16 bytes) that can be used across
    /// all computers and networks wherever a unique identifier is required.
    /// Such an identifier has a low probability of being duplicated.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Comb : IFormattable, IComparable, IComparable<Comb>, IEquatable<Comb>
    {
        #region fields

        #region Empty
        /// <summary>
        /// A read-only instance of <see cref="Comb"/> structure whose value
        /// is all zeros.
        /// </summary>
        /// <remarks>You can compare a <see cref="Comb"/> with the value of the
        /// <see cref="Comb.Empty"/> field to determine whether a
        /// <see cref="Comb"/> is non-zero.</remarks>
        public static readonly Comb Empty = new Comb(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        #endregion

        #region MaxDate
        /// <summary>
        /// Represents the greatest possible date and time value which can be
        /// held by a <see cref="Comb"/>.
        /// </summary>
        public static readonly DateTimeOffset MaxDate = new DateTimeOffset(9999, 12, 31, 23, 59, 5, 999, TimeSpan.Zero);
        #endregion

        #region MinDate
        /// <summary>
        /// Represents the earliest possible date and time value which can be
        /// held by a <see cref="Comb"/>.
        /// </summary>
        public static readonly DateTimeOffset MinDate = new DateTimeOffset(1, 1, 1, 0, 0, 0, TimeSpan.Zero);
        #endregion

        // SQL Server is only accurate to 1/300th of a millisecond, so we need
        // to account for that accuracy limitation in our calculations.
        private const double Accuracy = 3.333333f;

        // SQL Server compares GUID values in a different order than a standard
        // .NET GUID would be compared. This array specifies the byte order used
        // for comparisons. This byte order along with which bytes are used to
        // encode the time stamp allow COMB values to be sorted by date.
        private static readonly int[] CompareOrder = new int[16] { 10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3 };

        private int a;
        private short b;
        private short c;
        private byte d;
        private byte e;
        private byte f;
        private byte g;
        private byte h;
        private byte i;
        private byte j;
        private byte k;
        private DateTimeOffset dateTime;
        #endregion

        #region events
        #endregion

        #region constructors

        #region Comb(byte[] array)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure
        /// using the specified array of bytes.
        /// </summary>
        /// <param name="array">A 16 element byte array containing values with
        /// which to initialize the <see cref="Comb"/>.</param>
        public Comb(byte[] array)
            : this()
        {
            Contracts.Requires.NotNull(array, nameof(array));
            Contracts.Requires.IsTrue(array.Length == 16);

            this.a = (array[0] << 24) | (array[1] << 16) | (array[2] << 8) | array[3];
            this.b = (short)((array[4] << 8) | array[5]);
            this.c = (short)((array[6] << 8) | array[7]);
            this.d = array[8];
            this.e = array[9];
            this.f = array[10];
            this.g = array[11];
            this.h = array[12];
            this.i = array[13];
            this.j = array[14];
            this.k = array[15];
            this.dateTime = this.GetDateTimeOffset();
        }
        #endregion

        #region Comb(int a, short b, short c, byte[] d)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure using the specified integers and bytes.
        /// </summary>
        /// <param name="a">The first 4 bytes of the <see cref="Comb"/>.</param>
        /// <param name="b">The next 2 bytes of the <see cref="Comb"/>.</param>
        /// <param name="c">The next 2 bytes of the <see cref="Comb"/>.</param>
        /// <param name="d">The remaining 8 bytes of the <see cref="Comb"/>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "d", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public Comb(int a, short b, short c, byte[] d)
             : this()
        {
            Contracts.Requires.NotNull(d, nameof(d));
            Contracts.Requires.IsTrue(d.Length == 8);

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d[0];
            this.e = d[1];
            this.f = d[2];
            this.g = d[3];
            this.h = d[4];
            this.i = d[5];
            this.j = d[6];
            this.k = d[7];
            this.dateTime = this.GetDateTimeOffset();
        }
        #endregion

        #region Comb(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure using the specified integers and bytes.
        /// </summary>
        /// <param name="a">The first 4 bytes of the <see cref="Comb"/>.</param>
        /// <param name="b">The next 2 bytes of the <see cref="Comb"/>.</param>
        /// <param name="c">The next 2 bytes of the <see cref="Comb"/>.</param>
        /// <param name="d">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="e">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="f">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="g">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="h">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="i">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="j">The next byte of the <see cref="Comb"/>.</param>
        /// <param name="k">The next byte of the <see cref="Comb"/>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "d", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "e", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "f", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "j", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "k", Justification = "Reviewed.")]
        public Comb(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
             : this()
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
            this.h = h;
            this.i = i;
            this.j = j;
            this.k = k;
            this.dateTime = this.GetDateTimeOffset();
        }
        #endregion

        #region Comb(string value)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure using
        /// the specified integers and byte array.
        /// </summary>
        /// <param name="value"><para>A string that contains a
        /// <see cref="Comb"/> in the following format:</para>
        /// <para>hexadecimal digits are arranged in groups of
        /// 8, 4, 4, 4, and 12 digits with hyphens between the
        /// groups. The <see cref="Comb"/> can optionally be
        /// enclosed in matching braces.</para>
        /// <para>For example:
        /// dddddddd-dddd-dddd-dddd-dddddddddddd or
        /// {dddddddd-dddd-dddd-dddd-dddddddddddd}.</para>
        /// <para>Alternatively, the following format is permitted:
        /// {0xdddddddd,0xdddd, 0xdddd,{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd}},
        /// where d is a hexadecimal digit. If this format is used, all brackets and commas
        /// indicated are required, and all numbers must be prefixed with "0x" as shown.
        /// Fewer hexadecimal digits than shown can be used, but not more.</para>
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public Comb(string value)
        {
            Contracts.Requires.NotNullOrEmpty(value, nameof(value));

            if (!new CombParser(value).Parse(out var guid))
            {
                throw ExceptionBuilder.CreateFormatException(value);
            }

            this = guid;
        }
        #endregion

        #endregion

        #region properties

        #region DateTime
        /// <summary>
        /// Gets the date and time represented by the current instance.
        /// </summary>
        /// <value>A <see cref="DateTimeOffset"/> containing the data and time
        /// represented by the current instance.</value>
        public DateTimeOffset DateTime => this.dateTime;
        #endregion

        #endregion

        #region operators

        #region Equality
        /// <summary>
        /// Determines whether two specified <see cref="Comb"/>
        /// objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if both objects are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(Comb left, Comb right)
        {
            return left.Equals(right);
        }
        #endregion

        #region GreaterThan
        /// <summary>
        /// Determines whether one <see cref="Comb"/> instance is greater than
        /// the other.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the first instance is greater than the second;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(Comb left, Comb right)
        {
            return Compare(left, right) == 1;
        }
        #endregion

        #region Inequality
        /// <summary>
        /// Determines whether two specified <see cref="Comb"/>
        /// objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if both objects are not equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(Comb left, Comb right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region LessThan
        /// <summary>
        /// Determines whether one <see cref="Comb"/> instance is less than
        /// the other.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the first instance is less than the second;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(Comb left, Comb right)
        {
            return Compare(left, right) == -1;
        }
        #endregion

        #endregion

        #region methods

        #region NewComb

        #region NewComb()
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure.
        /// </summary>
        /// <returns>A new <see cref="Comb"/> object.</returns>
        /// <remarks>The date and time value contained by the new
        /// <see cref="Comb"/> is the current date and time
        /// as represented by <see cref="DateTimeOffset.UtcNow"/>.</remarks>
        public static Comb NewComb()
        {
            return NewComb(DateTimeOffset.UtcNow);
        }
        #endregion

        #region NewComb(DateTime date)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure.
        /// </summary>
        /// <param name="date">A date and time value to be contained by the
        /// new <see cref="Comb"/>.</param>
        /// <returns>A new <see cref="Comb"/> object.</returns>
        public static Comb NewComb(DateTime date)
        {
            return NewComb(new DateTimeOffset(date));
        }
        #endregion

        #region NewComb(DateTimeOffset date)
        /// <summary>
        /// Initializes a new instance of the <see cref="Comb"/> structure.
        /// </summary>
        /// <param name="date">A date and time value to be contained by the
        /// new <see cref="Comb"/>.</param>
        /// <returns>A new <see cref="Comb"/> object.</returns>
        public static Comb NewComb(DateTimeOffset date)
        {
            var buffer = new byte[16];
            buffer = Guid.NewGuid().ToByteArray();

            var utc = date;
            if (!date.IsUtcDateTime())
            {
                utc = date.ToUniversalTime();
            }

            var days = TimeSpan.FromTicks(utc.UtcTicks - MinDate.UtcTicks).Days;
            var milliseconds = utc.TimeOfDay.TotalMilliseconds / Comb.Accuracy;
            var msecsArray = BitConverter.GetBytes(milliseconds);

            buffer[0] = (byte)(days >> 8);
            buffer[1] = msecsArray[1];
            buffer[4] = (byte)(days >> 16);
            buffer[5] = (byte)(days >> 24);
            buffer[6] = (byte)days;
            buffer[7] = (byte)(((byte)(days >> 8) & 0x0f) | 0x40);
            buffer[8] = (byte)((msecsArray[1] & 0x3f) | 0x80);
            buffer[9] = msecsArray[0];
            buffer[10] = msecsArray[7];
            buffer[11] = msecsArray[6];
            buffer[12] = msecsArray[5];
            buffer[13] = msecsArray[4];
            buffer[14] = msecsArray[3];
            buffer[15] = msecsArray[2];

            var comb = new Comb(buffer);
            return comb;
        }
        #endregion

        #endregion NewComb

        #region Parse
        /// <summary>
        /// Converts the string representation of a COMB to the equivalent
        /// <see cref="Comb"/> structure.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is
        /// <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="input"/> is not
        /// in a recognized format.</exception>
        /// <remarks>
        /// <para>The Parse method converts the string representation of a
        /// COMB to a <see cref="Comb"/> value. This method can convert
        /// strings in any of the five formats produced by the
        /// <see cref="ToString(String)"/> and
        /// <see cref="ToString(String, IFormatProvider)"/> methods, as shown
        /// in the following table.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Description</term>
        /// <term>Format</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description>32 digits</description>
        /// <description>00000000000000000000000000000000</description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description>32 digits separated by hyphens</description>
        /// <description>00000000-0000-0000-0000-000000000000</description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description>32 digits separated by hyphens, enclosed in
        /// braces</description>
        /// <description>{00000000-0000-0000-0000-000000000000}</description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description>32 digits separated by hyphens, enclosed in
        /// parentheses</description>
        /// <description>(00000000-0000-0000-0000-000000000000)</description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces</description>
        /// <description>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</description>
        /// </item>
        /// </list>
        /// <para>The method throws a <see cref="FormatException"/> if it is
        /// unable to successfully parse the string. Some of the reasons why
        /// this might occur include:</para>
        /// <list type="bullet">
        /// <item>
        /// <description><paramref name="input"/> contains characters that are
        /// not part of the hexadecimal character set.</description>
        /// </item>
        /// <item>
        /// <description><paramref name="input"/> has too many or too few
        /// numeric characters.</description>
        /// </item>
        /// <item>
        /// <description><paramref name="input"/> has too many or too few of
        /// the non-numeric characters appropriate for a particular format.
        /// </description>
        /// </item>
        /// <item>
        /// <description><paramref name="input"/> is not in one of the
        /// formats recognized by the <see cref="ToString()"/> method and
        /// listed in the previous table.</description>
        /// </item>
        /// </list>
        /// <para>Use the <see cref="TryParse"/> method to catch any
        /// unsuccessful parse operations without having to handle an
        /// exception.</para>
        /// </remarks>
        public static Comb Parse(string input)
        {
            Contracts.Requires.NotNull(input, nameof(input));

            if (!TryParse(input, out var guid))
            {
                throw ExceptionBuilder.CreateFormatException(input);
            }

            return guid;
        }
        #endregion

        #region ParseExact
        /// <summary>
        /// Converts the string representation of a COMB to the equivalent
        /// <see cref="Comb"/> structure, provided that the string is in the
        /// specified format.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="format">One of the following specifiers that indicates
        /// the exact format to use when interpreting input: "N", "D", "B", "P",
        /// or "X".</param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or
        /// <paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="input"/> is not
        /// in the format specified by <paramref name="format"/>.</exception>
        /// <remarks>
        /// <para>The following table shows the accepted format specifiers
        /// for the <paramref name="format"/> parameter. "0" represents a
        /// digit; hyphens ("-"), braces ("{", "}"), and parentheses
        /// ("(", ")") appear as shown.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Format of return value</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description><para>32 digits:</para>
        /// <para>00000000000000000000000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description><para>32 digits separated by hyphens:</para>
        /// <para>00000000-0000-0000-0000-000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// braces:</para>
        /// <para>{00000000-0000-0000-0000-000000000000}</para></description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// parentheses:</para>
        /// <para>(00000000-0000-0000-0000-000000000000)</para></description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description><para>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces:</para>
        /// <para>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static Comb ParseExact(string input, string format)
        {
            Contracts.Requires.NotNull(input, nameof(input));
            Contracts.Requires.NotNull(format, nameof(format));

            if (!TryParseExact(input, format, out var guid))
            {
                throw ExceptionBuilder.CreateFormatException(input);
            }

            return guid;
        }
        #endregion

        #region TryParse
        /// <summary>
        /// Converts the string representation of a COMB to the equivalent
        /// <see cref="Comb"/> structure.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="result">The structure that will contain the parsed
        /// value. If the method returns <see langword="true"/>,
        /// <paramref name="result"/> contains a valid <see cref="Comb"/>.
        /// If the method returns <see langword="false"/>,
        /// <paramref name="result"/> equals <see cref="Comb.Empty"/>.</param>
        /// <returns><see langword="true"/> if the parse operation was
        /// successful; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// <para>This method is like the <see cref="Parse"/> method, except
        /// that instead of returning the parsed COMB, it returns
        /// <see langword="false"/> if <paramref name="input"/> is
        /// <see langword="null"/> or not in a recognized format and doesn't
        /// throw an exception. It converts strings in any of the five formats
        /// produced by the <see cref="ToString(String)"/> and
        /// <see cref="ToString(String, IFormatProvider)"/> methods, as shown
        /// in the following table.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Description</term>
        /// <term>Format</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description>32 digits</description>
        /// <description>00000000000000000000000000000000</description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description>32 digits separated by hyphens</description>
        /// <description>00000000-0000-0000-0000-000000000000</description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description>32 digits separated by hyphens, enclosed in
        /// braces</description>
        /// <description>{00000000-0000-0000-0000-000000000000}</description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description>32 digits separated by hyphens, enclosed in
        /// parentheses</description>
        /// <description>(00000000-0000-0000-0000-000000000000)</description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces</description>
        /// <description>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</description>
        /// </item>
        /// </list>
        /// </remarks>
        public static bool TryParse(string input, out Comb result)
        {
            if (input == null)
            {
                result = Empty;
                return false;
            }

            var parser = new CombParser(input);
            return parser.Parse(out result);
        }
        #endregion

        #region TryParseExact
        /// <summary>
        /// Converts the string representation of a COMB to the equivalent
        /// <see cref="Comb"/> structure provided that the string is in the
        /// specified format.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="format">One of the following specifiers that indicates
        /// the exact format to use when interpreting input: "N", "D", "B", "P",
        /// or "X".</param>
        /// <param name="result">The structure that will contain the parsed
        /// value. If the method returns <see langword="true"/>,
        /// <paramref name="result"/> contains a valid <see cref="Comb"/>.
        /// If the method returns <see langword="false"/>,
        /// <paramref name="result"/> equals <see cref="Comb.Empty"/>.</param>
        /// <returns><see langword="true"/> if the parse operation was
        /// successful; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// <para>This method returns <see langword="false"/> if
        /// <paramref name="input"/> is <see langword="null"/> or not in a
        /// recognized format and doesn't throw an exception.</para>
        /// <para>The following table shows the accepted format specifiers
        /// for the <paramref name="format"/> parameter. "0" represents a
        /// digit; hyphens ("-"), braces ("{", "}"), and parentheses
        /// ("(", ")") appear as shown.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Format of return value</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description><para>32 digits:</para>
        /// <para>00000000000000000000000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description><para>32 digits separated by hyphens:</para>
        /// <para>00000000-0000-0000-0000-000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// braces:</para>
        /// <para>{00000000-0000-0000-0000-000000000000}</para></description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// parentheses:</para>
        /// <para>(00000000-0000-0000-0000-000000000000)</para></description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description><para>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces:</para>
        /// <para>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public static bool TryParseExact(string input, string format, out Comb result)
        {
            if (input == null || format == null)
            {
                result = Empty;
                return false;
            }

            var parser = new CombParser(input);
            return parser.Parse(ParseFormat(format), out result);
        }
        #endregion

        #region CompareTo

        #region CompareTo(object obj)
        /// <summary>
        /// Compares this instance to a specified object and returns an
        /// indication of their relative values.
        /// </summary>
        /// <param name="obj">A boxed object to compare, or <see langword="null"/>.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the
        /// <paramref name="obj"/> parameter.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// This instance is less than <paramref name="obj"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// This instance is the same as <paramref name="obj"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// This instance is greater than <paramref name="obj"/>,
        /// or <paramref name="obj"/> is <see langword="null"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="obj"/> is not a <see cref="Comb"/>.
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj is Comb)
            {
                return this.CompareTo((Comb)obj);
            }

            throw new ArgumentException(Strings.Arg_MustBeSequentialGuid);
        }
        #endregion

        #region CompareTo(Comb other)
        /// <summary>
        /// Compares the value of this instance to a specified
        /// <see cref="UnixTimestamp"/> value and returns an integer that
        /// indicates whether this instance is earlier than, the same as, or
        /// later than the specified <see cref="UnixTimestamp"/> value.
        /// </summary>
        /// <param name="other">The object to compare to the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the
        /// <paramref name="other"/> parameter.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// This instance is earlier than <paramref name="other"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// This instance is the same as <paramref name="other"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// This instance is later than <paramref name="other"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(Comb other)
        {
            return Compare(this, other);
        }
        #endregion

        #endregion CompareTo

        #region Equals

        #region Equals(object obj)
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a
        /// <see cref="Comb"/> and has the same value as this instance;
        /// otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Comb))
            {
                return false;
            }
            else
            {
                var comb = (Comb)obj;
                return this.Equals(comb);
            }
        }
        #endregion

        #region Equals(Comb other)
        /// <summary>
        /// Returns a value indicating whether this instance and a specified
        /// <see cref="Comb"/> object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="other"/> has the
        /// same value as this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Comb other)
        {
            return Compare(this, other) == 0;
        }
        #endregion

        #endregion Equals

        #region GetHashCode
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.a ^ ((this.b << 16) | (ushort)this.c) ^ ((this.f << 24) | this.k);
        }
        #endregion

        #region ToString

        #region ToString()
        /// <summary>
        /// Returns a string representation of the value of this instance in
        /// registry format.
        /// </summary>
        /// <returns><para>The value of this <see cref="Comb"/>, formatted by
        /// using the "D" format specifier as follows:</para>
        /// <para>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</para>
        /// <para>where the value of the GUID is represented as a series of
        /// lowercase hexadecimal digits in groups of 8, 4, 4, 4, and 12 digits
        /// and separated by hyphens. An example of a return value is
        /// "382c74c3-721d-4f34-80e5-57657b6cbc27". To convert the hexadecimal
        /// digits from a through f to uppercase, call the
        /// <see cref="String.ToUpper"/> method on the returned string.</para>
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public override string ToString()
        {
            return this.ToString("D", null);
        }
        #endregion

        #region ToString(string format)
        /// <summary>
        /// Returns a string representation of the value of this
        /// <see cref="Comb"/> instance, according to the provided
        /// format specifier.
        /// </summary>
        /// <param name="format">A single format specifier that
        /// indicates how to format the value of this <see cref="Comb"/>.
        /// The format parameter can be "N", "D", "B", "P", or "X".
        /// If <paramref name="format"/> is <see langword="null"/> or an empty
        /// string (""), "D" is used.</param>
        /// <returns>The value of this <see cref="Comb"/>, represented
        /// as a series of lowercase hexadecimal digits in the specified
        /// format.</returns>
        /// <exception cref="System.FormatException">The value of
        /// <paramref name="format"/> is not <see langword="null"/>, an empty
        /// string (""), "N", "D", "B", "P", or "X".
        /// </exception>
        /// <remarks>The following table shows the accepted format specifiers
        /// for the <paramref name="format"/> parameter. "0" represents a
        /// digit; hyphens ("-"), braces ("{", "}"), and parentheses
        /// ("(", ")") appear as shown.
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Format of return value</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description><para>32 digits:</para>
        /// <para>00000000000000000000000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description><para>32 digits separated by hyphens:</para>
        /// <para>00000000-0000-0000-0000-000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// braces:</para>
        /// <para>{00000000-0000-0000-0000-000000000000}</para></description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// parentheses:</para>
        /// <para>(00000000-0000-0000-0000-000000000000)</para></description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description><para>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces:</para>
        /// <para>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</para></description>
        /// </item>
        /// </list>
        /// <para>The hexadecimal digits a through f are lowercase in the
        /// returned string. To convert them to uppercase, call the
        /// <see cref="String.ToUpper"/> method on the returned string.</para>
        /// </remarks>
        public string ToString(string format)
        {
            StringBuilder res = null;
            if (format.IsNullOrEmpty())
            {
                format = "D";
            }

            switch (format)
            {
                case "B":
                    res = new StringBuilder(38)
                        .Append('{')
                        .AppendAsHexadecimal(this.a)
                        .Append('-')
                        .AppendAsHexadecimal(this.b)
                        .Append('-')
                        .AppendAsHexadecimal(this.c)
                        .Append('-')
                        .AppendAsHexadecimal(this.d)
                        .AppendAsHexadecimal(this.e)
                        .Append('-')
                        .AppendAsHexadecimal(this.f)
                        .AppendAsHexadecimal(this.g)
                        .AppendAsHexadecimal(this.h)
                        .AppendAsHexadecimal(this.i)
                        .AppendAsHexadecimal(this.j)
                        .AppendAsHexadecimal(this.k)
                        .Append('}');
                    break;

                case "P":
                    res = new StringBuilder(38)
                        .Append('(')
                        .AppendAsHexadecimal(this.a)
                        .Append('-')
                        .AppendAsHexadecimal(this.b)
                        .Append('-')
                        .AppendAsHexadecimal(this.c)
                        .Append('-')
                        .AppendAsHexadecimal(this.d)
                        .AppendAsHexadecimal(this.e)
                        .Append('-')
                        .AppendAsHexadecimal(this.f)
                        .AppendAsHexadecimal(this.g)
                        .AppendAsHexadecimal(this.h)
                        .AppendAsHexadecimal(this.i)
                        .AppendAsHexadecimal(this.j)
                        .AppendAsHexadecimal(this.k)
                        .Append(')');
                    break;

                case "D":
                    res = new StringBuilder(36)
                        .AppendAsHexadecimal(this.a)
                        .Append('-')
                        .AppendAsHexadecimal(this.b)
                        .Append('-')
                        .AppendAsHexadecimal(this.c)
                        .Append('-')
                        .AppendAsHexadecimal(this.d)
                        .AppendAsHexadecimal(this.e)
                        .Append('-')
                        .AppendAsHexadecimal(this.f)
                        .AppendAsHexadecimal(this.g)
                        .AppendAsHexadecimal(this.h)
                        .AppendAsHexadecimal(this.i)
                        .AppendAsHexadecimal(this.j)
                        .AppendAsHexadecimal(this.k);
                    break;

                case "N":
                    res = new StringBuilder(32)
                        .AppendAsHexadecimal(this.a)
                        .AppendAsHexadecimal(this.b)
                        .AppendAsHexadecimal(this.c)
                        .AppendAsHexadecimal(this.d)
                        .AppendAsHexadecimal(this.e)
                        .AppendAsHexadecimal(this.f)
                        .AppendAsHexadecimal(this.g)
                        .AppendAsHexadecimal(this.h)
                        .AppendAsHexadecimal(this.i)
                        .AppendAsHexadecimal(this.j)
                        .AppendAsHexadecimal(this.k);
                    break;

                case "X":
                    res = new StringBuilder(68)
                        .Append(new[] { '{', '0', 'x' })
                        .AppendAsHexadecimal(this.a)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.b)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.c)
                        .Append(new[] { ',', '{', '0', 'x' })
                        .AppendAsHexadecimal(this.d)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.e)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.f)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.g)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.h)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.i)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.j)
                        .Append(new[] { ',', '0', 'x' })
                        .AppendAsHexadecimal(this.k)
                        .Append(new[] { '}', '}' });
                    break;

                default:
                    throw new NotImplementedException(Strings.Format_InvalidGuidFormatSpecification);
            }

            return res.ToString();
        }
        #endregion

        #region ToString(string format, IFormatProvider formatProvider)
        /// <summary>
        /// Returns a string representation of the value of this
        /// <see cref="Comb"/> instance, according to the provided
        /// format specifier and culture-specific format information.
        /// </summary>
        /// <param name="format">A single format specifier that
        /// indicates how to format the value of this <see cref="Comb"/>.
        /// The format parameter can be "N", "D", "B", "P", or "X".
        /// If <paramref name="format"/> is <see langword="null"/> or an empty
        /// string (""), "D" is used.</param>
        /// <param name="formatProvider">(Reserved) An object that supplies
        /// culture-specific formatting information.</param>
        /// <returns>The value of this <see cref="Comb"/>, represented
        /// as a series of lowercase hexadecimal digits in the specified
        /// format.</returns>
        /// <exception cref="System.FormatException">The value of
        /// <paramref name="format"/> is not <see langword="null"/>, an empty
        /// string (""), "N", "D", "B", "P", or "X".
        /// </exception>
        /// <remarks>
        /// <para>The <paramref name="formatProvider"/> parameter is reserved for
        /// future use and does not contribute to the execution of this
        /// method. You can pass <see langword="null"/> in the method call.</para>
        /// <para>The following table shows the accepted format specifiers
        /// for the <paramref name="format"/> parameter. "0" represents a
        /// digit; hyphens ("-"), braces ("{", "}"), and parentheses
        /// ("(", ")") appear as shown.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Specifier</term>
        /// <term>Format of return value</term>
        /// </listheader>
        /// <item>
        /// <term>N</term>
        /// <description><para>32 digits:</para>
        /// <para>00000000000000000000000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>D</term>
        /// <description><para>32 digits separated by hyphens:</para>
        /// <para>00000000-0000-0000-0000-000000000000</para></description>
        /// </item>
        /// <item>
        /// <term>B</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// braces:</para>
        /// <para>{00000000-0000-0000-0000-000000000000}</para></description>
        /// </item>
        /// <item>
        /// <term>P</term>
        /// <description><para>32 digits separated by hyphens, enclosed in
        /// parentheses:</para>
        /// <para>(00000000-0000-0000-0000-000000000000)</para></description>
        /// </item>
        /// <item>
        /// <term>X</term>
        /// <description><para>Four hexadecimal values enclosed in braces,
        /// where the fourth value is a subset of eight hexadecimal values that
        /// is also enclosed in braces:</para>
        /// <para>{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</para></description>
        /// </item>
        /// </list>
        /// <para>The hexadecimal digits a through f are lowercase in the
        /// returned string. To convert them to uppercase, call the
        /// <see cref="String.ToUpper"/> method on the returned string.</para>
        /// <para>Because the <paramref name="formatProvider"/> parameter is ignored,
        /// you cannot use it to provide a custom formatting solution. To
        /// represent a <see cref="Comb"/> value as a string in a format that
        /// isn't supported by the standard COMB format strings, call the
        /// <see cref="String.Format(IFormatProvider, String, Object[])"/>
        /// method with a provider object that implements both the
        /// <see cref="ICustomFormatter"/> and <see cref="IFormatProvider"/>
        /// interfaces. For more information, see the "Custom Formatting with
        /// ICustomFormatter" section in the
        /// <see href="http://msdn.microsoft.com/en-us/library/26etazsy(v=vs.110).aspx">Formatting Types</see>
        /// article.</para>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "Cadru.Comb.ToString(System.String)", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter", Justification = "Reviewed.")]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString(format);
        }
        #endregion

        #endregion ToString

        #region ToByteArray
        /// <summary>
        /// Returns a 16-element byte array that contains the value of the <see cref="Comb"/>.
        /// </summary>
        /// <returns>A 16-element byte array.</returns>
        public byte[] ToByteArray()
        {
            var buffer = new byte[16];
            buffer[0] = (byte)(this.a >> 24);
            buffer[1] = (byte)(this.a >> 16);
            buffer[2] = (byte)(this.a >> 8);
            buffer[3] = (byte)this.a;
            buffer[4] = (byte)(this.b >> 8);
            buffer[5] = (byte)this.b;
            buffer[6] = (byte)(this.c >> 8);
            buffer[7] = (byte)this.c;
            buffer[8] = this.d;
            buffer[9] = this.e;
            buffer[10] = this.f;
            buffer[11] = this.g;
            buffer[12] = this.h;
            buffer[13] = this.i;
            buffer[14] = this.j;
            buffer[15] = this.k;

            return buffer;
        }
        #endregion ToByteArray

        #region Compare
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        private static int Compare(Comb x, Comb y)
        {
            var xBuffer = x.ToByteArray();
            var yBuffer = y.ToByteArray();

            // Swap to the correct order to be compared
            for (var i = 0; i < 16; i++)
            {
                byte b1, b2;

                b1 = xBuffer[CompareOrder[i]];
                b2 = yBuffer[CompareOrder[i]];
                if (b1 != b2)
                {
                    return (b1 < b2) ? -1 : 1;
                }
            }

            return 0;
        }
        #endregion

        #region ParseFormat
        private static string ParseFormat(string format)
        {
            if (String.IsNullOrEmpty(format))
            {
                return "D";
            }

            switch (format[0])
            {
                case 'N':
                case 'n':
                    return "N";

                case 'D':
                case 'd':
                    return "D";

                case 'B':
                case 'b':
                    return "B";

                case 'P':
                case 'p':
                    return "P";

                case 'X':
                case 'x':
                    return "X";
            }

            throw new FormatException(Strings.Format_InvalidGuidFormatSpecification);
        }
        #endregion

        #region GetDateTimeOffset()
        private DateTimeOffset GetDateTimeOffset()
        {
            var buffer = this.ToByteArray();
            var msecsArray = new byte[]
            {
               buffer[9],
               buffer[1],
               buffer[15],
               buffer[14],
               buffer[13],
               buffer[12],
               buffer[11],
               buffer[10]
            };

            var days = buffer[6] + (buffer[0] << 8) + (buffer[4] << 16) + (buffer[5] << 24);
            var msecs = BitConverter.ToDouble(msecsArray, 0);
            var date = Comb.MinDate.AddDays(days).AddMilliseconds(msecs * Comb.Accuracy);
            return date;
        }
        #endregion

        #endregion methods
    }
}