namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// A <c>HAL</c> resource that wraps a state type.
    /// </summary>
    /// <typeparam name="T">The state type.</typeparam>
    internal class HalResource<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HalResource{T}" /> class.
        /// </summary>
        internal HalResource()
        {
            this.State = default(T);
            this.Links = new List<HalLink>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HalResource{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        internal HalResource(T state)
        {
            this.State = this.Clone(state);
            this.Links = new List<HalLink>();
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public T State { get; internal set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>The links.</value>
        public IEnumerable<HalLink> Links { get; internal set; }

        /// <summary>
        /// Gets or sets the embedded resources.
        /// </summary>
        /// <value>The embedded resources.</value>
        public HalEmbeddedResourceDictionary EmbeddedResources { get; internal set; }

        /// <summary>
        /// Clones the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The cloned source.</returns>
        private T Clone(T source)
        {
            var t = JsonConvert.SerializeObject(source);

            return JsonConvert.DeserializeObject<T>(t);
        }
    }
}