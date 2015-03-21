// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2015 by marcel suter, marcel@codeministry.ch
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codeministry.CopyForReview.Data {
    /// <summary>
    ///     A code snippet to copy for formatted output.
    /// </summary>
    public class Snippet : ISnippet {
        /// <summary>
        ///     Gets the selected text as simple string, with the indentation removed as much as possible.
        /// </summary>
        /// <value>
        ///     The selected text, with the indentation removed as much as possible.
        /// </value>
        public String DeindentedSelectedText {
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
        ///     Gets or sets the filename, without path.
        /// </summary>
        /// <value>
        ///     The the filename, without path.
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
        public IEnumerable<string> Lines {
            get {
                //parse the selected text along the line breaks.
                string[] lines = SelectedText.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
                return lines;
            }
        }

        /// <summary>
        ///     Gets the individual code lines of the snippet with the indentation removed as much as possible.
        /// </summary>
        /// <value>
        ///     The individual code lines of the snippet with the indentation removed as much as possible.
        /// </value>
        public IEnumerable<string> DeindentedLines {
            get {
                var commonIndent = GetCommonIndent();
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
        ///     Gets or sets the file extension.
        /// </summary>
        /// <value>
        ///     The file extension.
        /// </value>
        public String FileExtension { get; set; }

        /// <summary>
        ///     Deindents the specified line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="indentCount">The indent character removal count.</param>
        /// <returns></returns>
        public static String Deindent(String line, int indentCount) {
            int maxRemoval = Math.Min(line.Length, indentCount);
            return line.Substring(maxRemoval);
        }

        /// <summary>
        ///     Gets the common indent of the snippet's (non-empty) lines.
        /// </summary>
        /// <returns></returns>
        public int GetCommonIndent() {
            var codeLines = Lines.Where(line => (IsNonEmpty(line)));

            if (codeLines.Any()) {
                var commonIndent =
                    (from line in Lines
                        where (IsNonEmpty(line))
                        select GetLeadingWhitespaceCount(line)).Min();
                return commonIndent;
            }
            else {
                //no common indetation applicable
                return 0;
            }
        }

        /// <summary>
        ///     Determines whether the specified line is not emtpy.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether the specified line is not emtpy.</returns>
        private bool IsNonEmpty(string line) {
            return !String.IsNullOrWhiteSpace(line);
        }

        /// <summary>
        ///     Gets the leading whitespace count.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public static int GetLeadingWhitespaceCount(string line) {
            return Regex.Match(line, @"^([\s]+)").Groups[1].Value.Length;
        }
    }
}