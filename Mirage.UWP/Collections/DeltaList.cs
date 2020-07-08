using System;
using System.Collections.Generic;

namespace Mirage.Collections
{
    /// <summary>
    /// Represents a collection that provides all added or removed items as a separate list
    /// </summary>
    /// <typeparam name="T">The element type of the list</typeparam>
    public class DeltaList<T> : IEnumerable<DeltaListItem<T>> where T : IEquatable<T>, IComparable<T>
    {
        private readonly IDictionary<T, int> originalItems;
        private readonly IDictionary<T, DeltaListItem<T>> deltaItems;
        /// <summary>
        /// Create a new instance of <see cref="DeltaList{T}"/>
        /// </summary>
        /// <param name="originalItems">The initial collection to start computing deltas from</param>
        public DeltaList(IEnumerable<T> originalItems)
        {
            if (originalItems == null)
                throw new ArgumentNullException("originalItems", "original items cannot be null");

            this.originalItems = new Dictionary<T, int>();
            this.deltaItems = new Dictionary<T, DeltaListItem<T>>();

            int k = 0;
            foreach (T item in originalItems)
            {
                this.originalItems[item] = k++;
            }
        }
        /// <summary>
        /// Get all the original items
        /// </summary>
        public IDictionary<T, int> OriginalItems
        {
            get { return this.originalItems; }
        }
        /// <summary>
        /// Add a new item to the list
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            if (!this.originalItems.ContainsKey(item))
            {
                this.deltaItems[item] = new DeltaListItem<T>(DeltaListOperation.Added, item);
            }
            else
            {
                // If the item is found in the original list, this item
                // was probably removed and being re-added now. 
                if (this.deltaItems.ContainsKey(item))
                {
                    this.deltaItems.Remove(item);
                }
            }
        }
        /// <summary>
        /// Remove the item from the list
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            if (this.originalItems.ContainsKey(item))
            {
                this.deltaItems[item] = new DeltaListItem<T>(DeltaListOperation.Removed, item);
            }
            else
            {
                // If the item was not found on the original items list, remove it
                // from delta list as well
                if (this.deltaItems.ContainsKey(item))
                {
                    this.deltaItems.Remove(item);
                }
            }
        }
        /// <summary>
        /// Gets the count of the delta list
        /// </summary>
        public int Count
        {
            get { return this.deltaItems.Count; }
        }
        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<DeltaListItem<T>> GetEnumerator()
        {
            return this.deltaItems.Values.GetEnumerator();
        }
        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.deltaItems.Values.GetEnumerator();
        }
    }
}
