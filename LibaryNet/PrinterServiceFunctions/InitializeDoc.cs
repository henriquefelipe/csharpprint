using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class InitializeDoc : PrinterServiceFunction
    {
        public InitializeDoc(PrinterService service) 
            : base(service)
        {
        }

        protected override void Run()
        {
            if (Service.started)
                return;
            Service. started = true;
            Service.linefeed = false;
            Service.lastsep = -1;
            Service.row = 0;
            Service.col = 0;
            Service.compressset = false;
            Service.italicset = false;
            Service.boldset = false;
            Service.expandset = PrinterDriver.ExpandType;
            Service.columnsscaled = PrinterDriver.Columns;
            Service.text.Clear();
            if (PrinterDriver.BeginCmd != "")
                Service.text.Append(PrinterDriver.BeginCmd);
        }
    }
}
