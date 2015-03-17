﻿using System.Drawing;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A formatter to format C-Sharp code suitable for display in a foswiki.
    /// </summary>
    public class CSharpToFoswiki : DotLiquidFormatter
    {
        /// <summary>
        ///     Gets the template source.
        /// </summary>
        /// <value>
        ///     The template source.
        /// </value>
        public override string TemplateSource
        {
            get { return GetTextResource("CopyForReview.Formatters.CSharpToFoswiki.txt"); }
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public override string Name
        {
            get { return "Review in Foswiki"; }
        }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public override string Description
        {
            get { return "Formats the snippet to use with the syntax highlighter plugin in Foswiki"; }
        }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public override Bitmap IconImage
        {
            get { return GetBitmapResource("CopyForReview.Formatters.foswiki.png"); }
        }
    }
}