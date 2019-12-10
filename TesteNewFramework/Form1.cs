using LibaryNet.Driver;
using LibaryNet.Enum;
using LibraryNet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TesteNewFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cbDispositivos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDriver.DropDownStyle = ComboBoxStyle.DropDownList;
            FillDevices();
        }

        private void FillDevices()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                cbDispositivos.Items.Add(printer);
        }

        private void FillDrivers()
        {
            List<PrinterDriverBase> drivers = new List<PrinterDriverBase>();
            string dispositivo = cbDispositivos.SelectedItem.ToString();
            drivers.Add(new DefaultTextDriver(dispositivo));
            drivers.Add(new DefaultEpsonDriver(dispositivo));
            drivers.Add(new DefaultDarumaDriver(dispositivo));
            drivers.Add(new DefaultBematechDriver(dispositivo));
            drivers.Add(new DefaultElginDriver(dispositivo));

            cbDriver.DataSource = drivers;
            cbDriver.DisplayMember = "FriendlyName";
        }

        private void cbDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDrivers();
        }

        private void btEnviaTexto_Click(object sender, EventArgs e)
        {
            PrinterDriverBase driver = (PrinterDriverBase)cbDriver.SelectedValue;
            PrinterService service = new PrinterService(driver);
            service.Expand(ExpandType.Width);
            service.NewLine(service.PadCenter("Titulo"));
            service.WriteText(txTexto.Text);
            service.Texto("»" + txTexto.Text);
            service.SingleSep();
            service.Flush("Documento de teste", 1);
        }
    }
}
