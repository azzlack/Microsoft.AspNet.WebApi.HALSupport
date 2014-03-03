namespace Microsoft.AspNet.WebApi.HALSupport.Formatters
{
    using System;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    /// <summary>
    /// Media type formatter for <c>application/hal+json</c> requests.
    /// </summary>
    public class HalJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HalJsonMediaTypeFormatter"/> class.
        /// </summary>
        public HalJsonMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// Determines whether this <see cref="T:System.Net.Http.Formatting.JsonMediaTypeFormatter" /> can read objects of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type of object that will be read.</param>
        /// <returns>true if objects of this <paramref name="type" /> can be read, otherwise false.</returns>
        public override bool CanReadType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Determines whether this <see cref="T:System.Net.Http.Formatting.JsonMediaTypeFormatter" /> can write objects of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type of object that will be written.</param>
        /// <returns>true if objects of this <paramref name="type" /> can be written, otherwise false.</returns>
        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}
