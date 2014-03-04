namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    /// <summary>
    /// Mapping configuration options.
    /// </summary>
    /// <typeparam name="T">The source type.</typeparam>
    public class ClassMapping<T> : IClassMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassMapping{T}" /> class.
        /// </summary>
        public ClassMapping()
        {
            this.Type = typeof(T);
            this.LinkResolvers = new Dictionary<string, IValueResolver>();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the link resolvers.
        /// </summary>
        /// <value>The link resolvers.</value>
        public IDictionary<string, IValueResolver> LinkResolvers { get; private set; }

        /// <summary>
        /// Customize configuration for a link.
        /// </summary>
        /// <param name="relation">The link relation name.</param>
        /// <returns>The current mapping.</returns>
        public IPropertyMapping ForLink(string relation)
        {
            return new LinkMapping(this, relation);
        }
    }
}