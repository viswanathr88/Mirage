﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace Mirage.Collections
{
    public interface ILazyObservableCollection<T> : IList<T>,  ISupportIncrementalLoading
    {
        event EventHandler<EventArgs> Loading;

        event EventHandler<EventArgs> Loaded;
    }
}
