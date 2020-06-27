//------------------------------------------------------------------------------
// <copyright file="ExceptionHandling.cs"
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

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using System.Xml;
using System.Xml.Schema;

namespace Cadru.Build.Tasks.Internal
{
    /// <summary>
    /// Utility methods for classifying and handling exceptions.
    /// </summary>
    internal static class ExceptionHandling
    {
        static ExceptionHandling()
        {
            DebugDumpPath = GetDebugDumpPath();
        }

        /// <summary>
        /// Gets the location of the directory used for diagnostic log files.
        /// </summary>
        /// <returns></returns>
        private static string GetDebugDumpPath()
        {
            var debugPath = Environment.GetEnvironmentVariable("MSBUILDDEBUGPATH");
            return !String.IsNullOrEmpty(debugPath)
                    ? debugPath
                    : Path.GetTempPath();
        }

        /// <summary>
        /// The directory used for diagnostic log files.
        /// </summary>
        internal static string DebugDumpPath { get; private set; }

        /// <summary>
        /// If the given exception is "ignorable under some circumstances" return false.
        /// Otherwise it's "really bad", and return true.
        /// This makes it possible to catch(Exception ex) without catching disasters.
        /// </summary>
        /// <param name="e"> The exception to check. </param>
        /// <returns> True if exception is critical. </returns>
        internal static bool IsCriticalException(Exception e)
        {
            if (e is OutOfMemoryException
             || e is StackOverflowException
             || e is ThreadAbortException
             || e is ThreadInterruptedException
             || e is AccessViolationException
             )
            {
                // Ideally we would include NullReferenceException, because it should only ever be thrown by CLR (use ArgumentNullException for arguments)
                // but we should handle it if tasks and loggers throw it.

                // ExecutionEngineException has been deprecated by the CLR
                return true;
            }

            // Check if any critical exceptions

            if (e is AggregateException aggregateException)
            {
                // If the aggregate exception contains a critical exception it is considered a critical exception
                if (aggregateException.InnerExceptions.Any(innerException => IsCriticalException(innerException)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// If the given exception is file IO related or expected return false.
        /// Otherwise, return true.
        /// </summary>
        /// <param name="e">The exception to check.</param>
        /// <returns>True if exception is not IO related or expected otherwise false.</returns>
        internal static bool NotExpectedException(Exception e)
        {
            return !IsIoRelatedException(e);
        }

        /// <summary>
        /// Determine whether the exception is file-IO related.
        /// </summary>
        /// <param name="e">The exception to check.</param>
        /// <returns>True if exception is IO related.</returns>
        internal static bool IsIoRelatedException(Exception e)
        {
            // These all derive from IOException
            //     DirectoryNotFoundException
            //     DriveNotFoundException
            //     EndOfStreamException
            //     FileLoadException
            //     FileNotFoundException
            //     PathTooLongException
            //     PipeException
            return e is UnauthorizedAccessException
                   || e is NotSupportedException
                   || (e is ArgumentException && !(e is ArgumentNullException))
                   || e is SecurityException
                   || e is IOException;
        }

        /// <summary> Checks if the exception is an XML one. </summary>
        /// <param name="e"> Exception to check. </param>
        /// <returns> True if exception is related to XML parsing. </returns>
        internal static bool IsXmlException(Exception e)
        {
            return e is XmlException
                || e is XmlSchemaException
                || e is UriFormatException; // XmlTextReader for example uses this under the covers
        }

        /// <summary> Extracts line and column numbers from the exception if it is XML-related one. </summary>
        /// <param name="e"> XML-related exception. </param>
        /// <returns> Line and column numbers if available, (0,0) if not. </returns>
        /// <remarks> This function works around the fact that XmlException and XmlSchemaException are not directly related. </remarks>
        internal static LineAndColumn GetXmlLineAndColumn(Exception e)
        {
            var line = 0;
            var column = 0;

            if (e is XmlException xmlException)
            {
                line = xmlException.LineNumber;
                column = xmlException.LinePosition;
            }
            else
            {
                if (e is XmlSchemaException schemaException)
                {
                    line = schemaException.LineNumber;
                    column = schemaException.LinePosition;
                }
            }

            return new LineAndColumn
            {
                Line = line,
                Column = column
            };
        }

        /// <summary>
        /// If the given exception is file IO related or Xml related return false.
        /// Otherwise, return true.
        /// </summary>
        /// <param name="e">The exception to check.</param>
        internal static bool NotExpectedIoOrXmlException(Exception e)
        {
            if
            (
                IsXmlException(e)
                || !NotExpectedException(e)
            )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// If the given exception is reflection-related return false.
        /// Otherwise, return true.
        /// </summary>
        /// <param name="e">The exception to check.</param>
        internal static bool NotExpectedReflectionException(Exception e)
        {
            // We are explicitly not handling TargetInvocationException. Those are just wrappers around
            // exceptions thrown by the called code (such as a task or logger) which callers will typically
            // want to treat differently.
            if
            (
                e is TypeLoadException                  // thrown when the common language runtime cannot find the assembly, the type within the assembly, or cannot load the type
                || e is MethodAccessException           // thrown when a class member is not found or access to the member is not permitted
                || e is MissingMethodException          // thrown when code in a dependent assembly attempts to access a missing method in an assembly that was modified
                || e is MemberAccessException           // thrown when a class member is not found or access to the member is not permitted
                || e is BadImageFormatException         // thrown when the file image of a DLL or an executable program is invalid
                || e is ReflectionTypeLoadException     // thrown by the Module.GetTypes method if any of the classes in a module cannot be loaded
                || e is TargetParameterCountException   // thrown when the number of parameters for an invocation does not match the number expected
                || e is InvalidCastException
                || e is AmbiguousMatchException         // thrown when binding to a member results in more than one member matching the binding criteria
                || e is CustomAttributeFormatException  // thrown if a custom attribute on a data type is formatted incorrectly
                || e is InvalidFilterCriteriaException  // thrown in FindMembers when the filter criteria is not valid for the type of filter you are using
                || e is TargetException                 // thrown when an attempt is made to invoke a non-static method on a null object.  This may occur because the caller does not
                                                        //     have access to the member, or because the target does not define the member, and so on.
                || e is MissingFieldException           // thrown when code in a dependent assembly attempts to access a missing field in an assembly that was modified.
                || !NotExpectedException(e)             // Reflection can throw IO exceptions if the assembly cannot be opened

            )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Serialization has been observed to throw TypeLoadException as
        /// well as SerializationException and IO exceptions. (Obviously
        /// it has to do reflection but it ought to be wrapping the exceptions.)
        /// </summary>
        internal static bool NotExpectedSerializationException(Exception e)
        {
            if
            (
                e is SerializationException ||
                !NotExpectedReflectionException(e)
            )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns false if this is a known exception thrown by the registry API.
        /// </summary>
        internal static bool NotExpectedRegistryException(Exception e)
        {
            if (e is SecurityException
             || e is UnauthorizedAccessException
             || e is IOException
             || e is ObjectDisposedException
             || e is ArgumentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns false if this is a known exception thrown by function evaluation
        /// </summary>
        internal static bool NotExpectedFunctionException(Exception e)
        {
            if (e is InvalidCastException
             || e is ArgumentNullException
             || e is FormatException
             || e is InvalidOperationException
             || !NotExpectedReflectionException(e))
            {
                return false;
            }

            return true;
        }

        /// <summary> Line and column pair. </summary>
        internal struct LineAndColumn
        {
            /// <summary> Gets or sets line number. </summary>
            internal int Line { get; set; }

            /// <summary> Gets or sets column position. </summary>
            internal int Column { get; set; }
        }
    }
}