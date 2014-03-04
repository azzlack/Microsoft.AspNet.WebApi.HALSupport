namespace Microsoft.AspNet.WebApi.HALSupport
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.WebApi.HALSupport.Factories;
    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Models;

    /// <summary>
    /// Configuration class for configuring HAL links.
    /// </summary>
    public class HalMapper
    {
        /// <summary>
        /// The singleton
        /// </summary>
        private static readonly HalMapper Singleton = new HalMapper();

        /// <summary>
        /// The mappings
        /// </summary>
        private readonly IDictionary<Type, IClassMapping> mappings;

        /// <summary>
        /// The resource factory
        /// </summary>
        private readonly ResourceFactory resourceFactory;

        /// <summary>
        /// Prevents a default instance of the <see cref="HalMapper"/> class from being created.
        /// </summary>
        private HalMapper()
        {
            this.mappings = new Dictionary<Type, IClassMapping>();
            this.resourceFactory = new ResourceFactory();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static HalMapper Instance
        {
            get
            {
                return Singleton;
            }
        }

        /// <summary>
        /// Creates a mapping configuration for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to map.</typeparam>
        /// <returns>The mapping configuration.</returns>
        public ClassMapping<T> CreateMap<T>()
        {
            var m = new ClassMapping<T>();

            // Get exisiting mapping if one already exist
            if (this.mappings.ContainsKey(typeof(T)))
            {
                return this.mappings[typeof(T)] as ClassMapping<T>;
            }

            this.mappings.Add(typeof(T), m);

            return m;
        }

        /// <summary>
        /// Maps the specified source and returns the <c>HAL</c> resource.
        /// </summary>
        /// <typeparam name="T">The type to map.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The <c>HAL</c> resource.</returns>
        public Resource<T> Map<T>(T source)
        {
            if (this.mappings.ContainsKey(typeof(T)))
            {
                var m = this.mappings[typeof(T)];

                return this.resourceFactory.Create(source, m);
            }

            throw new ArgumentException(string.Format("No mapping found for type '{0}'", typeof(T)), "source");
        } 
    }
}