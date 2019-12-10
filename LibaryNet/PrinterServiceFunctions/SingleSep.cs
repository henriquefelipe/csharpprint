using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class SingleSep : PrinterServiceFunction
    {
        public SingleSep(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (Service.lastsep == Service.row)
                return;
            string s = "";
            for (int i = 1; i <= Service.columnsscaled; i++)
                s += PrinterDriver.SepSingle;
            Service.NewLine(s);
            Service.lastsep = Service.row;
        }
    }
}
