using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Eject : PrinterServiceFunction
    {
        public Eject(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (PrinterDriver.EjectLines > 0)
                Service.DevPos(Service.row + PrinterDriver.EjectLines, 0);
        }
    }
}
