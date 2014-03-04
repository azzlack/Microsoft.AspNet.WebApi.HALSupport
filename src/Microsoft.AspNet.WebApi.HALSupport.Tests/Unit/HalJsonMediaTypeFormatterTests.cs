namespace Microsoft.AspNet.WebApi.HALSupport.Tests.Unit
{
    using System;
    using System.IO;
    using System.Text;

    using Microsoft.AspNet.WebApi.HALSupport.Formatters;
    using Microsoft.AspNet.WebApi.HALSupport.Models;
    using Microsoft.AspNet.WebApi.HALSupport.Tests.Models;

    using NUnit.Framework;

    [TestFixture]
    public class HalJsonMediaTypeFormatterTests
    {
        private HalJsonMediaTypeFormatter formatter;

        [SetUp]
        public void SetUp()
        {
            this.formatter = new HalJsonMediaTypeFormatter();
        }

        [TestCase(typeof(TestPage))]
        [TestCase(typeof(User))]
        public void CanReadType_WhenGivenType_ShouldReturnTrue(Type type)
        {
            Assert.IsTrue(this.formatter.CanReadType(type));
        }
        
        [TestCase(typeof(TestPage))]
        [TestCase(typeof(User))]
        public void CanWriteType_WhenGivenType_ShouldReturnTrue(Type type)
        {
            Assert.IsTrue(this.formatter.CanWriteType(type));
        }

        [Test]
        public void WriteToStream_WhenGivenValidObject_ShouldReturnCorrectJson()
        {
            using (var ms = new MemoryStream())
            {
                this.formatter.WriteToStream(
                    typeof(TestPage),
                    new TestPage() { Name = "Test", Owner = new User() { Id = "oveand", Name = "Ove Andersen" } },
                    ms,
                    Encoding.UTF8);

                ms.Seek(0, SeekOrigin.Begin);

                using (var sr = new StreamReader(ms))
                {
                    var r = sr.ReadToEnd();

                    Assert.AreEqual("{\"Name\":\"Test\",\"_embedded\":{\"Owner\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}", r);
                }
            }
        }

        [Test]
        public void ReadFromStream_WhenGivenValidJson_ShouldReturnCorrectResource()
        {
            const string Source = "{\"Name\":\"Test\",\"_embedded\":{\"Owner\":{\"Id\":\"oveand\",\"Name\":\"Ove Andersen\"}}}";
            var buffer = Encoding.UTF8.GetBytes(Source);

            using (var ms = new MemoryStream(buffer))
            {
                var r = (TestPage)this.formatter.ReadFromStream(typeof(Resource<TestPage>), ms, Encoding.UTF8, null);

                Assert.AreEqual("Test", r.Name);
                Assert.AreEqual("oveand", r.Owner.Id);
                Assert.AreEqual("Ove Andersen", r.Owner.Name);
            }
        }
    }
}