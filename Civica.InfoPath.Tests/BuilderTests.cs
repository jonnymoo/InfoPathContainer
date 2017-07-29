using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Xml;
using System.Collections.Generic;

namespace Civica.InfoPath.Tests
{
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void Given_nothing_else_when_I_build_an_info_path_form_I_get_a_default_form()
        {

            var cab = new Cab();
            var cabService = new Mock<ICabService>();
            cabService.Setup(s => s.Create()).Returns(cab);

            var builder = new Builder(cabService.Object);

            var form = builder.Form;
            
            Assert.IsNotNull(form.XmlDocs["manifest.xsf"]);
            Assert.IsNotNull(form.XmlDocs["myschema.xsd"]);
            Assert.IsNotNull(form.XmlDocs["sampledata.xml"]);
            Assert.IsNotNull(form.XmlDocs["template.xml"]);
            Assert.IsNotNull(form.XmlDocs["view1.xsl"]);


        }
    }
}
