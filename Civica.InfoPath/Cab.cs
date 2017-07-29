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

        public Cab()
        {
            XmlDocs = new Dictionary<string, XmlDocument>();
        }
        public Dictionary<string, XmlDocument> XmlDocs { get; private set; }
  
    }
}
