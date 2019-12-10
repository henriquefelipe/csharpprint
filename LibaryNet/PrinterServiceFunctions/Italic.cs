using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Italic : PrinterServiceFunction
    {
        public Italic(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            bool italictype = GetParameter("italicType").GetBool();
            if (Service.italicset == italictype)
                return;
            Service.WriteText(PrinterDriver.Italic[Convert.ToByte(italictype)], true);
            // Sempre após o WriteTex, que inicializa variáveis
            Service.italicset = italictype;
        }
    }
}
