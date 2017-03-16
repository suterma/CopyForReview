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

using System;
using System.IO;
using Codeministry.CopyForReview.Data;
using EnvDTE;
using EnvDTE80;

namespace Codeministry.CopyForReview
{
    /// <summary>
    ///     Examines a code model by looking for the required information to compile a snippet.
    /// </summary>
    public class CodeModelExaminer
    {
        /// <summary>
        ///     The application object to use to examine the code.
        /// </summary>
        private readonly DTE2 _applicationObject;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeModelExaminer" /> class.
        /// </summary>
        /// <param name="applicationObject">The application object.</param>
        public CodeModelExaminer(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        ///     Sets the line range of the code location according to the current selection.
        /// </summary>
        /// <param name="codeLocationInfo">The code location information.</param>
        private void SetSelectionLineRange(ISnippet codeLocationInfo)
        {
            TextSelection selection = GetTextSelection();
            if (selection != null) {
                codeLocationInfo.LineNumberTop = selection.TopLine;
                codeLocationInfo.LineNumberBottom = selection.BottomLine;
            }
        }


        /// <summary>
        ///     Gets the text of the current selection in the application
        /// </summary>
        /// <param name="isSelectFullLines">if set to <c>true</c> full lines are selected before copying.</param>
        /// <returns></returns>
        private String CopySelection(bool isSelectFullLines)
        {
            TextSelection selection = GetTextSelection();

            if (selection == null) {
                //TODO later fail earlier with a nice dialog.
                return "The currently active document (" + GetFilename() + " in window " + GetWindowCaption() + ") does not support text selections";
            }
            if (isSelectFullLines) {
                var topLine = selection.TopLine;
                var bottomLine = selection.BottomLine;

                //Adapt the selection to include only full lines
                selection.GotoLine(topLine, true);
                selection.MoveTo(bottomLine, 0, true);
                selection.EndOfLine(true);
            }
            return selection.Text;
        }

        /// <summary>
        ///     Gets the text selection from the currently active document.
        /// </summary>
        /// <remarks>
        ///     This may be null, in case the currently active document is not a text document.
        ///     <para>
        ///         An example for a non-text document is the annotation view from subversion.
        ///     </para>
        /// </remarks>
        /// <returns></returns>
        private TextSelection GetTextSelection()
        {
            return (TextSelection) _applicationObject.ActiveDocument.Selection;
        }

        /// <summary>
        ///     Gets the code context where the selected code is located in.
        /// </summary>
        /// <param name="codeLocationInfo">The code location information.</param>
        private void GetCodeContext(ISnippet codeLocationInfo)
        {
            ProjectItem item = _applicationObject.ActiveDocument.ProjectItem;
            ExamineItem(item, codeLocationInfo);
        }


        /// <summary>
        ///     Examines the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="codeLocationInfo">The code location information.</param>
        private static void ExamineItem(ProjectItem item, ISnippet codeLocationInfo)
        {
            FileCodeModel2 model = (FileCodeModel2) item.FileCodeModel;
            if (model == null) {
                //no model, no elements, nothing to examine. 
                //Most probably, the item is from a non-object oriented language
                return;
            }
            foreach (CodeElement codeElement in model.CodeElements) {
                ExamineCodeElement(codeElement, codeLocationInfo);
            }
        }

        /// <summary>
        ///     Recursively examines the code elements.
        /// </summary>
        /// <param name="codeElement">The code element.</param>
        /// <param name="codeLocationInfo">The code location information.</param>
        private static void ExamineCodeElement(CodeElement codeElement, ISnippet codeLocationInfo)
        {
            try {
                if (codeElement.Kind == vsCMElement.vsCMElementClass) {
                    //Encloses selection?
                    if (Encloses(codeElement, codeLocationInfo)) {
                        codeLocationInfo.FullClassname = codeElement.FullName;
                    }
                }
                if (codeElement.Kind == vsCMElement.vsCMElementFunction) {
                    //Encloses selection?
                    if (Encloses(codeElement, codeLocationInfo)) {
                        codeLocationInfo.Methodname = codeElement.Name;
                    }
                }

                foreach (CodeElement childElement in codeElement.Children) {
                    ExamineCodeElement(childElement, codeLocationInfo);
                }
            }
            catch {
                //just swallow, leave codeLocationInfo as is
            }
        }

        /// <summary>
        ///     Tests, whether the code element encloses the selection.
        /// </summary>
        /// <param name="codeElement">The code element.</param>
        /// <param name="codeLocationInfo">The code location information with the selection.</param>
        /// <returns></returns>
        private static bool Encloses(CodeElement codeElement, ISnippet codeLocationInfo)
        {
            if (
                (codeElement.StartPoint.Line <= codeLocationInfo.LineNumberTop) &&
                (codeElement.EndPoint.Line >= codeLocationInfo.LineNumberBottom)
            ) {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Gets the filename of the currently active document.
        /// </summary>
        /// <returns>The filename</returns>
        private string GetFilename()
        {
            return _applicationObject.ActiveDocument.Name;
        }

        /// <summary>
        ///     Gets the caption of the currently active window.
        /// </summary>
        /// <returns>The caption</returns>
        private string GetWindowCaption()
        {
            return _applicationObject.ActiveWindow.Caption;
        }

        /// <summary>
        ///     Gets the full filename of the currently active document.
        /// </summary>
        /// <returns>The filename</returns>
        private string GetFullFilename()
        {
            return _applicationObject.ActiveDocument.FullName;
        }

        /// <summary>
        ///     Gets the snippet using the DTE
        /// </summary>
        /// <param name="isSelectionFullLines">if set to <c>true</c> the selection will be expanded to full lines.</param>
        /// <returns></returns>
        public ISnippet GetSnippet(bool isSelectionFullLines)
        {
            var snippet = new Snippet
            {
                FullFilename = GetFullFilename(),
                Filename = GetFilename(),
                FileExtension = GetFileExtension(),
                SelectedText = CopySelection(isSelectionFullLines)
            };
            SetSelectionLineRange(snippet);
            GetCodeContext(snippet);
            return snippet;
        }

        /// <summary>
        ///     Gets the file extension.
        /// </summary>
        /// <returns></returns>
        private string GetFileExtension()
        {
            return Path.GetExtension(_applicationObject.ActiveDocument.FullName);
        }
    }
}