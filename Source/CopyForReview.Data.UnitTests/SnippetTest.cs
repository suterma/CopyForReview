using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CopyForReview.Data.UnitTests
{
    [TestClass]
    public class SnippetTest
    {
        [TestMethod]
        public void DeindentSnippetWithLargeIndent()
        {
            //Arrange
            var sb = new System.Text.StringBuilder(229);
            sb.AppendLine(@"                        var document = item.Document;");
            sb.AppendLine(@"                        if (document.Name == doc.Name)");
            sb.AppendLine(@"                        {");
            sb.AppendLine(@"                            ExamineItem(item, codeLocationInfo);");
            sb.AppendLine(@"                        }");

            var testSnippet = new Snippet
            {
                SelectedText = sb.ToString(),
            };

            //Act
            string actual = testSnippet.DeindentedSelectedText;

            //Assert
            actual.Should().Be("var document = item.Document;\r\nif (document.Name == doc.Name)\r\n{\r\n    ExamineItem(item, codeLocationInfo);\r\n}\r\n");
        }

        [TestMethod]
        public void CommonIndentWithLargeIndent()
        {
            //Arrange
            var sb = new System.Text.StringBuilder(229);
            sb.AppendLine(@"                        var document = item.Document;");
            sb.AppendLine(@"                        if (document.Name == doc.Name)");
            sb.AppendLine(@"                        {");
            sb.AppendLine(@"                            ExamineItem(item, codeLocationInfo);");
            sb.AppendLine(@"                        }");

            var testSnippet = new Snippet
            {
                SelectedText = sb.ToString(),
            };

            //Act
            var actual = testSnippet.CommonIndent();

            //Assert
            actual.Should().Be(24);
        }

          [TestMethod]
        public void GetLeadingWhiteSpaceCountWithLargeWhitespaceTest()
        {
            //Arrange
            String line = @"                        var document = item.Document;";

            //Act
            var actual = Snippet.GetLeadingWhitespaceCount(line);

            //Assert
            actual.Should().Be(24);
        }

          [TestMethod]
          public void DeindentWithLargeWhitespaceTest()
          {
              //Arrange
              String line = @"                        var document = item.Document;";

              //Act
              var actual = Snippet.Deindent(line, 24);

              //Assert
              actual.Should().Be("var document = item.Document;");
          }

          [TestMethod]
          public void DeindentWithNoWhitespaceTest()
          {
              //Arrange
              String line = @"var document = item.Document;";

              //Act
              var actual = Snippet.Deindent(line, 0);

              //Assert
              actual.Should().Be("var document = item.Document;");
          }

          [TestMethod]
          public void DeindentWith1WhitespaceTest()
          {
              //Arrange
              String line = @" var document = item.Document;";

              //Act
              var actual = Snippet.Deindent(line, 1);

              //Assert
              actual.Should().Be("var document = item.Document;");
          }

          [TestMethod]
          public void DeindentWithEmptyLineTest()
          {
              //Arrange
              String line = @"";

              //Act
              var actual = Snippet.Deindent(line, 24);

              //Assert
              actual.Should().Be("");
          }
    }
}
