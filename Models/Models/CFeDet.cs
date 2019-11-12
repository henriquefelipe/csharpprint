using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class CFeDet
    {
        public CFeDet()
        {
            Prod = new CFeDetProd();
            Imposto = new CFeDetImposto();
        }

        public int NItem { get; set; }
        public CFeDetProd Prod { get; set; }
        public CFeDetImposto Imposto { get; set; }
        public string InfAdProd { get; set; }
    }
}
