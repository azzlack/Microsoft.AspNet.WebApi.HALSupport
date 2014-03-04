namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;

    public class ValueResolverMapping<T> : IValueResolverMapping<T> where T : IValueResolver
    {
        /// <summary>
        /// The class mapping
        /// </summary>
        private readonly IClassMapping classMapping;

        /// <summary>
        /// The key
        /// </summary>
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResolverMapping{T}" /> class.
        /// </summary>
        /// <param name="classMapping">The class mapping.</param>
        /// <param name="key">The key.</param>
        public ValueResolverMapping(IClassMapping classMapping, string key)
        {
            this.classMapping = classMapping;
            this.key = key;
        }

        /// <summary>
        /// Constructeds the by.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        public void ConstructedBy(Func<T> constructor)
        {
            this.classMapping.LinkResolvers[this.key] = constructor();
        }
    }
}