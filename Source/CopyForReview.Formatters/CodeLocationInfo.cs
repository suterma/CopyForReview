using System;

namespace CopyForReview.Formatters
{
    /// <summary>
    /// Provides Information about the code snippet to copy
    /// </summary>
    public class CodeLocationInfo
    {
        /// <summary>
        /// Gets or sets the full filename, with path and extension.
        /// </summary>
        /// <value>
        /// The the full filename, with path and extension.
        /// </value>
        public String Filename { get; set; }
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
        public String FullClassname { get; set; }
        /// <summary>
        /// Gets or sets the method name where the snippet is in (if any).
        /// </summary>
        /// <value>
        /// The method name where the snippet is in (if any).
        /// </value>
        public String Methodname { get; set; }
    }
}