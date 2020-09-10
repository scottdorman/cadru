using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Cadru.Extensions;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cadru.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// Extensions for working with <see cref="IHtmlHelper"/> in Razor files.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <inheritdoc />
        public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(this IHtmlHelper htmlHelper, string uiHint) where TEnum : struct
        {
            return GetEnumSelectList(htmlHelper, typeof(TEnum), uiHint);
        }

        /// <inheritdoc />
#pragma warning disable IDE0060 // Remove unused parameter

        public static IEnumerable<SelectListItem> GetEnumSelectList(this IHtmlHelper htmlHelper, Type enumType, string uiHint)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            Contracts.Requires.NotNull(uiHint, nameof(uiHint));
            Contracts.Requires.IsTrue(enumType.IsEnum && !enumType.IsFlagsEnum());

            var selectList = new List<SelectListItem>();
            foreach (var keyValuePair in GetEnumDisplayNamesAndValues(enumType, uiHint))
            {
                var selectListItem = new SelectListItem
                {
                    Text = keyValuePair.Key,
                    Value = keyValuePair.Value,
                };

                selectList.Add(selectListItem);
            }

            return selectList;
        }

        private static List<KeyValuePair<string, string>> GetEnumDisplayNamesAndValues(Type underlyingType, string uiHint)
        {
            // EnumDisplayNamesAndValues and EnumNamesAndValues
            //
            // Order EnumDisplayNamesAndValues to match Enum.GetNames(). That method orders by absolute value,
            // then its behavior is undefined (but hopefully stable). Add to EnumNamesAndValues in same order but
            // Dictionary does not guarantee order will be preserved.
            var displayNamesAndValues = new List<KeyValuePair<string, string>>();
            foreach (var name in Enum.GetNames(underlyingType))
            {
                var field = underlyingType.GetField(name);
                if (HasUiHint(field, uiHint))
                {
                    var displayName = GetDisplayName(field);
                    var value = ((Enum)field.GetValue(obj: null)).ToString("d");

                    displayNamesAndValues.Add(new KeyValuePair<string, string>(displayName, value));
                }
            }

            return displayNamesAndValues;
        }

        private static string GetDisplayName(FieldInfo field)
        {
            var display = field.GetCustomAttribute<DisplayAttribute>(inherit: false);
            if (display != null)
            {
                var name = display.GetName();
                if (!String.IsNullOrEmpty(name))
                {
                    return name;
                }
            }

            return field.Name;
        }

        private static bool HasUiHint(FieldInfo field, string uiHint)
        {
            return field.GetCustomAttributes<UIHintAttribute>().Any(hint => String.Compare(hint.UIHint, uiHint, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}