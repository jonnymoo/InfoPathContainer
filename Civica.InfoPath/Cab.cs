using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;



namespace Civica.InfoPath
{
    public class Cab
    {
        private Dictionary<string, XmlDocument> xmls = new Dictionary<string, XmlDocument>();

        private Dictionary<string, byte[]> binarys = new Dictionary<string, byte[]>();

        public Cab()
        {

        }

        public void Add(string fileName, XmlDocument xml)
        {
            // Add to xmls
            this.xmls.Add(fileName, xml);
        }

        public void Add(string fileName, byte[] contents)
        {
            // Add to binarys
            this.binarys.Add(fileName, contents);
        }
 
        public IEnumerable<KeyValuePair<string, XmlDocument>> XmlDocs
        {
            get
            {
                return this.xmls.AsEnumerable();
            }
        }

        public IEnumerable<KeyValuePair<string, byte[]>> Files
        {
            get
            {
                return this.binarys.AsEnumerable();
            }
        }
    }
}
