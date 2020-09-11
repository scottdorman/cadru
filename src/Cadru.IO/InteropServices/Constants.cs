//------------------------------------------------------------------------------
// <copyright file="Constants.cs"
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

using System.Diagnostics.CodeAnalysis;

namespace Cadru.IO.Interop
{
    /// <summary>
    /// An internal class that defines the p/invoke constants required by the
    /// Win32 API calls that are used inside the library.
    /// </summary>
    [type: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    internal static class Constants
    {
        internal const int ERROR_ACCESS_DENIED = 5;
        internal const int ERROR_MORE_DATA = 234;
        internal const int ERROR_SUCCESS = 0;
        internal const int MAX_PATH = 260;

        internal const int STD_OUTPUT_HANDLE = 11;
    }
}