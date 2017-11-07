using System;

namespace Mirage.Collections
{
    /// <summary>
    /// Represents a delta list operation
    /// </summary>
    public enum DeltaListOperation
    {
        /// <summary>
        /// Added to the delta list
        /// </summary>
        Added,
        /// <summary>
        /// Removed from the delta list
        /// </summary>
        Removed
    };
    /// <summary>
    /// Represents a delta list item
    /// </summary>
    /// <typeparam name="T">The element of type</typeparam>
    public class DeltaListItem<T> where T : IEquatable<T>, IComparable<T>
    {
        private readonly DeltaListOperation operation;
        private readonly T item;
        /// <summary>
        /// Create a new instance of <see cref="DeltaListItem{T}"/>
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="item"></param>
        public DeltaListItem(DeltaListOperation operation, T item)
        {
            this.operation = operation;
            this.item = item;
        }
        /// <summary>
        /// Get the operation
        /// </summary>
        public DeltaListOperation Operation
        {
            get { return this.operation; }
        }
        /// <summary>
        /// Get the item
        /// </summary>
        public T Item
        {
            get { return this.item; }
        }
    }
}
