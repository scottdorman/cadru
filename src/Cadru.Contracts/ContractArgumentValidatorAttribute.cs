//------------------------------------------------------------------------------
// <copyright file="ContractArgumentValidatorAttribute.cs"
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

namespace Cadru.Contracts
{
    using System;

    /// <summary>
    /// Enables factoring legacy if-then-throw into separate methods for reuse and full control over
    /// thrown exception and arguments.
    /// </summary>
    /// <devdoc>
    /// Important: the ContractArgumentValidatorAttribute type is not needed in the .Net Framework
    /// prior to mscorlib.dll 4.5. In order to use this feature in earlier versions, please add the
    /// file ContractExtensions.cs or ContractExtensions.vb to all of your projects that contain
    /// contract validator methods.
    /// </devdoc>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ContractArgumentValidatorAttribute : Attribute
    {
    }
}
