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

using Codeministry.CopyForReview.Data;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codeministry.CopyForReview.Formatters.UnitTests {
    [TestClass]
    public class ToFoswikiTests {
        [TestMethod]
        public void TestFormatMultiline1() {
            //Arrange
            const string selectedText = "class test";
            var testSnippet = new Snippet
            {
                SelectedText = selectedText,
                FullClassname = "testnamespace.TestClass",
                LineNumberTop = 10,
                LineNumberBottom = 11,
                FullFilename = @"c:\test\TestFileName.cs",
                Methodname = "TestMethod"
            };

            //Act
            var actual = new ToFoswiki().Format(testSnippet);

            //Assert
            actual.Should().Be("\r\n%CODE_DP{lang=\"C#\" firstline=\"10\"}%\r\nclass test\r\n%ENDCODE%\r\n_in method_ =TestMethod= <br> _in class_ =testnamespace.TestClass= <br> _in file <a href=\"c:\\test\\TestFileName.cs\">c:\\test\\TestFileName.cs</a>_ <br> _on lines 10 to 11_ <br> ");
        }

        [TestMethod]
        public void TestFormatMultilineWithoutMethod() {
            //Arrange
            var selectedText = new System.Text.StringBuilder(305);
            selectedText.AppendLine(@"        public const string guidCopyForReviewPkgString = ""193eba43-9462-4945-ba4e-79f04dbadc94"";");
            selectedText.AppendLine(@"        public const string guidCopyForReviewCmdSetString = ""4ae6ff5a-6e7e-48bd-86b0-37fd9ab20629"";");
            selectedText.AppendLine(@"");
            selectedText.AppendLine(@"        public static readonly Guid guidCopyForReviewCmdSet = new Guid(guidCopyForReviewCmdSetString);");
            ;
            var testSnippet = new Snippet
            {
                SelectedText = selectedText.ToString(),
                FullClassname = "Company.CopyForReview.GuidList",
                LineNumberTop = 10,
                LineNumberBottom = 13,
                FullFilename = @"c:\test\TestFileName.cs",
            };

            //Act
            var actual = new ToFoswiki().Format(testSnippet);

            //Assert
            actual.Should().Be("\r\n%CODE_DP{lang=\"C#\" firstline=\"10\"}%\r\npublic const string guidCopyForReviewPkgString = \"193eba43-9462-4945-ba4e-79f04dbadc94\";\r\npublic const string guidCopyForReviewCmdSetString = \"4ae6ff5a-6e7e-48bd-86b0-37fd9ab20629\";\r\n\r\npublic static readonly Guid guidCopyForReviewCmdSet = new Guid(guidCopyForReviewCmdSetString);\r\n\r\n%ENDCODE%\r\n_in class_ =Company.CopyForReview.GuidList= <br> _in file <a href=\"c:\\test\\TestFileName.cs\">c:\\test\\TestFileName.cs</a>_ <br> _on lines 10 to 13_ <br> ");
        }
    }
}