using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using Cadru.Extensions;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DateTimeOffsetExtensionsTests
    {
        private static int GCWeekOfYear(DateTimeOffset date, CalendarWeekRule rule, DayOfWeek firstDayOfWeek = (DayOfWeek)(-1))
        {
            if (firstDayOfWeek == (DayOfWeek)(-1))
            {
                firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            }

            GregorianCalendar gc = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return gc.GetWeekOfYear(date.DateTime, rule, firstDayOfWeek);
        }

        [TestMethod]
        public void AddWeekdays()
        {
            var date = new DateTimeOffset(2013, 7, 26, 0, 0, 0, DateTimeOffset.Now.Offset);
            Assert.AreEqual(new DateTimeOffset(2013, 7, 30, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddWeekdays(2));
            Assert.AreEqual(new DateTimeOffset(2013, 7, 24, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddWeekdays(-2));
        }

        [TestMethod]
        public void AddQuarters()
        {
            var date = new DateTimeOffset(2013, 7, 26, 0, 0, 0, DateTimeOffset.Now.Offset);
            Assert.AreEqual(new DateTimeOffset(2014, 1, 26, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddQuarters(2));
            Assert.AreEqual(new DateTimeOffset(2013, 1, 26, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddQuarters(-2));
        }

        [TestMethod]
        public void AddWeeks()
        {
            var date = new DateTimeOffset(2013, 7, 26, 0, 0, 0, DateTimeOffset.Now.Offset);
            Assert.AreEqual(new DateTimeOffset(2013, 8, 9, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddWeeks(2));
            Assert.AreEqual(new DateTimeOffset(2013, 7, 12, 0, 0, 0, DateTimeOffset.Now.Offset), date.AddWeeks(-2));
        }

        [TestMethod]
        public void FirstDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());

            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTimeOffset(2011, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfNextQuarter());
        }

        [TestMethod]
        public void DayOfQuarter()
        {
            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());

            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfQuarter());


            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());

            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 04, 30, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 05, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 08, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 11, 30, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
            Assert.AreEqual(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfQuarter());
        }

        [TestMethod]
        public void GetWeekOfYear()
        {
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstDay), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFullWeek), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2008, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 26, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear());

            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday));
            Assert.AreEqual(GCWeekOfYear(new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday), new DateTimeOffset(2005, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday));

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)8)).WithParameter("firstDayOfWeek");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear(CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)(-1))).WithParameter("firstDayOfWeek");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).GetWeekOfYear((CalendarWeekRule)24)).WithParameter("rule");
        }

        [TestMethod]
        public void Quarter()
        {
            Assert.AreEqual(1, new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(1, new DateTimeOffset(2010, 02, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(1, new DateTimeOffset(2010, 03, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 04, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 05, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 06, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 07, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 08, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 09, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 10, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 11, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 12, 01, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());

            Assert.AreEqual(1, new DateTimeOffset(2010, 01, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(1, new DateTimeOffset(2010, 02, 28, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(1, new DateTimeOffset(2010, 03, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 04, 30, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 05, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(2, new DateTimeOffset(2010, 06, 30, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 07, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 08, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(3, new DateTimeOffset(2010, 09, 30, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 10, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 11, 30, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
            Assert.AreEqual(4, new DateTimeOffset(2010, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset).Quarter());
        }

        [TestMethod]
        public void DaysInMonth()
        {
            Assert.AreEqual(31, new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).DaysInMonth());
            Assert.AreEqual(30, new DateTimeOffset(2013, 9, 1, 0, 0, 0, DateTimeOffset.Now.Offset).DaysInMonth());
            Assert.AreEqual(28, new DateTimeOffset(2013, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).DaysInMonth());
            Assert.AreEqual(29, new DateTimeOffset(2004, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).DaysInMonth());
        }

        [TestMethod]
        public void Elapsed()
        {
            Assert.AreEqual(1, DateTimeOffset.Now.AddDays(-1).Elapsed().Days);
            Assert.AreEqual(7, DateTimeOffset.Now.AddDays(-7).Elapsed().Days);
            Assert.AreEqual(-7, DateTimeOffset.Now.AddDays(7).Elapsed().Days);
        }

        [TestMethod]
        public void DayOfMonth()
        {
            Assert.AreEqual(new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 25, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 31, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfMonth());

            Assert.AreEqual(new DateTimeOffset(2013, 7, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 25, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 7, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 7, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 31, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 9, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 9, 25, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 9, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 9, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 9, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 9, 30, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2013, 2, 28, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
            Assert.AreEqual(new DateTimeOffset(2004, 2, 29, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2004, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfMonth());
        }

        [TestMethod]
        public void DaysOfWeek()
        {
            Assert.AreEqual(new DateTimeOffset(2013, 6, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek());
            Assert.AreEqual(new DateTimeOffset(2013, 7, 6, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfWeek());

            Assert.AreEqual(new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTimeOffset(2013, 7, 7, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfWeek(DayOfWeek.Monday));

            Assert.AreEqual(new DateTimeOffset(2013, 6, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTimeOffset(2013, 7, 6, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfWeek(DayOfWeek.Sunday));

            Assert.AreEqual(new DateTimeOffset(2014, 4, 28, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTimeOffset(2014, 5, 2, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Friday));
            Assert.AreEqual(new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTimeOffset(2014, 4, 27, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTimeOffset(2014, 4, 27, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 4, 30, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Sunday, DayOfWeek.Friday));
        }

        [TestMethod]
        public void DayOfYear()
        {
            Assert.AreEqual(new DateTimeOffset(2013, 1, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 12, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfYear());
            Assert.AreEqual(new DateTimeOffset(2013, 12, 31, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 12, 0, 0, 0, DateTimeOffset.Now.Offset).LastDayOfYear());
        }

        [TestMethod]
        public void LeapYears()
        {
            Assert.IsTrue(new DateTimeOffset(2004, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapYear());
            Assert.IsFalse(new DateTimeOffset(2012, 6, 18, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapMonth());
            Assert.IsTrue(new DateTimeOffset(2004, 2, 29, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapDay());
            Assert.IsFalse(new DateTimeOffset(2004, 2, 28, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapDay());

            Assert.IsFalse(new DateTimeOffset(2013, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapYear());
            Assert.IsFalse(new DateTimeOffset(2013, 2, 1, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapMonth());
            Assert.IsFalse(new DateTimeOffset(2013, 2, 2, 0, 0, 0, DateTimeOffset.Now.Offset).IsLeapDay());
        }

        [TestMethod]
        public void MonthNames()
        {
            var months = DateTimeOffsetExtensions.GetMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            months = DateTimeOffsetExtensions.GetAbbreviatedMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            Assert.AreEqual("July", new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).GetMonthName());
            Assert.AreEqual("Jul", new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).GetAbbreviatedMonthName());

            Assert.AreEqual(7, DateTimeOffsetExtensions.GetMonthNumber("July", false));
            Assert.AreEqual(7, DateTimeOffsetExtensions.GetMonthNumber("Jul", true));
            Assert.AreEqual(0, DateTimeOffsetExtensions.GetMonthNumber("ABC", false));
            Assert.AreEqual(0, DateTimeOffsetExtensions.GetMonthNumber("ABC", true));
        }

        [TestMethod]
        public void GetDate()
        {
            Assert.AreEqual(new DateTimeOffset(2013, 7, 3, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTimeOffset(2013, 6, 30, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2013, 7, 1, 0, 0, 0, DateTimeOffset.Now.Offset).GetDayOfWeek(DayOfWeek.Monday));
        }

        [TestMethod]
        public void IsWeekday()
        {
            Assert.IsTrue(new DateTimeOffset(2014, 4, 28, 0, 0, 0, DateTimeOffset.Now.Offset).IsWeekday());
            Assert.IsTrue(new DateTimeOffset(2014, 4, 22, 0, 0, 0, DateTimeOffset.Now.Offset).IsWeekday());
            Assert.IsFalse(new DateTimeOffset(2014, 5, 3, 0, 0, 0, DateTimeOffset.Now.Offset).IsWeekday());
            Assert.IsTrue(new DateTimeOffset(2014, 5, 3, 0, 0, 0, DateTimeOffset.Now.Offset).IsWeekend());
            Assert.IsFalse(new DateTimeOffset(2014, 4, 22, 0, 0, 0, DateTimeOffset.Now.Offset).IsWeekend());
        }

        [TestMethod]
        public void FirstDayOf()
        {
            Assert.AreEqual(new DateTimeOffset(2009, 12, 27, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek());
            Assert.AreEqual(new DateTimeOffset(2009, 12, 28, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2010, 01, 01, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTimeOffset(2014, 4, 25, 0, 0, 0, DateTimeOffset.Now.Offset), new DateTimeOffset(2014, 04, 30, 0, 0, 0, DateTimeOffset.Now.Offset).FirstDayOfWeek(DayOfWeek.Friday));
        }

        [TestMethod]
        public void IsUtcDateTime()
        {
            var localOffset = DateTimeOffset.Now.Offset;
            System.Console.WriteLine("Local offset is {0}", localOffset);
            System.Console.WriteLine("Local DateTime is {0}", DateTimeOffset.Now);
            System.Console.WriteLine("UTC DateTime is {0}", DateTimeOffset.UtcNow);

            // The AppVeyor CI server is setup such that it's timezone is
            // already UTC, so we have to test a different way to see if local
            // time is not UTC; otherwise, the tests fail when the tests are run
            // under the CI builds.
            if (TimeZoneInfo.Local.BaseUtcOffset == TimeSpan.Zero)
            {
                localOffset = new TimeSpan(4, 0, 0);
            }

            Assert.IsTrue(new DateTimeOffset(2004, 4, 28, 0, 0, 0, TimeSpan.Zero).IsUtcDateTime());
            Assert.IsFalse(new DateTimeOffset(2004, 4, 28, 0, 0, 0, localOffset).IsUtcDateTime());
        }

        [TestMethod]
        public void ToRelativeDateTimeString()
        {
            Assert.AreEqual("Today, 12:00 AM", new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 0, TimeSpan.Zero).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual("Tomorrow", DateTimeOffset.Now.AddDays(1).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual("Yesterday", DateTimeOffset.Now.AddDays(-1).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));

            Assert.AreEqual(DateTimeOffset.Now.AddDays(2).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(2).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-2).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-2).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(3).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(3).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-3).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-3).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(4).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(4).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-4).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-4).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(5).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(5).ToRelativeDateString());
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-5).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-5).ToRelativeDateString());

            Assert.AreEqual(DateTimeOffset.Now.AddDays(2).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(2).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-2).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-2).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(3).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(3).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-3).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-3).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(4).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(4).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-4).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-4).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(5).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(5).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-5).DayOfWeek.ToString(), DateTimeOffset.Now.AddDays(-5).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));

            Assert.AreEqual("2 days from now", DateTimeOffset.Now.AddDays(2).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("2 days ago", DateTimeOffset.Now.AddDays(-2).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("3 days from now", DateTimeOffset.Now.AddDays(3).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("3 days ago", DateTimeOffset.Now.AddDays(-3).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("4 days from now", DateTimeOffset.Now.AddDays(4).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("4 days ago", DateTimeOffset.Now.AddDays(-4).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("5 days from now", DateTimeOffset.Now.AddDays(5).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));
            Assert.AreEqual("5 days ago", DateTimeOffset.Now.AddDays(-5).ToRelativeDateString(Text.RelativeDateFormattingOptions.Days));

            Assert.AreEqual(DateTimeOffset.Now.AddDays(9).ToString("MMM d, yyyy"), DateTimeOffset.Now.AddDays(9).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
            Assert.AreEqual(DateTimeOffset.Now.AddDays(-9).ToString("MMM d, yyyy"), DateTimeOffset.Now.AddDays(-9).ToRelativeDateString(Text.RelativeDateFormattingOptions.DayNames));
        }

    }
}