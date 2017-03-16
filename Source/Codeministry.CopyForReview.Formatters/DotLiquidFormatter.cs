#region copyright

// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2017 by marcel suter, marcel@codeministry.ch
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Codeministry.CopyForReview.Data;
using DotLiquid;

namespace Codeministry.CopyForReview.Formatters {
    /// <summary>
    ///     A formatter to format code using a dotLiquid template.
    /// </summary>
    public abstract class DotLiquidFormatter : IFormatter {
        /// <summary>
        ///     Gets the text resource with the given name from the currently executing assembly.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The resource with the given name from the currently executing assembly.</returns>
        protected String GetTextResource(String resourceName) {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (StreamReader reader = new StreamReader(stream)) {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        /// <summary>
        ///     Gets the bitmap resource with the given name from the currently executing assembly.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The resource with the given name from the currently executing assembly.</returns>
        protected Bitmap GetBitmapResource(String resourceName) {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                return new Bitmap(Image.FromStream(stream));
            }
        }


        /// <summary>
        ///     Gets the template source.
        /// </summary>
        /// <value>
        ///     The template source.
        /// </value>
        public abstract String TemplateSource { get; }

        /// <summary>
        ///     Gets the formatted text for the given selection and code location.
        /// </summary>
        /// <param name="snippet">The snippet.</param>
        /// <returns>
        ///     The specified code snippet in the output format.
        /// </returns>
        /// <remarks>
        ///     This uses the foswiki syntax highlighter plugin for multiline code, but the much simpler monospace styles for
        ///     one-liners.
        /// </remarks>
        public virtual String Format(ISnippet snippet) {
            //Parse and Compile
            Template template = Template.Parse(TemplateSource); // Parses and compiles the template
            var output = template.Render(Hash.FromAnonymousObject(snippet)); // Renders the output

            //Output the formatted text
            return output;
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public abstract string Description { get; }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public abstract Bitmap IconImage { get; }
    }
}