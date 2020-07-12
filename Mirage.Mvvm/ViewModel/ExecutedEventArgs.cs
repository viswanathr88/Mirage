using System;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Enum that represents the execution state of the command
    /// </summary>
    public enum CommandExecutionState 
    {
        /// <summary>
        /// Command execution succeeded
        /// </summary>
        Success,
        /// <summary>
        /// Command executed failed
        /// </summary>
        Failure,
        /// <summary>
        /// Command execution was cancelled
        /// </summary>
        Cancelled
    };
    
    /// <summary>
    /// Event args for the executed event
    /// </summary>
    public class ExecutedEventArgs : EventArgs
    {
        private CommandExecutionState state;
        /// <summary>
        /// Create a new instance of <see cref="ExecutedEventArgs"/>
        /// </summary>
        /// <param name="state">State of the command execution</param>
        public ExecutedEventArgs(CommandExecutionState state)
        {
            this.state = state;
        }
        /// <summary>
        /// Get the state of the command execution
        /// </summary>
        public CommandExecutionState State
        {
            get { return this.state; }
        }
    }
}
