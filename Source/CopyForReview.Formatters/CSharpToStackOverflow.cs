using System.Drawing;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A formatter to format C-Sharp code as markup suitable for StackOverflow questions.
    /// </summary>
    public class CSharpToStackOverflow : DotLiquidFormatter
    {
        /// <summary>
        ///     Gets the template source.
        /// </summary>
        /// <value>
        ///     The template source.
        /// </value>
        public override string TemplateSource
        {
            get { return GetTextResource("CopyForReview.Formatters.CSharpToStackOverflow.txt"); }
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public override string Name
        {
            get { return "Question in Stackoverflow"; }
        }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public override string Description
        {
            get { return "Formats the snippet with the code markup for a question on the StackOverflow Q&A site."; }
        }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public override Bitmap IconImage
        {
            get { return GetBitmapResource("CopyForReview.Formatters.stackoverflow-icon.png"); }
        }
    }
}