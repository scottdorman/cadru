﻿//------------------------------------------------------------------------------
// <copyright file="UrlBuilder.cs"
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
using System.Diagnostics.CodeAnalysis;

using Cadru.Net.Http.Collections;

using Validation;

namespace Cadru.Net.Http
{
    /// <summary>
    /// Provides a custom constructor for uniform resource identifiers (URIs)
    /// and modifies URIs for the <see cref="System.Uri"/> class.
    /// </summary>
    public class UrlBuilder
    {
        private readonly UriBuilder builder;
        private readonly QueryStringParametersDictionary queryParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class.
        /// </summary>
        public UrlBuilder()
        {
            this.builder = new UriBuilder();
            this.queryParameters = new QueryStringParametersDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified URI.
        /// </summary>
        /// <param name="uri">A URI string.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="uri"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para>
        /// <paramref name="uri"/> is a zero length string or contains only spaces.
        /// </para>
        /// <para>-or-</para>
        /// <para>The parsing routine detected a scheme in an invalid form.</para>
        /// <para>-or-</para>
        /// <para>
        /// The parser detected more than two consecutive slashes in a URI that
        /// does not use the "file" scheme.
        /// </para>
        /// <para>-or-</para>
        /// <para>paramref name="uri"/&gt; is not a valid URI.</para>
        /// </exception>
        public UrlBuilder(string uri)
        {
            this.builder = new UriBuilder(uri);
            this.queryParameters = new QueryStringParametersDictionary(this.builder.Query);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified URI and path.
        /// </summary>
        /// <param name="uri">A URI string.</param>
        /// <param name="path">The path to the Internet resource.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="uri"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para>
        /// <paramref name="uri"/> is a zero length string or contains only spaces.
        /// </para>
        /// <para>-or-</para>
        /// <para>The parsing routine detected a scheme in an invalid form.</para>
        /// <para>-or-</para>
        /// <para>
        /// The parser detected more than two consecutive slashes in a URI that
        /// does not use the "file" scheme.
        /// </para>
        /// <para>-or-</para>
        /// <para>paramref name="uri"/&gt; is not a valid URI.</para>
        /// </exception>
        public UrlBuilder(string uri, string path)
        {
            this.builder = new UriBuilder(uri);
            this.queryParameters = new QueryStringParametersDictionary(this.builder.Query);
            this.builder.Path = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified <see cref="System.Uri"/> instance.
        /// </summary>
        /// <param name="uri">An instance of the <see cref="System.Uri"/> class.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="uri"/> is <see langword="null"/>.
        /// </exception>
        public UrlBuilder(Uri uri)
        {
            Requires.NotNull(uri, "uri");

            this.builder = new UriBuilder(uri);
            this.queryParameters = new QueryStringParametersDictionary(uri.Query);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified <see cref="System.Uri"/> instance.
        /// </summary>
        /// <param name="uri">An instance of the <see cref="System.Uri"/> class.</param>
        /// <param name="path">The path to the Internet resource.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="uri"/> is <see langword="null"/>.
        /// </exception>
        public UrlBuilder(Uri uri, string path)
        {
            Requires.NotNull(uri, "uri");

            this.builder = new UriBuilder(uri);
            this.queryParameters = new QueryStringParametersDictionary(uri.Query);
            this.builder.Path = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified scheme and host.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        public UrlBuilder(UriScheme scheme, string host)
        {
            this.builder = new UriBuilder(scheme, host);
            this.queryParameters = new QueryStringParametersDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified scheme and host.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="port"/> is less than -1 or greater than 65,535.
        /// </exception>
        public UrlBuilder(UriScheme scheme, string host, int port)
        {
            this.builder = new UriBuilder(scheme, host, port);
            this.queryParameters = new QueryStringParametersDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified scheme and host.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <param name="path">The path to the Internet resource.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="port"/> is less than -1 or greater than 65,535.
        /// </exception>
        public UrlBuilder(UriScheme scheme, string host, int port, string path)
        {
            this.builder = new UriBuilder(scheme, host, port, path);
            this.queryParameters = new QueryStringParametersDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class
        /// with the specified scheme and host.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <param name="path">The path to the Internet resource.</param>
        /// <param name="extraValue">A query string or fragment identifier.</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="extraValue"/> is neither <see langword="null"/> nor
        /// <see cref="System.String.Empty">System.String.Empty</see>, nor does
        /// a valid fragment identifier begin with a number sign (#), nor a
        /// valid query string begin with a question mark (?).
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="port"/> is less than -1 or greater than 65,535.
        /// </exception>
        public UrlBuilder(UriScheme scheme, string host, int port, string path, string extraValue)
        {
            this.builder = new UriBuilder(scheme, host, port, path, extraValue);
            this.queryParameters = new QueryStringParametersDictionary(this.builder.Query);
        }

        /// <summary>
        /// Gets or sets the fragment portion of the URI.
        /// </summary>
        /// <value>
        /// The fragment portion of the URI. The fragment identifier ("#") is
        /// added to the beginning of the fragment.
        /// </value>
        public string Fragment
        {
            get => this.builder.Fragment;
            set => this.builder.Fragment = value;
        }

        /// <summary>
        /// Gets or sets the Domain Name System (DNS) host name or IP address of
        /// a server.
        /// </summary>
        /// <value>The DNS host name or IP address of the server.</value>
        public string Host
        {
            get => this.builder.Host;
            set => this.builder.Host = value;
        }

        /// <summary>
        /// Gets or sets the password associated with the user that accesses the URI.
        /// </summary>
        /// <value>The password of the user that accesses the URI.</value>
        public string Password
        {
            get => this.builder.Password;
            set => this.builder.Password = value;
        }

        /// <summary>
        /// Gets or sets the path to the resource referenced by the URI.
        /// </summary>
        /// <value>The path to the resource referenced by the URI.</value>
        public string Path
        {
            get => this.builder.Path;
            set => this.builder.Path = value;
        }

        /// <summary>
        /// Gets or sets the port number of the URI.
        /// </summary>
        /// <value>The port number of the URI.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The port cannot be set to a value less than -1 or greater than 65,535.
        /// </exception>
        public int Port
        {
            get => this.builder.Port;
            set => this.builder.Port = value;
        }

        /// <summary>
        /// Gets or sets any query information included in the URI.
        /// </summary>
        /// <value>The query information included in the URI.</value>
        public string Query
        {
            get => this.queryParameters.ToQueryString();
            set => this.queryParameters.FillFromString(value);
        }

        /// <summary>
        /// Gets the collection of query parameters to be included in the URI.
        /// </summary>
        /// <value>
        /// The collection of query parameters to be included in the URI.
        /// </value>
        public QueryStringParametersDictionary QueryParameters => this.queryParameters;

        /// <summary>
        /// Gets or sets the scheme name of the URI.
        /// </summary>
        /// <value>The scheme of the URI.</value>
        /// <exception cref="System.ArgumentException">
        /// The scheme cannot be set to an invalid scheme name.
        /// </exception>
        public UriScheme Scheme
        {
            get => new UriScheme(this.builder.Scheme);

            set => this.builder.Scheme = value;
        }

        /// <summary>
        /// Gets the <see cref="System.Uri"/> instance constructed by the
        /// specified <see cref="UrlBuilder"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="System.Uri"/> that contains the URI constructed by the <see cref="UrlBuilder"/>.
        /// </value>
        /// <exception cref="System.FormatException">
        /// <para>
        /// The URI constructed by the <see cref="UrlBuilder"/> properties is invalid.
        /// </para>
        /// </exception>
        public Uri Uri
        {
            get
            {
                this.builder.Query = this.Query;
                return this.builder.Uri;
            }
        }

        /// <summary>
        /// Gets or sets the user name associated with the user that accesses
        /// the URI.
        /// </summary>
        /// <value>The user name of the user that accesses the URI.</value>
        public string UserName
        {
            get => this.builder.UserName;

            set => this.builder.UserName = value;
        }

        /// <summary>
        /// Compares an existing <see cref="System.Uri"/> instance with the
        /// contents of the <see cref="UrlBuilder"/> for equality.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> represents the same
        /// <see cref="System.Uri"/> as the <see cref="System.Uri"/> constructed
        /// by this <see cref="UrlBuilder"/> instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            this.builder.Query = this.Query;
            return this.builder.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code for the URI.
        /// </summary>
        /// <returns>The hash code generated for the URI.</returns>
        public override int GetHashCode()
        {
            this.builder.Query = this.Query;
            return this.builder.GetHashCode();
        }

        /// <summary>
        /// Returns the display string for the specified
        /// <see cref="UrlBuilder"/> instance.
        /// </summary>
        /// <returns>
        /// The string that contains the unescaped display string of the <see cref="UrlBuilder"/>.
        /// </returns>
        /// <exception cref="System.FormatException">
        /// <para>The <see cref="UrlBuilder"/> instance has a bad password.</para>
        /// </exception>
        [System.Diagnostics.DebuggerStepThrough]
        public override string ToString()
        {
            this.builder.Query = this.Query;
            return this.builder.ToString();
        }
    }
}