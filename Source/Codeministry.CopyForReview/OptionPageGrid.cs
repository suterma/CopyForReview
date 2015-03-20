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
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Codeministry.CopyForReview {
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionPageGrid : DialogPage {
        [Category("CopyForReview")]
        [DisplayName("Selected formatter")]
        [Description("Name of the selected formatter")]
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
        [Category("CopyForReview")]
        [DisplayName("Select full lines")]
        [Description("Determines whether the selection is expanded to contain full lines")]
        [DefaultValue(typeof (bool), "true")]
        public bool SelectFullLines { get; set; }

        /// <summary>
        ///     Reset settings to their default values.
        /// </summary>
        public override void ResetSettings() {
            base.ResetSettings();

            //TODO does not work
            SelectFullLines = true;
        }
    }
}