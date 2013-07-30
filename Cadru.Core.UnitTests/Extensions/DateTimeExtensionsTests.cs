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
        private static int GCWeekOfYear(System.DateTime date, CalendarWeekRule rule, DayOfWeek firstDayOfWeek = (DayOfWeek)(-1))
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
            Assert.AreEqual(new DateTime(2013, 7, 30), DateTimeExtensions.AddWeekdays(date, 2));
            Assert.AreEqual(new DateTime(2013, 7, 24), DateTimeExtensions.AddWeekdays(date, -2));
        }

        [TestMethod]
        public void AddQuarters()
        {
            var date = new DateTime(2013, 7, 26).Date;
            Assert.AreEqual(new DateTime(2014, 1, 26), DateTimeExtensions.AddQuarters(date, 2));
            Assert.AreEqual(new DateTime(2013, 1, 26), DateTimeExtensions.AddQuarters(date, -2));
        }

        [TestMethod]
        public void AddWeeks()
        {
            var date = new DateTime(2013, 7, 26).Date;
            Assert.AreEqual(new DateTime(2013, 8, 9), DateTimeExtensions.AddWeeks(date, 2));
            Assert.AreEqual(new DateTime(2013, 7, 12), DateTimeExtensions.AddWeeks(date, -2));
        }

        [TestMethod]
        public void FirstDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 01, 01)));
            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 02, 01)));
            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 03, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 04, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 05, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 06, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 07, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 08, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 09, 01)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 10, 01)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 11, 01)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 12, 01)));

            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 01, 31)));
            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 02, 28)));
            Assert.AreEqual(new DateTime(2010, 04, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 03, 31)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 04, 30)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 05, 31)));
            Assert.AreEqual(new DateTime(2010, 07, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 06, 30)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 07, 31)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 08, 31)));
            Assert.AreEqual(new DateTime(2010, 10, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 09, 30)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 10, 31)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 11, 30)));
            Assert.AreEqual(new DateTime(2011, 01, 01), DateTimeExtensions.FirstDayOfNextQuarter(new DateTime(2010, 12, 31)));
        }

        [TestMethod]
        public void DayOfQuarter()
        {
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 01, 01)));
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 02, 01)));
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 03, 01)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 04, 01)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 05, 01)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 06, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 07, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 08, 01)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 09, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 10, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 11, 01)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 12, 01)));
                                                      
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 01, 31)));
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 02, 28)));
            Assert.AreEqual(new DateTime(2010, 01, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 03, 31)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 04, 30)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 05, 31)));
            Assert.AreEqual(new DateTime(2010, 04, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 06, 30)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 07, 31)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 08, 31)));
            Assert.AreEqual(new DateTime(2010, 07, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 09, 30)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 10, 31)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 11, 30)));
            Assert.AreEqual(new DateTime(2010, 10, 01),DateTimeExtensions.FirstDayOfQuarter(new DateTime(2010, 12, 31)));


            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 01, 01)));
            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 02, 01)));
            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 03, 01)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 04, 01)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 05, 01)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 06, 01)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 07, 01)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 08, 01)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 09, 01)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 10, 01)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 11, 01)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 12, 01)));

            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 01, 31)));
            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 02, 28)));
            Assert.AreEqual(new DateTime(2010, 03, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 03, 31)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 04, 30)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 05, 31)));
            Assert.AreEqual(new DateTime(2010, 06, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 06, 30)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 07, 31)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 08, 31)));
            Assert.AreEqual(new DateTime(2010, 09, 30), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 09, 30)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 10, 31)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 11, 30)));
            Assert.AreEqual(new DateTime(2010, 12, 31), DateTimeExtensions.LastDayOfQuarter(new DateTime(2010, 12, 31)));
        }

        [TestMethod]
        public void GetWeekOfYearExceptions()
        {
            try
            {
                Assert.AreEqual(1, DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)8));
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "firstDayOfWeek")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                Assert.AreEqual(1, DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek, (DayOfWeek)(-1)));
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "firstDayOfWeek")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                Assert.AreEqual(1, DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), (CalendarWeekRule)24));
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "rule")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void GetWeekOfYear()
        {
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFourDayWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFourDayWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek));


            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstDay));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstDay));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstDay), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstDay));


            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFullWeek));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFullWeek));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFullWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFullWeek));



            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 01, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 02, 28)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 03, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 04, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 05, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 06, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 07, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 08, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 09, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 10, 31)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 11, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 26)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2010, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2010, 12, 31)));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 01, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 02, 28)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 03, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 04, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 05, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 06, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 07, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 07, 31)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 08, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 09, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 10, 31)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 11, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 26)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2008, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2008, 12, 31)));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 01, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 01, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 02, 28), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 02, 28)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 03, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 03, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 04, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 04, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 05, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 05, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 06, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 06, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 07, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 07, 31)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 08, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 08, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 09, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 09, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 10, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 10, 31)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 11, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 11, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 01), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 01)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 26), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 26)));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31)));

            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Wednesday));
            Assert.AreEqual(GCWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday), DateTimeExtensions.GetWeekOfYear(new DateTime(2005, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Thursday));

        }

        [TestMethod]
        public void Quarter()
        {
            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 01, 01)));
            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 02, 01)));
            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 03, 01)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 04, 01)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 05, 01)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 06, 01)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 07, 01)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 08, 01)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 09, 01)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 10, 01)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 11, 01)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 12, 01)));

            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 01, 31)));
            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 02, 28)));
            Assert.AreEqual(1, DateTimeExtensions.Quarter(new DateTime(2010, 03, 31)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 04, 30)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 05, 31)));
            Assert.AreEqual(2, DateTimeExtensions.Quarter(new DateTime(2010, 06, 30)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 07, 31)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 08, 31)));
            Assert.AreEqual(3, DateTimeExtensions.Quarter(new DateTime(2010, 09, 30)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 10, 31)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 11, 30)));
            Assert.AreEqual(4, DateTimeExtensions.Quarter(new DateTime(2010, 12, 31)));
        }

        [TestMethod]
        public void DaysInMonth()
        {
            Assert.AreEqual(31, DateTimeExtensions.DaysInMonth(new DateTime(2013, 7, 1)));
            Assert.AreEqual(30, DateTimeExtensions.DaysInMonth(new DateTime(2013, 9, 1)));
            Assert.AreEqual(28, DateTimeExtensions.DaysInMonth(new DateTime(2013, 2, 1)));
            Assert.AreEqual(29, DateTimeExtensions.DaysInMonth(new DateTime(2004, 2, 1)));
        }

        [TestMethod]
        public void Elapsed()
        {
            Assert.AreEqual(1, DateTimeExtensions.Elapsed(DateTime.Now.AddDays(-1)).Days);
            Assert.AreEqual(7, DateTimeExtensions.Elapsed(DateTime.Now.AddDays(-7)).Days);
            Assert.AreEqual(-7, DateTimeExtensions.Elapsed(DateTime.Now.AddDays(7)).Days);
        }

        [TestMethod]
        public void DayOfMonth()
        {
            Assert.AreEqual(new DateTime(2013, 7, 1), DateTimeExtensions.FirstDayOfMonth(new DateTime(2013, 7, 25)));
            Assert.AreEqual(new DateTime(2013, 7, 1), DateTimeExtensions.FirstDayOfMonth(new DateTime(2013, 7, 1)));
            Assert.AreEqual(new DateTime(2013, 7, 1), DateTimeExtensions.FirstDayOfMonth(new DateTime(2013, 7, 31)));

            Assert.AreEqual(new DateTime(2013, 7, 31), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 7, 25)));
            Assert.AreEqual(new DateTime(2013, 7, 31), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 7, 1)));
            Assert.AreEqual(new DateTime(2013, 7, 31), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 7, 31)));
            Assert.AreEqual(new DateTime(2013, 9, 30), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 9, 25)));
            Assert.AreEqual(new DateTime(2013, 9, 30), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 9, 1)));
            Assert.AreEqual(new DateTime(2013, 9, 30), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 9, 30)));
            Assert.AreEqual(new DateTime(2013, 2, 28), DateTimeExtensions.LastDayOfMonth(new DateTime(2013, 2, 1)));
            Assert.AreEqual(new DateTime(2004, 2, 29), DateTimeExtensions.LastDayOfMonth(new DateTime(2004, 2, 1)));
        }

        [TestMethod]
        public void DaysOfWeek()
        {
            Assert.AreEqual(new DateTime(2013, 6, 30), DateTimeExtensions.FirstDayOfWeek(new DateTime(2013, 7, 1)));
            Assert.AreEqual(new DateTime(2013, 7, 6), DateTimeExtensions.LastDayOfWeek(new DateTime(2013, 7, 1)));

            Assert.AreEqual(new DateTime(2013, 7, 1), DateTimeExtensions.FirstDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2013, 7, 7), DateTimeExtensions.LastDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Monday));

            Assert.AreEqual(new DateTime(2013, 6, 30), DateTimeExtensions.FirstDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 7, 6), DateTimeExtensions.LastDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Sunday));
        }

        [TestMethod]
        public void DayOfYear()
        {
            Assert.AreEqual(new DateTime(2013, 1, 1), DateTimeExtensions.FirstDayOfYear(new DateTime(2013, 7, 12)));
            Assert.AreEqual(new DateTime(2013, 12, 31), DateTimeExtensions.LastDayOfYear(new DateTime(2013, 7, 12)));
        }

        [TestMethod]
        public void LeapYears()
        {
            Assert.IsTrue(DateTimeExtensions.IsLeapYear(new DateTime(2004, 2, 1)));
            Assert.IsFalse(DateTimeExtensions.IsLeapMonth(new DateTime(2012, 6, 18)));
            Assert.IsTrue(DateTimeExtensions.IsLeapDay(new DateTime(2004, 2, 29)));
            Assert.IsFalse(DateTimeExtensions.IsLeapDay(new DateTime(2004, 2, 28)));

            Assert.IsFalse(DateTimeExtensions.IsLeapYear(new DateTime(2013, 2, 1)));
            Assert.IsFalse(DateTimeExtensions.IsLeapMonth(new DateTime(2013, 2, 1)));
            Assert.IsFalse(DateTimeExtensions.IsLeapDay(new DateTime(2013, 2, 2)));
        }

        [TestMethod]
        public void MonthNames()
        {
            var months = DateTimeExtensions.GetMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            months = DateTimeExtensions.GetAbbreviatedMonthNames();
            CustomAssert.IsNotEmpty((ICollection)months);

            Assert.AreEqual("July", DateTimeExtensions.GetMonthName(new DateTime(2013, 7, 1)));
            Assert.AreEqual("Jul", DateTimeExtensions.GetAbbreviatedMonthName(new DateTime(2013, 7, 1)));

            Assert.AreEqual(7, DateTimeExtensions.GetMonthNumber("July", false));
            Assert.AreEqual(7, DateTimeExtensions.GetMonthNumber("Jul", true));
            Assert.AreEqual(0, DateTimeExtensions.GetMonthNumber("ABC", false));
            Assert.AreEqual(0, DateTimeExtensions.GetMonthNumber("ABC", true));
        }

        [TestMethod]
        public void GetDate()
        {
            Assert.AreEqual(new DateTime(2013, 7, 3), DateTimeExtensions.GetDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2013, 6, 30), DateTimeExtensions.GetDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2013, 7, 1), DateTimeExtensions.GetDayOfWeek(new DateTime(2013, 7, 1), DayOfWeek.Monday));
        }
    }
}
