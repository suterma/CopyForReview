using System;
using System.Text.RegularExpressions;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A formatter to format C-Sharp code suitable for display in a foswiki.
    /// </summary>
    public class CodeReviewFormatterCSharpToFoswiki
    {
        /// <summary>
        ///     Gets the formatted text for the given selection and code location.
        /// </summary>
        /// <remarks>
        ///     This uses the foswiki syntax highlighter plugin for multiline code, but the much simpler monospace styles for
        ///     one-liners.
        /// </remarks>
        /// <param name="selectedText">The selected text.</param>
        /// <param name="codeLocationInfo">The code location information.</param>
        /// <returns></returns>
        public String GetFormattedText(string selectedText, CodeLocationInfo codeLocationInfo)
        {
            String reviewableText = String.Empty;
            const string formattingTag = "_";
            const string lineSeparator = " <br> ";
            //Single line styles with =
            if (codeLocationInfo.LineNumberTop == codeLocationInfo.LineNumberBottom)
            {
                reviewableText += "=" + selectedText + "=" + lineSeparator;
            }
            else
            {
            //Multiline styles with syntax highligher
            //http://www.w3c.br/System_bkp/DpSyntaxHighlighterPlugin
                reviewableText += @"%CODE_DP{lang=""C#"" firstline=""" + codeLocationInfo.LineNumberTop + @"""}%" +
                              Environment.NewLine +
                              selectedText + Environment.NewLine +
                              "%ENDCODE%" + Environment.NewLine;
            }
            if (!String.IsNullOrEmpty(codeLocationInfo.Methodname))
            {
                reviewableText += formattingTag + "in method " + EscapeWikiWord(codeLocationInfo.Methodname) + formattingTag +
                              lineSeparator;
            }
            if (!String.IsNullOrEmpty(codeLocationInfo.FullClassname))
            {
                reviewableText += formattingTag + "in class " + codeLocationInfo.FullClassname + formattingTag + lineSeparator;
            }
            reviewableText += formattingTag + @"in file <a href=""file:///" + codeLocationInfo.Filename + @""">" +
                          codeLocationInfo.Filename + "</a>" + formattingTag + lineSeparator;

            if (codeLocationInfo.LineNumberTop == codeLocationInfo.LineNumberBottom)
            {
                reviewableText += formattingTag + "on line " + codeLocationInfo.LineNumberTop + formattingTag +
                              lineSeparator;
            }
            else
            {
                reviewableText += formattingTag + "on lines " + codeLocationInfo.LineNumberTop + " to " +
                              codeLocationInfo.LineNumberBottom + formattingTag + lineSeparator;
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