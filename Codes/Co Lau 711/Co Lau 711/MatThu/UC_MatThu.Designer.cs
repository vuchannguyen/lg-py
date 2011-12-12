namespace Co_Lau_711
{
    partial class UC_MatThu
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
            this.gbNoiDung = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbLoiGiai = new System.Windows.Forms.GroupBox();
            this.lbError = new System.Windows.Forms.Label();
            this.btHoanTat = new System.Windows.Forms.Button();
            this.tbLoiGiai = new System.Windows.Forms.TextBox();
            this.lbTroChoi = new System.Windows.Forms.Label();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.gbNoiDung.SuspendLayout();
            this.gbLoiGiai.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNoiDung
            // 
            this.gbNoiDung.Controls.Add(this.label1);
            this.gbNoiDung.Location = new System.Drawing.Point(3, 169);
            this.gbNoiDung.Name = "gbNoiDung";
            this.gbNoiDung.Size = new System.Drawing.Size(694, 358);
            this.gbNoiDung.TabIndex = 0;
            this.gbNoiDung.TabStop = false;
            this.gbNoiDung.Text = "Nội dung";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ví dụ: COWF LAU 711";
            // 
            // gbLoiGiai
            // 
            this.gbLoiGiai.Controls.Add(this.lbError);
            this.gbLoiGiai.Controls.Add(this.btHoanTat);
            this.gbLoiGiai.Controls.Add(this.tbLoiGiai);
            this.gbLoiGiai.Location = new System.Drawing.Point(3, 33);
            this.gbLoiGiai.Name = "gbLoiGiai";
            this.gbLoiGiai.Size = new System.Drawing.Size(540, 130);
            this.gbLoiGiai.TabIndex = 1;
            this.gbLoiGiai.TabStop = false;
            this.gbLoiGiai.Text = "Lời giải";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(12, 105);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(27, 15);
            this.lbError.TabIndex = 69;
            this.lbError.Text = " Lỗi";
            // 
            // btHoanTat
            // 
            this.btHoanTat.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btHoanTat.ForeColor = System.Drawing.Color.Blue;
            this.btHoanTat.Location = new System.Drawing.Point(430, 20);
            this.btHoanTat.Name = "btHoanTat";
            this.btHoanTat.Size = new System.Drawing.Size(101, 82);
            this.btHoanTat.TabIndex = 4;
            this.btHoanTat.Text = "Hoàn tất";
            this.btHoanTat.UseVisualStyleBackColor = true;
            this.btHoanTat.Click += new System.EventHandler(this.btHoanTat_Click);
            // 
            // tbLoiGiai
            // 
            this.tbLoiGiai.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbLoiGiai.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLoiGiai.Location = new System.Drawing.Point(15, 20);
            this.tbLoiGiai.Multiline = true;
            this.tbLoiGiai.Name = "tbLoiGiai";
            this.tbLoiGiai.Size = new System.Drawing.Size(410, 82);
            this.tbLoiGiai.TabIndex = 0;
            this.tbLoiGiai.TextChanged += new System.EventHandler(this.tbLoiGiai_TextChanged);
            this.tbLoiGiai.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLoiGiai_KeyDown);
            this.tbLoiGiai.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbLoiGiai_KeyUp);
            // 
            // lbTroChoi
            // 
            this.lbTroChoi.AutoSize = true;
            this.lbTroChoi.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTroChoi.Location = new System.Drawing.Point(300, 3);
            this.lbTroChoi.Name = "lbTroChoi";
            this.lbTroChoi.Size = new System.Drawing.Size(100, 22);
            this.lbTroChoi.TabIndex = 19;
            this.lbTroChoi.Text = "MẬT THƯ";
            // 
            // gbTime
            // 
            this.gbTime.Location = new System.Drawing.Point(562, 33);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(135, 130);
            this.gbTime.TabIndex = 20;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Thời gian còn lại";
            // 
            // UC_MatThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTime);
            this.Controls.Add(this.lbTroChoi);
            this.Controls.Add(this.gbLoiGiai);
            this.Controls.Add(this.gbNoiDung);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_MatThu";
            this.Size = new System.Drawing.Size(700, 530);
            this.Load += new System.EventHandler(this.UC_MatThu_Load);
            this.gbNoiDung.ResumeLayout(false);
            this.gbNoiDung.PerformLayout();
            this.gbLoiGiai.ResumeLayout(false);
            this.gbLoiGiai.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNoiDung;
        private System.Windows.Forms.GroupBox gbLoiGiai;
        private System.Windows.Forms.TextBox tbLoiGiai;
        private System.Windows.Forms.Button btHoanTat;
        private System.Windows.Forms.Label lbTroChoi;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbTime;
    }
}
