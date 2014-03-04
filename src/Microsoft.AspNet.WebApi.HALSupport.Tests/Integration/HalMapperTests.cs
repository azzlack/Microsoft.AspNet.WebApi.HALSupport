namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Integration
{
    using System;

    using Microsoft.AspNet.WebApi.HALSupport.Resolvers;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using NUnit.Framework;

    [TestFixture]
    public class HalMapperTests
    {
        [Test]
        public void Map_WhenGivenLinkMappingWithStaticValue_ShouldReturnCorrectValue()
        {
            // Create map
            HalMapper.Instance.CreateMap<TestPage>()
                 .ForLink("self").Use(new Uri("/pages/1", UriKind.Relative));

            // Create source object
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } };

            // Map source object
            var r = HalMapper.Instance.Map(source);

            // Assert
            Assert.AreEqual("/pages/1", r.Links["self"].Target.ToString());
        }

        [Test]
        public void Map_WhenGivenLinkMappingWithCustomResolver_ShouldReturnCorrectValue()
        {
            HalMapper.Instance.CreateMap<TestPage>() // Use custom resolver
                 .ForLink("self").Use<HttpContextResolver>();

            // Create source object
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } };

            // Map source object
            var r = HalMapper.Instance.Map(source);

            // Assert
            Assert.AreEqual("/", r.Links["self"].Target.ToString());
        }

        [TestCase("æøå")]
        [TestCase(":")]
        public void Map_WhenGivenLinkMappingWithCustomResolverAndConstructorWithInvalidUri_ShouldNotReturnValue(string uri)
        {
            HalMapper.Instance.CreateMap<TestPage>() // Use custom resolver
                 .ForLink("self").Use<StaticValueResolver>().ConstructedBy(() => new StaticValueResolver(uri));

            // Create source object
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } };

            // Map source object
            var r = HalMapper.Instance.Map(source);

            // Assert
            Assert.IsNull(r.Links["self"]);
        }

        [Test]
        public void Map_WhenGivenLinkMappingWithCustomResolverAndConstructorWithValidUri_ShouldReturnCorrectValue()
        {
            HalMapper.Instance.CreateMap<TestPage>() // Use custom resolver
                 .ForLink("self").Use<StaticValueResolver>().ConstructedBy(() => new StaticValueResolver("/test"));

            // Create source object
            var source = new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } };

            // Map source object
            var r = HalMapper.Instance.Map(source);

            // Assert
            Assert.AreEqual("/test", r.Links["self"].ToString());
        }

        [Test]
        public void Map_WhenGivenNoLinkMapping_ShouldThrowException()
        {
            // Create source object
            var source = new User() { Id = "oveand", Name = "Ove Andersen" };
            
            Assert.Throws<ArgumentException>(() => HalMapper.Instance.Map(source));
        }
    }
}