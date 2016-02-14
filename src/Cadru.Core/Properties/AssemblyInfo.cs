//------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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
using System.Reflection;
using System.Runtime.CompilerServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Cadru.Core")]
[assembly: AssemblyDescription("A utility framework containing new APIs and extensions to the .NET Framework.")]

[assembly: AssemblyFileVersion("1.0.0")]
[assembly: AssemblyInformationalVersion("4.0.0-beta-27")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("Cadru.Core.UnitTests")]
[assembly: InternalsVisibleTo("Cadru.Core.Explorables")]
