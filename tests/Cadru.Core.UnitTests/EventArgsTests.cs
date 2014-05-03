using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EventArgsTests
    {
        [TestMethod]
        public void EventArgs()
        {
            var args = new EventArgs<string>("test");
            Assert.IsNotNull(args);
            Assert.IsTrue(args.Data.GetType() == typeof(string));
            Assert.AreEqual("test", args.Data);
        }

        [TestMethod]
        public void CancelEventArgs()
        {
            var args = new CancelEventArgs<string>("test");
            Assert.IsNotNull(args);
            Assert.IsTrue(args.Data.GetType() == typeof(string));
            Assert.AreEqual("test", args.Data);
        }
    }
}
