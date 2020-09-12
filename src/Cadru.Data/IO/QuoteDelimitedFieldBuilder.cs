using System.Text;
using System.Text.RegularExpressions;

namespace Cadru.Data.IO
{
    /// <summary>
    /// Helper class that when passed a line and an index to a quote delimited
    /// field will build the field and handle escaped quotes
    /// </summary>
    /// <remarks></remarks>
    internal partial class QuoteDelimitedFieldBuilder
    {
        // The regular expression used to find the next delimiter
        private readonly Regex m_DelimiterRegex;

        // String builder holding the field
        private readonly StringBuilder m_Field = new StringBuilder();

        // Chars that should be counted as space (and hence ignored if occurring
        // before or after a delimiter
        private readonly string m_SpaceChars;

        /// <summary>
        /// Creates an instance of the class and sets some properties
        /// </summary>
        /// <param name="delimiterRegex">
        /// The regex used to find any of the delimiters
        /// </param>
        /// <param name="spaceChars">
        /// Characters treated as space (usually space and tab)
        /// </param>
        /// <remarks></remarks>
        public QuoteDelimitedFieldBuilder(Regex delimiterRegex, string spaceChars)
        {
            this.m_DelimiterRegex = delimiterRegex;
            this.m_SpaceChars = spaceChars;
        }

        /// <summary>
        /// The length of the closing delimiter if one was found
        /// </summary>
        /// <value>The length of the delimiter</value>
        /// <remarks></remarks>
        public int DelimiterLength { get; private set; }

        /// <summary>
        /// The field being built
        /// </summary>
        /// <value>The field</value>
        /// <remarks></remarks>
        public string Field => this.m_Field.ToString();

        /// <summary>
        /// Indicates whether or not the field has been built.
        /// </summary>
        /// <value>True if the field has been built, otherwise False</value>
        /// <remarks>
        /// If the Field has been built, the Field property will return the
        /// entire field
        /// </remarks>
        public bool FieldFinished { get; private set; }

        /// <summary>
        /// The current index on the line. Used to indicate how much of the line
        /// was used to build the field
        /// </summary>
        /// <value>The current position on the line</value>
        /// <remarks></remarks>
        public int Index { get; private set; }

        /// <summary>
        /// Indicates that the current field breaks the subset of csv rules we enforce
        /// </summary>
        /// <value>True if the line is malformed, otherwise False</value>
        /// <remarks>
        /// The rules we enforce are: Embedded quotes must be escaped Only space
        /// characters can occur between a delimiter and a quote
        /// </remarks>
        public bool MalformedLine { get; private set; }

        /// <summary>
        /// Builds a field by walking through the passed in line starting at StartAt
        /// </summary>
        /// <param name="line">The line containing the data</param>
        /// <param name="startAt">
        /// The index at which we start building the field
        /// </param>
        /// <remarks></remarks>
        public void BuildField(string line, int startAt)
        {
            this.Index = startAt;
            var length = line.Length;
            while (this.Index < length)
            {
                if (line[this.Index] == '"')
                {
                    // Are we at the end of the file?
                    if (this.Index + 1 == length)
                    {
                        // We've found the end of the field
                        this.FieldFinished = true;
                        this.DelimiterLength = 1;

                        // Move index past end of line
                        this.Index += 1;
                        return;
                    }
                    // Check to see if this is an escaped quote
                    if (this.Index + 1 < line.Length && line[this.Index + 1] == '"')
                    {
                        this.m_Field.Append('"');
                        this.Index += 2;
                        continue;
                    }

                    // Find the next delimiter and make sure everything between
                    // the quote and the delimiter is ignorable
                    int limit;
                    var delimiterMatch = this.m_DelimiterRegex.Match(line, this.Index + 1);
                    if (!delimiterMatch.Success)
                    {
                        limit = length - 1;
                    }
                    else
                    {
                        limit = delimiterMatch.Index - 1;
                    }

                    for (int i = this.Index + 1, loopTo = limit; i <= loopTo; i++)
                    {
                        if (this.m_SpaceChars.IndexOf(line[i]) < 0)
                        {
                            this.MalformedLine = true;
                            return;
                        }
                    }

                    // The length of the delimiter is the length of the closing
                    // quote (1) + any spaces + the length of the delimiter we
                    // matched if any
                    this.DelimiterLength = 1 + limit - this.Index;
                    if (delimiterMatch.Success)
                    {
                        this.DelimiterLength += delimiterMatch.Length;
                    }

                    this.FieldFinished = true;
                    return;
                }
                else
                {
                    this.m_Field.Append(line[this.Index]);
                    this.Index += 1;
                }
            }
        }
    }
}