namespace VNSC
{
    partial class Form_Notice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Notice));
            this.lbNotice1 = new System.Windows.Forms.Label();
            this.lbNotice2 = new System.Windows.Forms.Label();
            this.pbHoanTat = new System.Windows.Forms.PictureBox();
            this.pbHuy = new System.Windows.Forms.PictureBox();
            this.pnNotice = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).BeginInit();
            this.pnNotice.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbNotice1
            // 
            this.lbNotice1.AutoSize = true;
            this.lbNotice1.BackColor = System.Drawing.Color.Transparent;
            this.lbNotice1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNotice1.ForeColor = System.Drawing.Color.Red;
            this.lbNotice1.Location = new System.Drawing.Point(83, 2);
            this.lbNotice1.Name = "lbNotice1";
            this.lbNotice1.Size = new System.Drawing.Size(66, 16);
            this.lbNotice1.TabIndex = 88;
            this.lbNotice1.Text = "lbNotice1";
            // 
            // lbNotice2
            // 
            this.lbNotice2.AutoSize = true;
            this.lbNotice2.BackColor = System.Drawing.Color.Transparent;
            this.lbNotice2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNotice2.ForeColor = System.Drawing.Color.Red;
            this.lbNotice2.Location = new System.Drawing.Point(83, 27);
            this.lbNotice2.Name = "lbNotice2";
            this.lbNotice2.Size = new System.Drawing.Size(66, 16);
            this.lbNotice2.TabIndex = 89;
            this.lbNotice2.Text = "lbNotice2";
            // 
            // pbHoanTat
            // 
            this.pbHoanTat.BackColor = System.Drawing.Color.Transparent;
            this.pbHoanTat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHoanTat.Location = new System.Drawing.Point(320, 110);
            this.pbHoanTat.Name = "pbHoanTat";
            this.pbHoanTat.Size = new System.Drawing.Size(50, 39);
            this.pbHoanTat.TabIndex = 91;
            this.pbHoanTat.TabStop = false;
            this.pbHoanTat.Click += new System.EventHandler(this.pbHoanTat_Click);
            this.pbHoanTat.MouseEnter += new System.EventHandler(this.pbHoanTat_MouseEnter);
            this.pbHoanTat.MouseLeave += new System.EventHandler(this.pbHoanTat_MouseLeave);
            // 
            // pbHuy
            // 
            this.pbHuy.BackColor = System.Drawing.Color.Transparent;
            this.pbHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHuy.Location = new System.Drawing.Point(150, 110);
            this.pbHuy.Name = "pbHuy";
            this.pbHuy.Size = new System.Drawing.Size(50, 39);
            this.pbHuy.TabIndex = 90;
            this.pbHuy.TabStop = false;
            this.pbHuy.Click += new System.EventHandler(this.pbHuy_Click);
            this.pbHuy.MouseEnter += new System.EventHandler(this.pbHuy_MouseEnter);
            this.pbHuy.MouseLeave += new System.EventHandler(this.pbHuy_MouseLeave);
            // 
            // pnNotice
            // 
            this.pnNotice.BackColor = System.Drawing.Color.Transparent;
            this.pnNotice.Controls.Add(this.lbNotice1);
            this.pnNotice.Controls.Add(this.lbNotice2);
            this.pnNotice.Location = new System.Drawing.Point(120, 45);
            this.pnNotice.Name = "pnNotice";
            this.pnNotice.Size = new System.Drawing.Size(274, 48);
            this.pnNotice.TabIndex = 101;
            // 
            // Form_Notice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 197);
            this.Controls.Add(this.pnNotice);
            this.Controls.Add(this.pbHoanTat);
            this.Controls.Add(this.pbHuy);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Notice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Notice";
            this.Load += new System.EventHandler(this.Form_Notice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHoanTat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuy)).EndInit();
            this.pnNotice.ResumeLayout(false);
            this.pnNotice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbNotice1;
        private System.Windows.Forms.Label lbNotice2;
        private System.Windows.Forms.PictureBox pbHoanTat;
        private System.Windows.Forms.PictureBox pbHuy;
        private System.Windows.Forms.Panel pnNotice;
    }
}