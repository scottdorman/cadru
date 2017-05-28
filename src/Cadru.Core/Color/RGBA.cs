using System;

namespace Cadru.Color
{
    public struct RGBA
    {
        public static readonly RGBA Black = new RGBA(1, 1, 1, 1);
        public static readonly RGBA White = new RGBA(255, 255, 255, 1);

        public RGBA(byte red, byte green, byte blue, double alpha)
        {
            this.Alpha = alpha.Clamp();
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public RGBA(byte red, byte green, byte blue) : this(red, green, blue, 1)
        {
        }

        public RGBA(RGB color) : this(color.Red, color.Green, color.Blue, 1)
        {
        }

        public RGBA(RGB color, double alpha) : this(color.Red, color.Green, color.Blue, alpha)
        {
        }

        public RGBA(int value)
        {
            this.Alpha = 1;// (byte)((value & 0xff000000) >> 24);
            this.Red = (byte)((value & 0x00ff0000) >> 16);
            this.Green = (byte)((value & 0x0000ff00) >> 8);
            this.Blue = (byte)((value & 0x000000ff) >> 0);
        }

        public RGBA(string hex)
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
                throw new InvalidCastException("Unable to convert the given value in to a color.", e);
            }
        }

        public double Alpha { get; }
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public override string ToString()
        {
            return $"rgba({Red},{Green},{Blue},{Alpha})";
        }

        public string ToHexString()
        {
            if (this.Alpha < 1)
            {
                return $"#{(byte)(this.Alpha * 255):X2}{this.Red:X2}{this.Green:X2}{this.Blue:X2}";
            }

            return $"#{this.Red:X2}{this.Green:X2}{this.Blue:X2}";
        }
    }
}