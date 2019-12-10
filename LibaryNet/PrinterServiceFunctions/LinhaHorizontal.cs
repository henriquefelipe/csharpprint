using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class LinhaHorizontal : PrinterServiceFunction
    {
        public LinhaHorizontal(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            if (Service.lastsep == Service.row)
            {
                FunctionResult = string.Empty;
                return;
            }

            string s = string.Empty;
            for (int i = 1; i <= Service.columnsscaled; i++)
            {
                s += PrinterDriver.SepSingle;
            }

            FunctionResult = Service.Texto(s);
        }
    }
}
