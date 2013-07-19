using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

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
        public void FirstDayOfQuarter()
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
        public void LastDayOfQuarter()
        {
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
    }
}
