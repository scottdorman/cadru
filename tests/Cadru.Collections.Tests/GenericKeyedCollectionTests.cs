using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class GenericKeyedCollectionTests
    {
        private class CollectionItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
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
    }
}
