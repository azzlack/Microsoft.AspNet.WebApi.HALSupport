namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// A <c>HAL</c> link.
    /// </summary>
    [DebuggerDisplay("Relation = {Relation}, Target = {Target.AbsoluteUri}")]
    public class Link : IKeyValuePair<string, Uri>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Link" /> class.
        /// </summary>
        /// <param name="relation">The relation.</param>
        /// <param name="target">The target.</param>
        public Link(string relation, Uri target)
        {
            this.Relation = relation;
            this.Target = target;
        }

        /// <summary>
        /// Gets the relation.
        /// </summary>
        /// <value>The relation.</value>
        public string Relation { get; private set; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public Uri Target { get; private set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [JsonIgnore]
        public string Key
        {
            get
            {
                return this.Relation;
            }

            set
            {
                this.Relation = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [JsonIgnore]
        public Uri Value
        {
            get
            {
                return this.Target;
            }

            set
            {
                this.Target = value;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return this.Target != null && !string.IsNullOrEmpty(this.Target.ToString())
                       ? this.Target.ToString()
                       : string.Empty;
        }
    }
}