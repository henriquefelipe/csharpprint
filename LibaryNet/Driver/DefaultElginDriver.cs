using LibaryNet.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryNet.Driver
{
    public class DefaultElginDriver : PrinterDriverBase
    {
        public DefaultElginDriver(string deviceName) : base(deviceName)
        {
        }

        protected override byte[] ComandoAberturaGavetaDriver()
        {
            return new byte[5] { 0x1B, 0x70, 0x00, 0x0A, 0x64 };
        }

        protected override void DriveLoad()
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
            impressoraModelo = (byte)ImpressoraModelo.ELGIN;
            drivername = "Default Elgin Driver";
        }
    }
}
