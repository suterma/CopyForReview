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
using System.IO;
using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace Codeministry.CopyForReview_IntegrationTests
{
    [TestClass]
    public class CPPProjectTests
    {
        #region fields

        private delegate void ThreadInvoker();

        private TestContext _testContext;

        #endregion

        #region properties

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        #endregion

        #region ctors

        public CPPProjectTests() {}

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

        [HostType("VS IDE")]
        [TestMethod]
        public void CPPWinformsApplication()
        {
            UIThreadInvoker.Invoke((ThreadInvoker) delegate()
            {
                //Solution and project creation parameters
                string solutionName = "CPPWinApp";
                string projectName = "CPPWinApp";

                //Template parameters
                string projectType = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";
                string projectTemplateName = Path.Combine("vcNet", "mc++appwiz.vsz");

                string itemTemplateName = "newc++file.cpp";
                string newFileName = "Test.cpp";

                DTE dte = (DTE) VsIdeTestHostContext.ServiceProvider.GetService(typeof (DTE));

                TestUtils testUtils = new TestUtils();

                testUtils.CreateEmptySolution(TestContext.TestDir, solutionName);
                Assert.AreEqual<int>(0, testUtils.ProjectCount());

                //Add new CPP Windows application project to existing solution
                string solutionDirectory = Directory.GetParent(dte.Solution.FullName).FullName;
                string projectDirectory = TestUtils.GetNewDirectoryName(solutionDirectory, projectName);
                string projectTemplatePath = Path.Combine(dte.Solution.get_TemplatePath(projectType), projectTemplateName);
                Assert.IsTrue(File.Exists(projectTemplatePath), string.Format("Could not find template file: {0}", projectTemplatePath));
                dte.Solution.AddFromTemplate(projectTemplatePath, projectDirectory, projectName, false);

                //Verify that the new project has been added to the solution
                Assert.AreEqual<int>(1, testUtils.ProjectCount());

                //Get the project
                Project project = dte.Solution.Item(1);
                Assert.IsNotNull(project);
                Assert.IsTrue(string.Compare(project.Name, projectName, StringComparison.InvariantCultureIgnoreCase) == 0);

                //Verify Adding new code file to project
                string newItemTemplatePath = Path.Combine(dte.Solution.ProjectItemsTemplatePath(projectType), itemTemplateName);
                Assert.IsTrue(File.Exists(newItemTemplatePath));
                ProjectItem item = project.ProjectItems.AddFromTemplate(newItemTemplatePath, newFileName);
                Assert.IsNotNull(item);
            });
        }
    }
}