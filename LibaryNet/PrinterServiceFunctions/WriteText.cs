using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class WriteText : PrinterServiceFunction
    {
        public WriteText(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            string s = GetParameter("string").GetString();

            if (s.Length == 0)
                return;
            if (!Service.started)
                Service.InitializeDoc();
            Service.text.Append(s);
        }
    }
}
