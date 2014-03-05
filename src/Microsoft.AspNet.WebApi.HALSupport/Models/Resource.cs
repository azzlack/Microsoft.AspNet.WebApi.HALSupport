namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// A <c>HAL</c> resource that wraps a state type.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        internal Resource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        internal Resource(object state)
        {
            this.State = state;
            this.Type = state.GetType();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonIgnore]
        public Type Type { get; internal set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public object State { get; internal set; }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public KeyedCollection<Link> Links { get; internal set; }

        /// <summary>
        /// Gets the embedded resources.
        /// </summary>
        /// <value>The embedded resources.</value>
        [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
        public KeyedCollection<EmbeddedResource> EmbeddedResources { get; internal set; }

        /// <summary>
        /// Converts this instance to a typed resource.
        /// </summary>
        /// <typeparam name="T">The state type</typeparam>
        /// <returns>A typed resource.</returns>
        public Resource<T> To<T>()
        {
            if (this.Type.IsInstanceOfType(typeof(T))) 
            {
                return new Resource<T>((T)this.State);
            }

            throw new ArgumentException(string.Format("'{0}' does not match the stored type: '{1}'", typeof(T), this.Type));
        }
    }

    /// <summary>
    /// A typed <c>HAL</c> resource that wraps a state type.
    /// </summary>
    /// <typeparam name="T">The state type</typeparam>
    public class Resource<T> : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}"/> class.
        /// </summary>
        internal Resource()
        {
            this.Type = typeof(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        internal Resource(T state) 
            : base(state)
        {
            base.State = state;
            this.Type = typeof(T);
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public new T State
        {
            get
            {
                return (T)base.State;
            }

            set
            {
                base.State = value;
            }
        }
    }
}