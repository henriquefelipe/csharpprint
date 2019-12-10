using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryNet;

namespace LibaryNet.PrinterServiceFunctions
{
    internal class NewLine : PrinterServiceFunction
    {
        public NewLine(PrinterService service) : base(service)
        {
        }

        protected override void Run()
        {
            string s = GetParameter("string").GetString();

            if (Service.linefeed)
                Service.DevPos(Service.row + 1, 0);
            if (s.IndexOf('»') >= 0)
            {
                char padchar;
                int p = s.IndexOf('»');
                string a, b;
                a = s.Substring(0, p);
                b = s.Substring(p + 1);
                if (a.StartsWith("--"))
                    padchar = '-';
                else
                    padchar = ' ';
                int l = Service.columnsscaled - (b.Length + 1);
                if (l >= a.Length)
                    s = a.PadRight(l, padchar) + " " + b;
                else
                    s = a.Substring(0, l) + " " + b;
            }
            Service.DevOut(s, true);
            Service.linefeed = true;
        }
    }
}
