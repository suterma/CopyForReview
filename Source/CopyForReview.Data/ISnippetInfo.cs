using System;
using System.Collections.Generic;

namespace CopyForReview.Data
{
    /// <summary>
    /// Defines a code snippet to copy for formatted output.
    /// </summary>
    public interface ISnippet
    {
        /// <summary>
        /// Gets or sets the full filename, with path and extension.
        /// </summary>
        /// <value>
        /// The the full filename, with path and extension.
        /// </value>
        String FullFilename { get; set; }

        /// <summary>
        /// Gets or sets the filename, without path.
        /// </summary>
        /// <value>
        /// The the filename, without path.
        /// </value>
        String Filename { get; set; }

        /// <summary>
        /// Gets or sets the line number of the topmost line.
        /// </summary>
        /// <value>
        /// The line line number of the topmost line.
        /// </value>
        int LineNumberTop { get; set; }
        /// <summary>
        /// Gets or sets the line number of the bottommost line.
        /// </summary>
        /// <value>
        /// The line number of the bottommost line.
        /// </value>
        int LineNumberBottom { get; set; }
        /// <summary>
        /// Gets or sets the fully qualified class name where the snippet is in (if any).
        /// </summary>
        /// <value>
        /// The fully qualified class name where the snippet is in (if any).
        /// </value>
        String FullClassname { get; set; }
        /// <summary>
        /// Gets or sets the method name where the snippet is in (if any).
        /// </summary>
        /// <value>
        /// The method name where the snippet is in (if any).
        /// </value>
        String Methodname { get; set; }

        /// <summary>
        /// Gets the individual code lines of the snippet.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        IEnumerable<String> Lines { get; }

        /// <summary>
        /// Gets the individual code lines of the snippet with the indentation removed as much as possible.
        /// </summary>
        /// <value>
        /// The individual code lines of the snippet with the indentation removed as much as possible.
        /// </value>
        IEnumerable<String> DeindentedLines { get; }

        /// <summary>
        /// Gets or sets the originally selected text as simple string.
        /// </summary>
        /// <value>
        /// The selected text.
        /// </value>
        string SelectedText { get; set; }
    }
}