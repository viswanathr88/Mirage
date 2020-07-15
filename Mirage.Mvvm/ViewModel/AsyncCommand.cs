using System;
using System.Threading.Tasks;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Represents a command that executes asynchronously. This class
    /// provides automatic progress reporting and error checking to commands
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    public abstract class AsyncCommand<T> : CommandBase<T>, IAsyncCommand<T>
    {
        /// <summary>
        /// Gets or sets whether the command will execute in a background thread
        /// </summary>
        public bool ExecuteInBackgroundThread { get; set; } = false;
        /// <summary>
        /// Gets whether the command can be executed with the given parameter
        /// </summary>
        /// <param name="param">Command parameter</param>
        /// <returns>True if the command can execute, false otherwise</returns>
        public abstract override bool CanExecute(T param);
        /// <summary>
        /// Runs the command to execute the command logic asynchronously
        /// </summary>
        /// <param name="param">Command parameter</param>
        protected abstract Task RunAsync(T param);
        /// <summary>
        /// Execute the command with the given typed parameter
        /// </summary>
        /// <param name="param">Typed command parameter</param>
        public async override void Execute(T param)
        {
            await ExecuteAsync(param);
        }
        /// <summary>
        /// Execute the command with the given typed parameter asynchronously
        /// </summary>
        /// <param name="param">Typed command parameter</param>
        public async Task ExecuteAsync(T param)
        {
            Error = null;
            if (CanExecute(param))
            {
                CommandExecutionState state = CommandExecutionState.Failure;
                CancelEventArgs args = new CancelEventArgs(false);
                RaiseExecuting(args);
                if (!args.Cancel)
                {
                    try
                    {
                        if (ExecuteInBackgroundThread)
                        {
                            await Task.Run(async () => await RunAsync(param));
                        }
                        else
                        {
                            await RunAsync(param);
                        }
                        state = CommandExecutionState.Success;
                    }
                    catch (Exception ex)
                    {
                        Error = ex;
                        state = CommandExecutionState.Failure;
                    }
                }
                else
                {
                    state = CommandExecutionState.Cancelled;
                }
                RaiseExecuted(state);
            }
        }
    }

    /// <summary>
    /// Represents a command that contains a typed parameter and a typed result
    /// that executes asynchronously
    /// </summary>
    /// <typeparam name="T1">Result Type</typeparam>
    /// <typeparam name="T2">Parameter Type</typeparam>
    public abstract class AsyncCommand<T1, T2> : AsyncCommand<T2>, IAsyncCommand<T1, T2>
    {
        /// <summary>
        /// Get the result of the command execution
        /// </summary>
        public T1 Result
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets whether the command can be executed with the given parameter
        /// </summary>
        /// <param name="param">Command parameter</param>
        /// <returns>True if the command can execute with the given parameter, false otherwise</returns>
        public abstract override bool CanExecute(T2 param);
        /// <summary>
        /// Run the command with the given typed parameter asynchronously
        /// </summary>
        /// <param name="param">Command parameter</param>
        protected abstract override Task RunAsync(T2 param);
    }

    /// <summary>
    /// Represents a async command class that does not need a parameter to execute
    /// </summary>
    public abstract class AsyncCommand : AsyncCommand<VoidType>
    {
        /// <summary>
        /// Gets whether the command can be executed
        /// </summary>
        /// <returns>True if the command can execute, false otherwise</returns>
        protected abstract bool CanExecute();
        /// <summary>
        /// Method implementation to be provided by derived commands
        /// </summary>
        protected abstract Task RunAsync();
        /// <summary>
        /// Gets whether the command can be executed
        /// </summary>
        /// <param name="param">Command Parameter</param>
        /// <returns>True if the command can execute, false otherwise</returns>
        public override bool CanExecute(VoidType param)
        {
            return CanExecute();
        }
        /// <summary>
        /// Run the command logic asynchronously
        /// </summary>
        /// <param name="param">Command parameter</param>
        protected async override Task RunAsync(VoidType param)
        {
            await RunAsync();
        }
    }
}
