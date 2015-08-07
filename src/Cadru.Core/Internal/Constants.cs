using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadru.Internal
{
    internal class Constants
    {
        public const int SecondsPerMinute = 60;
        public const int SecondsPerHour = SecondsPerMinute * 60; //3,600
        public const int SecondsPerDay = SecondsPerHour * 24; //86,400
        public const int ApproximateSecondsPerMonth = SecondsPerDay * 30; //2,592,000
        public const int ApproximateSecondsPerYear = ApproximateSecondsPerMonth * 12; //31,194,000
    }
}
