namespace Co_Lau_711
{
    partial class UC_DoanSo
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
            this.gbThongDiep = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btThongDiep = new System.Windows.Forms.Button();
            this.pnGame = new System.Windows.Forms.Panel();
            this.btCheat = new System.Windows.Forms.Button();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.gbPlay = new System.Windows.Forms.GroupBox();
            this.pnSteps = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btGuess = new System.Windows.Forms.Button();
            this.lbError_Play = new System.Windows.Forms.Label();
            this.lbRemain = new System.Windows.Forms.Label();
            this.tbGuess = new System.Windows.Forms.TextBox();
            this.gbCreate = new System.Windows.Forms.GroupBox();
            this.lbLength = new System.Windows.Forms.Label();
            this.lbError_Create = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.btPlay = new System.Windows.Forms.Button();
            this.btCustom = new System.Windows.Forms.Button();
            this.btAuto = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.nUD_Length = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nUD_Steps = new System.Windows.Forms.NumericUpDown();
            this.lbTroChoi = new System.Windows.Forms.Label();
            this.uc_HuongDan = new Co_Lau_711.UC_HelpDoanSo();
            this.gbThongDiep.SuspendLayout();
            this.pnGame.SuspendLayout();
            this.gbPlay.SuspendLayout();
            this.pnSteps.SuspendLayout();
            this.gbCreate.SuspendLayout();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Steps)).BeginInit();
            this.SuspendLayout();
            // 
            // gbThongDiep
            // 
            this.gbThongDiep.Controls.Add(this.label6);
            this.gbThongDiep.Controls.Add(this.label5);
            this.gbThongDiep.Controls.Add(this.label4);
            this.gbThongDiep.Controls.Add(this.btThongDiep);
            this.gbThongDiep.Location = new System.Drawing.Point(431, 60);
            this.gbThongDiep.Name = "gbThongDiep";
            this.gbThongDiep.Size = new System.Drawing.Size(310, 271);
            this.gbThongDiep.TabIndex = 14;
            this.gbThongDiep.TabStop = false;
            this.gbThongDiep.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(242, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Lưu ý chỉ xem thông điệp được 1 lần.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Nhấn vào nút bên dưới để tiếp tục.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(20, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chúc mừng bạn đã tìm ra Con số bí ẩn.";
            // 
            // btThongDiep
            // 
            this.btThongDiep.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThongDiep.ForeColor = System.Drawing.Color.Blue;
            this.btThongDiep.Location = new System.Drawing.Point(120, 160);
            this.btThongDiep.Name = "btThongDiep";
            this.btThongDiep.Size = new System.Drawing.Size(84, 56);
            this.btThongDiep.TabIndex = 11;
            this.btThongDiep.Text = "Thông điệp";
            this.btThongDiep.UseVisualStyleBackColor = true;
            this.btThongDiep.Click += new System.EventHandler(this.btThongDiep_Click);
            // 
            // pnGame
            // 
            this.pnGame.Controls.Add(this.btCheat);
            this.pnGame.Controls.Add(this.gbTime);
            this.pnGame.Controls.Add(this.gbPlay);
            this.pnGame.Controls.Add(this.gbCreate);
            this.pnGame.Location = new System.Drawing.Point(3, 33);
            this.pnGame.Name = "pnGame";
            this.pnGame.Size = new System.Drawing.Size(422, 433);
            this.pnGame.TabIndex = 13;
            // 
            // btCheat
            // 
            this.btCheat.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCheat.ForeColor = System.Drawing.Color.Red;
            this.btCheat.Location = new System.Drawing.Point(93, 95);
            this.btCheat.Name = "btCheat";
            this.btCheat.Size = new System.Drawing.Size(95, 26);
            this.btCheat.TabIndex = 13;
            this.btCheat.Text = "Cheat";
            this.btCheat.UseVisualStyleBackColor = true;
            this.btCheat.Click += new System.EventHandler(this.btCheat_Click);
            // 
            // gbTime
            // 
            this.gbTime.Location = new System.Drawing.Point(284, 5);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(135, 150);
            this.gbTime.TabIndex = 10;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Thời gian còn lại";
            // 
            // gbPlay
            // 
            this.gbPlay.Controls.Add(this.pnSteps);
            this.gbPlay.Controls.Add(this.btGuess);
            this.gbPlay.Controls.Add(this.lbError_Play);
            this.gbPlay.Controls.Add(this.lbRemain);
            this.gbPlay.Controls.Add(this.tbGuess);
            this.gbPlay.Enabled = false;
            this.gbPlay.Location = new System.Drawing.Point(3, 161);
            this.gbPlay.Name = "gbPlay";
            this.gbPlay.Size = new System.Drawing.Size(416, 269);
            this.gbPlay.TabIndex = 9;
            this.gbPlay.TabStop = false;
            // 
            // pnSteps
            // 
            this.pnSteps.Controls.Add(this.label3);
            this.pnSteps.Location = new System.Drawing.Point(6, 66);
            this.pnSteps.Name = "pnSteps";
            this.pnSteps.Size = new System.Drawing.Size(404, 197);
            this.pnSteps.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(131, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 69;
            this.label3.Text = "Lần 1: 2T - 2Đ";
            this.label3.Visible = false;
            // 
            // btGuess
            // 
            this.btGuess.Enabled = false;
            this.btGuess.Location = new System.Drawing.Point(6, 20);
            this.btGuess.Name = "btGuess";
            this.btGuess.Size = new System.Drawing.Size(64, 26);
            this.btGuess.TabIndex = 68;
            this.btGuess.Text = "Đoán";
            this.btGuess.UseVisualStyleBackColor = true;
            this.btGuess.Click += new System.EventHandler(this.btGuess_Click);
            // 
            // lbError_Play
            // 
            this.lbError_Play.AutoSize = true;
            this.lbError_Play.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError_Play.ForeColor = System.Drawing.Color.Red;
            this.lbError_Play.Location = new System.Drawing.Point(73, 48);
            this.lbError_Play.Name = "lbError_Play";
            this.lbError_Play.Size = new System.Drawing.Size(27, 15);
            this.lbError_Play.TabIndex = 67;
            this.lbError_Play.Text = " Lỗi";
            // 
            // lbRemain
            // 
            this.lbRemain.AutoSize = true;
            this.lbRemain.ForeColor = System.Drawing.Color.Black;
            this.lbRemain.Location = new System.Drawing.Point(278, 25);
            this.lbRemain.Name = "lbRemain";
            this.lbRemain.Size = new System.Drawing.Size(93, 16);
            this.lbRemain.TabIndex = 5;
            this.lbRemain.Text = "Số lần còn lại";
            // 
            // tbGuess
            // 
            this.tbGuess.Location = new System.Drawing.Point(76, 22);
            this.tbGuess.MaxLength = 4;
            this.tbGuess.Name = "tbGuess";
            this.tbGuess.Size = new System.Drawing.Size(67, 23);
            this.tbGuess.TabIndex = 3;
            this.tbGuess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbGuess.TextChanged += new System.EventHandler(this.tbGuess_TextChanged);
            this.tbGuess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbGuess_KeyDown);
            this.tbGuess.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbGuess_KeyUp);
            // 
            // gbCreate
            // 
            this.gbCreate.Controls.Add(this.lbLength);
            this.gbCreate.Controls.Add(this.lbError_Create);
            this.gbCreate.Controls.Add(this.tbNumber);
            this.gbCreate.Controls.Add(this.btPlay);
            this.gbCreate.Controls.Add(this.btCustom);
            this.gbCreate.Controls.Add(this.btAuto);
            this.gbCreate.Location = new System.Drawing.Point(3, 3);
            this.gbCreate.Name = "gbCreate";
            this.gbCreate.Size = new System.Drawing.Size(275, 152);
            this.gbCreate.TabIndex = 8;
            this.gbCreate.TabStop = false;
            // 
            // lbLength
            // 
            this.lbLength.AutoSize = true;
            this.lbLength.ForeColor = System.Drawing.Color.Black;
            this.lbLength.Location = new System.Drawing.Point(112, 98);
            this.lbLength.Name = "lbLength";
            this.lbLength.Size = new System.Drawing.Size(52, 16);
            this.lbLength.TabIndex = 67;
            this.lbLength.Text = "Length";
            this.lbLength.Visible = false;
            // 
            // lbError_Create
            // 
            this.lbError_Create.AutoSize = true;
            this.lbError_Create.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError_Create.ForeColor = System.Drawing.Color.Red;
            this.lbError_Create.Location = new System.Drawing.Point(122, 42);
            this.lbError_Create.Name = "lbError_Create";
            this.lbError_Create.Size = new System.Drawing.Size(27, 15);
            this.lbError_Create.TabIndex = 66;
            this.lbError_Create.Text = " Lỗi";
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNumber.Location = new System.Drawing.Point(90, 60);
            this.tbNumber.MaxLength = 4;
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(95, 29);
            this.tbNumber.TabIndex = 2;
            this.tbNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbNumber.UseSystemPasswordChar = true;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbNumber_TextChanged);
            this.tbNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNumber_KeyDown);
            this.tbNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbNumber_KeyUp);
            // 
            // btPlay
            // 
            this.btPlay.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPlay.ForeColor = System.Drawing.Color.Blue;
            this.btPlay.Location = new System.Drawing.Point(22, 35);
            this.btPlay.Name = "btPlay";
            this.btPlay.Size = new System.Drawing.Size(101, 82);
            this.btPlay.TabIndex = 3;
            this.btPlay.Text = "Bắt đầu";
            this.btPlay.UseVisualStyleBackColor = true;
            this.btPlay.Visible = false;
            this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
            // 
            // btCustom
            // 
            this.btCustom.Location = new System.Drawing.Point(25, 120);
            this.btCustom.Name = "btCustom";
            this.btCustom.Size = new System.Drawing.Size(64, 26);
            this.btCustom.TabIndex = 1;
            this.btCustom.Text = "Custom";
            this.btCustom.UseVisualStyleBackColor = true;
            this.btCustom.Visible = false;
            this.btCustom.Click += new System.EventHandler(this.btCustom_Click);
            // 
            // btAuto
            // 
            this.btAuto.Location = new System.Drawing.Point(170, 120);
            this.btAuto.Name = "btAuto";
            this.btAuto.Size = new System.Drawing.Size(75, 26);
            this.btAuto.TabIndex = 0;
            this.btAuto.Text = "Tạo mới";
            this.btAuto.UseVisualStyleBackColor = true;
            this.btAuto.Visible = false;
            this.btAuto.Click += new System.EventHandler(this.btAuto_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.nUD_Length);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.nUD_Steps);
            this.gbSettings.ForeColor = System.Drawing.Color.Blue;
            this.gbSettings.Location = new System.Drawing.Point(431, 462);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(171, 100);
            this.gbSettings.TabIndex = 16;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            this.gbSettings.Visible = false;
            // 
            // nUD_Length
            // 
            this.nUD_Length.Location = new System.Drawing.Point(97, 25);
            this.nUD_Length.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nUD_Length.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUD_Length.Name = "nUD_Length";
            this.nUD_Length.Size = new System.Drawing.Size(39, 23);
            this.nUD_Length.TabIndex = 4;
            this.nUD_Length.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(35, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Length:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(43, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Steps:";
            // 
            // nUD_Steps
            // 
            this.nUD_Steps.Location = new System.Drawing.Point(97, 64);
            this.nUD_Steps.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nUD_Steps.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nUD_Steps.Name = "nUD_Steps";
            this.nUD_Steps.Size = new System.Drawing.Size(39, 23);
            this.nUD_Steps.TabIndex = 5;
            this.nUD_Steps.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lbTroChoi
            // 
            this.lbTroChoi.AutoSize = true;
            this.lbTroChoi.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTroChoi.Location = new System.Drawing.Point(310, 3);
            this.lbTroChoi.Name = "lbTroChoi";
            this.lbTroChoi.Size = new System.Drawing.Size(140, 22);
            this.lbTroChoi.TabIndex = 17;
            this.lbTroChoi.Text = "CON SỐ BÍ ẨN";
            // 
            // uc_HuongDan
            // 
            this.uc_HuongDan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_HuongDan.Location = new System.Drawing.Point(431, 33);
            this.uc_HuongDan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uc_HuongDan.Name = "uc_HuongDan";
            this.uc_HuongDan.Size = new System.Drawing.Size(320, 430);
            this.uc_HuongDan.TabIndex = 15;
            // 
            // UC_DoanSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTroChoi);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.gbThongDiep);
            this.Controls.Add(this.pnGame);
            this.Controls.Add(this.uc_HuongDan);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_DoanSo";
            this.Size = new System.Drawing.Size(750, 470);
            this.Load += new System.EventHandler(this.UC_DoanSo_Load);
            this.gbThongDiep.ResumeLayout(false);
            this.gbThongDiep.PerformLayout();
            this.pnGame.ResumeLayout(false);
            this.gbPlay.ResumeLayout(false);
            this.gbPlay.PerformLayout();
            this.pnSteps.ResumeLayout(false);
            this.pnSteps.PerformLayout();
            this.gbCreate.ResumeLayout(false);
            this.gbCreate.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Steps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbThongDiep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btThongDiep;
        private System.Windows.Forms.Panel pnGame;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.GroupBox gbPlay;
        private System.Windows.Forms.Panel pnSteps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btGuess;
        private System.Windows.Forms.Label lbError_Play;
        private System.Windows.Forms.Label lbRemain;
        private System.Windows.Forms.TextBox tbGuess;
        private System.Windows.Forms.GroupBox gbCreate;
        private System.Windows.Forms.Label lbLength;
        private System.Windows.Forms.Label lbError_Create;
        private System.Windows.Forms.Button btPlay;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Button btCustom;
        private System.Windows.Forms.Button btAuto;
        private Co_Lau_711.UC_HelpDoanSo uc_HuongDan;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.NumericUpDown nUD_Length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nUD_Steps;
        private System.Windows.Forms.Label lbTroChoi;
        private System.Windows.Forms.Button btCheat;
    }
}
