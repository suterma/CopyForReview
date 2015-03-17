using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
