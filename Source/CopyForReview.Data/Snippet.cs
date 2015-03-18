﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CopyForReview.Data
{
    /// <summary>
    ///     A code snippet to copy for formatted output.
    /// </summary>
    public class Snippet : ISnippet
    {
        /// <summary>
        ///     Gets the selected text as simple string, with the indentation removed as much as possible.
        /// </summary>
        /// <value>
        ///     The selected text, with the indentation removed as much as possible.
        /// </value>
        public String DeindentedSelectedText
        {
            get { return String.Join(Environment.NewLine, DeindentedLines); }
        }

        /// <summary>
        ///     Gets or sets the full filename, with path and extension.
        /// </summary>
        /// <value>
        ///     The the full filename, with path and extension.
        /// </value>
        public string FullFilename { get; set; }

        /// <summary>
        /// Gets or sets the filename, without path.
        /// </summary>
        /// <value>
        /// The the filename, without path.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        ///     Gets or sets the line number of the topmost line.
        /// </summary>
        /// <value>
        ///     The line line number of the topmost line.
        /// </value>
        public int LineNumberTop { get; set; }

        /// <summary>
        ///     Gets or sets the line number of the bottommost line.
        /// </summary>
        /// <value>
        ///     The line number of the bottommost line.
        /// </value>
        public int LineNumberBottom { get; set; }

        /// <summary>
        ///     Gets or sets the fully qualified class name where the snippet is in (if any).
        /// </summary>
        /// <value>
        ///     The fully qualified class name where the snippet is in (if any).
        /// </value>
        public string FullClassname { get; set; }

        /// <summary>
        ///     Gets or sets the method name where the snippet is in (if any).
        /// </summary>
        /// <value>
        ///     The method name where the snippet is in (if any).
        /// </value>
        public string Methodname { get; set; }

        /// <summary>
        ///     Gets the individual code lines of the snippet.
        /// </summary>
        /// <value>
        ///     The lines.
        /// </value>
        public IEnumerable<string> Lines
        {
            get
            {
                //parse the selected text along the line breaks.
                string[] lines = SelectedText.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
                return lines;
            }
        }

        /// <summary>
        ///     Gets the individual code lines of the snippet with the indentation removed as much as possible.
        /// </summary>
        /// <value>
        ///     The individual code lines of the snippet with the indentation removed as much as possible.
        /// </value>
        public IEnumerable<string> DeindentedLines
        {
            get
            {
                var commonIndent = CommonIndent();
                var deindented = Lines.Select(line => Deindent(line, commonIndent));
                return deindented;
            }
        }

        /// <summary>
        ///     Gets or sets the selected text as simple string.
        /// </summary>
        /// <value>
        ///     The selected text.
        /// </value>
        public string SelectedText { get; set; }

        /// <summary>
        ///     Deindents the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="indentCount">The indent character removal count.</param>
        /// <returns></returns>
        public static String Deindent(String line, int indentCount)
        {
            int maxRemoval = Math.Min(line.Length, indentCount);
            return line.Substring(maxRemoval);
        }

        public int CommonIndent()
        {
            var commonIndent =
                (from line in Lines
                    where (IsNonEmpty(line))
                    select GetLeadingWhitespaceCount(line)).Min();
            return commonIndent;
        }

        /// <summary>
        ///     Determines whether the specified line is not emtpy.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether the specified line is not emtpy.</returns>
        private bool IsNonEmpty(string line)
        {
            return !String.IsNullOrWhiteSpace(line);
        }

        /// <summary>
        ///     Gets the leading whitespace count.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public static int GetLeadingWhitespaceCount(string line)
        {
            return Regex.Match(line, @"^([\s]+)").Groups[1].Value.Length;
        }
    }
}