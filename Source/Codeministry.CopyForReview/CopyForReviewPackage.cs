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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Codeministry.CopyForReview.Controls;
using Codeministry.CopyForReview.Data;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace Codeministry.CopyForReview {
    /// <summary>
    ///     This is the class that implements the package exposed by this assembly.
    ///     The minimum requirement for a class to be considered a valid package for Visual Studio
    ///     is to implement the IVsPackage interface and register itself with the shell.
    ///     This package uses the helper classes defined inside the Managed Package Framework (MPF)
    ///     to do it: it derives from the Package class that provides the implementation of the
    ///     IVsPackage interface and uses the registration attributes defined in the framework to
    ///     register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidCopyForReviewPkgString)]
    [ProvideOptionPage(typeof (OptionPageGrid),
        "CopyForReview", "General", 0, 0, true)]
    public sealed class CopyForReviewPackage : Package {
        /// <summary>
        ///     Default constructor of the package.
        ///     Inside this method you can place any initialization code that does not require
        ///     any Visual Studio service because at this point the package object is created but
        ///     not sited yet inside Visual Studio environment. The place to do all the other
        ///     initialization is the Initialize method.
        /// </summary>
        public CopyForReviewPackage() {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
        }

        #region Package Members

        /// <summary>
        ///     Initialization of the package; this method is called right after the package is sited, so this is the place
        ///     where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize() {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs) {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.GuidCopyForReviewCmdSet,
                    (int) PkgCmdIDList.cmdidCopyForReview);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);
            }

            //TODO set default, later do that only on install
            DTE2 dte = (DTE2) GetService(typeof (DTE));
            EnvDTE.Properties props =
                dte.get_Properties("CopyForReview", "General");
            props.Item("SelectFullLines").Value = true;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation

        /// <summary>
        ///     This function is the callback used to execute a command when the a menu item is clicked.
        ///     See the Initialize method to see how the menu item is associated to this function using
        ///     the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e) {
            DTE2 dte = (DTE2) GetService(typeof (DTE));

            //Apply the stored options to the selector
            EnvDTE.Properties props =
                dte.get_Properties("CopyForReview", "General");

            var selectedFormatterName = (String) props.Item("SelectedFormatterName").Value;
            var selectFullLines = (bool) props.Item("SelectFullLines").Value;
            var formatSelector = new FormatSelector(selectedFormatterName, selectFullLines);


            // Show the dialog. 
            if (formatSelector.ShowDialog() == true) {
                using (new WaitCursor()) {
                    var codeExaminer = new CodeModelExaminer(dte);

                    //Add file, class, method and line information to the text
                    var snippet = new Snippet
                    {
                        FullFilename = codeExaminer.GetFullFilename(),
                        Filename = codeExaminer.GetFilename(),
                        SelectedText = codeExaminer.CopySelection(formatSelector.IsSelectionFullLines)
                    };
                    codeExaminer.SetSelectionLineRange(snippet);
                    codeExaminer.GetCodeContext(snippet);

                    var output = formatSelector.SelectedFormatter.Format(snippet);
                    System.Windows.Clipboard.SetDataObject(output);

                    //Apply the eventually changed options
                    props.Item("SelectedFormatterName").Value = formatSelector.SelectedFormatter.Name;
                    props.Item("SelectFullLines").Value = formatSelector.IsSelectionFullLines;
                }
            }
        }
    }
}