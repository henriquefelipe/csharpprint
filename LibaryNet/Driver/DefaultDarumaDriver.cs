using LibaryNet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryNet.Driver
{
    public class DefaultDarumaDriver : PrinterDriverBase
    {
        public DefaultDarumaDriver(string deviceName)
           : base(deviceName)
        {
        }

        protected override byte[] ComandoAberturaGavetaDriver()
        {
            return new byte[2] { 0x1B, 0x70 };
        }

        protected override void DriveLoad()
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
            impressoraModelo = (byte)ImpressoraModelo.DARUMA;
            drivername = "Default Daruma Driver";
        }
    }
}
