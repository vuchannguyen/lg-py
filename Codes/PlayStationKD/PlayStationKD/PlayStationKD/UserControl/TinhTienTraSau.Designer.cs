namespace PlayStationKD
{
    partial class TinhTienTraSau
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
            this.components = new System.ComponentModel.Container();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.pnTTTS = new System.Windows.Forms.Panel();
            this.cbChuyenMay = new System.Windows.Forms.ComboBox();
            this.tbSeconds = new System.Windows.Forms.TextBox();
            this.tbMinutes = new System.Windows.Forms.TextBox();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTien = new System.Windows.Forms.Label();
            this.lbTimesIn = new System.Windows.Forms.Label();
            this.btDung = new System.Windows.Forms.Button();
            this.btBatDau = new System.Windows.Forms.Button();
            this.tbTien = new System.Windows.Forms.TextBox();
            this.lbSec = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.lbMay = new System.Windows.Forms.Label();
            this.lbHr = new System.Windows.Forms.Label();
            this.pnTTTS.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerCountDown
            // 
            this.timerCountDown.Interval = 1000;
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // pnTTTS
            // 
            this.pnTTTS.Controls.Add(this.cbChuyenMay);
            this.pnTTTS.Controls.Add(this.tbSeconds);
            this.pnTTTS.Controls.Add(this.tbMinutes);
            this.pnTTTS.Controls.Add(this.tbHours);
            this.pnTTTS.Controls.Add(this.label2);
            this.pnTTTS.Controls.Add(this.label1);
            this.pnTTTS.Controls.Add(this.lbTien);
            this.pnTTTS.Controls.Add(this.lbTimesIn);
            this.pnTTTS.Controls.Add(this.btDung);
            this.pnTTTS.Controls.Add(this.btBatDau);
            this.pnTTTS.Controls.Add(this.tbTien);
            this.pnTTTS.Controls.Add(this.lbSec);
            this.pnTTTS.Controls.Add(this.lbMin);
            this.pnTTTS.Controls.Add(this.lbMay);
            this.pnTTTS.Controls.Add(this.lbHr);
            this.pnTTTS.Location = new System.Drawing.Point(0, 0);
            this.pnTTTS.Name = "pnTTTS";
            this.pnTTTS.Size = new System.Drawing.Size(190, 170);
            this.pnTTTS.TabIndex = 10;
            // 
            // cbChuyenMay
            // 
            this.cbChuyenMay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChuyenMay.Font = new System.Drawing.Font("Arial", 12F);
            this.cbChuyenMay.FormattingEnabled = true;
            this.cbChuyenMay.Items.AddRange(new object[] {
            "Chuyển Máy"});
            this.cbChuyenMay.Location = new System.Drawing.Point(40, 140);
            this.cbChuyenMay.Name = "cbChuyenMay";
            this.cbChuyenMay.Size = new System.Drawing.Size(110, 26);
            this.cbChuyenMay.TabIndex = 31;
            this.cbChuyenMay.Visible = false;
            this.cbChuyenMay.DropDown += new System.EventHandler(this.cbChuyenMay_DropDown);
            this.cbChuyenMay.DropDownClosed += new System.EventHandler(this.cbChuyenMay_DropDownClosed);
            // 
            // tbSeconds
            // 
            this.tbSeconds.Font = new System.Drawing.Font("Arial", 12F);
            this.tbSeconds.Location = new System.Drawing.Point(120, 113);
            this.tbSeconds.MaxLength = 2;
            this.tbSeconds.Name = "tbSeconds";
            this.tbSeconds.Size = new System.Drawing.Size(24, 26);
            this.tbSeconds.TabIndex = 30;
            this.tbSeconds.Text = "00";
            this.tbSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSeconds.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbSeconds_MouseClick);
            this.tbSeconds.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSeconds_KeyDown);
            this.tbSeconds.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSeconds_KeyUp);
            // 
            // tbMinutes
            // 
            this.tbMinutes.Font = new System.Drawing.Font("Arial", 12F);
            this.tbMinutes.Location = new System.Drawing.Point(81, 113);
            this.tbMinutes.MaxLength = 2;
            this.tbMinutes.Name = "tbMinutes";
            this.tbMinutes.Size = new System.Drawing.Size(24, 26);
            this.tbMinutes.TabIndex = 29;
            this.tbMinutes.Text = "00";
            this.tbMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMinutes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbMinutes_MouseClick);
            this.tbMinutes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMinutes_KeyDown);
            this.tbMinutes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbMinutes_KeyUp);
            // 
            // tbHours
            // 
            this.tbHours.Font = new System.Drawing.Font("Arial", 12F);
            this.tbHours.Location = new System.Drawing.Point(45, 113);
            this.tbHours.MaxLength = 2;
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(24, 26);
            this.tbHours.TabIndex = 28;
            this.tbHours.Text = "00";
            this.tbHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHours.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbHours_MouseClick);
            this.tbHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyDown);
            this.tbHours.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = ":";
            // 
            // lbTien
            // 
            this.lbTien.AutoSize = true;
            this.lbTien.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTien.Location = new System.Drawing.Point(10, 35);
            this.lbTien.Name = "lbTien";
            this.lbTien.Size = new System.Drawing.Size(42, 18);
            this.lbTien.TabIndex = 10;
            this.lbTien.Text = "Tiền:";
            // 
            // lbTimesIn
            // 
            this.lbTimesIn.AutoSize = true;
            this.lbTimesIn.Font = new System.Drawing.Font("Arial", 12F);
            this.lbTimesIn.Location = new System.Drawing.Point(10, 62);
            this.lbTimesIn.Name = "lbTimesIn";
            this.lbTimesIn.Size = new System.Drawing.Size(74, 18);
            this.lbTimesIn.TabIndex = 9;
            this.lbTimesIn.Text = "Giờ chơi:";
            // 
            // btDung
            // 
            this.btDung.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btDung.ForeColor = System.Drawing.Color.Red;
            this.btDung.Location = new System.Drawing.Point(60, 82);
            this.btDung.Name = "btDung";
            this.btDung.Size = new System.Drawing.Size(70, 28);
            this.btDung.TabIndex = 8;
            this.btDung.Text = "Dừng";
            this.btDung.UseVisualStyleBackColor = true;
            this.btDung.Visible = false;
            this.btDung.Click += new System.EventHandler(this.btDung_Click);
            // 
            // btBatDau
            // 
            this.btBatDau.Font = new System.Drawing.Font("Arial", 12F);
            this.btBatDau.Location = new System.Drawing.Point(60, 82);
            this.btBatDau.Name = "btBatDau";
            this.btBatDau.Size = new System.Drawing.Size(70, 28);
            this.btBatDau.TabIndex = 3;
            this.btBatDau.Text = "Bắt đầu";
            this.btBatDau.UseVisualStyleBackColor = true;
            this.btBatDau.Click += new System.EventHandler(this.btBatDau_Click);
            // 
            // tbTien
            // 
            this.tbTien.Font = new System.Drawing.Font("Arial", 12F);
            this.tbTien.Location = new System.Drawing.Point(55, 32);
            this.tbTien.MaxLength = 6;
            this.tbTien.Name = "tbTien";
            this.tbTien.ReadOnly = true;
            this.tbTien.Size = new System.Drawing.Size(120, 26);
            this.tbTien.TabIndex = 1;
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Font = new System.Drawing.Font("Arial", 16F);
            this.lbSec.Location = new System.Drawing.Point(115, 113);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(36, 25);
            this.lbSec.TabIndex = 5;
            this.lbSec.Text = "00";
            // 
            // lbMin
            // 
            this.lbMin.AutoSize = true;
            this.lbMin.Font = new System.Drawing.Font("Arial", 16F);
            this.lbMin.Location = new System.Drawing.Point(76, 113);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(36, 25);
            this.lbMin.TabIndex = 4;
            this.lbMin.Text = "00";
            // 
            // lbMay
            // 
            this.lbMay.AutoSize = true;
            this.lbMay.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lbMay.Location = new System.Drawing.Point(60, 5);
            this.lbMay.Name = "lbMay";
            this.lbMay.Size = new System.Drawing.Size(52, 26);
            this.lbMay.TabIndex = 0;
            this.lbMay.Text = "Máy";
            // 
            // lbHr
            // 
            this.lbHr.AutoSize = true;
            this.lbHr.Font = new System.Drawing.Font("Arial", 16F);
            this.lbHr.Location = new System.Drawing.Point(40, 113);
            this.lbHr.Name = "lbHr";
            this.lbHr.Size = new System.Drawing.Size(36, 25);
            this.lbHr.TabIndex = 2;
            this.lbHr.Text = "00";
            // 
            // TinhTienTraSau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnTTTS);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TinhTienTraSau";
            this.Size = new System.Drawing.Size(190, 170);
            this.pnTTTS.ResumeLayout(false);
            this.pnTTTS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerCountDown;
        private System.Windows.Forms.Panel pnTTTS;
        private System.Windows.Forms.Label lbTien;
        private System.Windows.Forms.Label lbTimesIn;
        private System.Windows.Forms.Button btDung;
        private System.Windows.Forms.Button btBatDau;
        private System.Windows.Forms.TextBox tbTien;
        private System.Windows.Forms.Label lbSec;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.Label lbMay;
        private System.Windows.Forms.Label lbHr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSeconds;
        private System.Windows.Forms.TextBox tbMinutes;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.ComboBox cbChuyenMay;

    }
}
