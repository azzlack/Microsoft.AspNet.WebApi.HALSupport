namespace Microsoft.AspNet.WebApi.HALSupport.Serializers
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNet.WebApi.HALSupport.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// <c>JSON</c> serializer for <c>HAL</c> resources.
    /// </summary>
    public class HalResourceJsonSerializer : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            try
            {
                var resource = value.GetType().GetProperty("State", BindingFlags.Public | BindingFlags.Instance).GetValue(value);
                var links = value.GetType().GetProperty("Links", BindingFlags.Public | BindingFlags.Instance).GetValue(value) as KeyedCollection<Link>;
                var embedded = value.GetType().GetProperty("EmbeddedResources", BindingFlags.Public | BindingFlags.Instance).GetValue(value) as KeyedCollection<EmbeddedResource>;

                // Serialize inner resource and remove outer object notation
                var r = JsonConvert.SerializeObject(resource, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                r = r.Substring(r.IndexOf('{') + 1);
                r = r.Substring(0, r.LastIndexOf('}'));

                writer.WriteRaw(r);

                // Write out comma separator if links or embedded resources exist 
                if ((links != null && links.Any()) || (embedded != null && embedded.Any())) 
                {
                    writer.WriteRaw(",");
                }

                // Write out links
                if (links != null && links.Any()) 
                {
                    writer.WritePropertyName("_links");
                    writer.WriteStartObject();

                    foreach (var l in links)
                    {
                        writer.WritePropertyName(l.Relation);

                        writer.WriteStartObject();

                        writer.WritePropertyName("href");
                        writer.WriteValue(l.Target.ToString());

                        writer.WriteEndObject();
                    }

                    writer.WriteEndObject();
                }

                // Write out embedded resources
                if (embedded != null && embedded.Any())
                {
                    writer.WritePropertyName("_embedded");
                    writer.WriteStartObject();

                    foreach (var e in embedded)
                    {
                        writer.WritePropertyName(e.Key);
                        serializer.Serialize(writer, e.Value);
                    }

                    writer.WriteEndObject();
                }
            }
            catch (Exception ex)
            {
                writer.WritePropertyName("error");
                
                writer.WriteStartObject();
                writer.WritePropertyName("message");
                writer.WriteValue(ex.Message);
                writer.WritePropertyName("stacktrace");
                writer.WriteValue(ex.StackTrace);
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            try
            {
                return objectType.GetGenericTypeDefinition() == typeof(Resource<>);
            }
            catch
            {
                return false;
            }
        }
    }
}