using LibaryNet.Driver;
using LibaryNet.Enum;
using CSharpPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var driver = new DefaultTextDriver("Microsoft Print to PDF");
            var service = new PrinterService(driver);
            service.Expand(ExpandType.Width);
            service.NewLine("linha");
            service.WriteText("Texto de teste");
            service.Flush("ArquivoDeTeste", 1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
    }
}
