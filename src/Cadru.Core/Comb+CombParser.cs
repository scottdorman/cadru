//------------------------------------------------------------------------------
// <copyright file="Comb+CombParser.cs"
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

namespace Cadru
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed.")]
    public partial struct Comb
    {
        internal struct CombParser
        {
            private readonly string source;
            private int sourceLength;
            private int current;

            public CombParser(string input)
            {
                this.source = input.Trim();
                this.current = 0;
                this.sourceLength = this.source.Length;
            }

            private bool EOF
            {
                get
                {
                    return this.current >= this.sourceLength;
                }
            }

            public static bool FormatHasHyphen(string format)
            {
                var hasHyphen = false;
                switch (format)
                {
                    case "D":
                    case "B":
                    case "P":
                        hasHyphen = true;
                        break;
                }

                return hasHyphen;
            }

            public bool Parse(string format, out Comb result)
            {
                if (format == "X")
                {
                    return this.TryParseHex(out result);
                }

                return this.TryParse(format, out result);
            }

            public bool Parse(out Comb result)
            {
                var format = String.Empty;

                switch (this.sourceLength)
                {
                    case 32:
                        format = "N";
                        break;

                    case 36:
                        format = "D";
                        break;

                    case 38:
                        switch (this.source[0])
                        {
                            case '{':
                                format = "B";
                                break;

                            case '(':
                                format = "P";
                                break;
                        }

                        break;
                }

                if (this.TryParse(format, out result))
                {
                    return true;
                }

                this.Reset();
                return this.TryParseHex(out result);
            }

            private void Reset()
            {
                this.current = 0;
                this.sourceLength = this.source.Length;
            }

            private bool TryParse(string format, out Comb result)
            {
                result = new Comb();

                if ((format == "B" && !this.ParseChar('{')) || (format == "P" && !this.ParseChar('(')))
                {
                    return false;
                }

                if (!this.ParseHex(8, true, out var a))
                {
                    return false;
                }

                var hasHyphen = FormatHasHyphen(format);

                if (hasHyphen && !this.ParseChar('-'))
                {
                    return false;
                }

                if (!this.ParseHex(4, true, out var b))
                {
                    return false;
                }

                if (hasHyphen && !this.ParseChar('-'))
                {
                    return false;
                }

                if (!this.ParseHex(4, true, out var c))
                {
                    return false;
                }

                if (hasHyphen && !this.ParseChar('-'))
                {
                    return false;
                }

                var d = new byte[8];
                for (var i = 0; i < d.Length; i++)
                {
                    if (!this.ParseHex(2, true, out var dd))
                    {
                        return false;
                    }

                    if (i == 1 && hasHyphen && !this.ParseChar('-'))
                    {
                        return false;
                    }

                    d[i] = (byte)dd;
                }

                if ((format == "B" && !this.ParseChar('}')) || (format == "P" && !this.ParseChar(')')))
                {
                    return false;
                }

                if (!this.EOF)
                {
                    return false;
                }

                result = new Comb((int)a, (short)b, (short)c, d);
                return true;
            }

            private bool TryParseHex(out Comb result)
            {
                result = new Comb();

                if (!(this.ParseChar('{')
                && this.ParseHexPrefix()
                && this.ParseHex(8, false, out var a)
                && this.ParseCharWithWhiteSpaces(',')
                && this.ParseHexPrefix()
                && this.ParseHex(4, false, out var b)
                && this.ParseCharWithWhiteSpaces(',')
                && this.ParseHexPrefix()
                && this.ParseHex(4, false, out var c)
                && this.ParseCharWithWhiteSpaces(',')
                && this.ParseCharWithWhiteSpaces('{')))
                {
                    return false;
                }

                var d = new byte[8];
                for (var i = 0; i < d.Length; ++i)
                {
                    if (!(this.ParseHexPrefix() && this.ParseHex(2, false, out var dd)))
                    {
                        return false;
                    }

                    d[i] = (byte)dd;

                    if (i != 7 && !this.ParseCharWithWhiteSpaces(','))
                    {
                        return false;
                    }
                }

                if (!(this.ParseCharWithWhiteSpaces('}') && this.ParseCharWithWhiteSpaces('}')))
                {
                    return false;
                }

                if (!this.EOF)
                {
                    return false;
                }

                result = new Comb((int)a, (short)b, (short)c, d);
                return true;
            }

            private bool ParseHexPrefix()
            {
                for (; this.current < this.sourceLength - 1; ++this.current)
                {
                    var ch = this.source[this.current];
                    if (ch == '0')
                    {
                        ++this.current;
                        return this.source[this.current++] == 'x';
                    }

                    if (!Char.IsWhiteSpace(ch))
                    {
                        break;
                    }
                }

                return false;
            }

            private bool ParseCharWithWhiteSpaces(char c)
            {
                while (!this.EOF)
                {
                    var ch = this.source[this.current++];
                    if (ch == c)
                    {
                        return true;
                    }

                    if (!Char.IsWhiteSpace(ch))
                    {
                        break;
                    }
                }

                return false;
            }

            private bool ParseChar(char c)
            {
                if (!this.EOF && this.source[this.current] == c)
                {
                    this.current++;
                    return true;
                }

                return false;
            }

            private bool ParseHex(int length, bool strict, out ulong result)
            {
                result = 0;

                for (var i = 0; i < length; i++)
                {
                    if (this.EOF)
                    {
                        return !(strict && (i + 1 != length));
                    }

                    var c = this.source[this.current];
                    if (Char.IsDigit(c))
                    {
                        result = ((result * 16) + c) - '0';
                        this.current++;
                        continue;
                    }

                    if ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                    {
                        result = (((result * 16) + c) - (c >= 'a' ? 'a' : 'A')) + 10;
                        this.current++;
                        continue;
                    }

                    if (!strict)
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }
        }
    }
}