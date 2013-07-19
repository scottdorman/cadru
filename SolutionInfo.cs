//------------------------------------------------------------------------------
// <copyright file="SolutionInfo.cs" 
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
// Provides a central location for Assembly attributes. Assembly attributes 
// are values that provide information about an assembly. The attributes are
// divided into the following sets of information: 
//
//    * Assembly identity attributes. 
//    * Informational attributes. 
//    * Assembly manifest attributes. 
//    * Strong name attributes. 

using System.Reflection;
using System.Resources;


#region Assembly identity attributes

[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("en")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("2.0.*")]

#endregion

#region Informational attributes

[assembly: AssemblyCompany("Scott Dorman")]
[assembly: AssemblyCopyright("Copyright © 2001-2013 Scott Dorman.")]
[assembly: AssemblyTrademark(@"")]
[assembly: AssemblyProduct("Cadru for .NET 2.0")]
#endregion

#region Assembly manifest attributes
// Do not use in a globally included SolutionInfo.cs file. The attributes in this
// section apply at an assembly level and should be used in the project specific
// AssemblyInfo.cs files.
#endregion

#region Strong name attributes
// Do not use in a globally included SolutionInfo.cs file. The attributes in this
// section apply at an assembly level and should be used in the project specific
// AssemblyInfo.cs files.
#endregion
