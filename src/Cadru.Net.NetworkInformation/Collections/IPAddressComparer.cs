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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using Validation;

namespace Cadru.Net.NetworkInformation.Collections
{
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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer Default => new IPAddressComparer();

        /// <inheritdoc/>
        public int Compare(IPAddress x, IPAddress y)
        {
            Requires.NotNull(x, "x");
            Requires.NotNull(y, "y");

            int result;

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
            else
            {
                result = 1;
            }

            return result;
        }

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
        /// <remarks>The strings should be a valid date time format.</remarks>
        public int Compare(string x, string y)
        {
            Requires.NotNull(x, "x");
            Requires.NotNull(y, "y");


            if (!IPAddress.TryParse(x, out var t1))
            {
                throw new FormatException(Resources.Strings.Format_Dns_Bad_Ip_Address);
            }

            if (!IPAddress.TryParse(y, out var t2))
            {
                throw new FormatException(Resources.Strings.Format_Dns_Bad_Ip_Address);
            }

            return this.Compare(t1, t2);
        }

        /// <inheritdoc/>
        public bool Equals(IPAddress x, IPAddress y)
        {
            if (x == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <inheritdoc/>
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
                if (stringComparison)
                {
                    // The strings are equal, so now we need to test to see if
                    // they are valid IP addresses and if they are equal.
                    if (!IPAddress.TryParse(x, out var t1) || !IPAddress.TryParse(y, out var t2))
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

        /// <inheritdoc/>
        public int GetHashCode(IPAddress obj)
        {
            Requires.NotNull(obj, "obj");

            return obj.GetHashCode();
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
        {
            Requires.NotNull(obj, "obj");

            int hashCode;


            if (obj is IPAddress ip1)
            {
                return ip1.GetHashCode();
            }
            else
            {
                if (!(obj is string s1))
                {
                    hashCode = obj.GetHashCode();
                }
                else
                {
                    if (IPAddress.TryParse(s1, out var t1))
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

        /// <inheritdoc/>
        public int GetHashCode(string obj)
        {
            Requires.NotNull(obj, "obj");

            int hashCode;

            if (obj.Length == 0)
            {
                hashCode = obj.GetHashCode();
            }
            else
            {
                if (IPAddress.TryParse(obj, out var t1))
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

        /// <inheritdoc/>
        bool IEqualityComparer.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        private static uint Convert(byte[] bytes)
        {
            var ip = (uint)bytes[0] << 24;
            ip += (uint)bytes[1] << 16;
            ip += (uint)bytes[2] << 8;
            ip += bytes[3];

            return ip;
        }
    }
}