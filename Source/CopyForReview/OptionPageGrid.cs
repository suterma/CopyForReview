using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CopyForReview
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionPageGrid : DialogPage
    {


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
        public bool SelectFullLines

        { get; set; }

        /// <summary>
        /// Reset settings to their default values.
        /// </summary>
        public override void ResetSettings()
        {
            base.ResetSettings();

            //TODO does not work
            SelectFullLines = true;
        }
    }
}