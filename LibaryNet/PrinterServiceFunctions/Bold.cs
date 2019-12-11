using CSharpPrint;
using System;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Bold : PrinterServiceFunction
    {
        public Bold(PrinterService service)
            : base(service)
        {
        }

        protected override void Run()
        {
            bool boldtype = GetParameter("boldType").GetBool();
            if (Service.boldset == boldtype)
                return;
            Service.WriteText(PrinterDriver.Italic[Convert.ToByte(boldtype)], true);
            // Sempre após o WriteTex, que inicializa variáveis
            Service.boldset = boldtype;
        }
    }
}
