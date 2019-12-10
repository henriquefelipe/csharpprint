using LibaryNet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryNet.Driver
{
    public class DefaultBematechDriver : PrinterDriverBase
    {
        public DefaultBematechDriver(string deviceName) 
            : base(deviceName)
        {
        }

        protected override byte[] ComandoAberturaGavetaDriver()
        {
            return new byte[5] { 0x1B, 0x70, 0x00, 0x0A, 0x64 };
        }

        protected override void DriveLoad()
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
            impressoraModelo = (byte)ImpressoraModelo.BEMATECH;
            drivername = "Default Bematech Driver";
        }
    }
}
