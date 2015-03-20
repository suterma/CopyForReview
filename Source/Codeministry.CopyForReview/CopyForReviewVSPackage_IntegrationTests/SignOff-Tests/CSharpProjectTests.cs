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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace Codeministry.CopyForReview_IntegrationTests {
    [TestClass]
    public class CSharpProjectTests {
        #region fields

        private delegate void ThreadInvoker();

        private TestContext _testContext;

        #endregion

        #region properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext {
            get { return _testContext; }
            set { _testContext = value; }
        }

        #endregion

        #region ctors

        public CSharpProjectTests() {}

        #endregion

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        [HostType("VS IDE")]
        public void WinformsApplication() {
            UIThreadInvoker.Invoke((ThreadInvoker) delegate()
            {
                TestUtils testUtils = new TestUtils();

                testUtils.CreateEmptySolution(TestContext.TestDir, "CSWinApp");
                Assert.AreEqual<int>(0, testUtils.ProjectCount());

                //Create Winforms application project
                //TestUtils.CreateProjectFromTemplate("MyWindowsApp", "Windows Application", "CSharp", false);
                //Assert.AreEqual<int>(1, TestUtils.ProjectCount());

                //TODO Verify that we can debug launch the application

                //TODO Set Break point and verify that will hit

                //TODO Verify Adding new project item to project
            });
        }
    }
}