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
        public Builder(ICabService cabService)
        {
            this.CabService = cabService;

            // Create new cab file
            this.Form = cabService.Create();

            // Add default xmls
            addResource("myschema.xsd",Defaults.myschema);
            addResource("sampledata.xml", Defaults.sampledata);
            addResource("template.xml", Defaults.template);
            addResource("view1.xsl", Defaults.view1);
            addResource("manifest.xsf", Defaults.manifest);
        }

        public Cab Form { get; private set; }

        private void addResource(string resourceName, byte[] resourceBytes)
        {
            var resourceXml = new XmlDocument();
            resourceXml.LoadXml(UTF8Encoding.UTF8.GetString(resourceBytes));
            this.Form.XmlDocs[resourceName] = resourceXml;
        }

        private void addResource(string resourceName, string resource)
        {
            var resourceXml = new XmlDocument();
            resourceXml.LoadXml(resource);
            this.Form.XmlDocs[resourceName] = resourceXml;
        }

    }
}
