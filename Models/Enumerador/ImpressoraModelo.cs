using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Library.Enumerador
{
    public enum ImpressoraModelo
    {

        [Description("TEXTO")]
        TEXTO = 0,

        [Description("EPSON")]
        EPSON = 1,

        [Description("BEMATECH")]
        BEMATECH = 2,

        [Description("DARUMA")]
        DARUMA = 3,

        [Description("ELGIN")]
        ELGIN = 4,

        [Description("DIEBOLD")]
        DIEBOLD = 5
    }
}
