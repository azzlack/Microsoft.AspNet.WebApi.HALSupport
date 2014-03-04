namespace Microsoft.AspNet.WebApi.HALSupport.Models
{
    using System;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Resolvers;

    /// <summary>
    /// Link mapping
    /// </summary>
    public class LinkMapping : IPropertyMapping
    {
        /// <summary>
        /// The class mapping
        /// </summary>
        private IClassMapping classMapping;

        /// <summary>
        /// The relation
        /// </summary>
        private readonly string relation;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkMapping"/> class.
        /// </summary>
        /// <param name="classMapping">The class mapping.</param>
        /// <param name="relation">The relation.</param>
        public LinkMapping(IClassMapping classMapping, string relation)
        {
            this.classMapping = classMapping;
            this.relation = relation;
        }

        /// <summary>
        /// Uses the specified value when mapping the property.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Use(object value)
        {
            if (this.classMapping.LinkResolvers.ContainsKey(this.relation))
            {
                this.classMapping.LinkResolvers.Remove(this.relation);    
            }

            this.classMapping.LinkResolvers.Add(this.relation, new StaticValueResolver(value));
        }

        /// <summary>
        /// Uses the speficied value resolver when mapping the property.
        /// </summary>
        /// <typeparam name="T">The value resolver type</typeparam>
        /// <returns>The value resolver mapping configuration.</returns>
        public IValueResolverMapping<T> Use<T>() where T : IValueResolver
        {
            if (this.classMapping.LinkResolvers.ContainsKey(this.relation))
            {
                this.classMapping.LinkResolvers.Remove(this.relation);
            }

            try
            {
                this.classMapping.LinkResolvers.Add(this.relation, Activator.CreateInstance<T>());
            }
            catch (Exception ex)
            {
                this.classMapping.LinkResolvers.Add(this.relation, new ExceptionValueResolver<T>(ex));
            }

            return new ValueResolverMapping<T>(this.classMapping, this.relation);
        }
    }
}