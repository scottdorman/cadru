//------------------------------------------------------------------------------
// <copyright file="UriSchemeTests.cs"
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
using System.Net.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Net.Http.Extensions;
using Cadru.UnitTest.Framework;
using Cadru.Net.Http.Collections;

namespace Cadru.Net.Http.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class HttpExtensionsTests
    {
        [TestMethod]
        public void AsFormattedString()
        {
            HttpRequestMessage request = null;

            Assert.ThrowsException<ArgumentNullException>(() => { request.AsFormattedString(); });

            request = new HttpRequestMessage();
            StringAssert.That.IsNotEmpty(request.AsFormattedString());

            request.Content = new StringContent("body content");
            StringAssert.That.IsNotEmpty(request.AsFormattedString());
            StringAssert.Contains(request.AsFormattedString(), "Body:");
        }

        [TestMethod]
        public void CreateRequestMessage()
        {
            var client = new HttpClient();

            Assert.ThrowsException<InvalidOperationException>(() => { client.CreateRequestMessage(HttpMethod.Get, null); });
            Assert.ThrowsException<InvalidOperationException>(() => { client.CreateRequestMessage(HttpMethod.Get, new Uri("test", UriKind.Relative)); });

            client.BaseAddress = new Uri("https://example.com/");

            var requestMessage = client.CreateRequestMessage(HttpMethod.Get, null);

            Assert.IsNotNull(requestMessage);
            Assert.IsTrue(requestMessage.Method == HttpMethod.Get);
            Assert.AreEqual("https://example.com/", requestMessage.RequestUri.AbsoluteUri);

//            Assert.ThrowsException<InvalidOperationException>(() => { client.CreateRequestMessage(HttpMethod.Get, null); });

            requestMessage = client.CreateRequestMessage(HttpMethod.Get, new Uri("test", UriKind.Relative));

            Assert.IsNotNull(requestMessage);
            Assert.IsTrue(requestMessage.Method == HttpMethod.Get);
            Assert.AreEqual("https://example.com/test", requestMessage.RequestUri.AbsoluteUri);

            var queryStringParameters = new QueryStringParametersDictionary();
            queryStringParameters.Add("test", "testvalue");

            requestMessage = client.CreateRequestMessage(HttpMethod.Get, new Uri("test", UriKind.Relative), queryStringParameters);
            Assert.IsNotNull(requestMessage);
            Assert.IsTrue(requestMessage.Method == HttpMethod.Get);
            Assert.AreEqual("https://example.com/test?test=testvalue", requestMessage.RequestUri.AbsoluteUri);
            StringAssert.That.IsNotEmpty(requestMessage.RequestUri.Query);
        }
    }
}