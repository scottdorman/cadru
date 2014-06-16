using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Cadru.Core.UnitTests
{
    [TestClass]
    public class WeakReferenceTests
    {
        [TestMethod]
        public void WeakReference()
        {
            var stringBuilder = new StringBuilder("test");
 
            var weakReference = new WeakReference<StringBuilder>(stringBuilder);
            Assert.IsNotNull(weakReference.Target);
            Assert.IsInstanceOfType(weakReference.Target, typeof(StringBuilder));
            Assert.AreEqual("test", weakReference.Target.ToString());

            weakReference = new WeakReference<StringBuilder>(stringBuilder, trackResurrection: true);
            Assert.IsNotNull(weakReference.Target);
            Assert.IsInstanceOfType(weakReference.Target, typeof(StringBuilder));
            Assert.IsTrue(weakReference.TrackResurrection);
            Assert.AreEqual("test", weakReference.Target.ToString());

            weakReference = new WeakReference<StringBuilder>(stringBuilder, trackResurrection: false);
            Assert.IsNotNull(weakReference.Target);
            Assert.IsInstanceOfType(weakReference.Target, typeof(StringBuilder));
            Assert.IsFalse(weakReference.TrackResurrection);
            Assert.AreEqual("test", weakReference.Target.ToString());

            stringBuilder = new StringBuilder("test2");
            weakReference.Target = stringBuilder;
            Assert.IsNotNull(weakReference.Target);
            Assert.IsInstanceOfType(weakReference.Target, typeof(StringBuilder));
            Assert.IsFalse(weakReference.TrackResurrection);
            Assert.AreEqual("test2", weakReference.Target.ToString());

            GC.KeepAlive(stringBuilder);
        }
    }
}
