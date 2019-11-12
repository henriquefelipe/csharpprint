using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Library.Models
{
    public class InfCFe
    {
        public InfCFe()
        {
            Ide = new CFeIde();
            Emit = new CFeEmit();
            Dest = new CFeDest();
            Det = new Collection<CFeDet>();
            Entrega = new CFeEntrega();           
            Total = new CFeTotal();
            Pagto = new CFePgto();
            InfAdic = new CFeInfAdic();
        }

        public string Id { get; set; }           
        public decimal Versao { get; set; }     
        public decimal VersaoDadosEnt { get; set; }       
        public decimal VersaoSb { get; set; }      
        public CFeIde Ide { get; set; }      
        public CFeEmit Emit { get; set; }       
        public CFeDest Dest { get; set; }
        public Collection<CFeDet> Det { get; set; }
        public CFeEntrega Entrega { get; set; }                
        public CFeTotal Total { get; set; }        
        public CFePgto Pagto { get; set; }      
        public CFeInfAdic InfAdic { get; set; }
    }
}
