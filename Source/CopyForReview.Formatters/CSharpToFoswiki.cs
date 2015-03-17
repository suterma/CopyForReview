using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using CopyForReview.Data;
using DotLiquid;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A formatter to format C-Sharp code suitable for display in a foswiki.
    /// </summary>
    public class CSharpToFoswiki:IFormatter
    {
        /// <summary>
        /// Gets the resource with the given name from the currently executing assembly.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The resource with the given name from the currently executing assembly.</returns>
        private String GetResource(String resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }

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
        public String Format(ISnippet snippet)
        {
            String output = String.Empty;
            //Load the dotLiquid Template
            var templateSource = GetResource("CopyForReview.Formatters.CSharpToFoswiki.txt");

            //Parse and Compile
            Template template = Template.Parse(templateSource);  // Parses and compiles the template
            output = template.Render(Hash.FromAnonymousObject(snippet)); // Renders the output

            //Output the formatted text
            return output;
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