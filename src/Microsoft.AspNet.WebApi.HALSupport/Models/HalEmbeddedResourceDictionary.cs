namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// A collection of embedded <c>HAL</c> resources.
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    internal class HalEmbeddedResourceDictionary : Collection<HalEmbeddedResource>
    {
        /// <summary>
        /// Gets or sets the <see cref="HalResource{T}"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resource.</returns>
        public HalEmbeddedResource this[string key]
        {
            get
            {
                return this.FirstOrDefault(x => x.Key == key);
            }
        }
    }
}