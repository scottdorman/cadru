//------------------------------------------------------------------------------
// <copyright file="HSV.cs"
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

namespace Cadru.Color
{
    public struct HSV
    {
        public static readonly HSV Black = new HSV(1, 1, 0);

        public static readonly HSV White = new HSV(0, 0, 1);

        public HSV(double hue, double saturation, double value)
        {
            this.Hue = hue.Clamp(360, 0);
            this.Saturation = saturation.Clamp();
            this.Value = value.Clamp();
        }

        public double Hue
        {
            get;
        }

        public double Saturation
        {
            get;
        }

        public double Value
        {
            get;
        }
    }
}