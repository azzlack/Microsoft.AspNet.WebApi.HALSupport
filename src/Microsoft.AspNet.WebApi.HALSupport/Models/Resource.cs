namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// A <c>HAL</c> resource that wraps a state type.
    /// </summary>
    /// <typeparam name="T">The state type.</typeparam>
    public class Resource<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}" /> class.
        /// </summary>
        internal Resource()
        {
            this.State = default(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        internal Resource(T state)
        {
            this.State = state;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public T State { get; internal set; }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public KeyedCollection<Link> Links { get; internal set; }

        /// <summary>
        /// Gets the embedded resources.
        /// </summary>
        /// <value>The embedded resources.</value>
        public KeyedCollection<EmbeddedResource> EmbeddedResources { get; internal set; }
    }
}