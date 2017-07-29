using Civica.InfoPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCabBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var cabService = new WixCabService();

            var builder = new Builder(cabService);

            cabService.Save(builder.Form);
        }
    }
}
