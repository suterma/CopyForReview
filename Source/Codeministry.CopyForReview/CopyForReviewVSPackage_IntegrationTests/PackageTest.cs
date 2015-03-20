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
using Codeministry.CopyForReview;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace Codeministry.CopyForReview_IntegrationTests {
    /// <summary>
    ///     Integration test for package validation
    /// </summary>
    [TestClass]
    public class PackageTest {
        private delegate void ThreadInvoker();

        private TestContext testContextInstance;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void PackageLoadTest() {
            UIThreadInvoker.Invoke((ThreadInvoker) delegate()
            {
                //Get the Shell Service
                IVsShell shellService = VsIdeTestHostContext.ServiceProvider.GetService(typeof (SVsShell)) as IVsShell;
                Assert.IsNotNull(shellService);

                //Validate package load
                IVsPackage package;
                Guid packageGuid = new Guid(GuidList.GuidCopyForReviewPkgString);
                Assert.IsTrue(0 == shellService.LoadPackage(ref packageGuid, out package));
                Assert.IsNotNull(package, "Package failed to load");
            });
        }
    }
}