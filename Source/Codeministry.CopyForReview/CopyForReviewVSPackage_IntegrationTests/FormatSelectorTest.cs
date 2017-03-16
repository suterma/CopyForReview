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

using System.Collections.Generic;
using Codeministry.CopyForReview.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codeministry.CopyForReview_IntegrationTests {
    /// <summary>
    ///     Tests for the FormatSelector
    /// </summary>
    [TestClass]
    public class FormatSelectorTest {
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