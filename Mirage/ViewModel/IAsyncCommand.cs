using System.Threading.Tasks;

namespace Mirage.ViewModel.Commands
{
    public interface IAsyncCommand<T> : ICommand<T>
    {
        Task ExecuteAsync(T param);
    }

    public interface IAsyncCommand<T1, T2> : ICommand<T1, T2>, IAsyncCommand<T2>
    {

    }
}
