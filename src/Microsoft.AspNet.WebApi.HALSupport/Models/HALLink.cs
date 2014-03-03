namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;

    /// <summary>
    /// A <c>HAL</c> link.
    /// </summary>
    public class HalLink
    {
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public Uri Target { get; set; }

        /// <summary>
        /// Gets or sets the relation.
        /// </summary>
        /// <value>The relation.</value>
        public string Relation { get; set; }
    }
}