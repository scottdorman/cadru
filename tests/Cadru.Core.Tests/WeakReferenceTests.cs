//------------------------------------------------------------------------------
// <copyright file="WeakReferenceTests.cs"
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
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
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