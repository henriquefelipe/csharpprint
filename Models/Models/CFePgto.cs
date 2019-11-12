using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library.Models
{
    public class CFePgto
    {
        public CFePgto()
        {
            Pagamentos = new Collection<CFePgtoMp>();
        }

        public Collection<CFePgtoMp> Pagamentos { get; set; }
       
        public decimal VTroco { get; set; }
    }
}
