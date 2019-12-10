using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class UnderlineSep : PrinterServiceFunction
    {
        public UnderlineSep(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (Service.lastsep == Service.row)
                return;
            string s = "";
            for (int i = 1; i <= Service.columnsscaled; i++)
                s += PrinterDriver.Underline;
            Service.NewLine(s);
            Service.lastsep = Service.row;
        }
    }
}
