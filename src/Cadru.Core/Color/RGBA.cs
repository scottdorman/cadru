//------------------------------------------------------------------------------
// <copyright file="RGBA.cs"
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
    public struct RGBA
    {
        /// <summary>
        /// The color black as an <see cref="RGBA"/> value.
        /// </summary>
        public static readonly RGBA Black = new RGBA(1, 1, 1, 1);

        /// <summary>
        /// The color white as an <see cref="RGBA"/> value.
        /// </summary>
        public static readonly RGBA White = new RGBA(255, 255, 255, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="red">The red color component.</param>
        /// <param name="green">The green color component.</param>
        /// <param name="blue">The blue color component.</param>
        /// <param name="alpha">The alpha channel component.</param>
        public RGBA(byte red, byte green, byte blue, double alpha)
        {
            this.Alpha = alpha.Clamp();
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="red">The red color component.</param>
        /// <param name="green">The green color component.</param>
        /// <param name="blue">The blue color component.</param>
        public RGBA(byte red, byte green, byte blue) : this(red, green, blue, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="color">An <see cref="RGB"/> color.</param>
        public RGBA(RGB color) : this(color.Red, color.Green, color.Blue, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="color">An <see cref="RGB"/> color.</param>
        /// <param name="alpha">The alpha channel component.</param>
        public RGBA(RGB color, double alpha) : this(color.Red, color.Green, color.Blue, alpha)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="value">An integer value that represents the color.</param>
        public RGBA(int value)
        {
            this.Alpha = 1;
            this.Red = (byte)((value & 0x00ff0000) >> 16);
            this.Green = (byte)((value & 0x0000ff00) >> 8);
            this.Blue = (byte)((value & 0x000000ff) >> 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> structure.
        /// </summary>
        /// <param name="hex">A string containing a hexadecimal color representation.</param>
        public RGBA(string hex)
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
                    this.Alpha = 1;
                    this.Red = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Green = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Blue = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                }
                else if (hex.Length == 8)
                {
                    this.Alpha = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Red = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Green = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    this.Blue = Byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                }
                else
                {
                    this.Alpha = 1;
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
        /// The alpha channel component.
        /// </summary>
        public double Alpha
        {
            get;
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
        public static bool operator !=(RGBA left, RGBA right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator ==(RGBA left, RGBA right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is RGBA rgba)
            {
                return this.Equals(rgba);
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
        public bool Equals(RGBA other)
        {
            if (this.Red != other.Red)
            {
                return false;
            }

            return this.Green == other.Green && this.Blue == other.Blue && this.Alpha == other.Alpha;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Red ^ this.Blue ^ this.Green;
        }

        /// <summary>
        /// Returns a string representing the color in hex format.
        /// </summary>
        /// <returns>
        /// If the <see cref="Alpha"/> channel is less than one, a string
        /// representing the color in #aarrggbb format; otherwise, a string
        /// representing the color in #rrggbb format.
        /// </returns>
        public string ToHexString()
        {
            if (this.Alpha < 1)
            {
                return $"#{(byte)(this.Alpha * 255): X2}{this.Red: X2}{this.Green: X2}{this.Blue: X2}";
            }

            return $"#{this.Red: X2}{this.Green: X2}{this.Blue: X2}";
        }

        /// <summary>
        /// Returns a string representing the color in rgba(r,g,b) format.
        /// </summary>
        /// <returns>A string representing the color in rgba(r,g,b) format.</returns>
        public override string ToString()
        {
            return $"rgba({this.Red},{this.Green},{this.Blue},{this.Alpha})";
        }
    }
}