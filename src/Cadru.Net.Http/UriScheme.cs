﻿//------------------------------------------------------------------------------
// <copyright file="UriScheme.cs"
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

namespace Cadru.Net.Http
{
    using System;
    using Cadru.Contracts;

    /// <summary>
    /// A helper class for retrieving and comparing standard URI schemes.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
    public class UriScheme : IEquatable<UriScheme>
    {
        #region fields
        private static readonly UriScheme fileScheme = new UriScheme("file");
        private static readonly UriScheme ftpScheme = new UriScheme("ftp");
        private static readonly UriScheme gopherScheme = new UriScheme("gopher");
        private static readonly UriScheme httpScheme = new UriScheme("http");
        private static readonly UriScheme httpsScheme = new UriScheme("https");
        private static readonly UriScheme mailtoScheme = new UriScheme("mailto");
        private static readonly UriScheme newsScheme = new UriScheme("news");
        private string scheme;
        #endregion

        #region constructors
        internal UriScheme(string scheme)
        {
            Requires.NotNullOrEmpty(scheme, "scheme");
            this.scheme = scheme;
        }
        #endregion

        #region events
        #endregion

        #region properties

        #region File
        /// <summary>
        /// Represents a URI scheme for a resource which is a file on the
        /// local computer.
        /// </summary>
        /// <value>A URI scheme for a resource which is a file on the local
        /// computer.</value>
        public static UriScheme File
        {
            get
            {
                return fileScheme;
            }
        }
        #endregion

        #region Ftp
        /// <summary>
        /// Represents a URI scheme for a resource which is accessed through
        /// FTP.
        /// </summary>
        /// <value>A URI scheme for a resource which is accessed through
        /// FTP.</value>
        public static UriScheme Ftp
        {
            get
            {
                return ftpScheme;
            }
        }
        #endregion

        #region Gopher
        /// <summary>
        /// Represents a URI scheme for a resource which is accessed through
        /// the Gopher protocol.
        /// </summary>
        /// <value>A URI scheme for a resource which is accessed through
        /// the Gopher protocol.</value>
        public static UriScheme Gopher
        {
            get
            {
                return gopherScheme;
            }
        }
        #endregion

        #region Http
        /// <summary>
        /// Represents a URI scheme for a resource which is accessed through
        /// HTTP.
        /// </summary>
        /// <value>A URI scheme for a resource which is accessed through
        /// HTTP.</value>
        public static UriScheme Http
        {
            get
            {
                return httpScheme;
            }
        }
        #endregion

        #region Https
        /// <summary>
        /// Represents a URI scheme for a resource which is accessed through
        /// SSL-encrypted HTTP.
        /// </summary>
        /// <value>A URI scheme for a resource which is accessed through
        /// SSL-encrypted HTTP.</value>
        public static UriScheme Https
        {
            get
            {
                return httpsScheme;
            }
        }
        #endregion

        #region MailTo
        /// <summary>
        /// Represents a URI scheme for a resource which is an e-mail address
        /// and is access through SMTP.
        /// </summary>
        /// <value>A URI scheme for a resource which is an e-mail address and
        /// is accessed through SMTP.</value>
        public static UriScheme MailTo
        {
            get
            {
                return mailtoScheme;
            }
        }
        #endregion

        #region News
        /// <summary>
        /// Represents a URI scheme for a resource which is accessed through
        /// NNTP.
        /// </summary>
        /// <value>A URI scheme for a resource which is accessed through
        /// NNTP.</value>
        public static UriScheme News
        {
            get
            {
                return newsScheme;
            }
        }
        #endregion

        #endregion

        #region operators

        #region Implicit(UriScheme to string)
        /// <summary>
        /// Defines an implicit conversion from <see cref="UriScheme"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>
        /// The converted object.
        /// </returns>
        public static implicit operator string(UriScheme value)
        {
            Requires.NotNull(value, "value");

            return value.scheme;
        }
        #endregion

        #region Equality
        /// <summary>
        /// Determines whether two specified <see cref="UriScheme"/>
        /// objects represent the same scheme.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if both objects represent the same scheme;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(UriScheme left, UriScheme right)
        {
            if ((object)left == null || (object)right == null)
            {
                return Object.Equals(left, right);
            }

            return left.Equals(right);
        }
        #endregion

        #region Inequality
        /// <summary>
        /// Determines whether two specified <see cref="UriScheme"/>
        /// objects represent different schemes.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> and
        /// <paramref name="right"/> do not represent the same scheme;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(UriScheme left, UriScheme right)
        {
            return !(left == right);
        }
        #endregion

        #endregion

        #region methods

        #region Equals

        #region Equals(object obj)
        /// <summary>
        /// Returns a value indicating whether the value of this instance is
        /// equal to the value of the specified <see cref="UriScheme"/>
        /// instance.
        /// </summary>
        /// <param name="obj">The object to compare to this
        /// instance.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="obj"/> parameter
        /// equals the value of this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as UriScheme);
        }
        #endregion

        #region Equals(UriScheme other)
        /// <summary>
        /// Returns a value indicating whether the value of this instance is
        /// equal to the value of the specified <see cref="UriScheme"/>
        /// instance.
        /// </summary>
        /// <param name="other">The <see cref="UriScheme"/> to compare to this
        /// instance.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="other"/> parameter
        /// equals the value of this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(UriScheme other)
        {
            if (other == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this.scheme, other.scheme))
            {
                return true;
            }

            return String.Compare(this.scheme, other.scheme, StringComparison.OrdinalIgnoreCase) == 0;
        }
        #endregion

        #endregion

        #region GetHashCode
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return this.scheme.ToUpperInvariant().GetHashCode();
        }
        #endregion

        #region ToString
        /// <summary>
        /// Converts the value of the current <see cref="UriScheme"/>
        /// object to its equivalent string representation.
        /// </summary>
        /// <returns>
        /// A string representation of the value of the current <see cref="UriScheme"/> object.
        /// </returns>
        public override string ToString()
        {
            return this.scheme.ToString();
        }
        #endregion

        #endregion
    }
}
