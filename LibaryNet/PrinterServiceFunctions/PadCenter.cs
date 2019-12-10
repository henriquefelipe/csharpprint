using CSharpPrint;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class PadCenter : PrinterServiceFunction
    {
        public PadCenter(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            string s = GetParameter("string").GetString();
            int count = GetParameter("count").GetInt();
            char padingchar = GetParameter("paddingChar").GetChar();

            if (count == -1)
                count = Service.columnsscaled;
            int spaces = count - s.Length;
            int padleft = (spaces / 2) + s.Length;

            FunctionResult = s.PadLeft(padleft, padingchar).PadRight(count, padingchar);
        }
    }
}
