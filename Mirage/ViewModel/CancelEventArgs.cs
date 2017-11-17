using System;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Event arguments to determine if the command needs to be canceled
    /// </summary>
    public class CancelEventArgs : EventArgs
    {
        private bool cancel;
        /// <summary>
        /// Create a new instance of <see cref="CancelEventArgs"/>
        /// </summary>
        /// <param name="cancel">The value of cancel</param>
        public CancelEventArgs(bool cancel)
        {
            this.cancel = cancel;
        }
        /// <summary>
        /// Gets whether the command should be canceled or not
        /// </summary>
        public bool Cancel
        {
            get { return this.cancel; }
            set { this.cancel = value; }
        }
    }
}
