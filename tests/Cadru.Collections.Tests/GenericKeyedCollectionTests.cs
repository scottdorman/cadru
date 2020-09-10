//------------------------------------------------------------------------------
// <copyright file="GenericKeyedCollectionTests.cs"
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class GenericKeyedCollectionTests
    {
        [TestMethod]
        public void ComparerContstructor()
        {
            var keyedCollection = new GenericKeyedCollection<string, CollectionItem>(item => { return item.Key; }, StringComparer.CurrentCulture)
            {
                new CollectionItem { Key = "A", Value = "Test key A" },
                new CollectionItem { Key = "B", Value = "Test key B" }
            };

            var collectionItem = keyedCollection["A"];

            Assert.IsNotNull(collectionItem);
            Assert.IsNotNull(collectionItem.Key);
            Assert.AreEqual("A", collectionItem.Key);
            Assert.IsNotNull(collectionItem.Value);
            Assert.AreEqual("Test key A", collectionItem.Value);
        }

        [TestMethod]
        public void FullContstructor()
        {
            var keyedCollection = new GenericKeyedCollection<string, CollectionItem>(item => { return item.Key; }, StringComparer.CurrentCulture, 2)
            {
                new CollectionItem { Key = "A", Value = "Test key A" },
                new CollectionItem { Key = "B", Value = "Test key B" }
            };

            var collectionItem = keyedCollection["A"];

            Assert.IsNotNull(collectionItem);
            Assert.IsNotNull(collectionItem.Key);
            Assert.AreEqual("A", collectionItem.Key);
            Assert.IsNotNull(collectionItem.Value);
            Assert.AreEqual("Test key A", collectionItem.Value);
        }

        [TestMethod]
        public void SimpleContstructor()
        {
            var keyedCollection = new GenericKeyedCollection<string, CollectionItem>(item => { return item.Key; })
            {
                new CollectionItem { Key = "A", Value = "Test key A" },
                new CollectionItem { Key = "B", Value = "Test key B" }
            };

            var collectionItem = keyedCollection["A"];

            Assert.IsNotNull(collectionItem);
            Assert.IsNotNull(collectionItem.Key);
            Assert.AreEqual("A", collectionItem.Key);
            Assert.IsNotNull(collectionItem.Value);
            Assert.AreEqual("Test key A", collectionItem.Value);
        }

        private class CollectionItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}