namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System.Linq;

    using Microsoft.AspNet.WebApi.HALSupport.Factories;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using NUnit.Framework;

    [TestFixture]
    public class HalResourceFactoryTests
    {
        private HalResourceFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.factory = new HalResourceFactory();
        }

        [Test]
        public void Create_WhenGivenValidSourceObject_ShouldReturnValidHALResource()
        {
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" }};

            var result = this.factory.Create(source);

            Assert.AreEqual(source.Name, result.State.Name);
            Assert.IsNull(result.State.Owner);
            Assert.AreEqual(source.Owner.Id, result.EmbeddedResources["Owner"].ToResource<User>().State.Id);
            Assert.AreEqual(source.Owner.Name, result.EmbeddedResources["Owner"].ToResource<User>().State.Name);
            Assert.AreEqual("/", result.Links.First(x => x.Relation == "self").Target.ToString());
        }
    }
}