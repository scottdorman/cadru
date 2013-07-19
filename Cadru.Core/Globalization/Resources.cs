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
using System.Resources;

namespace Pantheon.Environment
{
   /// <summary>
   /// Summary description for Resources.
   /// </summary>
   public sealed class Resources
   {
      internal static ResourceManager ResMgr;
      // To avoid infinite loops when calling GetResourceString.  See comments
      // in GetResourceString for these two fields.
      internal static Object m_resMgrLockObject;
      internal static bool m_loadingResource;

      private Resources()
      {
      }

      private static ResourceManager InitResourceManager()
      {
         if (ResMgr == null) 
         {
            lock(typeof(System.Environment)) 
            {
               if (ResMgr == null) 
               {
                  // Do not reorder these two field assignments.
                  m_resMgrLockObject = new Object();
                  ResMgr = new ResourceManager("mscorlib", typeof(String).Assembly);
               }
            }
         }
         return ResMgr;
      }
        
      // This should ideally become visible only within mscorlib's Assembly.
      // 
      // Looks up the resource string value for key.
      // 
      internal static String GetResourceString(String key)
      {
         if (ResMgr == null)
            InitResourceManager();
         String s;
         // We unfortunately have a somewhat common potential for infinite 
         // loops with mscorlib's ResourceManager.  If "potentially dangerous"
         // code throws an exception, we will get into an infinite loop
         // inside the ResourceManager and this "potentially dangerous" code.
         // Potentially dangerous code includes the IO package, CultureInfo,
         // parts of the loader, some parts of Reflection, Security (including 
         // custom user-written permissions that may parse an XML file at
         // class load time), assembly load event handlers, etc.  Essentially,
         // this is not a bounded set of code, and we need to fix the problem.
         // Fortunately, this is limited to mscorlib's error lookups and is NOT
         // a general problem for all user code using the ResourceManager.
            
         // The solution is to make sure only one thread at a time can call 
         // GetResourceString.  If the same thread comes into GetResourceString
         // twice before returning, we're going into an infinite loop and we
         // should return a bogus string.                                       
         // Note: typeof(Environment) is used elsewhere - don't lock on it.
         lock(m_resMgrLockObject) 
         {
            if (m_loadingResource) 
            {
               // This may be bad, depending on how it happens.
               //               BCLDebug.Correctness(false, "Infinite recursion during resource lookup.  Resource name: "+key);
               return "[Resource lookup failed - infinite recursion detected.  Resource name: "+key+']';
            }
            m_loadingResource = true;
            s = ResMgr.GetString(key, null);
            m_loadingResource = false;
         }
         //         BCLDebug.Assert(s!=null, "Managed resource string lookup failed.  Was your resource name misspelled?  Did you rebuild mscorlib after adding a resource to resources.txt?  Debug this w/ cordbg and bug whoever owns the code that called Environment.GetResourceString.  Resource name was: \""+key+"\"");
         return s;
      }
   }
}
