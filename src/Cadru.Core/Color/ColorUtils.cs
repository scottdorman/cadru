//------------------------------------------------------------------------------
// <copyright file="ColorUtils.cs"
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
    using System;

    public static class ColorUtils
    {
        private const double rgbThreshold = 0.179;

        public static double Clamp(this double value, double max = 1.0, double min = 0.0)
        {
            return (value < min) ? min : (value > max) ? max : (Double.IsNaN(value)) ? min : value;
        }

        public static RGB GenerateColor(string value, string defaultColor = "#142583")
        {
            var color = new RGB(defaultColor);
            if (!String.IsNullOrWhiteSpace(value))
            {
                var hash = 0;
                for (var i = 0; i < value.Length; i++)
                {
                    hash = value[i] + ((hash << 5) - hash);
                }

                color = new RGB(hash);
            }

            return color;
        }

        public static RGB GetBestForegroundColor(this RGB color)
        {
            return color.Luminance() > rgbThreshold ? RGB.Black : RGB.White;
            //return (color.Red * 0.299 + color.Green * 0.587 + color.Blue * 0.114 > 160) ? RGB.White : RGB.Black;
        }

        public static RGBA GetBestForegroundColor(this RGBA color)
        {
            return color.Luminance() > rgbThreshold ? RGBA.Black : RGBA.White;
            //return (color.Red * 0.299 + color.Green * 0.587 + color.Blue * 0.114 > 160) ? RGBA.White : RGBA.Black;
        }

        public static HSV GetBestForegroundColor(this HSV color)
        {
            return (color.Value < .5) ? HSV.White : HSV.Black;
        }

        public static double Luminance(this RGB color)
        {
            return Luminance(color.Red, color.Green, color.Blue);
        }

        public static double Luminance(this RGBA color)
        {
            return Luminance(color.Red, color.Green, color.Blue);
        }

        private static double Luminance(byte red, byte green, byte blue)
        {
            //return 0.2126 * red + 0.7152 * green + 0.0722 * blue;
            return 0.299 * red + 0.587 * green + 0.114 * blue;
        }
    }
}