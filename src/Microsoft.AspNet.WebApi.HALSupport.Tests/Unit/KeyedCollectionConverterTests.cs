namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System.Linq;

    using Microsoft.AspNet.WebApi.HALSupport.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Serializers;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class KeyedCollectionConverterTests
    {
        private JsonSerializerSettings settings;

        [SetUp]
        public void SetUp()
        {
            this.settings = new JsonSerializerSettings() { Converters = new[] { new KeyedCollectionConverter() } };
        }

        [Test]
        public void WriteJson_WhenGivenValidCollection_ShouldReturnCorrectJson()
        {
            const string expectedResult = "{\"Owner\":{\"State\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}";
            var collection = new KeyedCollection<EmbeddedResource>
                                 {
                                     new EmbeddedResource(
                                         "Owner",
                                         new User()
                                             {
                                                 Id = "oveand",
                                                 Name = "Ove Andersen"
                                             })
                                 };

            var json = JsonConvert.SerializeObject(collection, this.settings);

            Assert.AreEqual(expectedResult, json);
        }

        [Test]
        public void ReadJson_WhenGivenValidJson_ShouldReturnCollection()
        {
            var o =
                JsonConvert.DeserializeObject<KeyedCollection<EmbeddedResource>>(
                    "{\"Owner\":{\"State\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}", this.settings);

            Assert.AreEqual("Owner", o.First().Key);
            Assert.AreEqual("oveand", o.First().Value.To<User>().State.Id);
            Assert.AreEqual("Ove Andersen", o.First().Value.To<User>().State.Name);
        }
    }
}