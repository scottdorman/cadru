// ------------------------------------------------------------------------------
// UnitedHealth Networks
// Audit and Recovery Operations - Network Operations Data Management
//
// Comb.cs
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
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Globalization;
using Pantheon.Environment;

namespace Pantheon
{
   [StructLayout(LayoutKind.Sequential)]
   [Serializable]
   public struct Comb : IFormattable, IComparable
   {
      #region class-wide variables

      /// <summary>
      /// Initializes a new instance of the Comb class.
      /// </summary>
      public static readonly Comb Empty = new Comb();

      #endregion class-wide variables

      #region private member variables

      private int   _a;
      private short _b;
      private short _c;
      private byte  _d;
      private byte  _e;
      private byte  _f;
      private byte  _g;
      private byte  _h;
      private byte  _i;
      private byte  _j;
      private byte  _k;

      #endregion private member variables

      #region private utility methods

      #region TryParse

      private static int TryParse(String str, int []parsePos, int requiredLength) 
      {
         int currStart = parsePos[0];
         int retVal;
         try
         {
            retVal = ParseNumbers.StringToInt(str, 16, 0, parsePos);
         }
         catch (FormatException)
         {
            throw new FormatException(("Format_GuidUnrecognized"));
         }

         //If we didn't parse enough characters, there's clearly an error.
         if (parsePos[0] - currStart != requiredLength) 
         {
            throw new FormatException(Resources.GetResourceString("Format_GuidUnrecognized"));
         }
         return retVal;
      }

      #endregion TryParse

      #region EatAllWhitespace

      private static String EatAllWhitespace(String str)
      {
         int newLength = 0;
         char[] chArr = new char[str.Length];
         char curChar;

         // Now get each char from str and if it is not whitespace add it to chArr
         for(int i = 0; i < str.Length; i++)
         {
            curChar = str[i];
            if(!Char.IsWhiteSpace(curChar))
            {
               chArr[newLength++] = curChar;
            }
         }

         // Return a new string based on chArr
         return new String(chArr, 0, newLength);
      }

      #endregion EatAllWhitespace

      #region IsHexPrefix

      private static bool IsHexPrefix(String str, int i)
      {
         if(str[i] == '0' && (Char.ToLower(str[i + 1], CultureInfo.InvariantCulture) == 'x'))
            return true;
         else
            return false;
      }

      #endregion IsHexPrefix

      #region GetResult

      private int GetResult(uint me, uint them) 
      {
         if (me < them) 
         {
            return -1;
         }
         return 1;
      }

      #endregion GetResult

      #region HexToChar

      private static char HexToChar(int a)
      {
         a = a & 0xf;
         return (char) ((a > 9) ? a - 10 + 0x61 : a + 0x30);
      }

      #endregion HexToChar

      #region HexToChars

      private static int HexsToChars(char[] guidChars, int offset, int a, int b)
      {
         guidChars[offset++] = HexToChar(a >> 4);
         guidChars[offset++] = HexToChar(a);
         guidChars[offset++] = HexToChar(b >> 4);
         guidChars[offset++] = HexToChar(b);
         return offset;
      }

      #endregion HexToChars

      #region Comb(bool blank)

      // Creates a new guid with all contents having value of 0
      private Comb(bool blank)
      {
         // Must initialize value class members even if the native
         // function reinitializes them.  Compiler appeasement.
         _a = 0;
         _b = 0;
         _c = 0;
         _d = 0;
         _e = 0;
         _f = 0;
         _g = 0;
         _h = 0;
         _i = 0;
         _j = 0;
         _k = 0;
      }
      
      #endregion Comb(bool blank)

      #endregion private utility methods

      #region Constructors

      #region Comb(byte[] b)

      /// <summary>
      /// Initializes a new instance of the Comb class using the specified array of bytes.
      /// </summary>
      /// <param name="b">A 16 element byte array containing values with which to initialize the Comb.</param>
      public Comb(byte[] b)
      {
         if (b==null)
            throw new ArgumentNullException("b");
         if (b.Length != 16)
            throw new ArgumentException(String.Format("Error", "16"));
         _a = ((int)b[0] << 24) | ((int)b[1] << 16) | ((int)b[2] << 8) | b[3];
         _b = (short)(((int)b[4] << 8) | b[5]);
         _c = (short)(((int)b[6] << 8) | b[7]);
         _d = b[8];
         _e = b[9];
         _f = b[10];
         _g = b[11];
         _h = b[12];
         _i = b[13];
         _j = b[14];
         _k = b[15];
      }

      #endregion Comb(byte[] b)

      #region Comb(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)

      /// <summary>
      /// Initializes a new instance of the Comb class using the specified unsigned integers and bytes. This constructor is not CLS-compliant.
      /// </summary>
      [CLSCompliant(false)]
      public Comb(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
      {
         _a = (int)a;
         _b = (short)b;
         _c = (short)c;
         _d = d;
         _e = e;
         _f = f;
         _g = g;
         _h = h;
         _i = i;
         _j = j;
         _k = k;
      }

      #endregion Comb(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)

      #region Comb(int a, short b, short c, byte[] d)

      /// <summary>
      /// Initializes a new instance of the Guid class using the specified integers and bytes.
      /// </summary>
      /// <param name="a">The first 4 bytes of the GUID.</param>
      /// <param name="b">The next 2 bytes of the GUID.</param>
      /// <param name="c">The next 2 bytes of the GUID.</param>
      /// <param name="d">The remaining 8 bytes of the GUID.</param>
      public Comb(int a, short b, short c, byte[] d)
      {
         if (d==null)
            throw new ArgumentNullException("d");
         // Check that array is not too big
         if(d.Length != 8)
            throw new ArgumentException(String.Format(("Arg_GuidArrayCtor"), "8"));

         _a  = a;
         _b  = b;
         _c  = c;
         _d = d[0];
         _e = d[1];
         _f = d[2];
         _g = d[3];
         _h = d[4];
         _i = d[5];
         _j = d[6];
         _k = d[7];
      }

      #endregion Comb(int a, short b, short c, byte[] d)

      #region Comb(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
      
      /// <summary>
      /// Initializes a new instance of the Comb class using the specified integers and bytes.
      /// </summary>
      /// <param name="a">The first 4 bytes of the Comb.</param>
      /// <param name="b">The next 2 bytes of the Comb.</param>
      /// <param name="c">The next 2 bytes of the Comb.</param>
      /// <param name="d">The next byte of the Comb.</param>
      /// <param name="e">The next byte of the Comb.</param>
      /// <param name="f">The next byte of the Comb.</param>
      /// <param name="g">The next byte of the Comb.</param>
      /// <param name="h">The next byte of the Comb.</param>
      /// <param name="i">The next byte of the Comb.</param>
      /// <param name="j">The next byte of the Comb.</param>
      /// <param name="k">The next byte of the Comb.</param>
      public Comb(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
      {
         _a = a;
         _b = b;
         _c = c;
         _d = d;
         _e = e;
         _f = f;
         _g = g;
         _h = h;
         _i = i;
         _j = j;
         _k = k;
      }

      #endregion Comb(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
 
      #region Comb(String g)

      /// <summary>
      /// Initializes a new instance of the Comb class using the specified integers and byte array.
      /// </summary>
      /// <param name="g">A String that contains a GUID in the following format: 
      /// hexadecimal digits are arranged in groups of 8, 4, 4, 4, and 12 digits with hyphens between the 
      /// groups. The GUID can optionally be enclosed in matching braces. For example: 
      /// dddddddd-dddd-dddd-dddd-dddddddddddd or {dddddddd-dddd-dddd-dddd-dddddddddddd}.
      /// Alternatively, the following format is permitted: 
      /// {0xdddddddd,0xdddd, 0xdddd,{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd},{0xdd}}, 
      /// where d is a hexadecimal digit. If this format is used, all brackets and commas 
      /// indicated are required, and all numbers must be prefixed with "0x" as shown. 
      /// Fewer hexadecimal digits than shown can be used, but no more. 
      /// </param>
      public Comb(String g)
      {
         if (g == null)
            throw new ArgumentNullException("g", "ArgumentNull_String");

         int startPos=0;
         int temp;
         long templ;
         int[] spArray = new int[1];

         try
         {
            // Check if it's of the form dddddddd-dddd-dddd-dddd-dddddddddddd
            if(g.IndexOf('-', 0) >= 0)
            {

               String guidString = g.Trim();  //Remove Whitespace

               // check to see that it's the proper length
               if (guidString[0] == '{') 
               {
                  if (guidString.Length != 38 || guidString[37] != '}') 
                  {
                     throw new FormatException(("Format_GuidInvLen"));
                  }
                  startPos=1;
               }
               else if (guidString[0] == '(')
               {
                  if (guidString.Length != 38 || guidString[37] != ')')
                  {
                     throw new FormatException(("Format_GuidInvLen"));
                  }
                  startPos=1;
               }
               else if(guidString.Length != 36) 
               {
                  throw new FormatException(("Format_GuidInvLen"));
               }
               if (guidString[8+startPos] != '-' ||
                  guidString[13+startPos] != '-' ||
                  guidString[18+startPos] != '-' ||
                  guidString[23+startPos] != '-') 
               {
                  throw new FormatException(("Format_GuidDashes"));
               }

               spArray[0]=startPos;
               _a = TryParse(guidString, spArray,8);
               spArray[0]++; //Increment past the '-';
               _b = (short)TryParse(guidString, spArray,4);
               spArray[0]++; //Increment past the '-';
               _c = (short)TryParse(guidString, spArray,4);
               spArray[0]++; //Increment past the '-';
               temp=TryParse(guidString, spArray,4);
               spArray[0]++; //Increment past the '-';
               startPos=spArray[0];
               templ = ParseNumbers.StringToLong(guidString, 16, 0, spArray);
               if (spArray[0]-startPos!=12) 
               {
                  throw new FormatException(String.Format(("Format_GuidInvLen")));
               }
               _d = (byte)(temp >> 8);
               _e = (byte)(temp);
               temp = (int)(templ >> 32);
               _f = (byte)(temp >> 8);
               _g = (byte)(temp);
               temp = (int)(templ);
               _h = (byte)(temp >> 24);
               _i = (byte)(temp >> 16);
               _j = (byte)(temp >> 8);
               _k = (byte)(temp);
            }
               // Else check if it is of the form
               // {0xdddddddd,0xdddd,0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}
            else if(g.IndexOf('{', 0) >= 0)
            {
               int numStart = 0;
               int numLen = 0;

               // Convert to lower case
               //g = g.ToLower();

               // Eat all of the whitespace
               g = EatAllWhitespace(g);

               // Check for leading '{'
               if(g[0] != '{')
                  throw new FormatException(("Format_GuidBrace"));

               // Check for '0x'
               if(!IsHexPrefix(g, 1))
                  throw new FormatException(String.Format(("Format_GuidHexPrefix"), "{0xdddddddd, etc}"));

               // Find the end of this hex number (since it is not fixed length)
               numStart = 3;
               numLen = g.IndexOf(',', numStart) - numStart;
               if(numLen <= 0)
                  throw new FormatException(("Format_GuidComma"));

               // Read in the number
               _a = (int) ParseNumbers.StringToInt(g.Substring(numStart, numLen), // first DWORD
                  16,                            // hex
                  ParseNumbers.IsTight);         // tight parsing

               // Check for '0x'
               if(!IsHexPrefix(g, numStart+numLen+1))
                  throw new FormatException(String.Format(("Format_GuidHexPrefix"), "{0xdddddddd, 0xdddd, etc}"));

               // +3 to get by ',0x'
               numStart = numStart + numLen + 3;
               numLen = g.IndexOf(',', numStart) - numStart;
               if(numLen <= 0)
                  throw new FormatException(("Format_GuidComma"));

               // Read in the number
               _b = (short) ParseNumbers.StringToInt(
                  g.Substring(numStart, numLen), // first DWORD
                  16,                            // hex
                  ParseNumbers.IsTight);         // tight parsing

               // Check for '0x'
               if(!IsHexPrefix(g, numStart+numLen+1))
                  throw new FormatException(String.Format(("Format_GuidHexPrefix"), "{0xdddddddd, 0xdddd, 0xdddd, etc}"));

               // +3 to get by ',0x'
               numStart = numStart + numLen + 3;
               numLen = g.IndexOf(',', numStart) - numStart;
               if(numLen <= 0)
                  throw new FormatException(("Format_GuidComma"));

               // Read in the number
               _c = (short) ParseNumbers.StringToInt(
                  g.Substring(numStart, numLen), // first DWORD
                  16,                            // hex
                  ParseNumbers.IsTight);         // tight parsing

               // Check for '{'
               if(g.Length <= numStart+numLen+1 || g[numStart+numLen+1] != '{')
                  throw new FormatException(("Format_GuidBrace"));

               // Prepare for loop
               numLen++;
               byte[] bytes = new byte[8];

               for(int i = 0; i < 8; i++)
               {
                  // Check for '0x'
                  if(!IsHexPrefix(g, numStart+numLen+1))
                     throw new FormatException(String.Format(("Format_GuidHexPrefix"), "{... { ... 0xdd, ...}}"));

                  // +3 to get by ',0x' or '{0x' for first case
                  numStart = numStart + numLen + 3;

                  // Calculate number length
                  if(i < 7)  // first 7 cases
                  {
                     numLen = g.IndexOf(',', numStart) - numStart;
                  }
                  else       // last case ends with '}', not ','
                  {
                     numLen = g.IndexOf('}', numStart) - numStart;
                  }
                  if(numLen <= 0)
                     throw new FormatException(("Format_GuidComma"));

                  // Read in the number
                  bytes[i] = (byte) Convert.ToInt32(g.Substring(numStart, numLen),16);
               }

               _d = bytes[0];
               _e = bytes[1];
               _f = bytes[2];
               _g = bytes[3];
               _h = bytes[4];
               _i = bytes[5];
               _j = bytes[6];
               _k = bytes[7];

               // Check for last '}'
               if(numStart + numLen + 1 >= g.Length || g[numStart + numLen + 1] != '}')
                  throw new FormatException(("Format_GuidEndBrace"));

               return;
            }
            else
               // Check if it's of the form dddddddddddddddddddddddddddddddd
            {
               String guidString = g.Trim();  //Remove Whitespace

               if(guidString.Length != 32) 
               {
                  throw new FormatException(("Format_GuidInvLen"));
               }

               _a = (int) ParseNumbers.StringToInt(g.Substring(startPos, 8), // first DWORD
                  16,                            // hex
                  ParseNumbers.IsTight);         // tight parsing
               startPos += 8;
               _b = (short) ParseNumbers.StringToInt(g.Substring(startPos, 4),
                  16,
                  ParseNumbers.IsTight);
               startPos += 4;
               _c = (short) ParseNumbers.StringToInt(g.Substring(startPos, 4),
                  16,
                  ParseNumbers.IsTight);

               startPos += 4;
               temp=(short) ParseNumbers.StringToInt(g.Substring(startPos, 4),
                  16,
                  ParseNumbers.IsTight);
               startPos += 4;
               spArray[0] = startPos;
               templ = ParseNumbers.StringToLong(guidString, 16, startPos, spArray);
               if (spArray[0] - startPos != 12) 
               {
                  throw new FormatException(String.Format(("Format_GuidInvLen")));
               }
               _d = (byte)(temp >> 8);
               _e = (byte)(temp);
               temp = (int)(templ >> 32);
               _f = (byte)(temp >> 8);
               _g = (byte)(temp);
               temp = (int)(templ);
               _h = (byte)(temp >> 24);
               _i = (byte)(temp >> 16);
               _j = (byte)(temp >> 8);
               _k = (byte)(temp);
            }
         }
         catch(IndexOutOfRangeException)
         {
            throw new FormatException(("Format_GuidUnrecognized"));
         }
      }

      #endregion Comb(String g)

      #endregion Constructors

      #region Equality methods

      #region GetHashCode

      /// <summary>
      /// Returns the hash code for this instance.
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return _a ^ (((int)_b << 16) | (int)(ushort)_c) ^ (((int)_f << 24) | _k);
      }

      #endregion GetHashCode

      #region Equals

      /// <summary>
      /// Returns a value indicating whether this instance is equal to a specified object.
      /// </summary>
      /// <param name="o">The object to compare with this instance.</param>
      /// <returns></returns>
      public override bool Equals(Object o)
      {
         Comb g;

         // Check that o is a Comb first
         if(o == null || !(o is Comb))
            return false;
         else g = (Comb) o;

         // Now compare each of the elements
         if(g._a != _a)
            return false;
         if(g._b != _b)
            return false;
         if(g._c != _c)
            return false;
         if (g._d != _d)
            return false;
         if (g._e != _e)
            return false;
         if (g._f != _f)
            return false;
         if (g._g != _g)
            return false;
         if (g._h != _h)
            return false;
         if (g._i != _i)
            return false;
         if (g._j != _j)
            return false;
         if (g._k != _k)
            return false;

         return true;
      }

      #endregion Equals

      #region CompareTo

      /// <summary>
      /// Compares this instance to a specified object and returns an indication of their relative values.
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public int CompareTo(Object value) 
      {
         if (value == null) 
         {
            return 1;
         }
         if (!(value is Guid)) 
         {
            throw new ArgumentException("Arguement must be a Comb.");
         }
         Comb g = (Comb)value;

         if (g._a != this._a) 
         {
            return GetResult((uint)this._a, (uint)g._a);
         }

         if (g._b != this._b) 
         {
            return GetResult((uint)this._b, (uint)g._b);
         }

         if (g._c != this._c) 
         {
            return GetResult((uint)this._c, (uint)g._c);
         }

         if (g._d != this._d) 
         {
            return GetResult((uint)this._d, (uint)g._d);
         }

         if (g._e != this._e) 
         {
            return GetResult((uint)this._e, (uint)g._e);
         }

         if (g._f != this._f) 
         {
            return GetResult((uint)this._f, (uint)g._f);
         }

         if (g._g != this._g) 
         {
            return GetResult((uint)this._g, (uint)g._g);
         }

         if (g._h != this._h) 
         {
            return GetResult((uint)this._h, (uint)g._h);
         }

         if (g._i != this._i) 
         {
            return GetResult((uint)this._i, (uint)g._i);
         }

         if (g._j != this._j) 
         {
            return GetResult((uint)this._j, (uint)g._j);
         }

         if (g._k != this._k) 
         {
            return GetResult((uint)this._k, (uint)g._k);
         }

         return 0;
      }

      #endregion CompareTo

      #region ==

      /// <summary>
      /// Returns an indication whether the values of two specified Comb objects are equal.
      /// </summary>
      /// <param name="a">A Comb object.</param>
      /// <param name="b">A Comb object.</param>
      /// <returns></returns>
      public static bool operator ==(Comb a, Comb b)
      {
         return a.Equals(b);
      }

      #endregion ==

      #region !=

      /// <summary>
      /// Returns an indication whether the values of two specified Guid objects are not equal.
      /// </summary>
      /// <param name="a">A Comb object.</param>
      /// <param name="b">A Comb object.</param>
      /// <returns></returns>
      public static bool operator !=(Comb a, Comb b)
      {
         return !a.Equals(b);
      }
      
      #endregion !=

      #endregion Equality methods

      #region ToString methods

      #region ToString()

      /// <summary>
      /// Returns a String representation of the value of this instance in Registry format.
      /// </summary>
      /// <returns></returns>
      public override String ToString()
      {
         return ToString("D", null);
      }

      #endregion ToString()

      #region ToString(String format)

      /// <summary>
      /// Returns a String representation of the value of this Comb instance, according to the provided format specifier.
      /// </summary>
      /// <param name="format"></param>
      /// <returns></returns>
      public String ToString(String format) 
      {
         return ToString(format, null);
      }

      #endregion ToString(String format)

      #region ToString(String format, IFormatProvider provider)

      /// <summary>
      /// Returns a String representation of the value of this instance of the Comb class, according to the provided format specifier and culture-specific format information.
      /// </summary>
      /// <param name="format"></param>
      /// <param name="provider"></param>
      /// <returns></returns>
      public String ToString(String format, IFormatProvider provider)
      {
         if (format == null || format.Length == 0)
            format = "D";

         char[] guidChars;
         int offset = 0;
         int strLength = 38;
         bool dash = true;

         if (String.Compare(format, "N", true, CultureInfo.InvariantCulture) == 0)
         {
            guidChars = new char[32];
            strLength = 32;
            dash = false;
         }
         else if (String.Compare(format, "B", true, CultureInfo.InvariantCulture) == 0)
         {
            guidChars = new char[38];
            guidChars[offset++] = '{';
            guidChars[37] = '}';
         }
         else if (String.Compare(format, "P", true, CultureInfo.InvariantCulture) == 0)
         {
            guidChars = new char[38];
            guidChars[offset++] = '(';
            guidChars[37] = ')';
         }
         else if (String.Compare(format, "D", true, CultureInfo.InvariantCulture) == 0)
         {
            guidChars = new char[36];
            strLength = 36;
         }
         else
            throw new FormatException("Format_InvalidGuidFormatSpecification");

         offset = HexsToChars(guidChars, offset, _a >> 24, _a >> 16);
         offset = HexsToChars(guidChars, offset, _a >> 8, _a);

         if (dash) guidChars[offset++] = '-';

         offset = HexsToChars(guidChars, offset, _b >> 8, _b);

         if (dash) guidChars[offset++] = '-';

         offset = HexsToChars(guidChars, offset, _c >> 8, _c);

         if (dash) guidChars[offset++] = '-';

         offset = HexsToChars(guidChars, offset, _d, _e);

         if (dash) guidChars[offset++] = '-';

         offset = HexsToChars(guidChars, offset, _f, _g);
         offset = HexsToChars(guidChars, offset, _h, _i);
         offset = HexsToChars(guidChars, offset, _j, _k);

         return new String(guidChars, 0, strLength);
      }

      #endregion ToString(String format, IFormatProvider provider)

      #endregion ToString methods

      #region ToByteArray

      /// <summary>
      /// Returns a 16-element byte array that contains the value of the Comb.
      /// </summary>
      /// <returns>A 16-element byte array.</returns>
      public byte[] ToByteArray()
      {
         byte[] g = new byte[16];

         g[0] = (byte)(_a >> 24);
         g[1] = (byte)(_a >> 16);
         g[2] = (byte)(_a >> 8);
         g[3] = (byte)(_a);
         g[4] = (byte)(_b >> 8);
         g[5] = (byte)(_b);
         g[6] = (byte)(_c >> 8);
         g[7] = (byte)(_c);
         g[8] = _d;
         g[9] = _e;
         g[10] = _f;
         g[11] = _g;
         g[12] = _h;
         g[13] = _i;
         g[14] = _j;
         g[15] = _k;

         return g;
      }

      #endregion ToByteArray

      #region NewComb methods

      #region NewComb()

      /// <summary>
      /// Initializes a new instance of the Comb class.
      /// </summary>
      /// <returns></returns>
      public static Comb NewComb() 
      {
         byte[] dateBytes = BitConverter.GetBytes(DateTime.Now.Ticks); 
         byte[] guidBytes = Guid.NewGuid().ToByteArray(); 
         // copy the last six bytes from the date to the last six bytes of the GUID 
         Array.Copy(dateBytes, dateBytes.Length - 7, guidBytes, guidBytes.Length - 7, 6); 
         return new Comb(guidBytes); 
      }

      #endregion NewComb()

      #region NewComb(bool precise)

      /// <summary>
      /// Initializes a new instance of the Comb class.
      /// </summary>
      /// <param name="precise"></param>
      /// <returns></returns>
      public static Comb NewComb(bool precise) 
      {
         if (precise) 
         {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray(); 

            DateTime baseDate = new DateTime(1900, 1, 1); 
            DateTime now = DateTime.Now; 

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks); 
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks)); 

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days); 
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333)); 

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray); 
            Array.Reverse(msecsArray); 

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2); 
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4); 

            return new Comb(guidArray); 
         }
         else
         {
            return NewComb();
         }
      }

      #endregion NewComb(bool precise)

      #endregion NewComb methods

      #region GetDateFromComb

      /// <summary>
      /// Returns the date encoded in the Comb object.
      /// </summary>
      /// <returns></returns>
      public DateTime GetDateFromComb() 
      { 
         DateTime baseDate = new DateTime(1900,1,1); 
         byte[] daysArray = new byte[4]; 
         byte[] msecsArray = new byte[4]; 
         byte[] guidArray = this.ToByteArray(); 

         // Copy the date parts of the guid to the respective byte arrays. 
         Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2); 
         Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4); 

         // Reverse the arrays to put them into the appropriate order 
         Array.Reverse(daysArray); 
         Array.Reverse(msecsArray); 

         // Convert the bytes to ints 
         int days = BitConverter.ToInt32(daysArray, 0); 
         int msecs = BitConverter.ToInt32(msecsArray, 0); 

         DateTime date = baseDate.AddDays(days); 
         date = date.AddMilliseconds(msecs * 3.333333); 

         return date; 
      }

      #endregion GetDateFromComb
   }
}
