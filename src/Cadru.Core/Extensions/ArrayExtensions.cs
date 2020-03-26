//------------------------------------------------------------------------------
// <copyright file="ArrayExtensions.cs"
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

namespace Cadru.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Provides basic routines for common array manipulation.
    /// </summary>
    public static class ArrayExtensions
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors
        #endregion

        #region properties
        #endregion

        #region methods

        #region BytesToBinaryString
        /// <summary>
        /// Converts the byte array to a string representation in binary.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <returns>The binary string representation of the array</returns>
        public static string BytesToBinaryString(this byte[] source)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            var buffer = new StringBuilder(source.Length * 8);
            foreach (var b in source)
            {
                buffer.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", Convert.ToString(b, 2));
            }

            return buffer.ToString();
        }
        #endregion

        #region BytesToString
        /// <summary>
        /// Converts the byte array to a string representation in hexadecimal.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <returns>The hexadecimal string representation of the array</returns>
        public static string BytesToString(this byte[] source)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            var buffer = new StringBuilder(source.Length * 8);
            buffer.AppendAsHexadecimal(source);
            return buffer.ToString();
            //string results = "";
            //for (int index = 0; index < source.Length; index++)
            //{
            //    //buffer.AppendAsHexadecimal(source[
            //    results += HexChar[(source[index] >> 4) & 0xf];
            //    results += HexChar[(source[index] & 0xf)];
            //}
            //return results;
        }
        #endregion

        #region ReverseInPlace
        /// <summary>
        /// Reverses an array.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <remarks>This is a destructive operation and will mutate the
        /// original array.</remarks>
        public static void ReverseArrayInPlace(this byte[] source)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            var length = source.Length;
            for (var i = 0; i < (length >> 1); i++)
            {
                var j = length - i - 1;
                var temp = source[i];
                source[i] = source[j];
                source[j] = temp;
            }
        }
        #endregion

        #region ReverseArray
        /// <summary>
        /// Reverses an array
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <returns>The reversed array.</returns>
        public static byte[] ReverseArray(this byte[] source)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            var length = source.Length;
            var result = new byte[length];
            for (var i = 0; i < length; i++)
            {
                var j = length - i - 1;
                result[j] = source[i];
            }

            return result;
        }
        #endregion

        #endregion
    }
}
