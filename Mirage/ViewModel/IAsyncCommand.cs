﻿using Mirage.ViewModel.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mirage.ViewModel
{
    public interface IAsyncCommand<T> : ICommand<T>
    {
        Task ExecuteAsync(T param);
    }

    public interface IAsyncCommand<T1, T2> : ICommand<T1, T2>, IAsyncCommand<T2>
    {

    }
}
