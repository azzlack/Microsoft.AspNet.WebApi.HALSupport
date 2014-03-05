namespace Microsoft.AspNet.WebApi.HALSupport.Serializers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNet.WebApi.HALSupport.Interfaces.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converts a keyed collections to dictionary
    /// </summary>
    public class KeyedCollectionConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var collection = value as IEnumerable<IKey<string>>;

            if (collection != null) 
            {
                writer.WriteStartObject();

                foreach (var item in collection)
                {
                    var prop = item.GetType().GetProperty("Value");

                    if (prop != null)
                    {
                        var v = prop.GetValue(item);

                        writer.WritePropertyName(item.Key);

                        serializer.Serialize(writer, v);
                    }
                }

                writer.WriteEndObject();
            }
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
            var collection = Activator.CreateInstance(objectType, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);
            var prop = objectType.GetMethod("Add");

            var obj = JObject.Load(reader);

            foreach (var o in obj)
            {
                prop.Invoke(collection, new[] { serializer.Deserialize(o.Value.CreateReader(), objectType.GenericTypeArguments.First()) });
            }

            return collection;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType
                   && objectType.GetGenericTypeDefinition().IsAssignableFrom(typeof(KeyedCollection<>));
        }
    }
}