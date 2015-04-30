# CopyForReview
**CopyForReview** is a _Visual Studio_ Extension that allows to copy code with a formatting. It removes unecessary whitespace, adds file, class, method and line info, suitable for sharing code or writing code reviews. Five output formats are currently supported: Text, Send via Email, Stack Overflow Markdown, Foswiki Markup and a customizable formatter.

![How to use it](https://raw.githubusercontent.com/suterma/CopyForReview/master/Doc/HowToUse/Visual%20Funtioning%20Overview%20Landscape.png)

1. Select code
2. Press CTRL+R, CTRL+C
3. Select the format (output goes to the clipboard)
4. Paste

Install it directly from the [Visual Studio Gallery](https://visualstudiogallery.msdn.microsoft.com/5d17a777-0964-47e3-a6e5-3eed5b31ea93).

## Custom template format
The templates use the [liquid markup](http://liquidmarkup.org/). The [default custom formatter template](https://github.com/suterma/CopyForReview/blob/master/Source/Codeministry.CopyForReview.Formatters/ToCustom.txt) shows how you can use the supported fields:
- **DeindentedSelectedText**
The selected text, with the indentation removed as much as possible.

- **FullFilename**
The the full filename, with path and extension.

- **Filename**
The filename, without path.

- **LineNumberTop**
The line number of the topmost line.

- **LineNumberBottom**
The line number of the bottommost line.

- **FullClassname**
The fully qualified class name where the snippet is in (if any).

- **Methodname**
The method name where the snippet is in (if any).

- **Lines**
The individual code lines of the snippet.

- **DeindentedLines**
The individual code lines of the snippet with the indentation removed as much as possible.

- **SelectedText**
The selected text.

- **FileExtension**
The file extension.

Feedback is highly welcome. Please email to marcel@codeministry.ch. Contribute, review, comment on [GitHub](https://github.com/suterma/CopyForReview).
