using System;
using System.Collections.Generic;
using System.Linq;

using Cadru.ApiClient.Models;

using Cadru.Diagnostics;

namespace Cadru.ApiClient.Extensions
{
    /// <summary>
    /// Extension methods for converting an <see cref="Exception"/> into a collection
    /// of <see cref="ErrorDetail"/> instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Converts the given <see cref="Exception"/> into a collection
        /// of <see cref="ErrorDetail"/> instances.
        /// </summary>
        /// <param name="e">The exception to convert.</param>
        /// <returns>A collection of <see cref="ErrorDetail"/> instances representing
        /// all of the exceptions in the exception hierarchy.</returns>
        public static IEnumerable<ErrorDetail> GetErrorDetails(this Exception e)
        {
            var details = new List<ErrorDetail>();
            details.AddRange(e.GetAllMessages().Select(m => new ErrorDetail { Message = m }));
            return details;
        }
    }
}
