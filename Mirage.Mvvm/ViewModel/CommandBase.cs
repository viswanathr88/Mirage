using System;
using System.ComponentModel;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Represents a base class to be implemented by all commands
    /// This base class makes the command parameters type safe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommandBase<T> : ICommand<T>
    {
        private bool isExecuting;
        private Exception error;
        /// <summary>
        /// Gets whether the command can be executed
        /// </summary>
        /// <param name="param">Command Parameter</param>
        /// <returns>True if the command can execute, false otherwise</returns>
        public abstract bool CanExecute(T param);
        /// <summary>
        /// Execute the command with the parameter
        /// </summary>
        /// <param name="param">Command parameter</param>
        public abstract void Execute(T param);
        /// <summary>
        /// Gets whether the command with the given object as parameter can be executed
        /// This method validates the type of the parameter as well
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>True if the command can be executed with the given parameter; false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            if (typeof(T) == typeof(VoidType) && parameter == null)
            {
                return CanExecute(VoidType.Empty);
            }

            if (parameter == null)
                return false;

            if (!(parameter is T))
                return false;

            if (IsExecuting)
            {
                return false;
            }

            return CanExecute((T)parameter);
        }
        /// <summary>
        /// Execute the command with the given object as parameter
        /// This method will execute only if the command can be executed
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                T param = GetSafeParam(parameter);
                Execute(param);
            }
        }
        /// <summary>
        /// Gets the error if any on the command while executing
        /// </summary>
        public Exception Error
        {
            get
            {
                return this.error;
            }
            protected set
            {
                if (this.error != value)
                {
                    this.error = value;
                    RaisePropertyChanged(nameof(Error));
                }
            }
        }
        /// <summary>
        /// Gets whether the command is executing
        /// </summary>
        public bool IsExecuting
        {
            get
            {
                return this.isExecuting;
            }
            protected set
            {
                if (this.isExecuting == value) return;
                this.isExecuting = value;
                RaisePropertyChanged(nameof(IsExecuting));
            }
        }
        /// <summary>
        /// Event that is raised when the command begins executing
        /// </summary>
        public event EventHandler<CancelEventArgs> Executing;
        protected void RaiseExecuting(CancelEventArgs args)
        {
            IsExecuting = true;
            RaiseCanExecuteChanged();
            Executing?.Invoke(this, args);
        }
        /// <summary>
        /// Event that is raised when the command is executed
        /// </summary>
        public event EventHandler<ExecutedEventArgs> Executed;
        /// <summary>
        /// Method to be called by derived commands to indicate completion
        /// </summary>
        /// <param name="state">State of the command</param>
        protected void RaiseExecuted(CommandExecutionState state)
        {
            IsExecuting = false;
            RaiseCanExecuteChanged();
            Executed?.Invoke(this, new ExecutedEventArgs(state));
        }
        /// <summary>
        /// Event that is raised when canexecute changes
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Protected method that should be called by derived classes to indicate canexecute has changed
        /// </summary>
        private void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        /// <summary>
        /// Event that is fired when a property on the command changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Method to be called by derived classes to raise a property changed event
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        /// <summary>
        /// Get a typesafe parameter
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>Type safe parameter</returns>
        protected T GetSafeParam(object parameter)
        {
            T param;
            if (parameter == null)
                param = default;
            else
                param = (T)parameter;

            return param;
        }
        /// <summary>
        /// Evaluate whether the command can be executed again
        /// </summary>
        public void EvaluateCanExecute()
        {
            RaiseCanExecuteChanged();
        }
    }
}
