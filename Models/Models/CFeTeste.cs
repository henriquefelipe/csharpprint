using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class CFeTeste
    {
        public CFe CFe()
        {
            var cfe = new CFe();
            cfe.InfCFe.Ide.CNPJ = "";           
            cfe.InfCFe.Ide.NumeroCaixa = 1;
            cfe.InfCFe.Ide.SignAC = "";
            cfe.InfCFe.Ide.DhEmissao = DateTime.Now;
            cfe.InfCFe.Ide.AssinaturaQrcode = "9999 9999 9999 9999 9999 9999 9999 9999 9999 9999 9999";
            cfe.InfCFe.Ide.NCFe = 1;
            cfe.InfCFe.Ide.NSerie = 1;

            cfe.InfCFe.Emit.XFant = "NOME FANTASIA";
            cfe.InfCFe.Emit.CNPJ = "00.000.000/0000-00";
            cfe.InfCFe.Emit.IM = "";
            cfe.InfCFe.Emit.IE = "000.000.000.00";
            cfe.InfCFe.Emit.EnderFoneEmit = "Rua Afonso Arinos, 1277 - Centro - Fone(00) 1234-5678";

            cfe.InfCFe.Dest.CPF = "000.000.000-00";
            cfe.InfCFe.Dest.Nome = "CONSUMIDOR";
            cfe.InfCFe.Entrega.XLgr = "Rua 3";
            cfe.InfCFe.Entrega.Nro = "112233";
            cfe.InfCFe.Entrega.XCpl = "A";
            cfe.InfCFe.Entrega.XBairro = "Industrial";
            cfe.InfCFe.Entrega.XMun = "Maracanau";
            cfe.InfCFe.Entrega.UF = "CE";

            for (var i = 0; i < 3; i++)
            {
                var det = new CFeDet();
                det.NItem = 1 + i;
                det.Prod.CProd = det.NItem.ToString();
                det.Prod.CEAN = "6291041500213";
                det.Prod.XProd = $"Teste {det.NItem}";
                det.Prod.NCM = "99";
                det.Prod.CFOP = "5120";
                det.Prod.UCom = "UN";
                det.Prod.QCom = 1;
                det.Prod.VUnCom = 120.00M;                
                det.Prod.VDesc = i != 1 ? 1 : 0;                

                var totalItem = det.Prod.QCom * det.Prod.VUnCom;
                det.Prod.VItem = totalItem;
                det.Imposto.VItem12741 = totalItem * 0.12M;

                det.Imposto.Icms = 18;
                det.Imposto.Pis = 0.0065M;               
                det.Imposto.Cofins = 0.0065M;
                det.InfAdProd = "Informacoes adicionais";

                cfe.InfCFe.Det.Add(det);
            }

            cfe.InfCFe.Total.DescAcrEntr  = 2;
            cfe.InfCFe.Total.VCFeLei12741 = 1.23M;

            var pgto1 = new CFePgtoMp();
            pgto1.Descricao = "Cartao de Credito";
            pgto1.VMp = 10;
            cfe.InfCFe.Pagto.Pagamentos.Add(pgto1);

            var pgto2 = new CFePgtoMp();
            pgto2.Descricao = "Dinheiro";
            pgto2.VMp = 10;
            cfe.InfCFe.Pagto.Pagamentos.Add(pgto2);

            return cfe;
        }        
    }
}
