﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Mirage.Collections
{
    public sealed class LazyObservableCollection<TViewModel, TModel> : ObservableCollection<TViewModel>, ILazyObservableCollection<TViewModel>, ISupportIncrementalLoading
    {
        private readonly Func<IEnumerable<TModel>> collectionSourceFn;
        private readonly Func<TModel, TViewModel> adapterFn;
        private bool hasMoreItems = true;

        public LazyObservableCollection(Func<IEnumerable<TModel>> sourceFn, Func<TModel, TViewModel> adapterFn)
        {
            if (sourceFn == null)
            {
                throw new ArgumentNullException(nameof(collectionSourceFn));
            }

            this.collectionSourceFn = sourceFn;
            this.adapterFn = adapterFn;
        }


        public bool HasMoreItems
        {
            get
            {
                return this.hasMoreItems;
            }
            private set
            {
                this.hasMoreItems = value;
            }
        }

        public event EventHandler<EventArgs> Loaded;
        private void RaiseLoaded() => Loaded?.Invoke(this, EventArgs.Empty);

        public event EventHandler<EventArgs> Loading;
        private void RaiseLoading() => Loading?.Invoke(this, EventArgs.Empty);

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            RaiseLoading();

            IEnumerable<TModel> collectionSource = collectionSourceFn.Invoke();
            if (Count <= 0 && collectionSource != null)
            {
                foreach (TModel model in collectionSource)
                {
                    var item = this.adapterFn.Invoke(model);
                    if (item != null)
                    {
                        Add(item);
                    }
                }
            }

            HasMoreItems = false;

            RaiseLoaded();

            return Task.FromResult<LoadMoreItemsResult>(new LoadMoreItemsResult() { Count = Convert.ToUInt32(Count) }).AsAsyncOperation<LoadMoreItemsResult>();
        }
    }
}
