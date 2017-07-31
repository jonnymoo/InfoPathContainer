using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "manifest.xsf"));
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "myschema.xsd"));
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "sampledata.xml"));
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "template.xml"));
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "view1.xsl"));
            Assert.IsNotNull(form.XmlDocs.Where(x => x.Key == "styles.css"));
        }

        [TestMethod]
        public void Given_a_form_when_I_add_resources_I_expect_the_manifest_to_contain_a_reference()
        {
            var cab = new Cab();
            var cabService = new Mock<ICabService>();
            cabService.Setup(s => s.Create()).Returns(cab);

            var builder = new Builder(cabService.Object);

            builder.AddResource("myfile", "Some content");

            // Add the namespace.  
            var nsmgr = new XmlNamespaceManager(builder.Manifest.NameTable);
            nsmgr.AddNamespace("xsf", "http://schemas.microsoft.com/office/infopath/2003/solutionDefinition");

            var node = builder.Manifest.SelectSingleNode("/xsf:xDocumentClass/xsf:package/xsf:files/xsf:file[@name='myfile']",nsmgr);

            Assert.IsNotNull(node);

        }
    }
}
