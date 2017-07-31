using Civica.InfoPath;
using Microsoft.Deployment.Compression.Cab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExampleCabBuilder
{
    public class WixCabService : ICabService
    {
        public Cab Create()
        {
            return new Cab();
        }

        public void Save(Cab cab)
        {
            var cabInfo = new CabInfo("test.xsn");

            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            
            foreach(var file in cab.XmlDocs)
            {
                using (XmlTextWriter wr = new XmlTextWriter(tempDirectory + "\\" + file.Key, Encoding.UTF8))
                {
                    wr.Formatting = Formatting.None;
                    file.Value.Save(wr);
                }
            }

            foreach(var file in cab.Files)
            {
                File.WriteAllBytes(tempDirectory + "\\" + file.Key, file.Value);
            }

            cabInfo.Pack(tempDirectory);

            Directory.Delete(tempDirectory, true);

        }
    }
}
