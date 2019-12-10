using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class DoubleSep : PrinterServiceFunction
    {
        public DoubleSep(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (Service.lastsep == Service.row)
                return;
            string s = "";
            for (int i = 1; i <= Service.columnsscaled; i++)
                s += PrinterDriver.SepDouble;
            Service.NewLine(s);
            Service.lastsep = Service.row;
        }
    }
}
