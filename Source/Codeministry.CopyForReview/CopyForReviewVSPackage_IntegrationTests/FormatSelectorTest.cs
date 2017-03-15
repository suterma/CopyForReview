using System;
using System.Windows.Documents;
using Codeministry.CopyForReview.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Codeministry.CopyForReview_IntegrationTests
{
    /// <summary>
    /// Tests for the FormatSelector
    /// </summary>
    [TestClass]
    public class FormatSelectorTest
    {
        [TestMethod]
        [Ignore] //because this is just for visual testing
        public void TestVisualAppearance() {
            FormatSelector target = new FormatSelector("test", new List<Codeministry.CopyForReview.Formatters.IFormatter>());
            //TODO use VS 2015 compatible window type 
            //target.ShowDialog();
            Assert.Inconclusive("Test for visual inspection");
        }
    }
}
