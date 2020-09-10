//------------------------------------------------------------------------------
// <copyright file="ColorUtils.cs"
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

namespace Cadru.Color
{
    /// <summary>
    /// Methods for working with color values.
    /// </summary>
    public static class ColorUtils
    {
        private const double rgbThreshold = 0.179;

        /// <summary>
        /// Generate a color derived from a string value.
        /// </summary>
        /// <param name="value">The value to generate a color for.</param>
        /// <param name="defaultColor">The default color.</param>
        /// <returns>An <see cref="RGB"/> color representing <paramref name="value"/>.</returns>
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

        /// <summary>
        /// Gets the best foreground color given the specified background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The best <see cref="RGB"/> foreground color.</returns>
        public static RGB GetBestForegroundColor(this RGB color)
        {
            return color.Luminance() > rgbThreshold ? RGB.Black : RGB.White;
        }

        /// <summary>
        /// Gets the best foreground color given the specified background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The best <see cref="RGBA"/> foreground color.</returns>
        public static RGBA GetBestForegroundColor(this RGBA color)
        {
            return color.Luminance() > rgbThreshold ? RGBA.Black : RGBA.White;
        }

        /// <summary>
        /// Gets the best foreground color given the specified background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        /// <returns>The best <see cref="HSV"/> foreground color.</returns>
        public static HSV GetBestForegroundColor(this HSV color)
        {
            return (color.Value < .5) ? HSV.White : HSV.Black;
        }

        /// <summary>
        /// Gets the luminance of the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The luminance value of the color.</returns>
        public static double Luminance(this RGB color)
        {
            return Luminance(color.Red, color.Green, color.Blue);
        }

        /// <summary>
        /// Gets the luminance of the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The luminance value of the color.</returns>
        public static double Luminance(this RGBA color)
        {
            return Luminance(color.Red, color.Green, color.Blue);
        }

        internal static double Clamp(this double value, double max = 1.0, double min = 0.0)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return Double.IsNaN(value) ? min : value;
        }

        /// <summary>
        /// Gets the luminance of the specified color.
        /// </summary>
        /// <param name="red">The red color components</param>
        /// <param name="green">The green color component</param>
        /// <param name="blue">The blue color component</param>
        /// <returns>The luminance value of the color.</returns>
        private static double Luminance(byte red, byte green, byte blue)
        {
            return 0.299 * red + 0.587 * green + 0.114 * blue;
        }
    }
}