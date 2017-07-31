using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Civica.InfoPath
{
    public class Builder
    {
        private ICabService CabService;

        private XmlDocument manifest;
        public Builder(ICabService cabService)
        {
            this.CabService = cabService;

            // Create new cab file
            this.Form = cabService.Create();

            // Add default xmls
            AddXmlResource("myschema.xsd",Defaults.myschema);
            AddXmlResource("sampledata.xml", Defaults.sampledata);
            AddXmlResource("template.xml", Defaults.template);
            AddXmlResource("view1.xsl", Defaults.view1);
            manifest = AddXmlResource("manifest.xsf", Defaults.manifest);
            AddResource("styles.css", Defaults.styles);
        }

        public Cab Form { get; private set; }

        private XmlDocument AddXmlResource(string resourceName, byte[] resourceBytes)
        {
            var resourceXml = new XmlDocument();
            resourceXml.LoadXml(UTF8Encoding.UTF8.GetString(resourceBytes));
            this.Form.Add(resourceName, resourceXml);
            return resourceXml;
        }

        private void AddXmlResource(string resourceName, string resource)
        {
            var resourceXml = new XmlDocument();
            resourceXml.LoadXml(resource);
            this.Form.Add(resourceName, resourceXml);
        }

        public void AddResource(string resourceName, string resource)
        {
            this.Form.Add(resourceName, UTF8Encoding.UTF8.GetBytes(resource));

            // Add the reference to the file
            var nsmgr = new XmlNamespaceManager(this.Manifest.NameTable);
            nsmgr.AddNamespace("xsf", "http://schemas.microsoft.com/office/infopath/2003/solutionDefinition");

            var file = this.Manifest.CreateElement("xsf", "file", "http://schemas.microsoft.com/office/infopath/2003/solutionDefinition");
            var name = this.Manifest.CreateAttribute("name");
            name.Value = resourceName;
            file.Attributes.Append(name);
            this.Manifest.SelectSingleNode("/xsf:xDocumentClass/xsf:package/xsf:files", nsmgr)
                .AppendChild(file);

        }

        public XmlDocument Manifest
        {
            get
            {
                return this.manifest;
            }
        }
    }
}
