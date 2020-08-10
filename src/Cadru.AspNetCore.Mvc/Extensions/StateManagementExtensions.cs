using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Newtonsoft.Json;

namespace Cadru.AspNetCore.Mvc.Extensions
{
    /// <summary>
    /// Extension methods that aid in working with ASP.NET Core session state by
    /// providing a common abstraction layer for <see cref="ISession"></see>,
    /// <see cref="ITempDataDictionary"></see>, and <see
    /// cref="ViewDataDictionary"></see>.
    /// </summary>
    public static class StateManagementExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ITempDataDictionary,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static T Get<T>(this ITempDataDictionary storageProvider)
        {
            storageProvider.TryGetValue<T>(typeof(T).FullName, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        public static T Get<T>(this ITempDataDictionary storageProvider, string key)
        {
            storageProvider.TryGetValue<T>(key, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ISession,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static T Get<T>(this ISession storageProvider)
        {
            storageProvider.TryGetValue<T>(typeof(T).FullName, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        public static T Get<T>(this ISession storageProvider, string key)
        {
            storageProvider.TryGetValue<T>(key, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ViewDataDictionary,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static T Get<T>(this ViewDataDictionary storageProvider)
        {
            storageProvider.TryGetValue<T>(typeof(T).FullName, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        public static T Get<T>(this ViewDataDictionary storageProvider, string key)
        {
            storageProvider.TryGetValue<T>(key, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key, without marking
        /// the key for deletion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see
        /// cref="Peek{T}(ITempDataDictionary, string)"></see> where the key is
        /// the full name of <typeparamref name="T"/>.
        /// </para>
        /// </remarks>
        public static T Peek<T>(this ITempDataDictionary storageProvider)
        {
            storageProvider.TryPeekValue<T>(typeof(T).FullName, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key, without marking
        /// the key for deletion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        public static T Peek<T>(this ITempDataDictionary storageProvider, string key)
        {
            storageProvider.TryPeekValue<T>(key, out var value);
            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ISession,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/> and is provided for easy of use when switching between
        /// different storage providers.
        /// </para>
        /// </remarks>
        public static T Peek<T>(this ISession storageProvider)
        {
            return storageProvider.Get<T>(typeof(T).FullName);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ISession,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/> and is provided for easy of use when switching between
        /// different storage providers.
        /// </para>
        /// </remarks>
        public static T Peek<T>(this ISession storageProvider, string key)
        {
            return storageProvider.Get<T>(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ViewDataDictionary,
        /// string)"></see> where the key is the full name of <typeparamref
        /// name="T"/> and is provided for easy of use when switching between
        /// different storage providers.
        /// </para>
        /// </remarks>
        public static T Peek<T>(this ViewDataDictionary storageProvider)
        {
            return storageProvider.Get<T>();
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type
        /// of the value parameter.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This is equivalent to calling <see cref="Get{T}(ViewDataDictionary,
        /// string)"></see> and is provided for easy of use when switching
        /// between different storage providers.
        /// </para>
        /// </remarks>
        public static T Peek<T>(this ViewDataDictionary storageProvider, string key)
        {
            storageProvider.TryPeekValue<T>(key, out var value);
            return value;
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// <para>
        /// This is equivalent to calling <see cref="Put{T}(ITempDataDictionary,
        /// string, T)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ITempDataDictionary storageProvider, T value)
        {
            storageProvider.Put(typeof(T).FullName, value);
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key to associate with the stored value.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ITempDataDictionary storageProvider, string key, T value)
        {
            storageProvider[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// <para>
        /// This is equivalent to calling <see cref="Put{T}(ISession,
        /// string, T)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ISession storageProvider, T value)
        {
            storageProvider.Put(typeof(T).FullName, value);
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key to associate with the stored value.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ISession storageProvider, string key, T value)
        {
            storageProvider.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// <para>
        /// This is equivalent to calling <see cref="Put{T}(ViewDataDictionary,
        /// string, T)"></see> where the key is the full name of <typeparamref
        /// name="T"/>.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ViewDataDictionary storageProvider, T value)
        {
            storageProvider.Put(typeof(T).FullName, value);
        }

        /// <summary>
        /// Adds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key to associate with the stored value.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// <para>
        /// The value is stored as a JSON serialized string.
        /// </para>
        /// </remarks>
        public static void Put<T>(this ViewDataDictionary storageProvider, string key, T value)
        {
            storageProvider[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryGetValue<T>(this ITempDataDictionary storageProvider, string key, out T value)
        {
            bool valid = false;
            value = default;

            if (storageProvider.TryGetValue(key, out var objValue))
            {
                try
                {
                    value = JsonConvert.DeserializeObject<T>(objValue.ToString());
                    valid = true;
                }
                catch (JsonException)
                {
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryGetValue<T>(this ISession storageProvider, string key, out T value)
        {
            bool valid = false;
            value = default;

            var objValue = storageProvider.GetString(key);
            if (objValue != null)
            {
                try
                {
                    value = JsonConvert.DeserializeObject<T>(objValue.ToString());
                    valid = true;
                }
                catch (JsonException)
                {
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryGetValue<T>(this ViewDataDictionary storageProvider, string key, out T value)
        {
            bool valid = false;
            value = default;

            if (storageProvider.TryGetValue(key, out var objValue))
            {
                try
                {
                    value = JsonConvert.DeserializeObject<T>(objValue.ToString());
                    valid = true;
                }
                catch (JsonException)
                {
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// Gets the value associated with the specified key, without marking
        /// the key for deletion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static bool TryPeekValue<T>(this ITempDataDictionary storageProvider, string key, out T value)
        {
            bool valid = false;
            value = default;

            if (storageProvider.ContainsKey(key))
            {
                var objValue = storageProvider.Peek(key);

                try
                {
                    value = JsonConvert.DeserializeObject<T>(objValue.ToString());
                    valid = true;
                }
                catch (JsonException)
                {
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        /// <remarks>
        /// This method is equivalent to calling <see
        /// cref="TryGetValue{T}(ISession, string, out T)"></see> and is
        /// provided for easy of use when switching between different storage
        /// providers.
        /// </remarks>
        public static bool TryPeekValue<T>(this ISession storageProvider, string key, out T value)
        {
            return storageProvider.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageProvider">The underlying storage
        /// provider.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated
        /// with the specified key, if the key is found; otherwise, the default
        /// value for the type of the value parameter. This parameter is passed
        /// uninitialized.</param>
        /// <returns><see langword="true"></see> if the <paramref
        /// name="storageProvider"/> contains an element with the specified key;
        /// otherwise, <see langword="false"></see>.</returns>
        /// <remarks>
        /// This method is equivalent to calling <see
        /// cref="TryGetValue{T}(ViewDataDictionary, string, out T)"></see> and
        /// is provided for easy of use when switching between different storage
        /// providers.
        /// </remarks>
        public static bool TryPeekValue<T>(this ViewDataDictionary storageProvider, string key, out T value)
        {
            return storageProvider.TryGetValue(key, out value);
        }
    }
}
