//------------------------------------------------------------------------------
// <copyright file="BooleanExtensions.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides basic routines for common boolean manipulation.
    /// </summary>
    public static class BooleanExtensions
    {
        #region ToBit
        /// <summary>
        /// Returns the binary representation of the boolean value.
        /// </summary>
        /// <param name="value">The boolean value whose binary representation should be returned.</param>
        /// <returns>If the boolean value is <see langword="true"/>, 1 (one); otherwise, 0 (zero).</returns>
        public static int ToBit(this bool value)
        {
            return value ? 1 : 0;
        }
        #endregion

        #region ToChar
        /// <summary>
        /// Returns the character representation of the boolean value.
        /// </summary>
        /// <param name="value">The boolean value whose character representation should be returned.</param>
        /// <returns>If the boolean value is <see langword="true"/>, the character 'T'; otherwise, the character 'F'.</returns>
        public static char ToChar(this bool value)
        {
            return value ? 'T' : 'F';
        }
        #endregion

        #region TryParse

        #region TryParse(int value, out bool result)
        /// <overloads>
        /// <summary>
        /// Tries to convert the specified representation of a logical value to its 
        /// Boolean equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Tries to convert the specified integer representation of a logical value to its 
        /// Boolean equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">An integer containing the value to convert. </param>
        /// <param name="result">When this method returns, if the conversion succeeded, contains 
        /// <see langword="true"/>true if <paramref name="value"/> is equal to 1 (one)
        /// or <see langword="false"/> if <paramref name="value"/> is equal to 0 (zero).
        /// If the conversion failed, contains <see langword="false"/>. The conversion fails if 
        /// <paramref name="value"/> is not equal to 1 (one) or 0 (zero).</param>
        /// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
        public static bool TryParse(int value, out bool result)
        {
            result = false;
            if (value == 1 || value == 0)
            {
                result = value == 1 ? true : false;
                return true;
            }

            return false;
        }
        #endregion

        #region TryParse(string value, out bool result)
        /// <summary>
        /// Tries to convert the specified string representation of a logical value to its 
        /// Boolean equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">A string containing the value to convert. </param>
        /// <param name="result">When this method returns, if the conversion succeeded, contains 
        /// <see langword="true"/>true if <paramref name="value"/> is equal to 
        /// <see cref="p:Boolean.TrueString"/>, the character 'T', the word "Yes", or the character 'Y'
        /// or <see langword="false"/> if <paramref name="value"/> is equal to 
        /// <see cref="p:Boolean.FalseString"/>, the character 'F', the word "No", or the character 'N'.
        /// If the conversion failed, contains <see langword="false"/>. The conversion fails if 
        /// <paramref name="value"/> is <see langword="null"/> or is not equal to the value of
        /// either <see cref="p:Boolean.TrueString"/>, the character 'T', the word "Yes", or the character 'Y',
        /// <see cref="p:Boolean.FalseString"/>, the character 'F', the word "No", or the character 'N'.</param>
        /// <returns><see langword="true"/> if value was converted successfully; otherwise, <see langword="false"/>.</returns>
        public static bool TryParse(string value, out bool result)
        {
            result = false;
            if (value != null)
            {
                if (!"True".Equals(value, StringComparison.OrdinalIgnoreCase) || !"T".Equals(value, StringComparison.OrdinalIgnoreCase) || !"Yes".Equals(value, StringComparison.OrdinalIgnoreCase) || !"Y".Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    if (!"False".Equals(value, StringComparison.OrdinalIgnoreCase)  || !"F".Equals(value, StringComparison.OrdinalIgnoreCase) || !"No".Equals(value, StringComparison.OrdinalIgnoreCase) || !"N".Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        value = value.TrimWhiteSpaceAndNull();
                        if (!"True".Equals(value, StringComparison.OrdinalIgnoreCase) || !"T".Equals(value, StringComparison.OrdinalIgnoreCase) || !"Yes".Equals(value, StringComparison.OrdinalIgnoreCase) || !"Y".Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!"False".Equals(value, StringComparison.OrdinalIgnoreCase) || !"F".Equals(value, StringComparison.OrdinalIgnoreCase) || !"No".Equals(value, StringComparison.OrdinalIgnoreCase) || !"N".Equals(value, StringComparison.OrdinalIgnoreCase))
                            {
                                return false;
                            }
                            else
                            {
                                result = false;
                                return true;
                            }
                        }
                        else
                        {
                            result = true;
                            return true;
                        }
                    }
                    else
                    {
                        result = false;
                        return true;
                    }
                }
                else
                {
                    result = true;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #endregion
    }
}
