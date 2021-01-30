//------------------------------------------------------------------------------
// <copyright file="RGB.cs"
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

using Cadru.Resources;

namespace Cadru.Color
{
    /// <summary>
    /// The RGB color model.
    /// </summary>
    public struct RGB
    {
        /// <summary>
        /// The color black as an <see cref="RGB"/> value.
        /// </summary>
        public static readonly RGB Black = new RGB(1, 1, 1);

        /// <summary>
        /// The color white as an <see cref="RGB"/> value.
        /// </summary>

        public static readonly RGB White = new RGB(255, 255, 255);

        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> structure.
        /// </summary>
        /// <param name="red">The red color component.</param>
        /// <param name="green">The green color component.</param>
        /// <param name="blue">The blue color component.</param>
        public RGB(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> structure.
        /// </summary>
        /// <param name="value">An integer value that represents the color.</param>
        public RGB(int value)
        {
            this.Red = (byte)((value & 0x00ff0000) >> 16);
            this.Green = (byte)((value & 0x0000ff00) >> 8);
            this.Blue = (byte)((value & 0x000000ff) >> 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> structure.
        /// </summary>
        /// <param name="hex">A string containing a hexadecimal color representation.</param>
        public RGB(string hex)
        {
            // Hex color values must start with a # and be followed by 6 digits
            // (2 for each color channel). If there are 8 digits, then the color
            // includes an alpha channel.
            if (!hex.StartsWith("#"))
            {
                throw new InvalidCastException(Strings.InvalidCast_Color);
            }

            try
            {
                hex = hex.TrimStart('#');
                if (hex.Length == 6)
                {
                    this.Red = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Green = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Blue = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                }
                else
                {
                    this.Red = 0;
                    this.Green = 0;
                    this.Blue = 0;
                }
            }
            catch (Exception e)
            {
                throw new InvalidCastException(Strings.InvalidCast_Color, e);
            }
        }

        /// <summary>
        /// The blue color component.
        /// </summary>
        public byte Blue
        {
            get;
        }

        /// <summary>
        /// The green color component.
        /// </summary>
        public byte Green
        {
            get;
        }

        /// <summary>
        /// The red color component.
        /// </summary>
        public byte Red
        {
            get;
        }

        /// <inheritdoc/>
        public static bool operator !=(RGB left, RGB right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator ==(RGB left, RGB right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is RGB rgb)
            {
                return this.Equals(rgb);
            }

            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> and this instance
        /// represent the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(RGB other)
        {
            if (this.Red != other.Red)
            {
                return false;
            }

            return this.Green == other.Green && this.Blue == other.Blue;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Red ^ this.Blue ^ this.Green;
        }

        /// <summary>
        /// Returns a string representing the color in hex format.
        /// </summary>
        /// <returns>A string representing the color in #rrggbb format.</returns>
        public string ToHexString()
        {
            return $"#{this.Red: X2}{this.Green: X2}{this.Blue: X2}";
        }

        /// <summary>
        /// Returns a string representing the color in rgb(r,g,b) format.
        /// </summary>
        /// <returns>A string representing the color in rgb(r,g,b) format.</returns>
        public override string ToString()
        {
            return $"rgb({this.Red},{this.Green},{this.Blue})";
        }
    }
}