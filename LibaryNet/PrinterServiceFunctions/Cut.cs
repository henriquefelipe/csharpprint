using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Cut : PrinterServiceFunction
    {
        public Cut(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (PrinterDriver.Cortar)
                Service.NewLine(PrinterDriver.Cut);
        }
    }
}
