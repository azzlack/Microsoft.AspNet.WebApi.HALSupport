namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of <c>HAL</c> resources.
    /// </summary>
    /// <typeparam name="T">The HAL resource type.</typeparam>
    internal class ResourceCollection<T> : Resource<T>, ICollection<Resource<T>>
    {
        /// <summary>
        /// The resources
        /// </summary>
        private readonly IList<Resource<T>> resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{T}"/> class.
        /// </summary>
        public ResourceCollection()
        {
            this.resources = new List<Resource<T>>();   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection{T}"/> class.
        /// </summary>
        /// <param name="resources">The resources.</param>
        public ResourceCollection(IList<Resource<T>> resources)
        {
            this.resources = resources;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <value>The count.</value>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count
        {
            get
            {
                return this.resources.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get
            {
                return this.resources.IsReadOnly;
            }
        }

        /// <summary>
        /// Gets the <see cref="Resource{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The resource.</returns>
        public Resource<T> this[int index]
        {
            get
            {
                return this.resources[index];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<Resource<T>> GetEnumerator()
        {
            return this.resources.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(Resource<T> item)
        {
            this.resources.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            this.resources.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        public bool Contains(Resource<T> item)
        {
            return this.resources.Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(Resource<T>[] array, int arrayIndex)
        {
            this.resources.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public bool Remove(Resource<T> item)
        {
            return this.resources.Remove(item);
        }
    }
}