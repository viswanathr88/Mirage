using System;

namespace Mirage.ViewModel
{
    /// <summary>
    /// Represents a view model for an item, typically in a list
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class ItemViewModel<T> : ViewModelBase, IItemViewModel<T>
    {
        private T item;
        private bool isSelected;
        private bool isLoading;
        /// <summary>
        /// Create a new instance of <see cref="ItemViewModel{T}"/>
        /// </summary>
        /// <param name="item">Model item that the view model wraps</param>
        public ItemViewModel(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.item = item;
        }
        /// <summary>
        /// Get the model item
        /// </summary>
        public T Item
        {
            get
            {
                return this.item;
            }
        }
        /// <summary>
        /// Gets or sets whether the item is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (SetProperty(ref this.isSelected, value))
                {
                    OnSelectionChanged();
                }
            }
        }
        /// <summary>
        /// Gets whether the item is loading
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            protected set
            {
                SetProperty(ref this.isLoading, value);
            }
        }
        /// <summary>
        /// Function that is called when the item is selected
        /// </summary>
        protected virtual void OnSelectionChanged()
        {

        }
        /// <summary>
        /// Gets the model object
        /// </summary>
        object IItemViewModel.Item
        {
            get
            {
                return Item;
            }
        }
    }
}
