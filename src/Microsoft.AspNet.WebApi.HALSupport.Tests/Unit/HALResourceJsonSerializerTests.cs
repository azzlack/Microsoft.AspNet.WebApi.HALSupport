namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System;
    using System.Collections.Generic;

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
            this.settings = new JsonSerializerSettings { Converters = new JsonConverter[] { new HalResourceJsonSerializer() } };
        }

        [Test]
        public void Serialize_WhenGivenValidClass_ShouldReturnSerializedHalResource()
        {
            const string ExpectedResult = "{\"Name\":\"Test\",\"_links\":{\"self\":{\"href\":\"/\"}},\"_embedded\":{\"Owner\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}";
            var source = new HalResource<TestPage>(new TestPage() { Name = "Test" })
                             {
                                 Links =
                                     new List<HalLink>
                                         {
                                             new HalLink
                                                 {
                                                     Relation = "self",
                                                     Target = new Uri("/", UriKind.RelativeOrAbsolute)
                                                 }
                                         },
                                 EmbeddedResources =
                                     new HalEmbeddedResourceDictionary 
                                     {
                                             new HalEmbeddedResource(
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
    }
}