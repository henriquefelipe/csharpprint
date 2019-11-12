using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Library.Enumerador
{
    public enum TipoAmbiente
    {

        [Description("Homologação")]
        Homologacao = 0,

        [Description("Produção")]
        Producao = 1
    }
}
