namespace Microsoft.AspNet.WebApi.HALSupport.Formatters
{
    using System;
    using System.IO;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text;

    using Microsoft.AspNet.WebApi.HALSupport.Factories;
    using Microsoft.AspNet.WebApi.HALSupport.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Serializers;

    using Newtonsoft.Json;

    /// <summary>
    /// Media type formatter for <c>application/hal+json</c> requests.
    /// </summary>
    public class HalJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// The resource factory
        /// </summary>
        private readonly ResourceFactory resourceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HalJsonMediaTypeFormatter"/> class.
        /// </summary>
        public HalJsonMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            this.resourceFactory = new ResourceFactory();
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

        /// <summary>
        /// Called during serialization to write an object of the specified type to the specified stream.
        /// </summary>
        /// <param name="type">The type of the object to write.</param>
        /// <param name="value">The object to write.</param>
        /// <param name="writeStream">The stream to write to.</param>
        /// <param name="effectiveEncoding">The encoding to use when writing.</param>
        public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
        {
            var resource = this.resourceFactory.Create(value);

            var json = JsonConvert.SerializeObject(resource, Formatting.None, new HalResourceJsonSerializer());

            var buffer = Encoding.Default.GetBytes(json);

            writeStream.Write(buffer, 0, buffer.Length);

            writeStream.Flush();
        }

        /// <summary>
        /// Called during deserialization to read an object of the specified type from the specified stream.
        /// </summary>
        /// <param name="type">The type of the object to read.</param>
        /// <param name="readStream">The stream from which to read.</param>
        /// <param name="effectiveEncoding">The encoding to use when reading.</param>
        /// <param name="formatterLogger">The logger to log events to.</param>
        /// <returns>The object that has been read.</returns>
        public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
        {
            using (var sr = new StreamReader(readStream)) 
            {
                var resource = JsonConvert.DeserializeObject(sr.ReadToEnd(), type, new HalResourceJsonSerializer());

                return resource;
            }
        }
    }
}
