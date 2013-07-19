// ------------------------------------------------------------------------------
// UnitedHealth Networks
// Audit and Recovery Operations - Network Operations Data Management
//
// ReadOnlyStringCollection.cs
//
//------------------------------------------------------------------------------
// Copyright (C) 2002-2003 UnitedHealth Networks
// All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ------------------------------------------------------------------------------
using System;

namespace Pantheon
{

   #region GenId class

   /// <summary>
   /// Provides basic routines for returning a string representation of a GUID.
   /// </summary>
   public sealed class GenId 
   {

		#region private utility methods & constructors

      //Since this class provides only static methods, make the default constructor private to prevent 
      //instances from being created with "new GenID()".
      private GenId() {}

		#endregion private utility methods & constructors
	
		#region GenGUIDStringEx

      /// <summary>
      /// Generates a GUID value without formatting marks.
      /// </summary>
      /// <returns>string representation of a GUID</returns>
      public static string GenGUIDStringEx() 
      {
         try 
         {
            string guid = System.Guid.NewGuid().ToString();
            guid = guid.Replace("{", "");
            guid = guid.Replace("}", "");
            guid = guid.Replace("-", "");
            return (guid);
         }
         catch (Exception e) 
         {
            throw new Exception("GenGUIDStringEx", e);
         }
      }

		#endregion GenGUIDStringEx
   }

	#endregion GenId class
}
