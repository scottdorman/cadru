using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Net.Http;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.Core.UnitTests.Net.Http
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UriSchemeTests
    {
        [TestMethod]
        public void SchemeName()
        {
            Assert.AreEqual("file", UriScheme.File);
            Assert.AreEqual("ftp", UriScheme.Ftp);
            Assert.AreEqual("gopher", UriScheme.Gopher);
            Assert.AreEqual("http", UriScheme.Http);
            Assert.AreEqual("https", UriScheme.Https);
            Assert.AreEqual("mailto", UriScheme.MailTo);
            Assert.AreEqual("news", UriScheme.News);
        }

        [TestMethod]
        public void Equality()
        {
            Assert.IsTrue(UriScheme.File.Equals((object)UriScheme.File));
            Assert.IsTrue(UriScheme.Ftp.Equals((object)UriScheme.Ftp));
            Assert.IsTrue(UriScheme.Gopher.Equals((object)UriScheme.Gopher));
            Assert.IsTrue(UriScheme.Http.Equals((object)UriScheme.Http));
            Assert.IsTrue(UriScheme.Https.Equals((object)UriScheme.Https));
            Assert.IsTrue(UriScheme.MailTo.Equals((object)UriScheme.MailTo));
            Assert.IsTrue(UriScheme.News.Equals((object)UriScheme.News));

            Assert.IsTrue(UriScheme.File.Equals(UriScheme.File));
            Assert.IsTrue(UriScheme.Ftp.Equals(UriScheme.Ftp));
            Assert.IsTrue(UriScheme.Gopher.Equals(UriScheme.Gopher));
            Assert.IsTrue(UriScheme.Http.Equals(UriScheme.Http));
            Assert.IsTrue(UriScheme.Https.Equals(UriScheme.Https));
            Assert.IsTrue(UriScheme.MailTo.Equals(UriScheme.MailTo));
            Assert.IsTrue(UriScheme.News.Equals(UriScheme.News));

            Assert.IsFalse(UriScheme.File.Equals((object)UriScheme.Ftp));
            Assert.IsFalse(UriScheme.Ftp.Equals((object)UriScheme.Gopher));
            Assert.IsFalse(UriScheme.Gopher.Equals((object)UriScheme.MailTo));
            Assert.IsFalse(UriScheme.Http.Equals((object)UriScheme.Https));
            Assert.IsFalse(UriScheme.Https.Equals((object)UriScheme.Http));
            Assert.IsFalse(UriScheme.MailTo.Equals((object)UriScheme.File));
            Assert.IsFalse(UriScheme.News.Equals((object)UriScheme.Gopher));

            Assert.IsFalse(UriScheme.File.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.Ftp.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.Gopher.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.Http.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.Https.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.MailTo.Equals((UriScheme)null));
            Assert.IsFalse(UriScheme.News.Equals((UriScheme)null));
            
            Assert.IsTrue(UriScheme.File == UriScheme.File);
            Assert.IsTrue(UriScheme.Ftp ==  UriScheme.Ftp);
            Assert.IsTrue(UriScheme.Gopher ==  UriScheme.Gopher);
            Assert.IsTrue(UriScheme.Http ==  UriScheme.Http);
            Assert.IsTrue(UriScheme.Https ==  UriScheme.Https);
            Assert.IsTrue(UriScheme.MailTo ==  UriScheme.MailTo);
            Assert.IsTrue(UriScheme.News ==  UriScheme.News);

            Assert.IsFalse(UriScheme.File != UriScheme.File);
            Assert.IsFalse(UriScheme.Ftp != UriScheme.Ftp);
            Assert.IsFalse(UriScheme.Gopher != UriScheme.Gopher);
            Assert.IsFalse(UriScheme.Http != UriScheme.Http);
            Assert.IsFalse(UriScheme.Https != UriScheme.Https);
            Assert.IsFalse(UriScheme.MailTo != UriScheme.MailTo);
            Assert.IsFalse(UriScheme.News != UriScheme.News);
        }

        [TestMethod]
        public void GetHashCodeTests()
        {
            var hash = "file".ToUpperInvariant().GetHashCode();
            Assert.AreEqual(hash, UriScheme.File.GetHashCode());
            Assert.AreNotEqual(hash, UriScheme.Ftp.GetHashCode());
        }

        [TestMethod]
        public void ToStringTests()
        {
            Assert.AreEqual("file", UriScheme.File.ToString());
            Assert.AreEqual("file", UriScheme.File);

            Assert.AreNotEqual("file", UriScheme.News.ToString());
            Assert.AreNotEqual("file", UriScheme.News);
        }
    }
}
