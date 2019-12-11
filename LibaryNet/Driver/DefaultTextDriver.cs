using LibaryNet.Enum;
using System;

namespace LibaryNet.Driver
{
    public class DefaultTextDriver : PrinterDriverBase
    {
        public DefaultTextDriver(string deviceName) : base(deviceName)
        {
        }

        protected override byte[] ComandoAberturaGavetaDriver()
        {
            throw new NotImplementedException("Impressora de texto não possui abertura de gaveta");
        }

        protected override void DriveLoad()
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
            columns = 48;
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
            impressoraModelo = (byte)ImpressoraModelo.TEXTO;
            drivername = "Default Text Driver";
        }
    }
}
