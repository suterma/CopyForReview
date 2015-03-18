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

using System.ComponentModel.Design;
using System.Globalization;
using Codeministry.CopyForReview;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace Codeministry.CopyForReview_IntegrationTests
{
    [TestClass()]
    public class MenuItemTest
    {
        private delegate void ThreadInvoker();

        private TestContext testContextInstance;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        ///     A test for lauching the command and closing the associated dialogbox
        /// </summary>
        /// <devdoc>Ignored, because we do not use the tested dialog box currently.</devdoc>
        [TestMethod()]
        [HostType("VS IDE")]
        [Ignore()]
        public void LaunchCommand()
        {
            UIThreadInvoker.Invoke((ThreadInvoker) delegate()
            {
                CommandID menuItemCmd = new CommandID(GuidList.GuidCopyForReviewCmdSet, (int) PkgCmdIDList.cmdidCopyForReview);

                // Create the DialogBoxListener Thread.
                string expectedDialogBoxText = string.Format(CultureInfo.CurrentCulture, "{0}\n\nInside {1}.MenuItemCallback()", "CopyForReview", "Codeministry.CopyForReview.CopyForReviewPackage");
                DialogBoxPurger purger = new DialogBoxPurger(NativeMethods.IDOK, expectedDialogBoxText);

                try {
                    purger.Start();

                    TestUtils testUtils = new TestUtils();
                    testUtils.ExecuteCommand(menuItemCmd);
                }
                finally {
                    Assert.IsTrue(purger.WaitForDialogThreadToTerminate(), "The dialog box has not shown");
                }
            });
        }
    }
}