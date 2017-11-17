using System;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Represents a command that executes synchronously. This class
    /// provides automatic progress reporting and error checking to commands
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    public abstract class Command<T> : CommandBase<T>
    {
        /// <summary>
        /// Gets whether the command can be executed with the given parameter
        /// </summary>
        /// <param name="param">Command parameter</param>
        /// <returns>True if the command can execute, false otherwise</returns>
        public abstract override bool CanExecute(T param);
        /// <summary>
        /// Runs the command to execute the command logic
        /// </summary>
        /// <param name="param"></param>
        protected abstract void Run(T param);
        /// <summary>
        /// Execute the command with the given typed parameter
        /// </summary>
        /// <param name="param">Typed command parameter</param>
        public override void Execute(T param)
        {
            if (CanExecute(param))
            {
                CommandExecutionState state = CommandExecutionState.Failure;
                CancelEventArgs args = new CancelEventArgs(false);
                RaiseExecuting(args);
                if (!args.Cancel)
                {
                    try
                    {
                        Run(param);
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
    /// that executes synchronously
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class Command<T1, T2> : Command<T2>, ICommand<T1, T2>
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
        /// Run the command with the given typed parameter
        /// </summary>
        /// <param name="param">Command parameter</param>
        protected abstract override void Run(T2 param);
    }

    /// <summary>
    /// Represents a command class that does not need a parameter to execute
    /// </summary>
    public abstract class Command : Command<VoidType>
    {
        /// <summary>
        /// Gets whether the command can be executed
        /// </summary>
        /// <returns>True if the command can execute, false otherwise</returns>
        protected abstract bool CanExecute();
        /// <summary>
        /// Method implementation to be provided by derived commands
        /// </summary>
        protected abstract void Run();
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
        /// Run the command logic
        /// </summary>
        /// <param name="param">Command parameter</param>
        protected override void Run(VoidType param)
        {
            Run();
        }
    }

}
