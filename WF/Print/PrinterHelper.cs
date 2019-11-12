using Library.Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WF.Net;

namespace WF.Print
{
    public class PrinterHelper
    {
        const char CHR_LF = (char)10;

        public enum Output
        {
            Printer,
            File
        }
        public enum Alignment
        {
            None,
            Left,
            Center,
            Right
        }        
        private StringBuilder text = new StringBuilder();
        private bool started;
        private bool linefeed = false;
        private int lastsep = -1;
        private int row = 0;
        private int col = 0;
        private bool compressset = false;
        private bool italicset = false;
        private bool boldset = false;
        private PrinterDriver.ExpandType expandset = PrinterDriver.ExpandType.None;        
        private int columnsscaled = 0;
        private Output output = Output.File;
        private PrinterDriver printerdriver;        
        public PrinterHelper(string caminho, byte modelo, int colunas)
        {
            //"\\SERVIDOR\CAIXA;DRIVER=DARUMA;COLUNAS=48";
            Clear();
            printerdriver = new PrinterDriver(caminho, modelo, colunas);
            //printerdriver = new PrinterDriver(caminho);            
            InitializeDoc();
        }

        public PrinterHelper(string caminho)
        {
            //"\\SERVIDOR\CAIXA;DRIVER=DARUMA;COLUNAS=48";
            Clear();           
            printerdriver = new PrinterDriver(caminho);
            InitializeDoc();
        }

        public int Columns
        {
            get { return printerdriver.Columns; }
        }
        public int ColumnsScaled
        {
            get { return columnsscaled; }
        }

        public string DeviceName
        {
            get { return printerdriver.DeviceName; }
        }
        public void Clear()
        {
            text.Clear();
            row = 0;
            col = 0;
        }
        private void ResetPos()
        {
            linefeed = false;
            row = 0;
            col = 0;
        }
        private void InitializeDoc()
        {
            if (started)
                return;
            //#if DEBUG
            //#else
            //#endif
            started = true;
            linefeed = false;
            lastsep = -1;
            row = 0;
            col = 0;
            compressset = false;
            italicset = false;
            boldset = false;
            expandset = PrinterDriver.ExpandType.None;
            columnsscaled = printerdriver.Columns;
            text.Clear();
            if (printerdriver.BeginCmd != "")
                text.Append(printerdriver.BeginCmd);
        }
        private void FinalizeDoc()
        {
            started = false;
            text.Clear();
        }
        public void WriteText(string s, bool controlchar)
        {
            if (s.Length == 0)
                return;
            if (!started)
                InitializeDoc();
            text.Append(s);
        }
        public void Bold(bool boldtype)
        {
            if (boldset == boldtype)
                return;
            WriteText(printerdriver.Italic[Convert.ToByte(boldtype)], true);
            // Sempre após o WriteTex, que inicializa variáveis
            boldset = boldtype;
        }
        public void Italic(bool italictype)
        {
            if (italicset == italictype)
                return;
            WriteText(printerdriver.Italic[Convert.ToByte(italictype)], true);
            // Sempre após o WriteTex, que inicializa variáveis
            italicset = italictype;
        }
        public void Expand(PrinterDriver.ExpandType expandtype)
        {
            if (expandset == expandtype)
                return;
            WriteText(printerdriver.Expand[(int)expandtype], true);
            // Sempre após o WriteTex, que inicializa variáveis
            expandset = expandtype;
            columnsscaled = printerdriver.Columns;
            if (compressset)
                columnsscaled = (int)Math.Truncate(columnsscaled * printerdriver.ScaleCompres);
            if ((expandset == PrinterDriver.ExpandType.Width) || (expandset == PrinterDriver.ExpandType.Double))
                columnsscaled = (int)Math.Truncate(columnsscaled * printerdriver.ScaleExpanded);
            if (printerdriver.AllowCR)                
                DevPos(row, 0);
        }
        private void DevPos(int newrow, int newcol)
        {
            if (newrow < row)
                Eject();
            else if (newrow > row)
            {
                while (row < newrow)
                {
                    //if not AbsolutePos and (FLines > FEjectLines) and ((Row mod FLines) >= (FLines - FEjectLines)) then
                    //  Row := FLines * Trunc(Ceil((Row + 1) / FLines));
                    WriteText(printerdriver.LineFeed, true);
                    ++row;
                }
                col = 0;
                linefeed = true;
            }
            if (newcol != col)
            {
                if (newcol < col)
                {
                    if (printerdriver.AllowCR)
                        WriteText(printerdriver.LineFeed, true);
                    col = 0;
                }
                else if (newcol > col)
                {
                    WriteText("".PadLeft(col - newcol), true);
                    col = newcol;
                }               
            }
        }
        public void DevOut(string s, bool wordwrap)
        {
            if (s.Length == 0)
                return;
            if (printerdriver.HasPlainText)
                s = printerdriver.ManagePlainText(s);
            string p;
            int i = 0;
            var lines = s.Split(CHR_LF);
            foreach (var line in lines)
            {          
                p = line;
                while (p.Length > 0)
                {
                    if (i > 0)
                        DevPos(row + 1, 0);
                    i = columnsscaled;
                    if ((i <= 0) || (i > p.Length))
                        i = p.Length;
                    WriteText(p.Substring(0, i), false);
                    p = p.Remove(0, i);
                    col += i;
                }
            }
        }
        public void NewLine(string s)
        {
            if (linefeed)
                DevPos(row + 1, 0);
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
                int l = columnsscaled - (b.Length + 1);
                if (l >= a.Length)
                    s = a.PadRight(l, padchar) + " " + b;
                else
                    s = a.Substring(0, l) + " " + b;
            }
            DevOut(s, true);
            linefeed = true;
        }
        public void SingleSep()
        {
            if (lastsep == row)
                return;
            string s = "";
            for (int i = 1; i <= columnsscaled; i++)
            {
                s += printerdriver.SepSingle;
            }
            NewLine(s);
            lastsep = row;           
        }
        public void DoubleSep()
        {
            if (lastsep == row)
                return;
            string s = "";
            for (int i = 1; i <= columnsscaled; i++)
            {
                s += printerdriver.SepDouble;
            }
            NewLine(s);
            lastsep = row;
        }
        public void UnderlineSep()
        {
            if (lastsep == row)
                return;
            string s = "";
            for (int i = 1; i <= columnsscaled; i++)
            {
                s += printerdriver.Underline;
            }
            NewLine(s);
            lastsep = row;
        }
        public void Eject()
        {
            if (printerdriver.EjectLines > 0)
                DevPos(row + printerdriver.EjectLines, 0);
        }
        public string TextPrinted
        {
            get
            {
                return text.ToString();
            }
        }
        public string PadCenter(string s, int count = -1, char padingchar = ' ')
        {
            if (count == -1)
                count = columnsscaled;
            int spaces = count - s.Length;
            int padleft = (spaces / 2) + s.Length;
            return s.PadLeft(padleft, padingchar).PadRight(count, padingchar);
        }
        public void Flush(string documentname, int copies)
        {
            Eject();
            Cut();
            while (--copies >= 0)
            {
                RawPrinterHelper.SendStringToPrinter(printerdriver.DeviceName, documentname, TextPrinted);
            }
            FinalizeDoc();
        }
        public void Cut()
        {
            if (printerdriver.Cortar)
                NewLine(printerdriver.Cut);
        }

        public void Guilhotina()
        {
            NewLine(printerdriver.Cut);
        }

        private string Chr(int asc)
        {
            string ret = "";
            ret += (char)asc;
            return ret;
        }

        public string NegritoOn
        {
            get
            {
                return Chr(27) + Chr(69);
            }
        }

        public string NegritoOff
        {
            get
            {
                return Chr(27) + Chr(70);
            }
        }

        /// <summary>
        /// Configura a impressora para impressão normal.
        /// </summary>
        public string Normal
        {
            get
            {
                return Chr(18);
            }
        }

        /// <summary>
        /// Configura a impressora para impressão em modo condensado.
        /// </summary>
        public string Comprimido
        {
            get
            {
                return Chr(15);
            }
        }

        /// <summary>
        /// Configura a impressora para impressão em modo expandido.
        /// </summary>
        public string Expandido
        {
            get
            {
                return Chr(14);
            }
        }

        /// <summary>
        /// Configura a impressora para impressão em modo expandido normal.
        /// </summary>
        public string ExpandidoNormal
        {
            get
            {
                return Chr(20);
            }
        }

        public string EjetaPagina()
        {
            return Chr(12);
        }

        public string ImpCol(int nCol, string sLinha)
        {
            string Cols = "";
            Cols = Cols.PadLeft(nCol, ' ');
            return Chr(13) + Cols + sLinha;
        }

        public string EspacoAEsquerda(string texto, int tamanho)
        {
            return texto.PadLeft(tamanho);
        }

        public string Texto(string s)
        {
            if (linefeed)
                DevPos(row + 1, 0);
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
                int l = columnsscaled - (b.Length + 1);
                if (l >= a.Length)
                    s = a.PadRight(l, padchar) + " " + b;
                else
                    s = a.Substring(0, l) + " " + b;
            }

            return s;
        }

        public string LinhaHorizontal()
        {
            if (lastsep == row)
                return "";
            string s = "";
            for (int i = 1; i <= columnsscaled; i++)
            {
                s += printerdriver.SepSingle;
            }
            return Texto(s);
        }        

        public void AbrirGaveta()
        {
            try
            {
                byte[] comandos = null;
                switch (printerdriver.impressoraModelo)
                {
                    case (byte)ImpressoraModelo.EPSON:
                        comandos = new byte[5] { 0x1B, 0x70, 0x00, 0x0A, 0x64 };
                        break;
                    case (byte)ImpressoraModelo.BEMATECH:
                        comandos = new byte[5] { 0x1B, 0x70, 0x00, 0x0A, 0x64 };
                        break;
                    case (byte)ImpressoraModelo.DARUMA:
                        comandos = new byte[2] { 0x1B, 0x70 };
                        break;
                    case (byte)ImpressoraModelo.ELGIN:
                        comandos = new byte[5] { 0x1B, 0x70, 0x00, 0x0A, 0x64 };
                        break;
                    default:
                        break;
                }

                IntPtr unmanagedPointer = Marshal.AllocHGlobal(comandos.Length);
                Marshal.Copy(comandos, 0, unmanagedPointer, comandos.Length);
                Int32 dwCount = comandos.Length;

                RawPrinterHelper.SendBytesToPrinter(printerdriver.DeviceName, "", unmanagedPointer, dwCount);
                Marshal.FreeCoTaskMem(unmanagedPointer);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}