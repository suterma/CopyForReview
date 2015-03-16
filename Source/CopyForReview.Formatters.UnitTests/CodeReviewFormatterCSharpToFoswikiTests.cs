using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyForReview.Formatters.UnitTests
{
    [TestClass]
    public class CodeReviewFormatterCSharpToFoswikiTests
    {
        [TestMethod]
        public void TestFormatMultiline1()
        {
            //Arrange
            const string selectedText = "class test";
            var testSnippet = new SnippetInfo
            {
                SelectedText = selectedText,
                FullClassname = "testnamespace.TestClass",
                LineNumberTop = 10,
                LineNumberBottom = 11,
                FullFilename = @"c:\test\TestFileName.cs",
                Methodname = "TestMethod"
            };

            //Act
            var actual = new CodeReviewFormatterCSharpToFoswiki().Format(testSnippet);

            //Assert
            actual.Should().Be(@"%CODE_DP{{lang=""C#"" firstline=""10""}}%"+Environment.NewLine+"class test"+Environment.NewLine+"%ENDCODE%"+Environment.NewLine+@"_in method !TestMethod_ <br> _in class testnamespace.TestClass_ <br> _in file <a href=""file:///c:\test\TestFileName.cs"">c:\test\TestFileName.cs</a>_ <br> _on lines 10 to 11_ <br> ");

        }
    }
}
