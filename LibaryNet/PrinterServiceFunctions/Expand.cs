using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryNet.Enum;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Expand : PrinterServiceFunction
    {
        public Expand(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            ExpandType expandtype = (ExpandType)GetParameter("expandType").Value;

            if (Service.expandset == expandtype)
                return;

            Service.WriteText(PrinterDriver.Expand[(int)expandtype], true);
            // Sempre após o WriteTex, que inicializa variáveis
            Service.expandset = expandtype;
            Service.columnsscaled = PrinterDriver.Columns;
            if (Service.compressset)
                Service.columnsscaled = (int)Math.Truncate(Service.columnsscaled * PrinterDriver.ScaleCompres);
            if ((Service.expandset == ExpandType.Width) || (Service.expandset == ExpandType.Double))
                Service.columnsscaled = (int)Math.Truncate(Service.columnsscaled * PrinterDriver.ScaleExpanded);
            if (PrinterDriver.AllowCR)
                Service.DevPos(Service.row, 0);
        }
    }
}
