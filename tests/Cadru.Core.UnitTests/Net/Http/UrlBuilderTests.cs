using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Net.Http;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.Core.UnitTests.Net.Http
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
        public void GetHashCodeTests()
        {
            var builder = new UrlBuilder();
            var uri = new UriBuilder();
            Assert.AreEqual(uri.GetHashCode(), builder.GetHashCode());
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
    }
}
