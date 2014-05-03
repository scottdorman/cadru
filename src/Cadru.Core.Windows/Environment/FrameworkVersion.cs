//------------------------------------------------------------------------------
// <copyright file="FrameworkVersion.cs" 
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

namespace Cadru
{
    /// <summary>
    /// Specifies the .NET Framework versions.
    /// </summary>
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter", Justification = "Reviewed.")]
    public enum FrameworkVersion
    {
        /// <summary>
        /// .NET Framework 1.0.
        /// </summary>
        Fx10,

        /// <summary>
        /// .NET Framework 1.1.
        /// </summary>
        Fx11,

        /// <summary>
        /// .NET Framework 2.0.
        /// </summary>
        Fx20,

        /// <summary>
        /// .NET Framework 3.0.
        /// </summary>
        Fx30,

        /// <summary>
        /// .NET Framework 3.5.
        /// </summary>
        Fx35,

        /// <summary>
        /// .NET Framework 3.5 Client Profile.
        /// </summary>
        Fx35ClientProfile,

        /// <summary>
        /// .NET Framework 3.5 Server Core Profile.
        /// </summary>
        Fx35ServerCoreProfile,

        /// <summary>
        /// .NET Framework 4.0.
        /// </summary>
        Fx40,

        /// <summary>
        /// .NET Framework 4.0 Client Profile.
        /// </summary>
        Fx40ClientProfile,

        /// <summary>
        /// .NET Framework 4.5.
        /// </summary>
        Fx45,
    }
}
