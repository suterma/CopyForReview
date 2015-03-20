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
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VsSDK.UnitTestLibrary;

namespace Codeministry.CopyForReview_UnitTests.MenuItemTests {
    internal static class UIShellServiceMock {
        private static GenericMockFactory uiShellFactory;

        #region UiShell Getters

        /// <summary>
        ///     Returns an IVsUiShell that does not implement any methods
        /// </summary>
        /// <returns></returns>
        internal static BaseMock GetUiShellInstance() {
            if (uiShellFactory == null) {
                uiShellFactory = new GenericMockFactory("UiShell", new Type[] {typeof (IVsUIShell), typeof (IVsUIShellOpenDocument)});
            }
            BaseMock uiShell = uiShellFactory.GetInstance();
            return uiShell;
        }

        /// <summary>
        ///     Get an IVsUiShell that implements SetWaitCursor, SaveDocDataToFile, ShowMessageBox
        /// </summary>
        /// <returns>uishell mock</returns>
        internal static BaseMock GetUiShellInstance0() {
            BaseMock uiShell = GetUiShellInstance();
            string name = string.Format("{0}.{1}", typeof (IVsUIShell).FullName, "SetWaitCursor");
            uiShell.AddMethodCallback(name, new EventHandler<CallbackArgs>(SetWaitCursorCallBack));

            name = string.Format("{0}.{1}", typeof (IVsUIShell).FullName, "SaveDocDataToFile");
            uiShell.AddMethodCallback(name, new EventHandler<CallbackArgs>(SaveDocDataToFileCallBack));

            name = string.Format("{0}.{1}", typeof (IVsUIShell).FullName, "ShowMessageBox");
            uiShell.AddMethodCallback(name, new EventHandler<CallbackArgs>(ShowMessageBoxCallBack));
            return uiShell;
        }

        #endregion

        #region Callbacks

        private static void SetWaitCursorCallBack(object caller, CallbackArgs arguments) {
            arguments.ReturnValue = VSConstants.S_OK;
        }

        private static void SaveDocDataToFileCallBack(object caller, CallbackArgs arguments) {
            arguments.ReturnValue = VSConstants.S_OK;
        }

        private static void ShowMessageBoxCallBack(object caller, CallbackArgs arguments) {
            arguments.ReturnValue = VSConstants.S_OK;
            arguments.SetParameter(10, (int) System.Windows.Forms.DialogResult.Yes);
        }

        #endregion
    }
}