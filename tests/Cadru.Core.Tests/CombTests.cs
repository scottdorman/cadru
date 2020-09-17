//------------------------------------------------------------------------------
// <copyright file="CombTests.cs"
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
using System.Threading;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CombTests
    {
        [TestMethod]
        public void CompareTo()
        {
            var a = new Comb("38ba4a03-0b00-b248-baa3-413faadaa2b8");
            var b = new Comb("385175e2-0b00-b248-911d-413fa76b7979");
            var c = new Comb("385175e2-0b00-b248-911d-413fa76b7979");

            Assert.AreEqual(1, a.CompareTo(b));
            Assert.AreEqual(-1, b.CompareTo(a));
            Assert.AreEqual(0, b.CompareTo(c));

            Assert.AreEqual(1, a.CompareTo(null));
            Assert.AreEqual(1, a.CompareTo((object)b));

            Assert.ThrowsException<ArgumentException>(() => a.CompareTo("test"));
        }

        [TestMethod]
        public void Constructors()
        {
            var c = new Comb("38ba4a03-0b00-b248-baa3-413faadaa2b8");
            Assert.ThrowsException<FormatException>(() => new Comb("3e3a6e75-0100-0f45-ae41a3e3d2536a57"));
            Assert.AreEqual("38ba4a03-0b00-b248-baa3-413faadaa2b8", c.ToString());

            c = new Comb();
            Assert.AreEqual(Comb.Empty, c);
        }

        [TestMethod]
        public void Empty()
        {
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString());
            Assert.AreEqual(0, Comb.Empty.DateTime.Ticks);
            Assert.AreEqual(Comb.MinDate, Comb.Empty.DateTime);
        }

        [TestMethod]
        public void Equality()
        {
            var left = Comb.NewComb();
            var right = Comb.NewComb();

            Assert.IsTrue(left != right);
            Assert.IsFalse(left == right);

            right = left;
            Assert.IsTrue(left == right);
            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(left.Equals((object)right));
            Assert.IsFalse(left.Equals(null));
            Assert.IsFalse(left.Equals("test"));

            var hash = Comb.Parse("385175e2-0b00-b248-911d-413fa76b7979");
            Assert.AreEqual(1917962195, hash.GetHashCode());
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreNotEqual(left.GetHashCode(), hash.GetHashCode());
        }

        [TestMethod]
        public void NewComb()
        {
            var comb = Comb.NewComb();
            Assert.AreNotEqual(Comb.Empty, comb);
            Assert.AreEqual(DateTime.UtcNow.Date, comb.DateTime.Date);

            var date = DateTime.Parse("2014-06-23 09:46:0.0");

            comb = Comb.NewComb(date);
            Assert.AreNotEqual(Comb.Empty, comb);
            Assert.AreEqual(date, comb.DateTime.LocalDateTime);

            var offset = new DateTimeOffset(date);
            comb = Comb.NewComb(offset);
            Assert.AreNotEqual(Comb.Empty, comb);
            Assert.AreEqual(offset, comb.DateTime.LocalDateTime);

            var count = 10;

            var list = new List<Comb>(count);

            for (var i = 0; i < count; i++)
            {
                var c = Comb.NewComb();
                list.Add(c);
                Console.WriteLine("{0} - {1}", c.ToString(), c.DateTime.ToString("o"));
                Thread.Sleep(325);
            }

            Console.WriteLine("Sorted:");
            list.Sort();
            foreach (var c in list)
            {
                Console.WriteLine("{0} - {1}", c.ToString(), c.DateTime.ToString("o"));
            }
        }

        [TestMethod]
        public void Operators()
        {
            var left = new Comb("38ad9d83-0b00-b248-adad-413fa7cd5e0f");
            var right = new Comb("38ba4a03-0b00-b248-baa3-413faadaa2b8");

            Assert.IsTrue(left < right);
            Assert.IsFalse(left > right);
        }

        [TestMethod]
        public void Parse()
        {
            Assert.IsInstanceOfType(Comb.Parse("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.Parse("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.Parse("3e3a6e7501000f45ae41a3e3d2536a57"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.Parse("3e3a6e75-0100-0f45-ae41-a3e3d2536a57"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.Parse("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.Parse("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)"), typeof(Comb));

            Assert.ThrowsException<ArgumentNullException>(() => Comb.Parse(null));
            Assert.ThrowsException<FormatException>(() => Comb.Parse("(00000000000000000000000000000000)"));
        }

        [TestMethod]
        public void ParseExact()
        {
            Assert.IsInstanceOfType(Comb.ParseExact("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}", "X"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}", "x"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "N"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "n"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "D"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", ""), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "B"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "b"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "P"), typeof(Comb));
            Assert.IsInstanceOfType(Comb.ParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "p"), typeof(Comb));

            Assert.ThrowsException<ArgumentNullException>(() => Comb.Parse(null));
            Assert.ThrowsException<FormatException>(() => Comb.Parse("(00000000000000000000000000000000)"));
            Assert.ThrowsException<FormatException>(() => Comb.ParseExact("3e3a6e75-0100-0f45-ae41a3e3d2536a57", "x"));
        }

        [TestMethod]
        public void ToByteArray()
        {
            var c = Comb.Parse("385175e2-0b00-b248-911d-413fa76b7979");
            var a = c.ToByteArray();
            Assert.IsNotNull(a);
            Assert.AreEqual(16, a.Length);
            CollectionAssert.AllItemsAreNotNull(a);
            CollectionAssert.AreEqual(new byte[] { 56, 81, 117, 226, 11, 0, 178, 72, 145, 29, 65, 63, 167, 107, 121, 121 }, a);

            var c2 = new Comb(a);
            Assert.IsTrue(c2 == c);
            Assert.IsTrue(c2.DateTime == c.DateTime);
        }

        [TestMethod]
        public void ToStringTests()
        {
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", $"{Comb.Empty}");
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString());
            Assert.AreEqual("00000000000000000000000000000000", Comb.Empty.ToString("N"));
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString("D"));
            Assert.AreEqual("(00000000-0000-0000-0000-000000000000)", Comb.Empty.ToString("P"));
            Assert.AreEqual("{00000000-0000-0000-0000-000000000000}", Comb.Empty.ToString("B"));
            Assert.AreEqual("{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}", Comb.Empty.ToString("X"));
            Assert.ThrowsException<NotImplementedException>(() => Comb.Empty.ToString("e"));
        }

        [TestMethod]
        public void TryParse()
        {
            Assert.IsTrue(Comb.TryParse("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}", out _));
            Assert.IsTrue(Comb.TryParse("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}", out _));
            Assert.IsTrue(Comb.TryParse("3e3a6e7501000f45ae41a3e3d2536a57", out _));
            Assert.IsTrue(Comb.TryParse("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", out _));
            Assert.IsTrue(Comb.TryParse("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", out _));
            Assert.IsTrue(Comb.TryParse("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", out _));

            Assert.IsFalse(Comb.TryParse(null, out _));
            Assert.IsFalse(Comb.TryParse("(00000000000000000000000000000000)", out _));
        }

        [TestMethod]
        public void TryParseExact()
        {
            Assert.IsTrue(Comb.TryParseExact("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}", "X", out var _));
            Assert.IsTrue(Comb.TryParseExact("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}", "x", out _));

            Assert.IsTrue(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "N", out _));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "n", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "n", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-01000f45ae41a3e3d2536a57", "n", out _));

            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "D", out _));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out _));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-01000f45ae41a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45ae41a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "d", out _));

            Assert.IsTrue(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "B", out _));
            Assert.IsTrue(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-01000f45ae41a3e3d2536a57}", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45ae41a3e3d2536a57}", "b", out _));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41a3e3d2536a57}", "b", out _));

            Assert.IsTrue(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "P", out _));
            Assert.IsTrue(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-01000f45ae41a3e3d2536a57)", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45ae41a3e3d2536a57)", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41a3e3d2536a57)", "p", out _));

            Assert.IsFalse(Comb.TryParseExact(null, "d", out _));
            Assert.IsFalse(Comb.TryParseExact("(00000000000000000000000000000000)", "p", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "x", out _));

            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a573e", "x", out _));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a", "x", out _));

            Assert.ThrowsException<FormatException>(() => Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "e", out _));
        }
    }
}