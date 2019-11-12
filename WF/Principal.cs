using Library.Enumerador;
using Library.Models;
using LibraryNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.Print;

namespace WF
{
    public partial class Principal : Form
    {
        private bool _conectado { get; set; }
        private PrinterHelper _printerHelper;
        private CFe _cfe;

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {            
            cboModelo.DisplayMember = "Name";
            cboModelo.ValueMember = "Name";
            cboModelo.DataSource = GenericEnum<ImpressoraModelo>.EnumList();
            cboModelo.SelectedIndex = -1;

            txtCaminho.Text = getCaminho();
            cboModelo.SelectedValue = getModelo();
            txtColunas.Text = getColunas();

            _cfe = new CFeTeste().CFe();
        }

        private void BtnConectar_Click(object sender, EventArgs e)
        {
            if (_conectado)
            {
                _conectado = false;
                btnConectar.Text = "Conectar";
            }
            else
            {
                if (string.IsNullOrEmpty(txtCaminho.Text))
                {
                    MessageBox.Show("Digite o caminho ou nome da impressora!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(cboModelo.Text))
                {
                    MessageBox.Show("Selecione o modelo da impressora!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtColunas.Text))
                {
                    MessageBox.Show("Digite a quantidade de colunas da impressora!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _printerHelper = new PrinterHelper(string.Format("{0};DRIVER={1};COLUNAS={2}", txtCaminho.Text, cboModelo.Text, txtColunas.Text));
                _conectado = true;
                btnConectar.Text = "Desconectar";

                setCaminho(txtCaminho.Text);
                setModelo(cboModelo.Text);
                setColunas(txtColunas.Text);
            }
        }

        private void BtnTeste_Click(object sender, EventArgs e)
        {
            if(!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(" Teste de Impressora"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.Flush("", 1);            
        }

        private void BtnNFCe_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.XFant));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("CNPJ {0} I.E. {1}", _cfe.InfCFe.Emit.CNPJ, _cfe.InfCFe.Emit.IE)));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.EnderFoneEmit));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("DANFE NFC-e - Documento Auxiliar"));
            _printerHelper.NewLine(_printerHelper.PadCenter("da Nota Fiscal Eletrônica para Consumidor Final"));
            _printerHelper.NewLine(_printerHelper.PadCenter("Não permite aproveitamento de crédito de ICMS"));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter("DETALHE DA VENDA"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine("CODIGO DESCRICAO" + "»" + "QTD UN   VLUNT   VLTOT");
            _printerHelper.SingleSep();
            foreach (var item in _cfe.InfCFe.Det)
            {
                _printerHelper.NewLine(string.Format("{0} {1} » {2} {3}  {4} {5}", item.Prod.CProd, item.Prod.XProd, item.Prod.QCom, item.Prod.UCom, item.Prod.VUnCom.ToString("N"), item.Prod.VItem.ToString("N")));
            }
            //_printerHelper.NewLine("222222 MANGUEIRA BORRACHA" + "»" + "01 PC   42,50   42,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("QTD. TOTAL DE ITENS" + "»" + _cfe.InfCFe.Det.Count);
            _printerHelper.NewLine("VALOR DOS PRODUTOS" + "»" + "112,50");
            _printerHelper.NewLine("DESCONTO" + "»" + "---");
            _printerHelper.NewLine("OUTRAS DESPESAS" + "»" + "---");
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine("VALOR TOTAL R$" + "»" + Convert.ToDecimal(112.50).ToString("N"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine("FORMAS DE PAGAMENTO" + "»" + "VALOR PAGO");
            _printerHelper.NewLine("Dinheiro" + "»" + "115,00");
            _printerHelper.NewLine("Troco" + "»" + "2,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSULTA PELA CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter("www.sefaz.gov.br/xxxx.aspx"));
            _printerHelper.NewLine(_printerHelper.PadCenter("CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Ide.AssinaturaQrcode));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSUMIDOR"));
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("NOME: {0}", _cfe.InfCFe.Dest.Nome)));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("N {0} Série {1}", _cfe.InfCFe.Ide.NCFe, _cfe.InfCFe.Ide.NSerie)));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("02/01/2019  Via Consumidor"));
            _printerHelper.NewLine(_printerHelper.PadCenter("PROTOCOLO DE AUTORIZAÇÃO"));
            _printerHelper.NewLine(_printerHelper.PadCenter("000000000 - 02/01/2019 - 11h09min"));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("Consulta via leitor de QR Code"));
            _printerHelper.NewLine(_printerHelper.PadCenter(""));
            
            _printerHelper.NewLine(QrCode());
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("AREA DE MENSAGEM DE INTERESSE DO CONTRIBUINTE"));
            _printerHelper.NewLine("Inf. dos Tributos Totais Incidentes (Lei Federal 12.741/2012)" + "»" + "3,85");
            _printerHelper.Flush("", 1);
        }

        public string QrCode()
        {
            string ESC = Convert.ToString((char)27);
            string GS = Convert.ToString((char)29);
            string center = ESC + "a" + (char)1; //align center
            string left = ESC + "a" + (char)0; //align left
            string right = ESC + "a" + (char)2; //align right
            string bold_on = ESC + "E" + (char)1; //turn on bold mode
            string bold_off = ESC + "E" + (char)0; //turn off bold mode
            string cut = ESC + "d" + (char)1 + GS + "V" + (char)66;

            string initp = ESC + (char)64;
            string buffer = "";
            string QrData = @"www.izzyway.com.br";
            buffer += center;

            Encoding m_encoding = Encoding.GetEncoding("iso-8859-1"); //set encoding for QRCode
            int store_len = (QrData).Length + 3; //3
            byte store_pL = (byte)(store_len % 256);
            byte store_pH = (byte)(store_len / 256);

            buffer += m_encoding.GetString(new byte[] { 29, 40, 107, 4, 0, 49, 65, 50, 0 });
            buffer += m_encoding.GetString(new byte[] { 29, 40, 107, 3, 0, 49, 67, 8 });
            buffer += m_encoding.GetString(new byte[] { 29, 40, 107, 3, 0, 49, 69, 48 });
            buffer += m_encoding.GetString(new byte[] { 29, 40, 107, store_pL, store_pH, 49, 80, 48 });
            buffer += QrData;
            buffer += m_encoding.GetString(new byte[] { 29, 40, 107, 3, 0, 49, 81, 48 });

            //buffer += initp;

            return buffer;
        }

        public string CodeBar()
        {
            StringBuilder print = new StringBuilder();
            var barcode = "1234567890";
            char commandGS = '\x1D';
            char linefeed = '\x0A';
            char esc = '\x1B';
            char commandFontSize = '\x21';
            char commandk = '\x6B';
            char code128 = '\x69';
            print.Append(commandGS);
            print.Append(commandk);
            print.Append(code128);
            print.Append(barcode.Length);
            print.Append(barcode);
            return print.ToString();
        }

        private void BtnMFe_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.XFant));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("CNPJ {0} I.E. {1}", _cfe.InfCFe.Emit.CNPJ, _cfe.InfCFe.Emit.IE)));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.EnderFoneEmit));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("Extrato 004243"));
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter("CUPOM FISCAL ELETRONICO - SAT"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("Detalhe Venda"));
            _printerHelper.SingleSep();
            _printerHelper.NewLine("# COD  DESC    QTD" + "»" + "UN   VL UNT R$   VL TOT R$");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("001 07892844004466 BOB. TERM. 80X40  1");
            _printerHelper.NewLine("     1UN x 12,90 T18,00%" + "»" + "12,90");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("Total bruto de itens" + "»" + "12,90");
            _printerHelper.NewLine("DESCONTO" + "»" + "---");
            _printerHelper.NewLine("OUTRAS DESPESAS" + "»" + "---");
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine("TOTAL R$" + "»" + Convert.ToDecimal(12.20).ToString("N"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine("FORMAS DE PAGAMENTO" + "»" + "VALOR PAGO");
            _printerHelper.NewLine("Dinheiro" + "»" + "15,00");
            _printerHelper.NewLine("Troco" + "»" + "2,10");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("Informacao dos Tributos Totais");
            _printerHelper.NewLine("Incidentes (Lei Federal 12.741/2012 R$" + "»" + "4,40");
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("SAT No 230.024.129"));
            _printerHelper.NewLine(_printerHelper.PadCenter("12/06/2019 - 17:49:35"));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Ide.AssinaturaQrcode));
            _printerHelper.NewLine(CodeBar());
            _printerHelper.NewLine(QrCode());
            
            _printerHelper.NewLine("");
            _printerHelper.Flush("", 1);
        }

        private void BtnSAT_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.XFant));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("CNPJ {0} I.E. {1}", _cfe.InfCFe.Emit.CNPJ, _cfe.InfCFe.Emit.IE)));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.EnderFoneEmit));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("Extrato No. 876578"));
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter("CUPOM FISCAL ELETRÔNICO - SAT"));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine("CPF/CNPJ do Consumidor: 222 222 222-99");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("#|COD|DESC|QTD|UN|VL UNIT R$|ST|ALIQ|VL ITEM R$");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("111111 TORNEIRA PLASTICA" + "»" + "02 PC   35,00   70,00");
            _printerHelper.NewLine("222222 MANGUEIRA BORRACHA" + "»" + "01 PC   42,50   42,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("QTD. TOTAL DE ITENS" + "»" + "2");
            _printerHelper.NewLine("VALOR DOS PRODUTOS" + "»" + "112,50");
            _printerHelper.NewLine("DESCONTO" + "»" + "---");
            _printerHelper.NewLine("OUTRAS DESPESAS" + "»" + "---");
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine("VALOR TOTAL R$" + "»" + Convert.ToDecimal(112.50).ToString("N"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine("FORMAS DE PAGAMENTO" + "»" + "VALOR PAGO");
            _printerHelper.NewLine("Dinheiro" + "»" + "115,00");
            _printerHelper.NewLine("Troco" + "»" + "2,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSULTA PELA CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter("www.sefaz.gov.br/xxxx.aspx"));
            _printerHelper.NewLine(_printerHelper.PadCenter("CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Ide.AssinaturaQrcode));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSUMIDOR"));
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("NOME: NÃO IDENTIFICADO", _cfe.InfCFe.Dest.Nome)));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("N {0} Série {1}", _cfe.InfCFe.Ide.NCFe, _cfe.InfCFe.Ide.NSerie)));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("02/01/2019  Via Consumidor"));
            //_printerHelperer.Expand(PrinterDriver.ExpandType.Width);
            //_printerHelperer.NewLine(printerhelper.PadCenter("EMITIDA EM CONTIGÊNCIA"));
            //_printerHelperer.Expand(PrinterDriver.ExpandType.None);
            //_printerHelperer.NewLine(printerhelper.PadCenter("Pendente de autorização"));
            _printerHelper.NewLine(_printerHelper.PadCenter(""));
            
            _printerHelper.NewLine(CodeBar());
            _printerHelper.NewLine(QrCode());
            _printerHelper.NewLine("Inf. dos Tributos Totais Incidentes (Lei Federal 12.741/2012)" + "»" + "3,85");
            _printerHelper.NewLine("");
            _printerHelper.Flush("", 1);
        }

        private void BtnNFCeContingencia_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.XFant));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("CNPJ {0} I.E. {1}", _cfe.InfCFe.Emit.CNPJ, _cfe.InfCFe.Emit.IE)));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Emit.EnderFoneEmit));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("DANFE NFC-e - Documento Auxiliar"));
            _printerHelper.NewLine(_printerHelper.PadCenter("da Nota Fiscal Eletrônica para Consumidor Final"));
            _printerHelper.NewLine(_printerHelper.PadCenter("Não permite aproveitamento de crédito de ICMS"));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter("EMITIDA EM CONTIGÊNCIA"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("Pendente de autorização"));
            _printerHelper.SingleSep();
            _printerHelper.NewLine("CODIGO DESCRICAO" + "»" + "QTD UN   VLUNT   VLTOT");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("111111 TORNEIRA PLASTICA" + "»" + "02 PC   35,00   70,00");
            _printerHelper.NewLine("222222 MANGUEIRA BORRACHA" + "»" + "01 PC   42,50   42,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine("QTD. TOTAL DE ITENS" + "»" + "2");
            _printerHelper.NewLine("VALOR DOS PRODUTOS" + "»" + "112,50");
            _printerHelper.NewLine("DESCONTO" + "»" + "---");
            _printerHelper.NewLine("OUTRAS DESPESAS" + "»" + "---");
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine("VALOR TOTAL R$" + "»" + Convert.ToDecimal(112.50).ToString("N"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.SingleSep();
            _printerHelper.NewLine("FORMAS DE PAGAMENTO" + "»" + "VALOR PAGO");
            _printerHelper.NewLine("Dinheiro" + "»" + "115,00");
            _printerHelper.NewLine("Troco" + "»" + "2,50");
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSULTA PELA CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter("www.sefaz.gov.br/xxxx.aspx"));
            _printerHelper.NewLine(_printerHelper.PadCenter("CHAVE DE ACESSO"));
            _printerHelper.NewLine(_printerHelper.PadCenter(_cfe.InfCFe.Ide.AssinaturaQrcode));
            _printerHelper.SingleSep();
            _printerHelper.NewLine(_printerHelper.PadCenter("CONSUMIDOR"));
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("NOME: {0}", _cfe.InfCFe.Dest.Nome)));
            _printerHelper.SingleSep();
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter(string.Format("N {0} Série {1}", _cfe.InfCFe.Ide.NCFe, _cfe.InfCFe.Ide.NSerie)));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("02/01/2019  Via Consumidor"));
            _printerHelper.Expand(PrinterDriver.ExpandType.Width);
            _printerHelper.NewLine(_printerHelper.PadCenter("EMITIDA EM CONTIGÊNCIA"));
            _printerHelper.Expand(PrinterDriver.ExpandType.None);
            _printerHelper.NewLine(_printerHelper.PadCenter("Pendente de autorização"));
            _printerHelper.NewLine(_printerHelper.PadCenter(""));
            _printerHelper.NewLine(QrCode());
            _printerHelper.NewLine("Inf. dos Tributos Totais Incidentes (Lei Federal 12.741/2012)" + "»" + "3,85");
            _printerHelper.NewLine("");
            _printerHelper.Flush("", 1);
        }
        

        #region Configuração        
        public void setCaminho(string nome)
        {
            SaveSettings("Caminho", nome);
        }

        public string getCaminho()
        {
            return ConfigurationManager.AppSettings["Caminho"] != null ?
                ConfigurationManager.AppSettings["Caminho"].ToString() : "";
        }

        public void setModelo(string nome)
        {
            SaveSettings("Modelo", nome);
        }

        public string getModelo()
        {
            return ConfigurationManager.AppSettings["Modelo"] != null ?
                ConfigurationManager.AppSettings["Modelo"].ToString() : "";
        }

        public void setColunas(string nome)
        {
            SaveSettings("Colunas", nome);
        }

        public string getColunas()
        {
            return ConfigurationManager.AppSettings["Colunas"] != null ?
                ConfigurationManager.AppSettings["Colunas"].ToString() : "";
        }

        public void SaveSettings(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            var settings = config.AppSettings.Settings;
            if (settings[key] != null)
                settings[key].Value = value;
            else
                settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }

        #endregion

        private void BtnGaveta_Click(object sender, EventArgs e)
        {
            if (!_conectado)
            {
                MessageBox.Show("Configure a impressora e conecte!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _printerHelper.AbrirGaveta();
        }
    }
}
