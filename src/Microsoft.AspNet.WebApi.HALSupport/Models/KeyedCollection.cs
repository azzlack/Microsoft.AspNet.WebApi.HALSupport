namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    /// <summary>
    /// A collection of embedded <c>HAL</c> resources.
    /// </summary>
    /// <typeparam name="T">The collection item type</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    public class KeyedCollection<T> : Collection<T> where T : IKey<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedCollection{T}" /> class.
        /// </summary>
        internal KeyedCollection()
        {
        }

        /// <summary>
        /// Gets or sets the item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resource.</returns>
        public T this[string key]
        {
            get
            {
                return this.FirstOrDefault(x => x.Key == key);
            }
        }
    }
}