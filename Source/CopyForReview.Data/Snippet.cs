using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace CopyForReview.Data
{
    /// <summary>
    /// A code snippet to copy for formatted output.
    /// </summary>
    public class Snippet : ISnippet
    {
        /// <summary>
        /// Gets or sets the full filename, with path and extension.
        /// </summary>
        /// <value>
        /// The the full filename, with path and extension.
        /// </value>
        public string FullFilename { get; set; }
        /// <summary>
        /// Gets or sets the line number of the topmost line.
        /// </summary>
        /// <value>
        /// The line line number of the topmost line.
        /// </value>
        public int LineNumberTop { get; set; }
        /// <summary>
        /// Gets or sets the line number of the bottommost line.
        /// </summary>
        /// <value>
        /// The line number of the bottommost line.
        /// </value>
        public int LineNumberBottom { get; set; }
        /// <summary>
        /// Gets or sets the fully qualified class name where the snippet is in (if any).
        /// </summary>
        /// <value>
        /// The fully qualified class name where the snippet is in (if any).
        /// </value>
        public string FullClassname { get; set; }
        /// <summary>
        /// Gets or sets the method name where the snippet is in (if any).
        /// </summary>
        /// <value>
        /// The method name where the snippet is in (if any).
        /// </value>
        public string Methodname { get; set; }

        /// <summary>
        /// Gets the individual code lines of the snippet.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public IEnumerable<string> Lines
        {
            get
            {
                //parse the selected text along the line breaks.
                string[] lines = SelectedText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                return lines;
            }
        }
        /// <summary>
        /// Gets or sets the originally selected text as simple string.
        /// </summary>
        /// <value>
        /// The selected text.
        /// </value>
        public string SelectedText { get; set; }
    }
}
