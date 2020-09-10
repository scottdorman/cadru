//------------------------------------------------------------------------------
// <copyright file="HSV.cs"
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

namespace Cadru.Color
{
    /// <summary>
    /// The HSV color model.
    /// </summary>
    public struct HSV
    {
        /// <summary>
        /// The color black as an <see cref="HSV" /> value.
        /// </summary>
        public static readonly HSV Black = new HSV(1, 1, 0);

        /// <summary>
        /// The color white as an <see cref="HSV" /> value.
        /// </summary>
        public static readonly HSV White = new HSV(0, 0, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="HSV" /> structure.
        /// </summary>
        /// <param name="hue">The hue component.</param>
        /// <param name="saturation">The saturation component.</param>
        /// <param name="value">The value component.</param>
        public HSV(double hue, double saturation, double value)
        {
            this.Hue = hue.Clamp(360, 0);
            this.Saturation = saturation.Clamp();
            this.Value = value.Clamp();
        }

        /// <summary>
        /// The hue color component.
        /// </summary>
        public double Hue
        {
            get;
        }

        /// <summary>
        /// The saturation color component.
        /// </summary>
        public double Saturation
        {
            get;
        }

        /// <summary>
        /// The value color component.
        /// </summary>
        public double Value
        {
            get;
        }

        /// <inheritdoc/>
        public static bool operator !=(HSV left, HSV right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator ==(HSV left, HSV right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is HSV hsv)
            {
                return this.Equals(hsv);
            }

            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns><see langword="true" /> if <paramref name="other" /> and this
        /// instance represent the same value; otherwise, <see
        /// langword="false" />.</returns>
        public bool Equals(HSV other)
        {
            if (this.Hue != other.Hue)
            {
                return false;
            }

            return this.Saturation == other.Saturation && this.Value == other.Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Hue.GetHashCode() ^ this.Saturation.GetHashCode() ^ this.Value.GetHashCode();
        }
    }
}