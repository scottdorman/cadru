
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

using Cadru.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Tests.Diagnostics
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StopwatchTimerTests
    {
        [TestMethod]
        public void StopwatchElapsedTimeFormatting()
        {
            var stopwatch = Stopwatch.StartNew();
            Thread.Sleep(600);
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;
            Assert.AreEqual(elapsedTime.ToString("hh':'mm':'ss'.'ff"), stopwatch.ToElapsedTime());
        }
    }
}
