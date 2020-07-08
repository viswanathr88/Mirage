using Mirage.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mirage.ViewModel
{
    /// <summary>
    /// Base class for all viewmodels that load data from model
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    public abstract class DataViewModel<TParam> : ViewModelBase, IDataViewModel<TParam>
    {
        private readonly IDictionary<ICommand, Action<ExecutedEventArgs>> commands;

        private bool isLoading;
        private bool isLoaded;
        private object error;

        public event EventHandler<EventArgs> Done;
        protected void RaiseDone() => Done?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Create an instance of DataViewModel
        /// </summary>
        public DataViewModel()
        {
            this.commands = new Dictionary<ICommand, Action<ExecutedEventArgs>>();
        }
        /// <summary>
        /// Gets whether the viewmodel is loading data
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            protected set
            {
                SetProperty(ref this.isLoading, value);
            }
        }
        /// <summary>
        /// Gets whether the viewmodel has loaded data
        /// </summary>
        public bool IsLoaded
        {
            get { return this.isLoaded; }
            protected set
            {
                SetProperty(ref this.isLoaded, value);
            }
        }
        /// <summary>
        /// Gets the error
        /// </summary>
        public object Error
        {
            get
            {
                return this.error;
            }
            protected set
            {
                SetProperty(ref this.error, value);
            }
        }
        /// <summary>
        /// Load data
        /// </summary>
        /// <param name="parameter">input param</param>
        /// <returns></returns>
        public async Task LoadAsync(object parameter)
        {
            try
            {
                TParam param = default(TParam);

                if (typeof(TParam) != typeof(VoidType))
                {
                    // Perform parameter validation
                    if (parameter == null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    // Check parameter type
                    if (!(parameter is TParam))
                    {
                        throw new ArgumentOutOfRangeException(nameof(parameter));
                    }

                    param = (TParam)parameter;
                }

                // Set the parameter on the VM
                Parameter = param;

                Reset();

                await LoadAsync(Parameter);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
        /// <summary>
        /// Load with typesafe parameter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract Task LoadAsync(TParam parameter);
        /// <summary>
        /// Reset method for ViewModel
        /// </summary>
        protected virtual void Reset()
        {
            Error = null;
            IsLoaded = false;
            IsLoading = false;
        }
        /// <summary>
        /// Gets the input parameter
        /// </summary>
        public TParam Parameter
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the parameter
        /// </summary>
        object IDataViewModel.Parameter
        {
            get
            {
                return Parameter;
            }
        }

        /// <summary>
        /// Register a command
        /// </summary>
        /// <param name="command">command to register</param>
        /// <param name="callback">callback when command execution completes</param>
        protected void RegisterCommand(ICommandEx command, Action<ExecutedEventArgs> callback)
        {
            if (command == null || callback == null)
            {
                throw new ArgumentNullException("command or callback");
            }

            if (this.commands.ContainsKey(command))
            {
                return;
            }


            command.Executing += OnCommandExecuting;
            command.Executed += OnCommandExecuted;

            this.commands.Add(command, callback);
        }
        /// <summary>
        /// Deregister a command
        /// </summary>
        /// <param name="command"></param>
        protected void DeregisterCommand(ICommandEx command)
        {
            if (this.commands.ContainsKey(command))
            {
                command.Executing -= OnCommandExecuting;
                command.Executed -= OnCommandExecuted;

                this.commands.Remove(command);

            }
        }
        /// <summary>
        /// Method execute when command begins execution
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">event args</param>
        protected virtual void OnCommandExecuting(object sender, CancelEventArgs e)
        {
            IsLoading = true;
        }
        /// <summary>
        /// Callback when the command has finished execution
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">event args</param>
        private void OnCommandExecuted(object sender, ExecutedEventArgs e)
        {
            IsLoading = false;

            if (sender == null)
            {
                return;
            }

            ICommand command = sender as ICommand;
            if (command == null)
            {
                return;
            }

            if (!commands.ContainsKey(command))
            {
                return;
            }

            try
            {
                this.commands[command].Invoke(e);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
    }

    public abstract class DataViewModel : DataViewModel<VoidType>
    {
        public override async Task LoadAsync(VoidType parameter)
        {
            await LoadAsync();
        }

        public abstract Task LoadAsync();
    }
}
