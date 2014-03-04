namespace Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;

    /// <summary>
    /// Mapping configuration options.
    /// </summary>
    public interface IClassMapping
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        Type Type { get; }

        /// <summary>
        /// Gets the link resolvers.
        /// </summary>
        /// <value>The link resolvers.</value>
        IDictionary<string, IValueResolver> LinkResolvers { get; } 
            
        /// <summary>
        /// Customize configuration for a link.
        /// </summary>
        /// <param name="relation">The link relation name.</param>
        /// <returns>The current mapping.</returns>
        IPropertyMapping ForLink(string relation);
    }
}