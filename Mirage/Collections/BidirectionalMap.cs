using System.Collections.Generic;

namespace Mirage.Collections
{
    /// <summary>
    /// Represents a bidirectional map that can be used to query based on key and value
    /// </summary>
    /// <typeparam name="T1">Type of key</typeparam>
    /// <typeparam name="T2">Type of value</typeparam>
    public class BidirectionalMap<T1,T2>
    {
        private IDictionary<T1, T2> forwardMap;
        private IDictionary<T2, T1> reverseMap;
        /// <summary>
        /// Create a new instance of <see cref="BidirectionalMap{T1, T2}"/>
        /// </summary>
        public BidirectionalMap()
        {
            forwardMap = new Dictionary<T1, T2>();
            reverseMap = new Dictionary<T2, T1>();
        }
        /// <summary>
        /// Add a new key value pair to the map
        /// </summary>
        /// <param name="key">key of type <see cref="T1"/></param>
        /// <param name="value">value of type <see cref="T2"/></param>
        public void Add(T1 key, T2 value)
        {
            forwardMap.Add(key, value);
            reverseMap.Add(value, key);
        }
        /// <summary>
        /// Remove all elements from the map
        /// </summary>
        public void Clear()
        {
            forwardMap.Clear();
            reverseMap.Clear();
        }
        /// <summary>
        /// Gets whether a key of type <see cref="T1"/> is contained in the map
        /// </summary>
        /// <param name="key">Key of type <see cref="T1"/></param>
        /// <returns></returns>
        public bool Contains(object key)
        {
            if (key is T1 && forwardMap.ContainsKey((T1)key))
            {
                return true;
            }
            else if (key is T2 && reverseMap.ContainsKey((T2)key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gets whether the map is of a fixed size
        /// </summary>
        public bool IsFixedSize
        {
            get { return false; }
        }
        /// <summary>
        /// Gets whether the map is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// Remove a key value pair of key type <see cref="T1"/>
        /// </summary>
        /// <param name="key">Key of type <see cref="T1"/></param>
        public void Remove(T1 key)
        {
            forwardMap.Remove(key);
        }
        /// <summary>
        /// Remove a key of type <see cref="T2"/> from the map
        /// </summary>
        /// <param name="key"></param>
        public void Remove(T2 key)
        {
            reverseMap.Remove(key);
        }
        /// <summary>
        /// Get the value of type <see cref="T2"/> given a key of type <see cref="T1"/>
        /// </summary>
        /// <param name="key">Key of type <see cref="T1"/></param>
        /// <exception cref="KeyNotFoundException">Thrown when the key does not exist in the map</exception>
        /// <returns>Value of type <see cref="T2"/></returns>
        public T2 this[T1 key]
        {
            get
            {
                return forwardMap[key];
            }
            set
            {
                forwardMap[key] = value;
            }
        }
        /// <summary>
        /// Get the value of type <see cref="T1"/> given a key of type <see cref="T2"/>
        /// </summary>
        /// <param name="key">Key of type <see cref="T2"/></param>
        /// <exception cref="KeyNotFoundException">Thrown when the key does not exist in the map</exception>
        /// <returns>Value of type <see cref="T1"/></returns>
        public T1 this[T2 key]
        {
            get
            {
                return reverseMap[key];
            }
            set
            {
                reverseMap[key] = value;
            }
        }
        /// <summary>
        /// Get the number of elements in the map
        /// </summary>
        public int Count
        {
            get { return forwardMap.Count; }
        }
        /// <summary>
        /// Get all the keys of the map as a list
        /// </summary>
        /// <returns></returns>
        public List<T1> GetKeys()
        {
            return new List<T1>(this.forwardMap.Keys);
        }
        /// <summary>
        /// Get all values of the map as a list
        /// </summary>
        /// <returns></returns>
        public List<T2> GetValues()
        {
            return new List<T2>(this.reverseMap.Keys);
        }
    }
}
