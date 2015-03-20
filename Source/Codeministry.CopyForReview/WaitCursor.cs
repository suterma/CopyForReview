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
using System.Windows.Input;

namespace Codeministry.CopyForReview {
    /// <summary>
    ///     A wait cursor that disposes itself after usage.
    /// </summary>
    public sealed class WaitCursor : IDisposable {
        private readonly Cursor _previousCursor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaitCursor" /> class.
        /// </summary>
        public WaitCursor() {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}