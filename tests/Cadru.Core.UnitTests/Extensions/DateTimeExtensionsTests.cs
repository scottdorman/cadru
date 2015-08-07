using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using Cadru.Extensions;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DateTimeExtensionsTests
    {
        private static int GCWeekOfYear(DateTime date, CalendarWeekRule rule, DayOfWeek firstDayOfWeek = (DayOfWeek)(-1))
        {
            if (firstDayOfWeek == (DayOfWeek)(-1))
            {
                firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            }

            GregorianCalendar gc = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return gc.GetWeekOfYear(date, rule, firstDayOfWeek);
        }

        [TestMethod]
        public void AddWeekdays()
        {
            var date = new DateTime(2013, 7, 26).Date;
            Assert.AreEqual(new DateTime(2013, 7, 30), date.AddWeekdays(2));
            Assert.AreEqual(new DateTime(2013, 7, 24), date.AddWeekdays(-2));
        }

        [TestMethod]
        public void AddQuarters()
        {
            var date = new DateTime(2013, 7, 26).Date;
            Assert.AreEqual(new DateTime(2014, 1, 26), date.AddQuarters(2));
            Assert.AreEqual(new DateTime(2013, 1, 26), date.AddQuarters(-2));
        }

        [TestMethod]
        public void AddWeeks()
        {
            var date = new DateTime(2013, 7, 26).Date;
            Assert.AreEqual(new DateTime(2013, 8, 9), date.AddWeeks(2));
            Assert.AreEqual(new DateTime(2013, 7, 12), date.AddWeeks(-2));
        }

        [TestMethod]
        public void FirstDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 01, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 02, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 03, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 04, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 05, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 06, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 07, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 08, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 09, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 10, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 11, 01).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 12, 01).FirstDayOfNextQuarter());

            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 01, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 02, 28).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 03, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 04, 30).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 05, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 06, 30).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 07, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 08, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 09, 30).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 10, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 11, 30).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 01, 01), new DateTime(2010, 12, 31).FirstDayOfNextQuarter());
        }

        [TestMethod]
        public void DayOfQuarter()
        {
            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 01, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 02, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 03, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 04, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 05, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 06, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 07, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 08, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 09, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 10, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 11, 01).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 12, 01).FirstDayOfQuarter());

            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 01, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 02, 28).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 01, 01), new DateTime(2010, 03, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 04, 30).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 05, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 04, 01), new DateTime(2010, 06, 30).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 07, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 08, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 07, 01), new DateTime(2010, 09, 30).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 10, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 11, 30).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 10, 01), new DateTime(2010, 12, 31).FirstDayOfQuarter());


            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 01, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 02, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 03, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 04, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 05, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 06, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 07, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 08, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 09, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 10, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 11, 01).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 12, 01).LastDayOfQuarter());

            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 01, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 02, 28).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 03, 31), new DateTime(2010, 03, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 04, 30).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 05, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 06, 30), new DateTime(2010, 06, 30).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 07, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 08, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 09, 30), new DateTime(2010, 09, 30).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 10, 31).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 11, 30).LastDayOfQuarter());
            Assert.AreEqual(new DateTime(2010, 12, 31), new DateTime(2010, 12, 31).LastDayOfQuarter());
        }

        [TestMethod]
        public void GetWeekOfYear()
        {
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstDay), new DateTime(2010, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstDay), new DateTime(2010, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstDay), new DateTime(2010, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstDay), new DateTime(2010, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstDay), new DateTime(2010, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstDay), new DateTime(2008, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstDay), new DateTime(2008, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstDay), new DateTime(2008, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstDay), new DateTime(2008, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstDay), new DateTime(2008, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstDay), new DateTime(2008, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstDay), new DateTime(2005, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstDay), new DateTime(2005, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstDay), new DateTime(2005, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstDay), new DateTime(2005, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstDay), new DateTime(2005, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstDay), new DateTime(2005, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2010, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2008, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 02, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 02, 28).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 03, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 04, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 05, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 06, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 07, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 07, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 08, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 09, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 10, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 10, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 11, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 12, 01).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 12, 26).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFullWeek), new DateTime(2005, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 01, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 02, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 02, 28).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 03, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 04, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 05, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 06, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 07, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 08, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 09, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 10, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 10, 31).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 11, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 26).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2010, 12, 31).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 01, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 02, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 02, 28).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 03, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 04, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 05, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 06, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 07, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 07, 31).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 08, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 09, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 10, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 10, 31).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 11, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 26).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2008, 12, 31).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 01, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 02, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 02, 28).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 03, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 04, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 05, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 06, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 07, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 07, 31).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 08, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 09, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 10, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 10, 31).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 11, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 01).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 26).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek), new DateTime(2005, 12, 31).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday), new DateTime(2005, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday), new DateTime(2005, 12, 31).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday));

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTime(2010, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)8)).WithParameter("firstDayOfWeek");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTime(2010, 01, 01).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)(-1))).WithParameter("firstDayOfWeek");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTime(2010, 01, 01).GetWeekOfYear((CalendarWeekRule)24)).WithParameter("rule");
        }

        [TestMethod]
        public void Quarter()
        {
            Assert.AreEqual(1, new DateTime(2010, 01, 01).Quarter());
            Assert.AreEqual(1, new DateTime(2010, 02, 01).Quarter());
            Assert.AreEqual(1, new DateTime(2010, 03, 01).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 04, 01).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 05, 01).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 06, 01).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 07, 01).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 08, 01).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 09, 01).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 10, 01).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 11, 01).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 12, 01).Quarter());

            Assert.AreEqual(1, new DateTime(2010, 01, 31).Quarter());
            Assert.AreEqual(1, new DateTime(2010, 02, 28).Quarter());
            Assert.AreEqual(1, new DateTime(2010, 03, 31).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 04, 30).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 05, 31).Quarter());
            Assert.AreEqual(2, new DateTime(2010, 06, 30).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 07, 31).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 08, 31).Quarter());
            Assert.AreEqual(3, new DateTime(2010, 09, 30).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 10, 31).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 11, 30).Quarter());
            Assert.AreEqual(4, new DateTime(2010, 12, 31).Quarter());
        }

        [TestMethod]
        public void DaysInMonth()
        {
            Assert.AreEqual(31, new DateTime(2013, 7, 1).DaysInMonth());
            Assert.AreEqual(30, new DateTime(2013, 9, 1).DaysInMonth());
            Assert.AreEqual(28, new DateTime(2013, 2, 1).DaysInMonth());
            Assert.AreEqual(29, new DateTime(2004, 2, 1).DaysInMonth());
        }

        [TestMethod]
        public void Elapsed()
        {
            Assert.AreEqual(1, DateTime.Now.AddDays(-1).Elapsed().Days);
            Assert.AreEqual(7, DateTime.Now.AddDays(-7).Elapsed().Days);
            Assert.AreEqual(-7, DateTime.Now.AddDays(7).Elapsed().Days);
        }

        [TestMethod]
        public void DayOfMonth()
        {
            Assert.AreEqual(new DateTime(2013, 7, 1), new DateTime(2013, 7, 25).FirstDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 7, 1), new DateTime(2013, 7, 1).FirstDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 7, 1), new DateTime(2013, 7, 31).FirstDayOfMonth());

            Assert.AreEqual(new DateTime(2013, 7, 31), new DateTime(2013, 7, 25).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 7, 31), new DateTime(2013, 7, 1).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 7, 31), new DateTime(2013, 7, 31).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 9, 30), new DateTime(2013, 9, 25).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 9, 30), new DateTime(2013, 9, 1).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 9, 30), new DateTime(2013, 9, 30).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2013, 2, 28), new DateTime(2013, 2, 1).LastDayOfMonth());
            Assert.AreEqual(new DateTime(2004, 2, 29), new DateTime(2004, 2, 1).LastDayOfMonth());
        }

        [TestMethod]
        public void DaysOfWeek()
        {
            Assert.AreEqual(new DateTime(2013, 6, 30), new DateTime(2013, 7, 1).FirstDayOfWeek());
            Assert.AreEqual(new DateTime(2013, 7, 6), new DateTime(2013, 7, 1).LastDayOfWeek());

            Assert.AreEqual(new DateTime(2013, 7, 1), new DateTime(2013, 7, 1).FirstDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2013, 7, 7), new DateTime(2013, 7, 1).LastDayOfWeek(DayOfWeek.Monday));

            Assert.AreEqual(new DateTime(2013, 6, 30), new DateTime(2013, 7, 1).FirstDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 7, 6), new DateTime(2013, 7, 1).LastDayOfWeek(DayOfWeek.Sunday));

            Assert.AreEqual(new DateTime(2014, 4, 28), new DateTime(2014, 4, 30).GetDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2014, 5, 2), new DateTime(2014, 4, 30).GetDayOfWeek(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2014, 4, 30), new DateTime(2014, 4, 30).GetDayOfWeek(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2014, 4, 27), new DateTime(2014, 4, 30).GetDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2014, 4, 27), new DateTime(2014, 4, 30).GetDayOfWeek(DayOfWeek.Sunday, DayOfWeek.Friday));
        }

        [TestMethod]
        public void DayOfYear()
        {
            Assert.AreEqual(new DateTime(2013, 1, 1), new DateTime(2013, 7, 12).FirstDayOfYear());
            Assert.AreEqual(new DateTime(2013, 12, 31), new DateTime(2013, 7, 12).LastDayOfYear());
        }

        [TestMethod]
        public void LeapYears()
        {
            Assert.IsTrue(new DateTime(2004, 2, 1).IsLeapYear());
            Assert.IsFalse(new DateTime(2012, 6, 18).IsLeapMonth());
            Assert.IsTrue(new DateTime(2004, 2, 29).IsLeapDay());
            Assert.IsFalse(new DateTime(2004, 2, 28).IsLeapDay());

            Assert.IsFalse(new DateTime(2013, 2, 1).IsLeapYear());
            Assert.IsFalse(new DateTime(2013, 2, 1).IsLeapMonth());
            Assert.IsFalse(new DateTime(2013, 2, 2).IsLeapDay());
        }

        [TestMethod]
        public void MonthNames()
        {
            var months = DateTimeExtensions.GetMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            months = DateTimeExtensions.GetAbbreviatedMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            Assert.AreEqual("July", new DateTime(2013, 7, 1).GetMonthName());
            Assert.AreEqual("Jul", new DateTime(2013, 7, 1).GetAbbreviatedMonthName());

            Assert.AreEqual(7, DateTimeExtensions.GetMonthNumber("July", false));
            Assert.AreEqual(7, DateTimeExtensions.GetMonthNumber("Jul", true));
            Assert.AreEqual(0, DateTimeExtensions.GetMonthNumber("ABC", false));
            Assert.AreEqual(0, DateTimeExtensions.GetMonthNumber("ABC", true));
        }

        [TestMethod]
        public void GetDate()
        {
            Assert.AreEqual(new DateTime(2013, 7, 3), new DateTime(2013, 7, 1).GetDayOfWeek(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 6, 30), new DateTime(2013, 7, 1).GetDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 7, 1), new DateTime(2013, 7, 1).GetDayOfWeek(DayOfWeek.Monday));
        }

        [TestMethod]
        public void IsWeekday()
        {
            Assert.IsTrue(new DateTime(2014, 4, 28).IsWeekday());
            Assert.IsTrue(new DateTime(2014, 4, 22).IsWeekday());
            Assert.IsFalse(new DateTime(2014, 5, 3).IsWeekday());
            Assert.IsTrue(new DateTime(2014, 5, 3).IsWeekend());
            Assert.IsFalse(new DateTime(2014, 4, 22).IsWeekend());
        }

        [TestMethod]
        public void FirstDayOf()
        {
            Assert.AreEqual(new DateTime(2009, 12, 27), new DateTime(2010, 01, 01).FirstDayOfWeek());
            Assert.AreEqual(new DateTime(2009, 12, 28), new DateTime(2010, 01, 01).FirstDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2014, 4, 25), new DateTime(2014, 04, 30).FirstDayOfWeek(DayOfWeek.Friday));
        }

        [TestMethod]
        public void IsUtcDateTime()
        {
            Assert.IsTrue(DateTime.UtcNow.IsUtcDateTime());
            Assert.IsFalse(DateTime.Now.IsUtcDateTime());

            Assert.IsTrue(new DateTime(2004, 4, 28, 9, 13, 0, DateTimeKind.Utc).IsUtcDateTime());
            Assert.IsFalse(new DateTime(2004, 4, 28, 9, 13, 0, DateTimeKind.Local).IsUtcDateTime());
            Assert.IsFalse(new DateTime(2004, 4, 28, 9, 13, 0, DateTimeKind.Unspecified).IsUtcDateTime());
        }

        [TestMethod]
        public void ToRelativeDateTimeString()
        {
            Assert.AreEqual("Today, 12:00 AM", DateTime.Today.ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual("Tomorrow", DateTime.Today.AddDays(1).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual("Yesterday", DateTime.Today.AddDays(-1).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));

            Assert.AreEqual(DateTime.Today.AddDays(2).DayOfWeek.ToString(), DateTime.Today.AddDays(2).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(-2).DayOfWeek.ToString(), DateTime.Today.AddDays(-2).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(3).DayOfWeek.ToString(), DateTime.Today.AddDays(3).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(-3).DayOfWeek.ToString(), DateTime.Today.AddDays(-3).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(4).DayOfWeek.ToString(), DateTime.Today.AddDays(4).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(-4).DayOfWeek.ToString(), DateTime.Today.AddDays(-4).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(5).DayOfWeek.ToString(), DateTime.Today.AddDays(5).ToRelativeDateString());
            Assert.AreEqual(DateTime.Today.AddDays(-5).DayOfWeek.ToString(), DateTime.Today.AddDays(-5).ToRelativeDateString());

            Assert.AreEqual(DateTime.Today.AddDays(2).DayOfWeek.ToString(), DateTime.Today.AddDays(2).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(-2).DayOfWeek.ToString(), DateTime.Today.AddDays(-2).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(3).DayOfWeek.ToString(), DateTime.Today.AddDays(3).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(-3).DayOfWeek.ToString(), DateTime.Today.AddDays(-3).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(4).DayOfWeek.ToString(), DateTime.Today.AddDays(4).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(-4).DayOfWeek.ToString(), DateTime.Today.AddDays(-4).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(5).DayOfWeek.ToString(), DateTime.Today.AddDays(5).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(-5).DayOfWeek.ToString(), DateTime.Today.AddDays(-5).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));

            Assert.AreEqual("2 days from now", DateTime.Today.AddDays(2).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("2 days ago", DateTime.Today.AddDays(-2).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("3 days from now", DateTime.Today.AddDays(3).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("3 days ago", DateTime.Today.AddDays(-3).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("4 days from now", DateTime.Today.AddDays(4).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("4 days ago", DateTime.Today.AddDays(-4).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("5 days from now", DateTime.Today.AddDays(5).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("5 days ago", DateTime.Today.AddDays(-5).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));

            Assert.AreEqual(DateTime.Today.AddDays(9).ToString("MMM d, yyyy"), DateTime.Today.AddDays(9).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTime.Today.AddDays(-9).ToString("MMM d, yyyy"), DateTime.Today.AddDays(-9).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
        }

        [TestMethod]
        public void YesterdayTomorrow()
        {
            Assert.AreEqual(DateTime.Today.AddDays(1), DateTime.Today.Tomorrow());
            Assert.AreEqual(DateTime.Today.AddDays(-1), DateTime.Today.Yesterday());
            Assert.AreNotEqual(DateTime.Today.AddDays(2), DateTime.Today.Tomorrow());
            Assert.AreNotEqual(DateTime.Today.AddDays(-2), DateTime.Today.Yesterday());
        }

        [TestMethod]
        public void NextLast()
        {
            var date = new DateTime(2015, 8, 7).Date;
            Assert.AreEqual(new DateTime(2015, 8, 10), date.Next(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2015, 8, 14), date.Next(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2015, 8, 9), date.Next(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 8, 15), date.Next(DayOfWeek.Saturday));

            Assert.AreEqual(new DateTime(2015, 8, 3), date.Last(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2015, 7, 31), date.Last(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2015, 8, 2), date.Last(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 8, 1), date.Last(DayOfWeek.Saturday));
        }
    }
}