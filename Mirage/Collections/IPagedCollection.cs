﻿
using System;
using System.Threading.Tasks;
namespace Mirage.Collections
{
    /// <summary>
    /// Interface for a paged collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedCollection<T> : IAsyncEnumerable<T>
    {
        int Count
        {
            get;
        }

        int Size
        {
            get;
        }

        int CurrentPage
        {
            get;
        }

        int PageSize
        {
            get;
        }

        T this[int index]
        {
            get;
        }

        Task<bool> LoadPage();

        void Clear();
    }
}
