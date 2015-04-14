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

using Codeministry.CopyForReview;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codeministry.CopyForReview_UnitTests {
    [TestClass()]
    public class PackageTest {
        [TestMethod()]
        public void CreateInstance() {
            CopyForReviewPackage package = new CopyForReviewPackage();
        }

        [TestMethod()]
        public void IsIVsPackage() {
            CopyForReviewPackage package = new CopyForReviewPackage();
            Assert.IsNotNull(package as IVsPackage, "The object does not implement IVsPackage");
        }
    }
}