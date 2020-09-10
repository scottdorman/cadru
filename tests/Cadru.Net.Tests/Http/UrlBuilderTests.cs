//------------------------------------------------------------------------------
// <copyright file="UrlBuilderTests.cs"
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
using System.Linq;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Net.Http.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UrlBuilderTests
    {
        [TestMethod]
        public void BasicConstructors()
        {
            var builder = new UrlBuilder();
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("localhost", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(-1, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder("http://example.com");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(80, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder("http://example.com:8080");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(8080, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder(new Uri("http://example.com"));
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(80, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);
        }

        [TestMethod]
        public void Equality()
        {
            var builder1 = new UrlBuilder();
            var builder2 = new UrlBuilder();
            var builder3 = new UrlBuilder("example.com");
            var builder4 = new UrlBuilder("example.com");

            Assert.AreEqual(builder1, builder2);
            Assert.AreNotEqual(builder1, builder3);
            Assert.AreEqual(builder3, builder4);
        }

        [TestMethod]
        public void GetHashCodeTests()
        {
            var builder = new UrlBuilder();
            var uri = new UriBuilder();
            Assert.AreEqual(uri.GetHashCode(), builder.GetHashCode());
        }

        [TestMethod]
        public void PathConstructors()
        {
            var builder = new UrlBuilder("http://example.com", "foo");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("foo", builder.Path);
            Assert.AreEqual(80, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder(new Uri("http://example.com"), "foo");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("foo", builder.Path);
            Assert.AreEqual(80, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);
        }

        [TestMethod]
        public void PropertyConstructors()
        {
            var builder = new UrlBuilder(UriScheme.Http, "example.com");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(-1, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder(UriScheme.Http, "example.com", 8080);
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(8080, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder(UriScheme.Http, "example.com", 8080, "foo");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("foo", builder.Path);
            Assert.AreEqual(8080, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder = new UrlBuilder(UriScheme.Http, "example.com", 8080, "foo", "#bar");
            Assert.IsNotNull(builder);
            Assert.AreEqual("#bar", builder.Fragment);
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("foo", builder.Path);
            Assert.AreEqual(8080, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);
        }

        [TestMethod]
        public void PropertyTests()
        {
            var builder = new UrlBuilder()
            {
                Fragment = "bar",
                Host = "example.com",
                Port = 8080,
                Path = "foo",
                Scheme = UriScheme.Http
            };

            Assert.AreEqual("#bar", builder.Fragment);
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("foo", builder.Path);
            Assert.AreEqual(8080, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder.UserName = "baz";
            builder.Password = "fuzz";
            Assert.AreEqual("baz", builder.UserName);
            Assert.AreEqual("fuzz", builder.Password);
        }

        [TestMethod]
        public void QueryString()
        {
            var builder = new UrlBuilder();
            builder.QueryParameters.Add("foo", "bar");

            Assert.AreNotEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(1, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(1, builder.QueryParameters.Values.Count);
            Assert.AreEqual("foo", builder.QueryParameters.Keys.First());
            Assert.AreEqual("bar", builder.QueryParameters["foo"]);

            Assert.AreEqual("foo=bar", builder.Query);

            builder.QueryParameters.Add("baz", "fuzz");

            Assert.AreNotEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(2, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(2, builder.QueryParameters.Values.Count);
            Assert.AreEqual("baz", builder.QueryParameters.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", builder.QueryParameters["baz"]);

            Assert.AreEqual("foo=bar&baz=fuzz", builder.Query);

            builder.QueryParameters.Clear();
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
        }

        [TestMethod]
        public void QueryStringParsing()
        {
            var builder = new UrlBuilder("http://example.com");
            Assert.IsNotNull(builder);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Fragment));
            Assert.AreEqual("example.com", builder.Host);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Password));
            Assert.AreEqual("/", builder.Path);
            Assert.AreEqual(80, builder.Port);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.Query));
            Assert.AreEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(UriScheme.Http, builder.Scheme);
            Assert.IsTrue(String.IsNullOrWhiteSpace(builder.UserName));
            Assert.IsNotNull(builder.Uri);

            builder.Query = "foo=bar";
            Assert.AreNotEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(1, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(1, builder.QueryParameters.Values.Count);
            Assert.AreEqual("foo", builder.QueryParameters.Keys.First());
            Assert.AreEqual("bar", builder.QueryParameters["foo"]);
            Assert.AreEqual("foo=bar", builder.Query);

            builder.QueryParameters.Clear();
            builder.Query = "foo=bar&baz=fuzz";
            Assert.AreNotEqual(0, builder.QueryParameters.Count);
            Assert.AreEqual(2, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(2, builder.QueryParameters.Values.Count);
            Assert.AreEqual("baz", builder.QueryParameters.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", builder.QueryParameters["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", builder.Query);

            builder.QueryParameters.Clear();
            builder.Query = "foo=bar";
            Assert.AreEqual(1, builder.QueryParameters.Count);
            Assert.AreEqual(1, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(1, builder.QueryParameters.Values.Count);
            Assert.AreEqual("foo", builder.QueryParameters.Keys.First());
            Assert.AreEqual("bar", builder.QueryParameters["foo"]);
            Assert.AreEqual("foo=bar", builder.Query);
            builder.Query = "baz=fuzz";
            Assert.AreEqual(2, builder.QueryParameters.Count);
            Assert.AreEqual(2, builder.QueryParameters.Keys.Count);
            Assert.AreEqual(2, builder.QueryParameters.Values.Count);
            Assert.AreEqual("baz", builder.QueryParameters.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", builder.QueryParameters["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", builder.Query);

            ExceptionAssert.Throws<ArgumentException>(() => builder.Query = "foo=bar&baz=fuzz");

            builder.QueryParameters.Clear();
            ExceptionAssert.Throws<InvalidOperationException>(() => builder.Query = "foo");
        }

        [TestMethod]
        public void ToStringTests()
        {
            var builder = new UrlBuilder();
            Assert.AreEqual("http://localhost/", builder.ToString());

            builder.QueryParameters.Add("foo", "bar");
            Assert.AreEqual("http://localhost/?foo=bar", builder.ToString());

            builder.QueryParameters.Add("baz", "fuzz");
            Assert.AreEqual("http://localhost/?foo=bar&baz=fuzz", builder.ToString());
        }
    }
}