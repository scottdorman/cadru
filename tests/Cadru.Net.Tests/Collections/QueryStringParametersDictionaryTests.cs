﻿//------------------------------------------------------------------------------
// <copyright file="QueryStringParametersDictionaryTests.cs"
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Net.Http.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Net.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class QueryStringParametersDictionaryTests
    {
        [TestMethod]
        public void BasicConstructors()
        {
            var q = new QueryStringParametersDictionary();
            Assert.IsNotNull(q);
            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.Keys.Count);
            Assert.AreEqual(0, q.Values.Count);

            q = new QueryStringParametersDictionary(16);
            Assert.IsNotNull(q);
            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.Keys.Count);
            Assert.AreEqual(0, q.Values.Count);
        }

        [TestMethod]
        public void DictionaryConstructor()
        {
            var q = new QueryStringParametersDictionary(new Dictionary<string, string>());

            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.Keys.Count);
            Assert.AreEqual(0, q.Values.Count);
            Assert.AreEqual("", q.ToQueryString());

            q = new QueryStringParametersDictionary(new Dictionary<string, string>()
            {
                { "foo", "bar" }
            });

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(1, q.Keys.Count);
            Assert.AreEqual(1, q.Values.Count);
            Assert.AreEqual("foo", q.Keys.First());
            Assert.AreEqual("bar", q["foo"]);
            Assert.AreEqual("foo=bar", q.ToQueryString());

            q = new QueryStringParametersDictionary(new Dictionary<string, string>()
            {
                { "foo", "bar" },
                { "baz", "fuzz" }
            });

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary(new Dictionary<string, string>()
            {
                { "foo", "" },
                { "baz", "fuzz" }
            });

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary(new Dictionary<string, string>()
            {
                { "foo", "bar" },
                { "baz", "" }
            });

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("", q["baz"]);
            Assert.AreEqual("foo=bar&baz=", q.ToQueryString());

            Assert.ThrowsException<ArgumentNullException>(() => new QueryStringParametersDictionary((Dictionary<string, string>)null));
        }

        [TestMethod]
        public void ParsingConstructor()
        {
            var q = new QueryStringParametersDictionary("");

            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.Keys.Count);
            Assert.AreEqual(0, q.Values.Count);
            Assert.AreEqual("", q.ToQueryString());

            q = new QueryStringParametersDictionary((string)null);

            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.Keys.Count);
            Assert.AreEqual(0, q.Values.Count);
            Assert.AreEqual("", q.ToQueryString());

            q = new QueryStringParametersDictionary("?foo=bar");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(1, q.Keys.Count);
            Assert.AreEqual(1, q.Values.Count);
            Assert.AreEqual("foo", q.Keys.First());
            Assert.AreEqual("bar", q["foo"]);
            Assert.AreEqual("foo=bar", q.ToQueryString());

            q = new QueryStringParametersDictionary("&foo=bar");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(1, q.Keys.Count);
            Assert.AreEqual(1, q.Values.Count);
            Assert.AreEqual("foo", q.Keys.First());
            Assert.AreEqual("bar", q["foo"]);
            Assert.AreEqual("foo=bar", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=bar");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(1, q.Keys.Count);
            Assert.AreEqual(1, q.Values.Count);
            Assert.AreEqual("foo", q.Keys.First());
            Assert.AreEqual("bar", q["foo"]);
            Assert.AreEqual("foo=bar", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=bar&baz=fuzz");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=bar&baz=fuzz&");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary("&foo=bar&baz=fuzz");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=bar&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=&baz=fuzz");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("fuzz", q["baz"]);
            Assert.AreEqual("foo=&baz=fuzz", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=bar&baz=");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("", q["baz"]);
            Assert.AreEqual("foo=bar&baz=", q.ToQueryString());

            q = new QueryStringParametersDictionary("foo=bar&baz=&");

            Assert.AreNotEqual(0, q.Count);
            Assert.AreEqual(2, q.Keys.Count);
            Assert.AreEqual(2, q.Values.Count);
            Assert.AreEqual("baz", q.Keys.Skip(1).First());
            Assert.AreEqual("", q["baz"]);
            Assert.AreEqual("foo=bar&baz=", q.ToQueryString());

            Assert.ThrowsException<InvalidOperationException>(() => new QueryStringParametersDictionary("foo"));
        }
    }
}