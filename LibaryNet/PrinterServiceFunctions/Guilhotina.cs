using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class Guilhotina : PrinterServiceFunction
    {
        public Guilhotina(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            Service.NewLine(PrinterDriver.Cut);
        }
    }
}
