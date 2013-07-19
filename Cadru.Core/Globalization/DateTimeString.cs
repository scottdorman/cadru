// ---------------------------------------------------------------------------
// Campari Software
//
// DateTimeString.cs
//
// This is a string parsing helper which wraps a String object.
// It has a Index property which tracks the current parsing pointer
// of the string.
//
// ---------------------------------------------------------------------------
// Copyright (C) 2006 Campari Software
// All rights reserved.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;

namespace Campari.Software
{
    #region struct DateTimeString
    internal struct DateTimeString
    {
        #region fields
        private static char[] WhiteSpaceChecks = new char[] { ' ', '\u00A0' };

        internal string Value;
        internal int Index;
        internal int Length;
        internal char CurrentCharacter;
        private CompareInfo info;
        #endregion

        #region properties
        #endregion

        #region methods

        #region constructor
        internal DateTimeString(string str)
        {
            Index = -1;
            Value = str;
            Length = Value.Length;

            CurrentCharacter = '\0';
            info = CultureInfo.InvariantCulture.CompareInfo;
        }
        #endregion

        #region CompareInfo
        internal CompareInfo CompareInfo
        {
            get
            {
                return info;
            }
        }
        #endregion

        #region GetChar
        //
        // Get the current character.
        //
        internal char GetChar()
        {
            Debug.Assert(Index >= 0 && Index < Length, "Index >= 0 && Index < Length");
            return (Value[Index]);
        }
        #endregion

        #region GetDigit
        //
        // Convert the current character to a digit, and return it.
        //
        internal int GetDigit()
        {
            Debug.Assert(Index >= 0 && Index < Length, "Index >= 0 && Index < Length");
            Debug.Assert(IsoDateTimeParse.IsDigit(Value[Index]), "IsDigit(Value[Index])");
            return (Value[Index] - '0');
        }
        #endregion

        #region GetNext
        //
        // Advance the Index.
        // Return true if Index is NOT at the end of the string.
        //
        // Typical usage:
        // while (str.GetNext())
        // {
        //     char ch = str.GetChar()
        // }
        internal bool GetNext()
        {
            Index++;
            if (Index < Length)
            {
                CurrentCharacter = Value[Index];
                return (true);
            }
            return (false);
        }
        #endregion

        #region GetNextDigit
        // Return false when end of string is encountered or a non-digit character is found.
        internal bool GetNextDigit()
        {
            if (++Index >= Length)
            {
                return (false);
            }
            return (IsoDateTimeParse.IsDigit(Value[Index]));
        }
        #endregion

        #region GetRepeatCount
        //
        // Get the number of repeat character after the current character.
        // For a string "hh:mm:ss" at Index of 3. GetRepeatCount() = 2, and Index
        // will point to the second ':'.
        //
        internal int GetRepeatCount()
        {
            char repeatChar = Value[Index];
            int pos = Index + 1;
            while ((pos < Length) && (Value[pos] == repeatChar))
            {
                pos++;
            }
            int repeatCount = (pos - Index);

            // Update the Index to the end of the repeated characters.
            // So the following GetNext() opeartion will get
            // the next character to be parsed.
            Index = pos - 1;
            return (repeatCount);
        }
        #endregion

        #region Match

        #region Match(char ch)
        internal bool Match(char ch)
        {
            if (++Index >= Length)
            {
                return (false);
            }
            if (Value[Index] == ch)
            {
                CurrentCharacter = ch;
                return (true);
            }
            Index--;
            return (false);
        }
        #endregion

        #region Match(String str)
        //
        // Check to see if the string starting from Index is a prefix of
        // str.
        // If a match is found, true value is returned and Index is updated to the next character to be parsed.
        // Otherwise, Index is unchanged.
        //
        internal bool Match(String str)
        {
            if (++Index >= Length)
            {
                return (false);
            }

            if (str.Length > (Value.Length - Index))
            {
                return false;
            }

            if (info.Compare(Value, Index, str.Length, str, 0, str.Length, CompareOptions.Ordinal) == 0)
            {
                // Update the Index to the end of the matching string.
                // So the following GetNext()/Match() opeartion will get
                // the next character to be parsed.
                Index += (str.Length - 1);
                return (true);
            }
            return (false);
        }
        #endregion

        #endregion

        #region MatchSpecifiedWord

        #region MatchSpecifiedWord(String target)
        internal bool MatchSpecifiedWord(String target)
        {
            return MatchSpecifiedWord(target, target.Length + Index);
        }
        #endregion

        #region MatchSpecifiedWord(String target, int endIndex)
        internal bool MatchSpecifiedWord(String target, int endIndex)
        {
            int count = endIndex - Index;

            if (count != target.Length)
            {
                return false;
            }

            if (Index + count > Length)
            {
                return false;
            }

            return (info.Compare(Value, Index, count, target, 0, count, CompareOptions.IgnoreCase) == 0);
        }
        #endregion

        #endregion

        #region RemoveLeadingInQuoteSpaces
        // Trim the leading spaces within a quoted string.
        // Call this after the leading spaces before quoted string are trimmed.
        internal void RemoveLeadingInQuoteSpaces()
        {
            if (Length <= 2)
            {
                return;
            }
            int i = 0;
            char ch = Value[i];
            // Check if the last character is a quote.
            if (ch == '\'' || ch == '\"')
            {
                while ((i + 1) < Length && Char.IsWhiteSpace(Value[i + 1]))
                {
                    i++;
                }
                if (i != 0)
                {
                    Value = Value.Remove(1, i);
                    Length = Value.Length;
                }
            }
        }
        #endregion

        #region RemoveTrailingInQuoteSpaces
        // Trim the trailing spaces within a quoted string.
        // Call this after TrimTail() is done.
        internal void RemoveTrailingInQuoteSpaces()
        {
            int i = Length - 1;
            if (i <= 1)
            {
                return;
            }
            char ch = Value[i];
            // Check if the last character is a quote.
            if (ch == '\'' || ch == '\"')
            {
                if (Char.IsWhiteSpace(Value[i - 1]))
                {
                    i--;
                    while (i >= 1 && Char.IsWhiteSpace(Value[i - 1]))
                    {
                        i--;
                    }
                    Value = Value.Remove(i, Value.Length - 1 - i);
                    Length = Value.Length;
                }
            }
        }
        #endregion

        #region SkipWhiteSpaces
        //
        // Eat White Space ahead of the current position
        //
        // Return false if end of string is encountered.
        //
        internal void SkipWhiteSpaces()
        {
            // Look ahead to see if the next character
            // is a whitespace.
            while (Index + 1 < Length)
            {
                char ch = Value[Index + 1];
                if (!Char.IsWhiteSpace(ch))
                {
                    return;
                }
                Index++;
            }
            return;
        }
        #endregion

        #region TrimTail
        internal void TrimTail()
        {
            int i = Length - 1;
            while (i >= 0 && Char.IsWhiteSpace(Value[i]))
            {
                i--;
            }
            Value = Value.Substring(0, i + 1);
            Length = Value.Length;
        }
        #endregion

        #endregion
    }
    #endregion
}
