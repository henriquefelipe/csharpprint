using LibaryNet.Driver;
using LibaryNet.Enum;
using LibaryNet.PrinterServiceFunctions;
using CSharpPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPrint
{
    public class PrinterService
    {
        internal const char CHR_LF = (char)10;

        internal StringBuilder text = new StringBuilder();
        internal bool started;
        internal bool linefeed = false;
        internal int lastsep = -1;
        internal int row = 0;
        internal int col = 0;
        internal bool compressset = false;
        internal bool italicset = false;
        internal bool boldset = false;
        internal ExpandType expandset = ExpandType.None;
        internal int columnsscaled = 0;
        internal Output output = Output.File;
        internal PrinterDriverBase printerdriver;

        public PrinterService(PrinterDriverBase driver)
        {
            Clear();
            printerdriver = driver;
            InitializeDoc();
        }

        public void Clear()
        {
            text.Clear();
            row = 0;
            col = 0;
        }

        internal void InitializeDoc()
        {
            PrinterServiceFunction function = new InitializeDoc(this);
            function.RunFunction();
        }

        internal void FinalizeDoc()
        {
            started = false;
            text.Clear();
        }

        public void WriteText(string s, bool controlchar = true)
        {
            PrinterServiceFunction function = new WriteText(this);
            function.AddParameter("string", s);
            function.RunFunction();
        }

        public void Bold(bool boldtype)
        {
            PrinterServiceFunction function = new Bold(this);
            function.AddParameter("boldType", boldtype);
            function.RunFunction();
        }

        public void Italic(bool italictype)
        {
            PrinterServiceFunction function = new Italic(this);
            function.AddParameter("italicType", italictype);
            function.RunFunction();
        }

        public void Expand(ExpandType expandtype)
        {
            PrinterServiceFunction function = new Expand(this);
            function.AddParameter("expandType", expandtype);
            function.RunFunction();
        }

        internal void DevPos(int newrow, int newcol)
        {
            PrinterServiceFunction f = new DevPOS(this);
            f.AddParameter("newRow", newrow);
            f.AddParameter("newCol", newcol);
            f.RunFunction();
        }

        public void DevOut(string s, bool wordwrap)
        {
            PrinterServiceFunction f = new DevOut(this);
            f.AddParameter("string", s);
            f.AddParameter("wordWrap", wordwrap);
            f.RunFunction();

        }
        public void NewLine(string s)
        {
            PrinterServiceFunction f = new NewLine(this);
            f.AddParameter("string", s);
            f.RunFunction();
        }

        public void SingleSep()
        {
            PrinterServiceFunction f = new SingleSep(this);
            f.RunFunction();
        }

        public void DoubleSep()
        {
            PrinterServiceFunction f = new DoubleSep(this);
            f.RunFunction();
        }

        public void UnderlineSep()
        {
            PrinterServiceFunction f = new UnderlineSep(this);
            f.RunFunction();
        }

        public void Eject()
        {
            PrinterServiceFunction f = new Eject(this);
            f.RunFunction();
        }
        
        public string PadCenter(string s, int count = -1, char padingchar = ' ')
        {
            PrinterServiceFunction f = new PadCenter(this);
            f.AddParameter("string", s);
            f.AddParameter("count", count);
            f.AddParameter("paddingChar", padingchar);
            f.RunFunction();
            return f.FunctionResult.ToString();
        }

        public void Flush(string documentname, int copies)
        {
            PrinterServiceFunction f = new Flush(this);
            f.AddParameter("documentName", documentname);
            f.AddParameter("copies", copies);
            f.RunFunction();
        }

        public void Cut()
        {
            PrinterServiceFunction f = new Cut(this);
            f.RunFunction();
        }

        public void Guilhotina()
        {
            PrinterServiceFunction f = new Guilhotina(this);
            f.RunFunction();
        }

        internal string Chr(int asc)
        {
            string ret = "";
            ret += (char)asc;
            return ret;
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
            PrinterServiceFunction f = new Texto(this);
            f.AddParameter("string", s);
            f.RunFunction();
            return f.FunctionResult.ToString();
        }

        public string LinhaHorizontal()
        {
            PrinterServiceFunction f = new LinhaHorizontal(this);
            f.RunFunction();
            return f.FunctionResult.ToString();
        }

        public void AbrirGaveta()
        {
            printerdriver.AbrirGaveta();
        }
    }
}