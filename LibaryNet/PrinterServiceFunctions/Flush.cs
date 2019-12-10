using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Flush : PrinterServiceFunction
    {
        public Flush(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            string documentname = GetParameter("documentName").GetString();
            int copies = GetParameter("copies").GetInt();

            Service.Eject();
            Service.Cut();
            while (--copies >= 0)
                PrinterDriver.SendStringToPrinter(PrinterDriver.DeviceName,
                    documentname, Service.text.ToString());
            Service.FinalizeDoc();
        }
    }
}
