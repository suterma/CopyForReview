// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2015 by marcel suter, marcel@codeministry.ch
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

using System;
using System.Drawing;

namespace Codeministry.CopyForReview.Formatters {
    /// <summary>
    ///     A customizable formatter, using a custom dotLiquid template source.
    /// </summary>
    public class ToCustomDotLiquidFormatter : DotLiquidFormatter {
        private readonly String _templateSource;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToCustomDotLiquidFormatter" /> class.
        /// </summary>
        /// <param name="templateSource">The template source.</param>
        public ToCustomDotLiquidFormatter(String templateSource) {
            _templateSource = templateSource;
        }

        /// <summary>
        /// Gets the template source.
        /// </summary>
        /// <value>
        /// The template source.
        /// </value>
        public override string TemplateSource {
            get { return _templateSource; }
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public override string Name {
            get { return "Custom"; }
        }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public override string Description {
            get { return "Custom formatter"; }
        }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public override Bitmap IconImage {
            get { return GetBitmapResource("Codeministry.CopyForReview.Formatters.kblackbox_48x48.png"); }
        }
    }
}