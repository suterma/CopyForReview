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
using Codeministry.CopyForReview.Data;
using EnvDTE;
using EnvDTE80;

namespace Codeministry.CopyForReview
{
    /// <summary>
    ///     Examines a code model by looking for the class and method that encloses a code location.
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
        public void SetSelectionLineRange(ISnippet codeLocationInfo)
        {
            //get the text document
            TextDocument txt = (TextDocument) _applicationObject.ActiveDocument.Object("TextDocument");

            codeLocationInfo.LineNumberTop = txt.Selection.TopLine;
            codeLocationInfo.LineNumberBottom = txt.Selection.BottomLine;
        }


        /// <summary>
        ///     Gets the text of the current selection in the application
        /// </summary>
        /// <param name="isSelectFullLines">if set to <c>true</c> full lines are selected before copying.</param>
        /// <returns></returns>
        public String CopySelection(bool isSelectFullLines)
        {
            //get the text document
            TextDocument txt = (TextDocument) _applicationObject.ActiveDocument.Object("TextDocument");

            if (isSelectFullLines) {
                var topLine = txt.Selection.TopLine;
                var bottomLine = txt.Selection.BottomLine;

                //Adapt the selection to include only full lines
                txt.Selection.GotoLine(topLine, true);
                txt.Selection.MoveTo(bottomLine, 0, true);
                txt.Selection.EndOfLine(true);
            }
            return txt.Selection.Text;
        }

        /// <summary>
        ///     Gets the code context where the selected code is located in.
        /// </summary>
        /// <param name="codeLocationInfo">The code location information.</param>
        public void GetCodeContext(ISnippet codeLocationInfo)
        {
            // get the solution
            Solution solution = _applicationObject.Solution;
            Console.WriteLine(solution.FullName);
            ExamineProjects(solution.Projects, _applicationObject.ActiveDocument, codeLocationInfo);
        }

        private static void ExamineProjects(Projects projects, Document doc, ISnippet codeLocationInfo)
        {
            // get all the projects
            foreach (Project project in projects) {
                Console.WriteLine("\t{0}", project.FullName);

                ExamineProjectItems(project.ProjectItems, doc, codeLocationInfo);
            }
        }

        private static void ExamineProjectItems(ProjectItems projectItems, Document doc,
            ISnippet codeLocationInfo)
        {
            // get all the items in each project
            foreach (ProjectItem item in projectItems) {
                Console.WriteLine("\t\t{0}", item.Name);

                if (item.SubProject != null) {
                    ExamineProjectItems(item.SubProject.ProjectItems, doc, codeLocationInfo);
                }

                if (item.ProjectItems != null) {
                    ExamineProjectItems(item.ProjectItems, doc, codeLocationInfo);
                }

                // find this file and examine it
                if (item.Name.Contains(".cs")) {
                    if (item.Document != null) {
                        var document = item.Document;
                        if (document.Name == doc.Name) {
                            ExamineItem(item, codeLocationInfo);
                        }
                    }
                }
            }
        }


        // examine an item
        private static void ExamineItem(ProjectItem item, ISnippet codeLocationInfo)
        {
            FileCodeModel2 model = (FileCodeModel2) item.FileCodeModel;
            foreach (CodeElement codeElement in model.CodeElements) {
                ExamineCodeElement(codeElement, codeLocationInfo);
            }
        }

        // recursively examine code elements
        private static void ExamineCodeElement(CodeElement codeElement, ISnippet codeLocationInfo)
        {
            try {
                Console.WriteLine("{0} {1}",
                    codeElement.Name, codeElement.Kind.ToString());

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
                Console.WriteLine("codeElement without name: {0}", codeElement.Kind.ToString());
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
        internal string GetFilename()
        {
            return _applicationObject.ActiveDocument.Name;
        }

        /// <summary>
        ///     Gets the full filename of the currently active document.
        /// </summary>
        /// <returns>The filename</returns>
        internal string GetFullFilename()
        {
            return _applicationObject.ActiveDocument.FullName;
        }
    }
}