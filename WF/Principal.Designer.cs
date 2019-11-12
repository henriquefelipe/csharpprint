namespace WF
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTeste = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColunas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboModelo = new System.Windows.Forms.ComboBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnMFe = new System.Windows.Forms.Button();
            this.btnSAT = new System.Windows.Forms.Button();
            this.btnNFCe = new System.Windows.Forms.Button();
            this.btnNFCeContingencia = new System.Windows.Forms.Button();
            this.btnGaveta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTeste
            // 
            this.btnTeste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTeste.Location = new System.Drawing.Point(27, 180);
            this.btnTeste.Name = "btnTeste";
            this.btnTeste.Size = new System.Drawing.Size(110, 39);
            this.btnTeste.TabIndex = 0;
            this.btnTeste.Text = "Teste";
            this.btnTeste.UseVisualStyleBackColor = true;
            this.btnTeste.Click += new System.EventHandler(this.BtnTeste_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Caminho";
            // 
            // txtCaminho
            // 
            this.txtCaminho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaminho.Location = new System.Drawing.Point(133, 23);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Size = new System.Drawing.Size(625, 26);
            this.txtCaminho.TabIndex = 2;
            this.txtCaminho.Text = "ELGIN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Colunas";
            // 
            // txtColunas
            // 
            this.txtColunas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColunas.Location = new System.Drawing.Point(594, 67);
            this.txtColunas.Name = "txtColunas";
            this.txtColunas.Size = new System.Drawing.Size(164, 26);
            this.txtColunas.TabIndex = 4;
            this.txtColunas.Text = "64";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Modelo";
            // 
            // cboModelo
            // 
            this.cboModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboModelo.FormattingEnabled = true;
            this.cboModelo.Location = new System.Drawing.Point(133, 67);
            this.cboModelo.Name = "cboModelo";
            this.cboModelo.Size = new System.Drawing.Size(351, 28);
            this.cboModelo.TabIndex = 6;
            // 
            // btnConectar
            // 
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.Location = new System.Drawing.Point(787, 31);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(135, 39);
            this.btnConectar.TabIndex = 7;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.BtnConectar_Click);
            // 
            // btnMFe
            // 
            this.btnMFe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMFe.Location = new System.Drawing.Point(182, 180);
            this.btnMFe.Name = "btnMFe";
            this.btnMFe.Size = new System.Drawing.Size(110, 39);
            this.btnMFe.TabIndex = 8;
            this.btnMFe.Text = "MFe";
            this.btnMFe.UseVisualStyleBackColor = true;
            this.btnMFe.Click += new System.EventHandler(this.BtnMFe_Click);
            // 
            // btnSAT
            // 
            this.btnSAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSAT.Location = new System.Drawing.Point(338, 180);
            this.btnSAT.Name = "btnSAT";
            this.btnSAT.Size = new System.Drawing.Size(110, 39);
            this.btnSAT.TabIndex = 9;
            this.btnSAT.Text = "SAT";
            this.btnSAT.UseVisualStyleBackColor = true;
            this.btnSAT.Click += new System.EventHandler(this.BtnSAT_Click);
            // 
            // btnNFCe
            // 
            this.btnNFCe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNFCe.Location = new System.Drawing.Point(488, 180);
            this.btnNFCe.Name = "btnNFCe";
            this.btnNFCe.Size = new System.Drawing.Size(110, 39);
            this.btnNFCe.TabIndex = 10;
            this.btnNFCe.Text = "NFC-e";
            this.btnNFCe.UseVisualStyleBackColor = true;
            this.btnNFCe.Click += new System.EventHandler(this.BtnNFCe_Click);
            // 
            // btnNFCeContingencia
            // 
            this.btnNFCeContingencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNFCeContingencia.Location = new System.Drawing.Point(631, 180);
            this.btnNFCeContingencia.Name = "btnNFCeContingencia";
            this.btnNFCeContingencia.Size = new System.Drawing.Size(227, 39);
            this.btnNFCeContingencia.TabIndex = 11;
            this.btnNFCeContingencia.Text = "NFC-e Contigência";
            this.btnNFCeContingencia.UseVisualStyleBackColor = true;
            this.btnNFCeContingencia.Click += new System.EventHandler(this.BtnNFCeContingencia_Click);
            // 
            // btnGaveta
            // 
            this.btnGaveta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGaveta.Location = new System.Drawing.Point(27, 255);
            this.btnGaveta.Name = "btnGaveta";
            this.btnGaveta.Size = new System.Drawing.Size(178, 39);
            this.btnGaveta.TabIndex = 12;
            this.btnGaveta.Text = "Abrir Gaveta";
            this.btnGaveta.UseVisualStyleBackColor = true;
            this.btnGaveta.Click += new System.EventHandler(this.BtnGaveta_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 497);
            this.Controls.Add(this.btnGaveta);
            this.Controls.Add(this.btnNFCeContingencia);
            this.Controls.Add(this.btnNFCe);
            this.Controls.Add(this.btnSAT);
            this.Controls.Add(this.btnMFe);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.cboModelo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtColunas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCaminho);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTeste);
            this.Name = "Principal";
            this.Text = "CSharp Print";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaminho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColunas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboModelo;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnMFe;
        private System.Windows.Forms.Button btnSAT;
        private System.Windows.Forms.Button btnNFCe;
        private System.Windows.Forms.Button btnNFCeContingencia;
        private System.Windows.Forms.Button btnGaveta;
    }
}

