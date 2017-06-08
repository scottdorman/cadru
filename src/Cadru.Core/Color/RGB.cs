//------------------------------------------------------------------------------
// <copyright file="RGB.cs"
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

    public struct RGB
    {
        public static readonly RGB Black = new RGB(1, 1, 1);

        public static readonly RGB White = new RGB(255, 255, 255);

        public RGB(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public RGB(int value)
        {
            this.Red = (byte)((value & 0x00ff0000) >> 16);
            this.Green = (byte)((value & 0x0000ff00) >> 8);
            this.Blue = (byte)((value & 0x000000ff) >> 0);
        }

        public RGB(string hex)
        {
            // Hex color values must start with a # and be followed
            // by 6 digits (2 for each color channel). If there are
            // 8 digits, then the color includes an alpha channel.
            if (!hex.StartsWith("#"))
            {
                throw new InvalidCastException("Unable to convert the given value in to a color.");
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
                throw new InvalidCastException("Unable to convert the given value in to a color.", e);
            }
        }

        public byte Red
        {
            get;
        }

        public byte Green
        {
            get;
        }

        public byte Blue
        {
            get;
        }

        public override string ToString()
        {
            return $"rgb({Red},{Green},{Blue})";
        }

        public string ToHexString()
        {
            return $"#{this.Red: X2}{this.Green: X2}{this.Blue: X2}";
        }
    }
}