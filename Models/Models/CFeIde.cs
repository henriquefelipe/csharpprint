using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class CFeIde
    {       
       
        public int CNf { get; set; }
       
        public int Modelo { get; set; }
       
        public int NSerie { get; set; }
        
        public int NCFe { get; set; }

        public DateTime? DhEmissao { get; set; }

      
        public int CDv { get; set; }
        
        public byte TpAmb { get; set; }
        
        public string CNPJ { get; set; }
       
        public string SignAC { get; set; }
       
        public string AssinaturaQrcode { get; set; }
       
        public int NumeroCaixa { get; set; }
    }
}
