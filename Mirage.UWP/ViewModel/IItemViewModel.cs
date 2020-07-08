using System.ComponentModel;

namespace Mirage.ViewModel
{
    /// <summary>
    /// Represents an interface for an item's ViewModel
    /// </summary>
    public interface IItemViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets whether the item is selected
        /// </summary>
        bool IsSelected
        {
            get;
            set;
        }
        /// <summary>
        /// Get the model item
        /// </summary>
        object Item
        {
            get;
        }
    }

    /// <summary>
    /// Represents an interface for an item's ViewModel that wraps a model item
    /// </summary>
    public interface IItemViewModel<TModel> : IItemViewModel
    {
        /// <summary>
        /// Get the model item
        /// </summary>
        new TModel Item
        {
            get;
        }
    }
}
