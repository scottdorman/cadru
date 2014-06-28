using Cadru.UnitTest.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cadru.Core.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CombTests
    {
        private static readonly DateTimeOffset BaseDate = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private const double Accuracy = 3.333333;

        [TestMethod]
        public void Scratch()
        {
            for (int i = 0; i < 1000; i++)
            {
                var ticks = DateTimeOffset.UtcNow.UtcTicks;
                Console.WriteLine(ticks);
            }
        }

        [TestMethod]
        public void Empty()
        {
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString());
            Assert.AreEqual(0, Comb.Empty.DateTime.Ticks);
            Assert.AreEqual(Comb.MinDate, Comb.Empty.DateTime);
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

            //comb = new Comb2("3e3a6e75-0100-0f45-ae41-a3e3d2536a57");
            //Assert.AreEqual("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", comb.ToString());
            //Assert.AreEqual(new DateTimeOffset(2014, 06, 25, 14, 43, 45, 550, TimeSpan.Zero), comb.DateTime);

            //s = SequentialGuid.NewSequentialGuid();
            //var d = s.GetDateTimeOffset();

            //Assert.AreNotEqual("00000000-0000-0000-0000-000000000000", s.ToString());
            //Assert.AreEqual(DateTime.Today.Date, s.GetDateTime().Date);

            int count = 10;

            var list = new List<Comb>(count);

            for (int i = 0; i < count; i++)
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
        public void ToStringTests()
        {
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString());
            Assert.AreEqual("00000000000000000000000000000000", Comb.Empty.ToString("N"));
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", Comb.Empty.ToString("D"));
            Assert.AreEqual("(00000000-0000-0000-0000-000000000000)", Comb.Empty.ToString("P"));
            Assert.AreEqual("{00000000-0000-0000-0000-000000000000}", Comb.Empty.ToString("B"));
            Assert.AreEqual("{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}", Comb.Empty.ToString("X"));
            ExceptionAssert.Throws<NotImplementedException>(() => Comb.Empty.ToString("e"));
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

            ExceptionAssert.Throws<ArgumentNullException>(() => Comb.Parse(null));
            ExceptionAssert.Throws<FormatException>(() => Comb.Parse("(00000000000000000000000000000000)"));
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

            ExceptionAssert.Throws<ArgumentNullException>(() => Comb.Parse(null));
            ExceptionAssert.Throws<FormatException>(() => Comb.Parse("(00000000000000000000000000000000)"));
            ExceptionAssert.Throws<FormatException>(() => Comb.ParseExact("3e3a6e75-0100-0f45-ae41a3e3d2536a57", "x"));
        }

        [TestMethod]
        public void TryParse()
        {
            Comb c;

            Assert.IsTrue(Comb.TryParse("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}", out c));
            Assert.IsTrue(Comb.TryParse("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}", out c));
            Assert.IsTrue(Comb.TryParse("3e3a6e7501000f45ae41a3e3d2536a57", out c));
            Assert.IsTrue(Comb.TryParse("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", out c));
            Assert.IsTrue(Comb.TryParse("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", out c));
            Assert.IsTrue(Comb.TryParse("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", out c));

            Assert.IsFalse(Comb.TryParse(null, out c));
            Assert.IsFalse(Comb.TryParse("(00000000000000000000000000000000)", out c));
        }

        [TestMethod]
        public void TryParseExact()
        {
            Comb c;

            Assert.IsTrue(Comb.TryParseExact("{0x3e3a6e75,0x0100,0x0f45,{0xae,0x41,0xa3,0xe3,0xd2,0x53,0x6a,0x57}}", "X", out c));
            Assert.IsTrue(Comb.TryParseExact("{0x3E3A6E75,0x0100,0x0F45,{0xAE,0x41,0xA3,0xE3,0xD2,0x53,0x6A,0x57}}", "x", out c));

            Assert.IsTrue(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "N", out c));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "n", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "n", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-01000f45ae41a3e3d2536a57", "n", out c));
            //Assert.IsFalse(Comb.TryParseExact("3e3a6e750100-0f45ae41a3e3d2536a57", "n", out c));
            //Assert.IsFalse(Comb.TryParseExact("3e3a6e7501000f45-ae41a3e3d2536a57", "n", out c));
            //Assert.IsFalse(Comb.TryParseExact("3e3a6e7501000f45ae41-a3e3d2536a57", "n", out c));

            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "D", out c));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out c));
            Assert.IsTrue(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e7501000f45ae41a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-01000f45ae41a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45ae41a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "d", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "d", out c));

            Assert.IsTrue(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "B", out c));
            Assert.IsTrue(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57}", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-01000f45ae41a3e3d2536a57}", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45ae41a3e3d2536a57}", "b", out c));
            Assert.IsFalse(Comb.TryParseExact("{3e3a6e75-0100-0f45-ae41a3e3d2536a57}", "b", out c));
            
            Assert.IsTrue(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "P", out c));
            Assert.IsTrue(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-01000f45ae41a3e3d2536a57)", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45ae41a3e3d2536a57)", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41a3e3d2536a57)", "p", out c));

            Assert.IsFalse(Comb.TryParseExact(null, "d", out c));
            Assert.IsFalse(Comb.TryParseExact("(00000000000000000000000000000000)", "p", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a57", "x", out c));

            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a573e", "x", out c));
            Assert.IsFalse(Comb.TryParseExact("3e3a6e75-0100-0f45-ae41-a3e3d2536a", "x", out c));

            ExceptionAssert.Throws<FormatException>(() => Comb.TryParseExact("(3e3a6e75-0100-0f45-ae41-a3e3d2536a57)", "e", out c));
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
            Assert.IsFalse(left.Equals((object)null));
            Assert.IsFalse(left.Equals("test"));

            var hash = Comb.Parse("385175e2-0b00-b248-911d-413fa76b7979");
            Assert.AreEqual(1917962195, hash.GetHashCode());
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreNotEqual(left.GetHashCode(), hash.GetHashCode());
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
        }

        [TestMethod]
        public void Constructors()
        {
            var c = new Comb("38ba4a03-0b00-b248-baa3-413faadaa2b8");
            ExceptionAssert.Throws<FormatException>(() => new Comb("3e3a6e75-0100-0f45-ae41a3e3d2536a57"));
            Assert.AreEqual("38ba4a03-0b00-b248-baa3-413faadaa2b8", c.ToString());

            c = new Comb();
            Assert.AreEqual(Comb.Empty, c);


        }

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
            
            ExceptionAssert.Throws<ArgumentException>(() => a.CompareTo("test"));
        }
    }
}
