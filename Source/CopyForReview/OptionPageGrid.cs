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

        [Category("CopyForReview")]
        [DisplayName("Select full lines")]
        [Description("Determines whether the selection is expanded to contain full lines")]
        public bool SelectFullLines { get; set; }
    }
}