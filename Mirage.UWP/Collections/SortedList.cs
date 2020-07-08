using System;
using System.Collections.Generic;

namespace Mirage.Collections
{
    /// <summary>
    /// Represents a list that is always sorted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SortedList<T> : ICollection<T>
    {
        private readonly List<T> list = new List<T>();
        private readonly IComparer<T> comparer;
        /// <summary>
        /// Create a sorted list using the default comparer
        /// </summary>
        public SortedList()
        {

        }
        /// <summary>
        /// Create a new sorted list with the given comparer
        /// </summary>
        /// <param name="comparer">Comparer to compare items of the list</param>
        public SortedList(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.comparer = comparer;
        }
        /// <summary>
        /// Add a new item to the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            int index = (comparer == null) ? list.BinarySearch(item) : list.BinarySearch(item, comparer);
            if (index < 0)
                index = -(index + 1);
            list.Insert(index, item);
        }
        /// <summary>
        /// Clear all the items in the list
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
        }
        /// <summary>
        /// Gets whether an item is contained in the list
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return comparer == null ? (this.list.BinarySearch(item) >= 0) : (this.list.BinarySearch(item, comparer) >= 0);
        }
        /// <summary>
        /// Copy all items from the list into an array
        /// </summary>
        /// <param name="array">Destination array to copy to</param>
        /// <param name="arrayIndex">Starting index into the array</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Gets the number of items in the list
        /// </summary>
        public int Count
        {
            get { return this.list.Count; }
        }
        /// <summary>
        /// Gets the index of an item in the list
        /// Returns -1 if the item is not found in the list
        /// </summary>
        /// <param name="item">Item to find the index of</param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            int index = comparer == null ? this.list.BinarySearch(item) : this.list.BinarySearch(item, comparer);
            return (index >= 0) ? index : -1;
        }
        /// <summary>
        /// Array style accessor to get an item using the index
        /// </summary>
        /// <param name="index">Index of the item</param>
        /// <returns>List item at the given index</returns>
        public T this[int index]
        {
            get
            {
                return this.list[index];
            }
        }
        /// <summary>
        /// Gets whether the list is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// Remove an item from the list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            int index = comparer == null ? this.list.BinarySearch(item) : this.list.BinarySearch(item, comparer);
            if (index >= 0)
            {
                this.list.RemoveAt(index);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Gets an emumerator for the list
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
        /// <summary>
        /// Gets an enumerator for the list
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
}
