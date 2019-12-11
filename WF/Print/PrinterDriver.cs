using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Library.Enumerador;

namespace WF.Print
{
    public class PrinterDriver
    {
        const string CHR_GS = "\x1D";
        const string CHR_ESC = "\x1B";
        const int INDEX_FALSE = 0;
        const int INDEX_TRUE = 1;
        public enum DriverType
        {
            Text = 0,
            EPSON = 10,
            BEMATECH = 20,
            DARUMA = 30,
            ELGIN = 40
        }
        const byte BOOL_COUNT = 2;
        public enum ExpandType
        {
            None = 0,
            Width = 1,
            Heigth = 2,
            Double = 3
        }
        const byte EXPAND_COUNT = 4;

        private string devicename = "";
        private string drivername = "";        
        private string begincmd = "";
        private string endcmd = "";
        private string cut = "";
        private string linefeed = "";
        private string eject = "";
        private string sepsingle = "-";
        private string sepdouble = "=";
        private string underline = "_";
        private string bel = "";
        private double scalecompres = 1;
        private double scaleexpanded = 1;        
        private int defaultcolumns = 48;
        private int defaultejectlines = 0;
        private bool hasplaintext = false;
        private string[] compress = new string[BOOL_COUNT] {"", ""};
        private string[] italic = new string[BOOL_COUNT] {"", ""};
        private string[] bold = new string[BOOL_COUNT] {"", ""};
        private string[] expand = new string[EXPAND_COUNT] {"", "", "", ""};
        private bool allowcr = true;
        private string ean13 = "";
        private bool hasreserverdchars = false;
        private string reservedchars = "";
        private string openbin = "";
        private int lines = 0;
        private int columns = 48;
        private int ejectlines = 0;
        private bool cortar = false;
        public byte impressoraModelo;
        public PrinterDriver(string caminho, byte modelo, int colunas)
        {
            this.devicename = caminho.Substring(1, caminho.Length - 1);
            impressoraModelo = modelo;

            switch (modelo)
            {
                case (byte)ImpressoraModelo.TEXTO:                   
                    DriverText();
                    break;
                case (byte)ImpressoraModelo.EPSON:                   
                    DriverEPSON();
                    break;
                case (byte)ImpressoraModelo.BEMATECH:                   
                    DriverBEMATECH();
                    break;
                case (byte)ImpressoraModelo.DARUMA:                    
                    DriverDARUMA();
                    break;
                case (byte)ImpressoraModelo.ELGIN:                    
                    DriverELGIN();
                    break;
                default:
                    DriverText();
                    break;
            }

            this.drivername = GenericEnum<ImpressoraModelo>.GetDescription(modelo);

            if (columns < 20)
                this.columns = defaultcolumns;
            else
                this.columns = colunas;

            this.cortar = true;
            this.ejectlines = 1;
        }

        public PrinterDriver(string configurations)
        {
            int p = configurations.IndexOf(';');
            if (p > 0)
            {
                devicename = configurations.Substring(0, p);
                configurations = configurations.Substring(p + 1);
            }
            else
            {
                devicename = configurations;
                configurations = "";
            }
            if (devicename.StartsWith("$"))
                devicename = devicename.Substring(1);           
            var aconfigurations = configurations.Split(';');
            var dconfigurations = new Dictionary<string, string>();
            foreach (var item in aconfigurations)
            {
                var aconfigurationsitem = item.Split('=');
                if (aconfigurationsitem.Length > 1)
                    dconfigurations.Add(aconfigurationsitem[0], aconfigurationsitem[1]);
            }
            string keyvalue = "";
            // DRIVER
            if (!dconfigurations.TryGetValue("DRIVER", out keyvalue))
                keyvalue = "";
            else if (keyvalue.StartsWith("BEMATECH_"))
            {
                keyvalue = "BEMATECH";
            }
            else if (keyvalue.StartsWith("EPSON_"))
            {
                keyvalue = "EPSON";
            }
            else if (keyvalue.StartsWith("DARUMA_"))
            {
                keyvalue = "DARUMA";                
            }
            if (keyvalue == "")
                keyvalue = DriverType.Text.ToString();
            DriverType driver;
            if (!Enum.TryParse(keyvalue, true, out driver))
                driver = DriverType.Text;
            switch (driver)
            {
                case DriverType.Text:
                    DriverText();
                    break;
                case DriverType.EPSON:
                    DriverEPSON();
                    impressoraModelo = (byte)ImpressoraModelo.EPSON;
                    break;
                case DriverType.BEMATECH:
                    DriverBEMATECH();
                    impressoraModelo = (byte)ImpressoraModelo.BEMATECH;
                    break;
                case DriverType.DARUMA:
                    DriverDARUMA();
                    impressoraModelo = (byte)ImpressoraModelo.DARUMA;
                    break;
                case DriverType.ELGIN:
                    DriverELGIN();
                    impressoraModelo = (byte)ImpressoraModelo.ELGIN;
                    break;
                default:
                    DriverText();
                    break;
            }
            drivername = driver.ToString();
            // COLUNAS
            if (!dconfigurations.TryGetValue("COLUNAS", out keyvalue))
                keyvalue = "";
            if (!Int32.TryParse(keyvalue, out columns))
                columns = 0;
            if (columns < 20)
                columns = defaultcolumns;
            // CORTAR
            if (!dconfigurations.TryGetValue("GUILHOTINA", out keyvalue))
                keyvalue = "";
            if (!bool.TryParse(keyvalue, out cortar))
                cortar = true;
            // LINHAS PARA EJETAR
            if (!dconfigurations.TryGetValue("EJETAR", out keyvalue))
                keyvalue = "";
            if (!Int32.TryParse(keyvalue, out ejectlines))
                ejectlines = 0;

            if (ejectlines < 0)
                ejectlines = defaultejectlines;
        }

        private void DriverText()
        {
            begincmd = "";
            endcmd = "";
            cut = "";
            linefeed = Environment.NewLine;
            eject = "";
            sepsingle = "-";
            sepdouble = "=";
            bel = "";
            scalecompres = 1;
            scaleexpanded = 1;
            defaultcolumns = 48;
            defaultejectlines = 0;
            hasplaintext = true;
            compress[INDEX_FALSE] = "";
            compress[INDEX_TRUE] = "";
            italic[INDEX_FALSE] = "";
            italic[INDEX_TRUE] = "";
            bold[INDEX_FALSE] = "";
            bold[INDEX_TRUE] = "";
            expand[(int)ExpandType.None] = "";
            expand[(int)ExpandType.Width] = "";
            expand[(int)ExpandType.Heigth] = "";
            expand[(int)ExpandType.Double] = "";
            allowcr = false;
            ean13 = "";
            hasreserverdchars = false;
            reservedchars = "";
            openbin = "";            
        }
        private void DriverBEMATECH()
        {
            begincmd = CHR_ESC + "@";// "\x1D\xF9\x29\x30"
            endcmd = "";
            cut = CHR_ESC + "w";
            linefeed = Environment.NewLine;
            eject = "";
            sepsingle = "-";
            sepdouble = "=";
            bel = "";
            scalecompres = 1.0;
            scaleexpanded = 0.5;
            defaultcolumns = 42;
            defaultejectlines = 10;
            hasplaintext = true;
            compress[INDEX_FALSE] = "";
            compress[INDEX_TRUE] = "";
            italic[INDEX_FALSE] = ""; // CHR_ESC + "5";
            italic[INDEX_TRUE] = ""; //CHR_ESC + "4";
            bold[INDEX_FALSE] = ""; //CHR_ESC + "F";
            bold[INDEX_TRUE] = ""; //CHR_ESC + "E";
            expand[(int)ExpandType.None] = CHR_ESC + "!" + (char)127;
            expand[(int)ExpandType.Width] = CHR_ESC + "!" + (char)32;
            expand[(int)ExpandType.Heigth] = CHR_ESC + "!" + (char)16;
            expand[(int)ExpandType.Double] = CHR_ESC + "!" + (char)48;
            allowcr = false;
            ean13 = "";
            hasreserverdchars = false;
            reservedchars = "";
            openbin = "";
        }
        private void DriverDARUMA()
        {
            begincmd = CHR_ESC + "@" + CHR_ESC + "H" + (char)20;
            endcmd = "";
            cut = CHR_ESC + "m";
            linefeed = Environment.NewLine;
            eject = "";
            sepsingle = "-";
            sepdouble = "=";
            bel = "";
            scalecompres = 1.0;
            scaleexpanded = 0.5;
            defaultcolumns = 48;
            defaultejectlines = 10;
            hasplaintext = true;
            compress[INDEX_FALSE] = "";
            compress[INDEX_TRUE] = "";
            italic[INDEX_FALSE] = CHR_ESC + "4" + (char)127;
            italic[INDEX_TRUE] = CHR_ESC + "4" + (char)1;
            bold[INDEX_FALSE] = CHR_ESC + "F";
            bold[INDEX_TRUE] = CHR_ESC + "E";
            expand[(int)ExpandType.None] = (char)20 + "";
            expand[(int)ExpandType.Width] = (char)14 + "";
            expand[(int)ExpandType.Heigth] = (char)20 + "";
            expand[(int)ExpandType.Double] = (char)14 + "";
            allowcr = false;
            ean13 = "";
            hasreserverdchars = false;
            reservedchars = "";
            openbin = "";
        }
        private void DriverEPSON()
        {
            begincmd = CHR_ESC + "@";
            endcmd = "";
            cut = CHR_GS + "V1";
            linefeed = Environment.NewLine;
            eject = "";
            sepsingle = "-";
            sepdouble = "=";
            bel = "";
            scalecompres = 1.0;
            scaleexpanded = 0.5;
            defaultcolumns = 48;
            defaultejectlines = 10;
            hasplaintext = true;
            compress[INDEX_FALSE] = "";
            compress[INDEX_TRUE] = "";
            italic[INDEX_FALSE] = CHR_ESC + "5";
            italic[INDEX_TRUE] = CHR_ESC + "4";
            bold[INDEX_FALSE] = CHR_ESC + "F";
            bold[INDEX_TRUE] = CHR_ESC + "E";
            expand[(int)ExpandType.None] = CHR_ESC + "!" + (char)1;
            expand[(int)ExpandType.Width] = CHR_ESC + "!" + (char)33;
            expand[(int)ExpandType.Heigth] = CHR_ESC + "!" + (char)17;
            expand[(int)ExpandType.Double] = CHR_ESC + "!" + (char)49;
            allowcr = false;
            ean13 = "";
            hasreserverdchars = false;
            reservedchars = "";
            openbin = "";
        }
        private void DriverELGIN()
        {
            begincmd = CHR_ESC + "@";
            endcmd = "";
            cut = CHR_ESC + "m";
            //cut = "#29V#0";
            linefeed = Environment.NewLine;
            eject = "";
            sepsingle = "-";
            sepdouble = "=";
            bel = "";
            scalecompres = 1.0;
            scaleexpanded = 0.5;
            defaultcolumns = 48;
            defaultejectlines = 10;
            hasplaintext = true;
            compress[INDEX_FALSE] = "";
            compress[INDEX_TRUE] = "";
            italic[INDEX_FALSE] = CHR_ESC + "5";
            italic[INDEX_TRUE] = CHR_ESC + "4";
            bold[INDEX_FALSE] = CHR_ESC + "F";
            bold[INDEX_TRUE] = CHR_ESC + "E";
            expand[(int)ExpandType.None] = CHR_ESC + "!" + (char)1;
            expand[(int)ExpandType.Width] = CHR_ESC + "!" + (char)33;
            expand[(int)ExpandType.Heigth] = CHR_ESC + "!" + (char)17;
            expand[(int)ExpandType.Double] = CHR_ESC + "!" + (char)49;
            allowcr = false;
            ean13 = "";
            hasreserverdchars = false;
            reservedchars = "";
            openbin = "";
        }


        private string PlainText(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "";
            s = s.Normalize(NormalizationForm.FormD);
            var chars = s.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public string DeviceName
        {
            get { return devicename; }
        }
        public string DriverName
        { 
            get { return drivername; }
        }
        public string BeginCmd
        {
            get { return begincmd; }
        }
        public string EndCmd
        {
            get { return endcmd; }
        }
        public string Cut
        {
            get { return cut; }
        }
        public string LineFeed
        {
            get { return linefeed; }
        }
        public string Eject
        {
            get { return eject; }
        }
        public string SepSingle
        {
            get { return sepsingle; }
        }
        public string SepDouble
        {
            get { return sepdouble; }
        }
        public string Underline
        {
            get { return underline; }
        }
        public string Bel
        {
            get { return bel; }
        }
        public double ScaleCompres
        {
            get { return scalecompres; }
        }
        public double ScaleExpanded
        {
            get { return scaleexpanded; }
        }
        public int DefaultColumns
        {
            get { return defaultcolumns; }
        }
        public bool HasPlainText
        {
            get { return hasplaintext; }
        }
        public string[] Compress
        {
            get { return compress; }
        }
        public string[] Italic
        {
            get { return italic; }
        }
        public string[] Bold
        {
            get { return bold; }
        }
        public string[] Expand
        {
            get { return expand; }
        }
        public bool AllowCR
        {
            get { return allowcr; }
        }
        public string EAN13
        {
            get { return ean13; }
        }
        public bool HasReservedChars
        {
            get { return hasreserverdchars; }
        }
        public string ReservedChars
        {
            get { return reservedchars; }
        }
        public string OpenBin
        {
            get { return openbin; }
        }
        public int Lines
        {
            get { return lines; }
        }
        public int Columns
        {
            get { return columns; }
        }
        public int EjectLines
        {
            get { return ejectlines; }
        }
        public string ManageReserverdChars(string s)
        {
            return s;
        }
        public string ManagePlainText(string s)
        {
            return PlainText(s);
        }
        public bool Cortar
        {
            get { return cortar; }
        }
    }
}