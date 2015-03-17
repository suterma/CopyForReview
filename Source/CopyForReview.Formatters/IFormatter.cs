using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CopyForReview.Data;

namespace CopyForReview.Formatters
{
    /// <summary>
    /// Defines a formatter for a code snippet.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats the specified code snippet into the output format.
        /// </summary>
        /// <param name="snippet">The snippet.</param>
        /// <returns>The specified code snippet in the output format.</returns>
        String Format(ISnippet snippet);

        /// <summary>
        /// Gets the name of this formatter.
        /// </summary>
        /// <value>
        /// The name of this formatter.
        /// </value>
        String Name { get; }

        /// <summary>
        /// Gets the description for this formatter.
        /// </summary>
        /// <value>
        /// The description for this formatter.
        /// </value>
        String Description { get; }

        /// <summary>
        /// Gets the icon image.
        /// </summary>
        /// <value>
        /// The icon image.
        /// </value>
        Bitmap IconImage { get; }
    }
}
