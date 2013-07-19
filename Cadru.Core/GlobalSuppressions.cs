//------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" 
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
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cadru.Collections", Justification = "We want this namespace, but don't have enough types to satisfy the rule.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cadru.Globalization", Justification = "We want this namespace, but don't have enough types to satisfy the rule.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cadru.Text", Justification = "We want this namespace, but don't have enough types to satisfy the rule.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cadru", Justification = "We want this namespace, but don't have enough types to satisfy the rule.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "non-parsable", Scope = "resource", Target = "Cadru.Core.Properties.Resources.resources", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Iso", Scope = "resource", Target = "Cadru.Core.Properties.Resources.resources", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cadru", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cadru", Scope = "namespace", Target = "Cadru.Collections", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cadru", Scope = "namespace", Target = "Cadru.Text", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cadru", Scope = "namespace", Target = "Cadru.Globalization", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cadru", Scope = "namespace", Target = "Cadru", Justification = "Reviewed.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Iso", Scope = "resource", Target = "Cadru.Properties.Resources.resources")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "non-parsable", Scope = "resource", Target = "Cadru.Properties.Resources.resources")]
