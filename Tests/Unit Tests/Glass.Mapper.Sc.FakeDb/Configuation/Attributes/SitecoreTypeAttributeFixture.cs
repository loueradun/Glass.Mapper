

using System;
using System.Linq;
using Glass.Mapper.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using NUnit.Framework;
using Sitecore.Data;

namespace Glass.Mapper.Sc.FakeDb.Configuation.Attributes
{
    [TestFixture]
    public class SitecoreTypeAttributeFixture
    {
        [Test]
        public void Does_SitecoreTypeAttribute_Extend_AbstractClassAttribute()
        {
            Assert.IsTrue(typeof(AbstractTypeAttribute).IsAssignableFrom(typeof(SitecoreTypeAttribute)));
        }

        [Test]
        [TestCase("TemplateId")]
        [TestCase("BranchId")]
        public void Does_SitecoreTypeAttribute_Have_Properties(string propertyName)
        {
            var properties = typeof(SitecoreTypeAttribute).GetProperties();
            Assert.IsTrue(properties.Any(x => x.Name == propertyName));
        }

        #region Method - Configure

        [Test]
        public void Configure_ConfigurationIsOfCorrectType_NoExceptionThrown()
        {
            //Assign
            var attr = new SitecoreTypeAttribute();
            var type = typeof (StubClass);

            var templateIdExpected = Guid.Empty;
            var branchIdExptected = Guid.Empty;

            //Act
            var config = attr.Configure(type) as SitecoreTypeConfiguration;

            //Assert
            Assert.AreEqual(type, config.Type);
            Assert.AreEqual(new ID(templateIdExpected), config.TemplateId);
            Assert.AreEqual(new ID(branchIdExptected), config.BranchId);
        }

        [Test]
        public void Configure_AttributeHasTemplateId_TemplateIdSetOnConfig()
        {
            //Assign
            var attr = new SitecoreTypeAttribute();
            var type = typeof(StubClass);

            var templateIdExpected = new Guid();
            var branchIdExptected = Guid.Empty;

            attr.TemplateId = templateIdExpected.ToString();

            //Act
            var config = attr.Configure(type) as SitecoreTypeConfiguration;

            //Assert
            Assert.AreEqual(type, config.Type);
              Assert.AreEqual(new ID( templateIdExpected), config.TemplateId);
            Assert.AreEqual(new ID(branchIdExptected), config.BranchId);
        }

        [Test]
        public void Configure_AttributeHasBranchId_BranchIdSetOnConfig()
        {
            //Assign
            var attr = new SitecoreTypeAttribute();
            var type = typeof(StubClass);

            var templateIdExpected = Guid.Empty;
            var branchIdExptected = new Guid();

            attr.BranchId = branchIdExptected.ToString();

            //Act
           var config = attr.Configure(type) as SitecoreTypeConfiguration;

            //Assert
            Assert.AreEqual(type, config.Type);
            Assert.AreEqual(new ID(templateIdExpected), config.TemplateId);
            Assert.AreEqual(new ID(branchIdExptected), config.BranchId);
        }

        [Test]
        public void Configure_AttributeHasInvalidTemplateId_ExceptionThrown()
        {
            //Assign
            var attr = new SitecoreTypeAttribute();
            var type = typeof(StubClass);

            var templateIdExpected = Guid.Empty;
            var branchIdExptected = Guid.Empty;

            attr.TemplateId = "not a guid";

            //Act

            Assert.Throws<MapperException>(() =>
            {
                var config = attr.Configure(type) as SitecoreTypeConfiguration;
            });
            //Assert

        }

        [Test]
        public void Configure_AttributeHasInvalidBranchId_ExceptionThrown()
        {
            //Assign
            var attr = new SitecoreTypeAttribute();
            var type = typeof(StubClass);

            var templateIdExpected = Guid.Empty;
            var branchIdExptected = Guid.Empty;

            attr.BranchId = "not a guid";

            //Act
            Assert.Throws<MapperException>(() =>
            {
                var config = attr.Configure(type) as SitecoreTypeConfiguration;
            });
            //Assert

        }

        #endregion

        #region Stubs

        public class StubClass
        {

        }

        #endregion
    }
}




