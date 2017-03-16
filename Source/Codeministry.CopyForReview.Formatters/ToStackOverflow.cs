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

using System.Drawing;

namespace Codeministry.CopyForReview.Formatters {
    /// <summary>
    ///     A formatter to format code as markup suitable for StackOverflow questions.
    /// </summary>
    public class ToStackOverflow : DotLiquidFormatter {
        /// <summary>
        ///     Gets the template source.
        /// </summary>
        /// <value>
        ///     The template source.
        /// </value>
        public override string TemplateSource {
            get { return GetTextResource("Codeministry.CopyForReview.Formatters.ToStackOverflow.txt"); }
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public override string Name {
            get { return "Question in Stackoverflow"; }
        }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public override string Description {
            get { return "Formats the snippet with the code markup for a question on the StackOverflow Q&A site."; }
        }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public override Bitmap IconImage {
            get { return GetBitmapResource("Codeministry.CopyForReview.Formatters.so-icon_48x48.png"); }
        }
    }
}