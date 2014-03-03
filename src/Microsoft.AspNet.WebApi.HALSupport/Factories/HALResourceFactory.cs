namespace Microsoft.AspNet.WebApi.HALSupport.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    using Microsoft.AspNet.WebApi.HALSupport.Models;

    /// <summary>
    /// Factory for creating <c>HAL</c> resources from a source object.
    /// </summary>
    internal class HalResourceFactory
    {
        /// <summary>
        /// Creates a <c>HAL</c> resource using the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A <c>HAL</c> resource.</returns>
        public HalResource<object> Create(object state)
        {
            var h = new HalResource<object>(state);

            this.BuildLinks(h);
            this.BuildEmbeddedResources(h);

            return h;
        }

        /// <summary>
        /// Creates a <c>HAL</c> resource using the specified state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="state">The state.</param>
        /// <returns>A <c>HAL</c> resource.</returns>
        public HalResource<T> Create<T>(T state)
        {
            var h = new HalResource<T>(state);

            this.BuildLinks(h);
            this.BuildEmbeddedResources(h);

            return h;
        }

        /// <summary>
        /// Builds the resource links from the state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="resource">The resource.</param>
        private void BuildLinks<T>(HalResource<T> resource)
        {
            var l = new List<HalLink>();

            // Create 'self' link using current http context
            try
            {
                if (HttpContext.Current != null) 
                {
                    var s = HttpContext.Current.Request.RawUrl;

                    if (!string.IsNullOrEmpty(s) && Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute))
                    {
                        l.Add(new HalLink() { Relation = "self", Target = new Uri(s, UriKind.RelativeOrAbsolute) });
                    }
                }

                throw new UriFormatException("Invalid URI: The current request url is not valid.");
            }
            catch
            {
                l.Add(new HalLink() { Relation = "self", Target = new Uri("/", UriKind.Relative) });
            }

            // TODO: Get links from mapping

            resource.Links = l;
        }

        /// <summary>
        /// Builds the embedded resources from the state.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="resource">The resource.</param>
        private void BuildEmbeddedResources<T>(HalResource<T> resource)
        {
            var externalProperties =
                resource.State.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary");

            if (externalProperties.Any()) 
            {
                resource.EmbeddedResources = new HalEmbeddedResourceDictionary();

                // Move external classes to embedded resource
                foreach (var property in externalProperties)
                {
                    var value = property.GetValue(resource.State);

                    resource.EmbeddedResources.Add(new HalEmbeddedResource(property.Name, value));

                    // Set original property value to null
                    property.SetValue(resource.State, null);
                }
            }
        }
    }
}