namespace AppEncrypter
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTextoOriginal = new System.Windows.Forms.TextBox();
            this.lblTextoOriginal = new System.Windows.Forms.Label();
            this.lblTextoResultado = new System.Windows.Forms.Label();
            this.txtTextoResultado = new System.Windows.Forms.TextBox();
            this.btnCifrar = new System.Windows.Forms.Button();
            this.btnDesencriptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTextoOriginal
            // 
            this.txtTextoOriginal.Location = new System.Drawing.Point(137, 22);
            this.txtTextoOriginal.Name = "txtTextoOriginal";
            this.txtTextoOriginal.Size = new System.Drawing.Size(376, 20);
            this.txtTextoOriginal.TabIndex = 0;
            // 
            // lblTextoOriginal
            // 
            this.lblTextoOriginal.AutoSize = true;
            this.lblTextoOriginal.Location = new System.Drawing.Point(12, 29);
            this.lblTextoOriginal.Name = "lblTextoOriginal";
            this.lblTextoOriginal.Size = new System.Drawing.Size(72, 13);
            this.lblTextoOriginal.TabIndex = 1;
            this.lblTextoOriginal.Text = "Texto Original";
            // 
            // lblTextoResultado
            // 
            this.lblTextoResultado.AutoSize = true;
            this.lblTextoResultado.Location = new System.Drawing.Point(12, 55);
            this.lblTextoResultado.Name = "lblTextoResultado";
            this.lblTextoResultado.Size = new System.Drawing.Size(85, 13);
            this.lblTextoResultado.TabIndex = 2;
            this.lblTextoResultado.Text = "Texto Resultado";
            // 
            // txtTextoResultado
            // 
            this.txtTextoResultado.Location = new System.Drawing.Point(137, 48);
            this.txtTextoResultado.Name = "txtTextoResultado";
            this.txtTextoResultado.Size = new System.Drawing.Size(376, 20);
            this.txtTextoResultado.TabIndex = 3;
            // 
            // btnCifrar
            // 
            this.btnCifrar.Location = new System.Drawing.Point(137, 74);
            this.btnCifrar.Name = "btnCifrar";
            this.btnCifrar.Size = new System.Drawing.Size(185, 23);
            this.btnCifrar.TabIndex = 4;
            this.btnCifrar.Text = "Cifrar";
            this.btnCifrar.UseVisualStyleBackColor = true;
            this.btnCifrar.Click += new System.EventHandler(this.BtnCifrar_Click);
            // 
            // btnDesencriptar
            // 
            this.btnDesencriptar.Location = new System.Drawing.Point(328, 74);
            this.btnDesencriptar.Name = "btnDesencriptar";
            this.btnDesencriptar.Size = new System.Drawing.Size(185, 23);
            this.btnDesencriptar.TabIndex = 5;
            this.btnDesencriptar.Text = "Desencriptar";
            this.btnDesencriptar.UseVisualStyleBackColor = true;
            this.btnDesencriptar.Click += new System.EventHandler(this.BtnDesencriptar_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 107);
            this.Controls.Add(this.btnDesencriptar);
            this.Controls.Add(this.btnCifrar);
            this.Controls.Add(this.txtTextoResultado);
            this.Controls.Add(this.lblTextoResultado);
            this.Controls.Add(this.lblTextoOriginal);
            this.Controls.Add(this.txtTextoOriginal);
            this.Name = "frmMain";
            this.Text = "Encriptador de Textos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextoOriginal;
        private System.Windows.Forms.Label lblTextoOriginal;
        private System.Windows.Forms.Label lblTextoResultado;
        private System.Windows.Forms.TextBox txtTextoResultado;
        private System.Windows.Forms.Button btnCifrar;
        private System.Windows.Forms.Button btnDesencriptar;
    }
}

