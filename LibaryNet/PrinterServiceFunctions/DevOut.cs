using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class DevOut : PrinterServiceFunction
    {
        public DevOut(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            string s = GetParameter("string").GetString();
            bool wordwrap = GetParameter("wordWrap").GetBool();

            if (s.Length == 0)
                return;
            if (PrinterDriver.HasPlainText)
                s = PrinterDriver.ManagePlainText(s);
            string p;
            int i = 0;
            var lines = s.Split(PrinterService.CHR_LF);
            foreach (var line in lines)
            {
                p = line;
                while (p.Length > 0)
                {
                    if (i > 0)
                        Service.DevPos(Service.row + 1, 0);
                    i = Service.columnsscaled;
                    if ((i <= 0) || (i > p.Length))
                        i = p.Length;
                    Service.WriteText(p.Substring(0, i), false);
                    p = p.Remove(0, i);
                    Service.col += i;
                }
            }
        }
    }
}
