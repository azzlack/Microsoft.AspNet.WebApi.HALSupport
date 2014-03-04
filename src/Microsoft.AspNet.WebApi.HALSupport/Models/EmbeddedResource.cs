namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System.Diagnostics;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// A <c>HAL</c> embedded resource.
    /// </summary>
    [DebuggerDisplay("Key = {Key}, Type = {Value.State.GetType()}")]
    public class EmbeddedResource : IKeyValuePair<string, Resource<object>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResource" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        internal EmbeddedResource(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResource"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal EmbeddedResource(string key, object value) 
            : this(key)
        {
            this.Value = new Resource<object>(value);
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Resource<object> Value { get; set; }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <returns>The resource.</returns>
        public Resource<T> ToResource<T>()
        {
            var r = new Resource<T>
                        {
                            EmbeddedResources = this.Value.EmbeddedResources,
                            Links = this.Value.Links
                        };

            var v = JsonConvert.SerializeObject(this.Value.State);
            r.State = JsonConvert.DeserializeObject<T>(v);

            return r;
        }
    }
}