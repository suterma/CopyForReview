using System;
using System.Text.RegularExpressions;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A formatter to format C-Sharp code suitable for display in a foswiki.
    /// </summary>
    public class CodeReviewFormatterCSharpToFoswiki:IFormatter
    {
        /// <summary>
        /// Gets the formatted text for the given selection and code location.
        /// </summary>
        /// <param name="snippet">The snippet.</param>
        /// <returns>
        /// The specified code snippet in the output format.
        /// </returns>
        /// <remarks>
        /// This uses the foswiki syntax highlighter plugin for multiline code, but the much simpler monospace styles for
        /// one-liners.
        /// </remarks>
        public String Format(SnippetInfo snippet)
        {
            String reviewableText = String.Empty;
            const string formattingTag = "_";
            const string lineSeparator = " <br> ";
            //Single line styles with =
            if (snippet.LineNumberTop == snippet.LineNumberBottom)
            {
                reviewableText += "=" + snippet.SelectedText + "=" + lineSeparator;
            }
            else
            {
            //Multiline styles with syntax highligher
            //http://www.w3c.br/System_bkp/DpSyntaxHighlighterPlugin
                reviewableText += @"%CODE_DP{lang=""C#"" firstline=""" + snippet.LineNumberTop + @"""}%" +
                              Environment.NewLine +
                              snippet.SelectedText + Environment.NewLine +
                              "%ENDCODE%" + Environment.NewLine;
            }
            if (!String.IsNullOrEmpty(snippet.Methodname))
            {
                reviewableText += formattingTag + "in method " + EscapeWikiWord(snippet.Methodname) + formattingTag +
                              lineSeparator;
            }
            if (!String.IsNullOrEmpty(snippet.FullClassname))
            {
                reviewableText += formattingTag + "in class " + snippet.FullClassname + formattingTag + lineSeparator;
            }
            reviewableText += formattingTag + @"in file <a href=""file:///" + snippet.FullFilename + @""">" +
                          snippet.FullFilename + "</a>" + formattingTag + lineSeparator;

            if (snippet.LineNumberTop == snippet.LineNumberBottom)
            {
                reviewableText += formattingTag + "on line " + snippet.LineNumberTop + formattingTag +
                              lineSeparator;
            }
            else
            {
                reviewableText += formattingTag + "on lines " + snippet.LineNumberTop + " to " +
                              snippet.LineNumberBottom + formattingTag + lineSeparator;
            }

            return reviewableText;
        }

        /// <summary>
        ///     Escapes any WikiWord in the given Text
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private static string EscapeWikiWord(string text)
        {
            //See https://regex101.com/#
            if (Regex.IsMatch(text, "^([A-Z]+[a-z]+){2,}[A-Z]*$"))
            {
                return "!" + text;
            }
            return text;
        }
    }
}