namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.WebApi.HALSupport.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Serializers;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class HalResourceJsonSerializerTests
    {
        private JsonSerializerSettings settings;

        [SetUp]
        public void SetUp()
        {
            this.settings = new JsonSerializerSettings { Converters = new JsonConverter[] { new HalResourceJsonConverter() } };
        }

        [Test]
        public void Serialize_WhenGivenValidClass_ShouldReturnSerializedHalResource()
        {
            const string ExpectedResult = "{\"Name\":\"Test\",\"_links\":{\"self\":{\"href\":\"/\"}},\"_embedded\":{\"Owner\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}";
            var source = new Resource<TestPage>(new TestPage() { Name = "Test" })
                             {
                                 Links =
                                     new KeyedCollection<Link>
                                         {
                                             new Link("self", new Uri("/", UriKind.RelativeOrAbsolute))
                                         },
                                 EmbeddedResources =
                                     new KeyedCollection<EmbeddedResource> 
                                     {
                                             new EmbeddedResource(
                                                 "Owner", 
                                                 new User()
                                                     {
                                                         Id = "oveand",
                                                         Name = "Ove Andersen"
                                                     })
                                         }
                             };

            var result = JsonConvert.SerializeObject(source, this.settings);

            Assert.AreEqual(ExpectedResult, result);
        }
        
        [Test]
        public void Deserialize_WhenGivenValidClass_ShouldReturnSerializedHalResource()
        {
            const string json = "{\"Name\":\"Test\",\"_links\":{\"self\":{\"href\":\"/\"}},\"_embedded\":{\"Owner\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}";

            var obj = JsonConvert.DeserializeObject<Resource<TestPage>>(json, this.settings);

            Assert.AreEqual("Test", obj.State.Name);
            Assert.AreEqual("/", obj.Links["self"].Target.ToString());
            Assert.AreEqual("oveand", obj.EmbeddedResources["Owner"].ToResource<User>().State.Id);
            Assert.AreEqual("Ove Andersen", obj.EmbeddedResources["Owner"].ToResource<User>().State.Name);
        }
    }
}