using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Library.Enumerador
{
    public enum TipoAmbiente
    {

        [Description("Homologa��o")]
        Homologacao = 0,

        [Description("Produ��o")]
        Producao = 1
    }
}
