//------------------------------------------------------------------------------
// <copyright file="IPAddressComparer.cs"
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

namespace Cadru.Net.NetworkInformation.Collections
{
#if !DOTNET5_1

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Compares two IP Addresses or string IP address representations for equivalence.
    /// </summary>
    public sealed class IPAddressComparer : IComparer, IEqualityComparer, IComparer<IPAddress>, IEqualityComparer<IPAddress>, IComparer<string>, IEqualityComparer<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressComparer"/> class.
        /// </summary>
        public IPAddressComparer()
        {
        }

        /// <summary>
        /// Represents an instance of <see cref="IPAddressComparer"/>.
        /// </summary>
        /// <value>The default <see cref="IPAddressComparer"/></value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer Default => new IPAddressComparer();

        /// <summary>
        /// Performs a comparison of two <see cref="IPAddress"/> objects and
        /// returns a value indicating whether one is less than, equal to or
        /// greater than the other.
        /// </summary>
        /// <param name="x">The first <see cref="IPAddress"/> to compare.</param>
        /// <param name="y">The second <see cref="IPAddress"/> to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description><paramref name="x"/> is less than <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description><paramref name="x"/> equals <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description><paramref name="x"/> is greater than <paramref name="y"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public int Compare(IPAddress x, IPAddress y)
        {
            Contracts.Requires.NotNull(x, "x");
            Contracts.Requires.NotNull(y, "y");

            var result = 0;

            var u1 = Convert(x.GetAddressBytes());
            var u2 = Convert(y.GetAddressBytes());

            if (u1 < u2)
            {
                result = -1;
            }
            else if (u1 == u2)
            {
                result = 0;
            }
            else if (u1 > u2)
            {
                result = 1;
            }

            return result;
        }

        /// <summary>
        /// Performs a comparison of two objects and returns a value indicating
        /// whether one is less than, equal to or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description><paramref name="x"/> is less than <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description><paramref name="x"/> equals <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description><paramref name="x"/> is greater than <paramref name="y"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public int Compare(object x, object y)
        {
            var left = x as string;
            var right = y as string;

            int result;
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

            return result;
        }

        /// <summary>
        /// Performs a comparison of two string objects and returns a value
        /// indicating whether one is less than, equal to or greater than the other.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description><paramref name="x"/> is less than <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description><paramref name="x"/> equals <paramref name="y"/>.</description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description><paramref name="x"/> is greater than <paramref name="y"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>The strings should be a valid date time format.</remarks>
        public int Compare(string x, string y)
        {
            Contracts.Requires.NotNull(x, "x");
            Contracts.Requires.NotNull(y, "y");

            IPAddress t1;
            IPAddress t2;

            if (!IPAddress.TryParse(x, out t1))
            {
                throw new FormatException(Resources.Strings.Format_Dns_Bad_Ip_Address);
            }

            if (!IPAddress.TryParse(y, out t2))
            {
                throw new FormatException(Resources.Strings.Format_Dns_Bad_Ip_Address);
            }

            return this.Compare(t1, t2);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of
        /// <see cref="IPAddress"/> are equal.
        /// </summary>
        /// <param name="x">The first <see cref="IPAddress"/> to compare.</param>
        /// <param name="y">The second <see cref="IPAddress"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the two <see cref="IPAddress"/> values are
        /// equal; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IPAddress x, IPAddress y)
        {
            if (x == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of string are equal.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the two string values are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(string x, string y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else
            {
                var stringComparison = String.Equals(x, y, StringComparison.OrdinalIgnoreCase);

                // First, test to see if the strings are equal.
                if (stringComparison == true)
                {
                    // The strings are equal, so now we need to test to see if
                    // they are valid IP addresses and if they are equal.
                    IPAddress t1;
                    IPAddress t2;

                    if (!IPAddress.TryParse(x, out t1) || !IPAddress.TryParse(y, out t2))
                    {
                        // At least of the strings was not a valid IP address,
                        // so return the string comparison result.
                        return stringComparison;
                    }
                    else
                    {
                        // Both of the strings were a valid IP address.
                        return t1.Equals(t2);
                    }
                }
                else
                {
                    // The string values aren't equal, so the IP Address
                    // representations can't be equal.
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="IPAddress"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IPAddress"/> for which a hash code is to be returned.
        /// </param>
        /// <returns>A hash code for the specified <see cref="IPAddress"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and
        /// <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(IPAddress obj)
        {
            Contracts.Requires.NotNull(obj, "obj");

            return obj.GetHashCode();
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The Object for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and
        /// <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(object obj)
        {
            Contracts.Requires.NotNull(obj, "obj");

            int hashCode;

            var ip1 = obj as IPAddress;

            if (ip1 != null)
            {
                return ip1.GetHashCode();
            }
            else
            {
                var s1 = obj as string;

                if (s1 == null)
                {
                    hashCode = obj.GetHashCode();
                }
                else
                {
                    IPAddress t1;

                    if (IPAddress.TryParse(s1, out t1))
                    {
                        hashCode = t1.GetHashCode();
                    }
                    else
                    {
                        hashCode = obj.GetHashCode();
                    }
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Returns a hash code for the specified string.
        /// </summary>
        /// <param name="obj">The string for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified string.</returns>
        /// <exception cref="ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and
        /// <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(string obj)
        {
            Contracts.Requires.NotNull(obj, "obj");

            int hashCode;

            if (obj.Length == 0)
            {
                hashCode = obj.GetHashCode();
            }
            else
            {
                IPAddress t1;

                if (IPAddress.TryParse(obj, out t1))
                {
                    hashCode = t1.GetHashCode();
                }
                else
                {
                    hashCode = obj.GetHashCode();
                }
            }

            return hashCode;
        }

        bool IEqualityComparer.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        private static uint Convert(byte[] bytes)
        {
            var ip = (uint)bytes[0] << 24;
            ip += (uint)bytes[1] << 16;
            ip += (uint)bytes[2] << 8;
            ip += (uint)bytes[3];

            return ip;
        }
    }

#endif
}