namespace TesteNewFramework
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDriver = new System.Windows.Forms.ComboBox();
            this.txTexto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btEnviaTexto = new System.Windows.Forms.Button();
            this.btGuilhotina = new System.Windows.Forms.Button();
            this.btGaveta = new System.Windows.Forms.Button();
            this.cbDispositivos = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dispositivo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Driver";
            // 
            // cbDriver
            // 
            this.cbDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDriver.FormattingEnabled = true;
            this.cbDriver.Location = new System.Drawing.Point(159, 38);
            this.cbDriver.Name = "cbDriver";
            this.cbDriver.Size = new System.Drawing.Size(381, 21);
            this.cbDriver.TabIndex = 3;
            // 
            // txTexto
            // 
            this.txTexto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txTexto.Location = new System.Drawing.Point(15, 94);
            this.txTexto.Multiline = true;
            this.txTexto.Name = "txTexto";
            this.txTexto.Size = new System.Drawing.Size(525, 41);
            this.txTexto.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Texto";
            // 
            // btEnviaTexto
            // 
            this.btEnviaTexto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btEnviaTexto.Location = new System.Drawing.Point(15, 154);
            this.btEnviaTexto.Name = "btEnviaTexto";
            this.btEnviaTexto.Size = new System.Drawing.Size(525, 23);
            this.btEnviaTexto.TabIndex = 6;
            this.btEnviaTexto.Text = "ENVIAR TEXTO";
            this.btEnviaTexto.UseVisualStyleBackColor = true;
            this.btEnviaTexto.Click += new System.EventHandler(this.btEnviaTexto_Click);
            // 
            // btGuilhotina
            // 
            this.btGuilhotina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btGuilhotina.Location = new System.Drawing.Point(15, 183);
            this.btGuilhotina.Name = "btGuilhotina";
            this.btGuilhotina.Size = new System.Drawing.Size(525, 23);
            this.btGuilhotina.TabIndex = 7;
            this.btGuilhotina.Text = "ACIONAR GUILHOTINA";
            this.btGuilhotina.UseVisualStyleBackColor = true;
            // 
            // btGaveta
            // 
            this.btGaveta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btGaveta.Location = new System.Drawing.Point(15, 212);
            this.btGaveta.Name = "btGaveta";
            this.btGaveta.Size = new System.Drawing.Size(525, 23);
            this.btGaveta.TabIndex = 8;
            this.btGaveta.Text = "ABRIR GAVETA";
            this.btGaveta.UseVisualStyleBackColor = true;
            // 
            // cbDispositivos
            // 
            this.cbDispositivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDispositivos.FormattingEnabled = true;
            this.cbDispositivos.Location = new System.Drawing.Point(159, 12);
            this.cbDispositivos.Name = "cbDispositivos";
            this.cbDispositivos.Size = new System.Drawing.Size(381, 21);
            this.cbDispositivos.TabIndex = 9;
            this.cbDispositivos.SelectedIndexChanged += new System.EventHandler(this.cbDispositivos_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 251);
            this.Controls.Add(this.cbDispositivos);
            this.Controls.Add(this.btGaveta);
            this.Controls.Add(this.btGuilhotina);
            this.Controls.Add(this.btEnviaTexto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txTexto);
            this.Controls.Add(this.cbDriver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharpPrint - Teste Framework (Novo)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDriver;
        private System.Windows.Forms.TextBox txTexto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btEnviaTexto;
        private System.Windows.Forms.Button btGuilhotina;
        private System.Windows.Forms.Button btGaveta;
        private System.Windows.Forms.ComboBox cbDispositivos;
    }
}

