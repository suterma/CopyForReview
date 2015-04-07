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
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Codeministry.CopyForReview {
    /// <summary>
    /// A dialog page containing the option for Copy for Review
    /// </summary>
    /// <devdoc>Later create more complex options using
    /// https://msdn.microsoft.com/en-us/library/cc138529.aspx
    /// </devdoc>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
   
    public class OptionPageGrid : DialogPage {
        [Category("Copy For Review")]
        [DisplayName("Selected formatter")]
        [Description("Name of the selected formatter")]
        [DefaultValue(typeof(String), "Send by email")]
        public string SelectedFormatterName { get; set; }

        /// <summary>
        ///     Determines whether the selection is expanded to contain full lines.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [select full lines]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        ///     Default is true.
        /// </remarks>
        [Category("Copy For Review")]
        [DisplayName("Select full lines")]
        [Description("Determines whether the selection is expanded to contain full lines")]
        [DefaultValue(typeof (bool), "True")]
        public bool SelectFullLines { get; set; }

        /// <summary>
        ///     Gets or sets the custom formatter template.
        /// </summary>
        [Category("Copy For Review")]
        [DisplayName("Custom formatter template")]
        [Description("Defines the template for the custom formatter. See online docs for more information.")]
        [EditorAttribute(typeof(MultilineStringEditor),
                 typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(typeof(String), @"This is an example template. See the online doc at https://github.com/suterma/CopyForReview/wiki for more information.")]
        public string CustomFormatterTemplateSource { get; set; }


        /// <summary>
        ///     Reset settings to their default values.
        /// </summary>
        public override void ResetSettings() {
            base.ResetSettings();

            //TODO does this work?
            SelectFullLines = true;
            SelectedFormatterName = "Send by email";
            var sb = new System.Text.StringBuilder(687);
            sb.AppendLine(@"{% comment %}");
            sb.AppendLine(@"This is an example dotLiquid template that shows the capabilities of a custom template.");
            sb.AppendLine(@"Method and class are optional and not provided if null");
            sb.AppendLine(@"{% endcomment %}");
            sb.AppendLine(@"This is an example template. See the online doc at https://github.com/suterma/CopyForReview/wiki for more information.");
            sb.AppendLine(@"8<------------------------------------------------------------------------------");
            sb.AppendLine(@"{{DeindentedSelectedText}}");
            sb.AppendLine(@"8<------------------------------------------------------------------------------");
            sb.AppendLine(@"{% if Methodname %}in method ""{{Methodname}}""");
            sb.AppendLine(@"{% endif %}{% if FullClassname %}in class ""{{FullClassname}}""");
            sb.AppendLine(@"{% endif %}in file {{FullFilename}}");
            sb.AppendLine(@"on lines {{LineNumberTop}} to {{LineNumberBottom}}");
            CustomFormatterTemplateSource = sb.ToString();
        }
    }
}