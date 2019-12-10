using LibaryNet.Enum;
using LibraryNet;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LibaryNet.Driver
{
    public abstract class PrinterDriverBase
    {
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

        public ExpandType ExpandType
        {
            get
            {
                return expandType;
            }
        }

        protected const string CHR_GS = "\x1D";
        protected const string CHR_ESC = "\x1B";
        protected const int INDEX_FALSE = 0;
        protected const int INDEX_TRUE = 1;
        protected const byte BOOL_COUNT = 2;
        protected const byte EXPAND_COUNT = 4;

        protected string devicename = "";
        protected string drivername = "";
        protected string begincmd = "";
        protected string endcmd = "";
        protected string cut = "";
        protected string linefeed = "";
        protected string eject = "";
        protected string sepsingle = "-";
        protected string sepdouble = "=";
        protected string underline = "_";
        protected string bel = "";
        protected double scalecompres = 1;
        protected double scaleexpanded = 1;
        protected int defaultcolumns = 48;
        protected int defaultejectlines = 0;
        protected bool hasplaintext = false;
        protected string[] compress = new string[BOOL_COUNT] { "", "" };
        protected string[] italic = new string[BOOL_COUNT] { "", "" };
        protected string[] bold = new string[BOOL_COUNT] { "", "" };
        protected string[] expand = new string[EXPAND_COUNT] { "", "", "", "" };
        protected bool allowcr = true;
        protected string ean13 = "";
        protected bool hasreserverdchars = false;
        protected string reservedchars = "";
        protected string openbin = "";
        protected int lines = 0;
        protected int columns = 48;
        protected int ejectlines = 0;
        protected bool cortar = false;
        protected byte impressoraModelo;
        protected ExpandType expandType;

        public PrinterDriverBase(string deviceName)
        {
            devicename = deviceName;
            DriveLoad();
        }

        protected abstract void DriveLoad();

        protected abstract byte[] ComandoAberturaGavetaDriver();

        internal void AbrirGaveta()
        {
            byte[] comandos = ComandoAberturaGavetaDriver();
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(comandos.Length);
            Marshal.Copy(comandos, 0, unmanagedPointer, comandos.Length);
            Int32 dwCount = comandos.Length;

            LowLevelPrinterServices.SendBytesToPrinter(DeviceName, "", unmanagedPointer, dwCount);
            Marshal.FreeCoTaskMem(unmanagedPointer);
        }

        private string PlainText(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "";
            s = s.Normalize(NormalizationForm.FormD);
            var chars = s.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public override string ToString()
        {
            return $"{drivername}, on device {devicename}";
        }

        public string FriendlyName
        {
            get
            {
                return ToString();
            }
        }

        internal bool SendBytesToPrinter(string szPrinterName, string docname, 
            IntPtr pBytes, Int32 dwCount)
        {
           return  LowLevelPrinterServices.SendBytesToPrinter(szPrinterName,
                docname, pBytes, dwCount);
        }

        internal bool SendFileToPrinter(string szPrinterName,
            string docname, string szFileName)
        {
            return LowLevelPrinterServices.SendFileToPrinter(szPrinterName,
                docname, szFileName);
        }

        internal bool SendStringToPrinter(string szPrinterName, 
            string docname, string szString)
        {
            return LowLevelPrinterServices.SendStringToPrinter(szPrinterName,
                docname, szString);
        }
    }
}
