using System;
using Codeministry.CopyForReview.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            FormatSelector target = new FormatSelector("test", true, true);
            target.ShowDialog();
            Assert.Inconclusive("Test for visual inspection");
        }
    }
}
