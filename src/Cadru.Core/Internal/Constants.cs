//------------------------------------------------------------------------------
// <copyright file="Constants.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
