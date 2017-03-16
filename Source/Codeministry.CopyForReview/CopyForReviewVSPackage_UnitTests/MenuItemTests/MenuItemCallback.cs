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

using System.ComponentModel.Design;
using System.Reflection;
using Codeministry.CopyForReview;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.UnitTestLibrary;

namespace Codeministry.CopyForReview_UnitTests.MenuItemTests {
    [TestClass()]
    public class MenuItemTest {
        /// <summary>
        ///     Verify that a new menu command object gets added to the OleMenuCommandService.
        ///     This action takes place In the Initialize method of the Package object
        /// </summary>
        /// <devdoc>Ignored, because we do not use the tested item.</devdoc>
        [TestMethod]
        [Ignore]
        public void InitializeMenuCommand() {
            // Create the package
            IVsPackage package = new CopyForReviewPackage() as IVsPackage;
            Assert.IsNotNull(package, "The object does not implement IVsPackage");

            // Create a basic service provider
            OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

            // Site the package
            Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

            //Verify that the menu command can be found
            CommandID menuCommandID = new CommandID(GuidList.GuidCopyForReviewCmdSet, (int) PkgCmdIDList.cmdidCopyForReview);
            System.Reflection.MethodInfo info = typeof(Package).GetMethod("GetService", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info);
            OleMenuCommandService mcs = info.Invoke(package, new object[] {(typeof(IMenuCommandService))}) as OleMenuCommandService;
            Assert.IsNotNull(mcs.FindCommand(menuCommandID));
        }

        /// <devdoc>Ignored, because we do not use the tested item.</devdoc>
        [TestMethod]
        [Ignore]
        public void MenuItemCallback() {
            // Create the package
            IVsPackage package = new CopyForReviewPackage() as IVsPackage;
            Assert.IsNotNull(package, "The object does not implement IVsPackage");

            // Create a basic service provider
            OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

            // Create a UIShell service mock and proffer the service so that it can called from the MenuItemCallback method
            BaseMock uishellMock = UIShellServiceMock.GetUiShellInstance();
            serviceProvider.AddService(typeof(SVsUIShell), uishellMock, true);

            // Site the package
            Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

            //Invoke private method on package class and observe that the method does not throw
            System.Reflection.MethodInfo info = package.GetType().GetMethod("MenuItemCallback", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, "Failed to get the private method MenuItemCallback throug refplection");
            info.Invoke(package, new object[] {null, null});

            //Clean up services
            serviceProvider.RemoveService(typeof(SVsUIShell));
        }
    }
}