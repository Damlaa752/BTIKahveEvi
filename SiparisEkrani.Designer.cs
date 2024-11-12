namespace BTIKahveEvi
{
    partial class SiparisEkrani
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
            this.lblToplamSiparis = new System.Windows.Forms.Label();
            this.lblToplamTutar = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblToplamSiparis
            // 
            this.lblToplamSiparis.AutoSize = true;
            this.lblToplamSiparis.Location = new System.Drawing.Point(38, 24);
            this.lblToplamSiparis.Name = "lblToplamSiparis";
            this.lblToplamSiparis.Size = new System.Drawing.Size(51, 20);
            this.lblToplamSiparis.TabIndex = 0;
            this.lblToplamSiparis.Text = "label1";
            // 
            // lblToplamTutar
            // 
            this.lblToplamTutar.AutoSize = true;
            this.lblToplamTutar.Location = new System.Drawing.Point(38, 80);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(51, 20);
            this.lblToplamTutar.TabIndex = 0;
            this.lblToplamTutar.Text = "label1";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(199, 143);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 50);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click_1);
            // 
            // SiparisEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 211);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblToplamTutar);
            this.Controls.Add(this.lblToplamSiparis);
            this.Name = "SiparisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SiparisEkrani";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblToplamSiparis;
        private System.Windows.Forms.Label lblToplamTutar;
        private System.Windows.Forms.Button btnOk;
    }
}