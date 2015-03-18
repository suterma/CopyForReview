using System;
using System.Windows.Input;

namespace CopyForReview
{
    /// <summary>
    ///     A wait cursor that disposes itself after usage.
    /// </summary>
    public sealed class WaitCursor : IDisposable
    {
        private readonly Cursor _previousCursor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaitCursor" /> class.
        /// </summary>
        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}