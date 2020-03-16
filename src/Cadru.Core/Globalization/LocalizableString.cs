//------------------------------------------------------------------------------
// <copyright file="LocalizableString.cs"
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

namespace Cadru.Globalization
{
    using System;
    using System.Globalization;
    using System.Reflection;

    using Cadru.Resources;

    using Contracts;

    /// <summary>
    /// A helper class for providing a localizable string property.
    /// </summary>
    public class LocalizableString
    {
        #region fields
        private string propertyName;
        private string propertyValue;
        private Type resourceType;
        private Func<string> cachedResult;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableString"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property being localized.
        /// This name will be used within exceptions thrown as a result of
        /// localization failures.</param>
        public LocalizableString(string propertyName)
        {
            Requires.NotNullOrWhiteSpace(propertyName, nameof(propertyName));

            this.propertyName = propertyName;
        }
        #endregion

        #region properties

        #region ResourceType
        /// <summary>
        /// Gets or sets the resource type to be used for localization.
        /// </summary>
        public Type ResourceType
        {
            get
            {
                return this.resourceType;
            }
            set
            {
                if (this.resourceType != value)
                {
                    ClearCache();
                    this.resourceType = value;
                }
            }
        }
        #endregion

        #region Value
        /// <summary>
        /// Gets or sets the value of this localizable string.
        /// </summary>
        /// <remarks>This value can be either the literal,
        /// non-localized value, or it can be a resource name
        /// found on the resource type supplied to
        /// <see cref="GetLocalizableValue"/>.</remarks>
        public string Value
        {
            get
            {
                return this.propertyValue;
            }
            set
            {
                if (this.propertyValue != value)
                {
                    ClearCache();
                    this.propertyValue = value;
                }
            }
        }
        #endregion

        #endregion

        #region Methods

        /// <summary>
        ///     Clears any cached values, forcing <see cref="GetLocalizableValue" /> to
        ///     perform evaluation.
        /// </summary>
        private void ClearCache()
        {
            this.cachedResult = null;
        }

        #region IsBadlyConfigured
        private static bool IsBadlyConfigured(Type resourceType, PropertyInfo property)
        {
            // We need to detect bad configurations so that we can throw exceptions accordingly
            bool badlyConfigured = false;

            // Make sure we found the property and it's the correct type, and that the type itself is public
            if (!resourceType.GetTypeInfo().IsVisible || property == null || property.PropertyType != typeof(string))
            {
                badlyConfigured = true;
            }
            else
            {
                // Ensure the getter for the property is available as public static
                var getter = property.GetMethod;
                if (getter == null || !(getter.IsPublic && getter.IsStatic))
                {
                    badlyConfigured = true;
                }
            }

            return badlyConfigured;
        }
        #endregion

        #region GetLocalizableValue
        /// <summary>
        /// Gets the potentially localized value.
        /// </summary>
        /// <remarks>
        /// If <see cref="ResourceType"/> has been specified and <see cref="Value"/> is not
        /// null, then localization will occur and the localized value will be returned.
        /// <para>
        /// If <see cref="ResourceType"/> is null then <see cref="Value"/> will be returned
        /// as a literal, non-localized string.
        /// </para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if localization fails. This can occur if <see cref="ResourceType"/> has been
        /// specified, <see cref="Value"/> is not null, but the resource could not be
        /// accessed.  <see cref="ResourceType"/> must be a public class, and <see cref="Value"/>
        /// must be the name of a public static string property that contains a getter.
        /// </exception>
        /// <returns>
        /// Returns the potentially localized value.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        public string GetLocalizableValue()
        {
            if (this.cachedResult == null)
            {
                // If the property value is null, then just cache that value
                // If the resource type is null, then property value is literal, so cache it
                if (this.propertyValue == null || this.resourceType == null)
                {
                    this.cachedResult = () => this.propertyValue;
                }
                else
                {
                    // Get the property from the resource type for this resource key
                    var property = this.resourceType.GetRuntimeProperty(this.propertyValue);

                    if (IsBadlyConfigured(this.resourceType, property))
                    {
                        var exceptionMessage = String.Format(CultureInfo.CurrentCulture, Strings.InvalidOperation_LocalizationFailed,
                            this.propertyName, this.resourceType.FullName, this.propertyValue);
                        this.cachedResult = () => { throw new InvalidOperationException(exceptionMessage); };
                    }
                    else
                    {
                        // We have a valid property, so cache the resource
                        this.cachedResult = () => (string)property.GetValue(null, null);
                    }
                }
            }

            return this.cachedResult();
        }
        #endregion

        #endregion
    }
}
