using System.Threading.Tasks;

namespace Mirage.ViewModel.Commands
{
    /// <summary>
    /// Represents an interface for an asynchronous command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncCommand<T> : ICommand<T>
    {
        Task ExecuteAsync(T param);
    }

    /// <summary>
    /// Represents an interface for an asynchronous command with a parameter and a return value
    /// </summary>
    /// <typeparam name="T1">Type of parameter to the command</typeparam>
    /// <typeparam name="T2">Type of return value</typeparam>
    public interface IAsyncCommand<T1, T2> : ICommand<T1, T2>, IAsyncCommand<T2>
    {

    }
}
