using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class DevPOS : PrinterServiceFunction
    {
        public DevPOS(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            int newrow = GetParameter("newRow").GetInt();
            int newcol = GetParameter("newCol").GetInt();

            if (newrow < Service.row)
                Service.Eject();
            else if (newrow > Service.row)
            {
                while (Service.row < newrow)
                {
                    //if not AbsolutePos and (FLines > FEjectLines) and ((Row mod FLines) >= (FLines - FEjectLines)) then
                    //  Row := FLines * Trunc(Ceil((Row + 1) / FLines));
                    Service.WriteText(PrinterDriver.LineFeed, true);
                    ++Service.row;
                }
                Service.col = 0;
                Service.linefeed = true;
            }
            if (newcol != Service.col)
            {
                if (newcol < Service.col)
                {
                    if (PrinterDriver.AllowCR)
                        Service.WriteText(PrinterDriver.LineFeed, true);
                    Service.col = 0;
                }
                else if (newcol > Service.col)
                {
                    Service.WriteText("".PadLeft(Service.col - newcol), true);
                    Service.col = newcol;
                }
            }
        }
    }
}
