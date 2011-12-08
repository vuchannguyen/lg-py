namespace Co_Lau_711
{
    partial class UC_HelpDoanHinh
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbHelp = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbHelp
            // 
            this.gbHelp.Controls.Add(this.label7);
            this.gbHelp.Controls.Add(this.label5);
            this.gbHelp.Controls.Add(this.label1);
            this.gbHelp.Location = new System.Drawing.Point(3, 3);
            this.gbHelp.Name = "gbHelp";
            this.gbHelp.Size = new System.Drawing.Size(150, 180);
            this.gbHelp.TabIndex = 1;
            this.gbHelp.TabStop = false;
            this.gbHelp.Text = "Hướng dẫn";
            this.gbHelp.Enter += new System.EventHandler(this.gbHelp_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 48);
            this.label7.TabIndex = 6;
            this.label7.Text = "- Thắng khi tìm hết\r\ncác cặp hình giống\r\nnhau.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 32);
            this.label5.TabIndex = 4;
            this.label5.Text = "- Thời gian cho mỗi\r\nlượt là \"5 phút\".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "- Tìm cặp hình giống\r\nnhau.";
            // 
            // UC_HelpDoanHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbHelp);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UC_HelpDoanHinh";
            this.Size = new System.Drawing.Size(155, 190);
            this.gbHelp.ResumeLayout(false);
            this.gbHelp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHelp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}
