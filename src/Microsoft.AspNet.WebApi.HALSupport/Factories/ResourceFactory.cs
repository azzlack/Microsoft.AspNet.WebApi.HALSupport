namespace Microsoft.AspNet.WebApi.HALSupport.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Models;

    /// <summary>
    /// Factory for creating <c>HAL</c> resources from a source object.
    /// </summary>
    public class ResourceFactory
    {
        /// <summary>
        /// Creates a <c>HAL</c> resource using the specified state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="state">The state.</param>
        /// <returns>A <c>HAL</c> resource.</returns>
        public Resource<T> Create<T>(T state)
        {
            var h = new Resource<T>(state);

            this.BuildLinks(h, null);
            this.BuildEmbeddedResources(h);

            return h;
        }

        /// <summary>
        /// Creates a <c>HAL</c> resource using the specified state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="state">The state.</param>
        /// <param name="mappingConfiguration">The mapping configuration.</param>
        /// <returns>A <c>HAL</c> resource.</returns>
        public Resource<T> Create<T>(T state, IClassMapping mappingConfiguration)
        {
            var h = new Resource<T>(state);

            this.BuildLinks(h, mappingConfiguration);
            this.BuildEmbeddedResources(h);

            return h;
        }

        /// <summary>
        /// Builds the resource links from the state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="mappingConfiguration">The mapping configuration.</param>
        /// <exception cref="System.UriFormatException">Invalid URI: The current request url is not valid.</exception>
        private void BuildLinks<T>(Resource<T> resource, IClassMapping mappingConfiguration)
        {
            var l = new KeyedCollection<Link>();

            if (mappingConfiguration != null) 
            {
                foreach (var map in mappingConfiguration.LinkResolvers)
                {
                    Uri v;

                    if (map.Value.Resolve(resource, out v))
                    {
                        l.Add(new Link(map.Key, v));
                    }
                }

                resource.Links = l;
            }
        }

        /// <summary>
        /// Builds the embedded resources from the state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="resource">The resource.</param>
        private void BuildEmbeddedResources<T>(Resource<T> resource)
        {
            var externalProperties =
                resource.State.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary");

            if (externalProperties.Any()) 
            {
                resource.EmbeddedResources = new KeyedCollection<EmbeddedResource>();

                // Move external classes to embedded resource
                foreach (var property in externalProperties)
                {
                    var value = property.GetValue(resource.State);

                    resource.EmbeddedResources.Add(new EmbeddedResource(property.Name, value));

                    // Set original property value to null
                    if (property.SetMethod != null) 
                    {
                        property.SetValue(resource.State, null);
                    }
                }
            }
        }
    }
}