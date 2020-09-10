//------------------------------------------------------------------------------
// <copyright file="Column.cs"
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
using System.Globalization;

namespace Cadru.Data.Csv
{
    /// <summary>
    /// Metadata about a CSV column.
    /// </summary>
    public class Column
    {
        private Type type;
        private string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column()
        {
            this.Type = typeof(string);
            this.Culture = CultureInfo.CurrentCulture;
            this.NumberStyles = NumberStyles.Any;
            this.DateTimeStyles = DateTimeStyles.None;
            this.DateParseExact = null;
        }

        public CultureInfo Culture { get; set; }

        public string DateParseExact { get; set; }

        public DateTimeStyles DateTimeStyles { get; set; }

        /// <summary>
        /// Get or set the default value of the column.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        public NumberStyles NumberStyles { get; set; }

        /// <summary>
        /// Get or set the override value of the column.
        /// </summary>
        public string OverrideValue { get; set; }

        /// <summary>
        /// Get or set the type.
        /// </summary>
        public Type Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                this.typeName = value.Name;
            }
        }

        /// <summary>
        /// Converts the value into the column type.
        /// </summary>
        /// <param name="value">Value to use</param>
        /// <returns>Converted value.</returns>
        public object Convert(string value)
        {
            this.TryConvert(value, out var x);

            return x;
        }

        /// <summary>
        /// Converts the value into the column type.
        /// </summary>
        /// <param name="value">Value to use</param>
        /// <param name="result">Object to hold the converted value.</param>
        /// <returns>true if the conversion was successful, otherwise false.</returns>
        public bool TryConvert(string value, out object result)
        {
            bool converted;

            switch (this.typeName)
            {
                case "Guid":
                    try
                    {
                        result = new Guid(value);
                        converted = true;
                    }
                    catch
                    {
                        result = Guid.Empty;
                        converted = false;
                    }
                    break;

                case "Byte[]":
                    {
                        try
                        {
                            result = System.Convert.FromBase64String(value);
                            converted = true;
                        }
                        catch
                        {
                            result = new byte[0];
                            converted = false;
                        }
                    }
                    break;

                case "Boolean":
                    {
                        int x;
                        converted = Int32.TryParse(value, this.NumberStyles, this.Culture, out x);
                        if (converted)
                        {
                            result = x != 0;
                        }
                        else
                        {
                            bool y;
                            converted = Boolean.TryParse(value, out y);
                            result = y;
                        }
                    }
                    break;

                case "Int32":
                    {
                        int x;
                        converted = Int32.TryParse(value, this.NumberStyles, this.Culture, out x);
                        result = x;
                    }
                    break;

                case "Int64":
                    {
                        long x;
                        converted = Int64.TryParse(value, this.NumberStyles, this.Culture, out x);
                        result = x;
                    }
                    break;

                case "Single":
                    {
                        float x;
                        converted = Single.TryParse(value, this.NumberStyles, this.Culture, out x);
                        result = x;
                    }
                    break;

                case "Double":
                    {
                        double x;
                        converted = Double.TryParse(value, this.NumberStyles, this.Culture, out x);
                        result = x;
                    }
                    break;

                case "Decimal":
                    {
                        decimal x;
                        converted = Decimal.TryParse(value, this.NumberStyles, this.Culture, out x);
                        result = x;
                    }
                    break;

                case "DateTime":
                    {
                        DateTime x;
                        if (!String.IsNullOrEmpty(this.DateParseExact))
                        {
                            converted = DateTime.TryParseExact(value, this.DateParseExact, this.Culture, this.DateTimeStyles, out x);
                        }
                        else
                        {
                            converted = DateTime.TryParse(value, this.Culture, this.DateTimeStyles, out x);
                        }
                        result = x;
                    }
                    break;

                default:
                    converted = false;
                    result = value;
                    break;
            }

            return converted;
        }
    }
}