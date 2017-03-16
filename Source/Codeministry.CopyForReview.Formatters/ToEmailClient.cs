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
using Codeministry.CopyForReview.Data;

namespace Codeministry.CopyForReview.Formatters {
    /// <summary>
    ///     A formatter to format code suitable for sending in an email client.
    /// </summary>
    public class ToEmailClient : DotLiquidFormatter {
        /// <summary>
        ///     Gets the template source.
        /// </summary>
        /// <value>
        ///     The template source.
        /// </value>
        public override string TemplateSource {
            get { return GetTextResource("Codeministry.CopyForReview.Formatters.ToText.txt"); }
        }

        /// <summary>
        ///     Gets the name of this formatter.
        /// </summary>
        /// <value>
        ///     The name of this formatter.
        /// </value>
        public override string Name {
            get { return "Send by email"; }
        }

        /// <summary>
        ///     Gets the description for this formatter.
        /// </summary>
        /// <value>
        ///     The description for this formatter.
        /// </value>
        public override string Description {
            get { return "Formats as text message for sending with the default email client"; }
        }

        /// <summary>
        ///     Gets the icon image.
        /// </summary>
        /// <value>
        ///     The icon image.
        /// </value>
        public override Bitmap IconImage {
            get { return GetBitmapResource("Codeministry.CopyForReview.Formatters.email_48x48.png"); }
        }

        /// <summary>
        ///     Formats the specified snippet.
        /// </summary>
        /// <param name="snippet">The snippet.</param>
        /// <returns></returns>
        public override string Format(ISnippet snippet) {
            var output = base.Format(snippet);
            var subject = MailToEncode(String.Format("About the code in {0}", snippet.Filename));
            var recipient = MailToEncode("");
            var body = MailToEncode(output);

            var mailTo = string.Format("mailto:{0}?subject={1}&body={2}", recipient, subject, body);

            //Send mail via default mail application
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = mailTo;
            proc.Start();
            return null;
        }

        /// <summary>
        ///     Encodes a string for usage as part of a mailto URL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The input as encoded string</returns>
        /// <devdoc>This has been successfully tested with Thunderbird.</devdoc>
        private String MailToEncode(string input) {
            if (input != null) {
                input = Uri.EscapeUriString(input);
            }
            return input;
        }
    }
}