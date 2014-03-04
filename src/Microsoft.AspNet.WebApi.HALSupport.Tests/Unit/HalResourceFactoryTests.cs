namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System.Linq;

    using Microsoft.AspNet.WebApi.HALSupport.Factories;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using NUnit.Framework;

    [TestFixture]
    public class HalResourceFactoryTests
    {
        private ResourceFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new ResourceFactory();
        }

        [Test]
        public void Create_WhenGivenTypedSourceObject_ShouldReturnValidHALResource()
        {
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" }};

            var result = this.factory.Create(source);

            Assert.AreEqual(source.Name, result.State.Name);
            Assert.IsNull(result.State.Owner);
            Assert.AreEqual("oveand", result.EmbeddedResources["Owner"].ToResource<User>().State.Id);
            Assert.AreEqual("Ove Andersen", result.EmbeddedResources["Owner"].ToResource<User>().State.Name);
            Assert.IsNull(result.Links);
        }

        [Test]
        public void Create_WhenGivenAnonyomousSourceObject_ShouldReturnValidHALResource()
        {
            var source = new { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } };

            var result = this.factory.Create(source);

            Assert.AreEqual(source.Name, result.State.Name);
            Assert.IsNotNull(result.State.Owner);
            Assert.AreEqual(source.Owner.Id, result.EmbeddedResources["Owner"].ToResource<User>().State.Id);
            Assert.AreEqual(source.Owner.Name, result.EmbeddedResources["Owner"].ToResource<User>().State.Name);
            Assert.IsNull(result.Links);
        }

        [Test]
        public void Create_WhenGivenStruct_ShouldReturnValidHALResource()
        {
            var result = this.factory.Create("test");

            Assert.AreEqual("test", result.State);
            Assert.IsNull(result.EmbeddedResources);
            Assert.IsNull(result.Links);
        }
    }
}