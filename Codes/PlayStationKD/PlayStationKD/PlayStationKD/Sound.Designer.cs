namespace PlayStationKD
{
    partial class Sound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sound));
            this.label1 = new System.Windows.Forms.Label();
            this.tbSound = new System.Windows.Forms.TextBox();
            this.btChon = new System.Windows.Forms.Button();
            this.btHoanTat = new System.Windows.Forms.Button();
            this.btHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kiểu chuông:";
            // 
            // tbSound
            // 
            this.tbSound.BackColor = System.Drawing.Color.White;
            this.tbSound.Location = new System.Drawing.Point(36, 68);
            this.tbSound.Name = "tbSound";
            this.tbSound.ReadOnly = true;
            this.tbSound.Size = new System.Drawing.Size(316, 26);
            this.tbSound.TabIndex = 1;
            // 
            // btChon
            // 
            this.btChon.Location = new System.Drawing.Point(381, 68);
            this.btChon.Name = "btChon";
            this.btChon.Size = new System.Drawing.Size(79, 26);
            this.btChon.TabIndex = 2;
            this.btChon.Text = "Chọn";
            this.btChon.UseVisualStyleBackColor = true;
            this.btChon.Click += new System.EventHandler(this.btChon_Click);
            // 
            // btHoanTat
            // 
            this.btHoanTat.Location = new System.Drawing.Point(306, 119);
            this.btHoanTat.Name = "btHoanTat";
            this.btHoanTat.Size = new System.Drawing.Size(79, 43);
            this.btHoanTat.TabIndex = 3;
            this.btHoanTat.Text = "Hoàn tất";
            this.btHoanTat.UseVisualStyleBackColor = true;
            this.btHoanTat.Click += new System.EventHandler(this.btHoanTat_Click);
            // 
            // btHuy
            // 
            this.btHuy.Location = new System.Drawing.Point(100, 119);
            this.btHuy.Name = "btHuy";
            this.btHuy.Size = new System.Drawing.Size(79, 43);
            this.btHuy.TabIndex = 4;
            this.btHuy.Text = "Hủy";
            this.btHuy.UseVisualStyleBackColor = true;
            this.btHuy.Click += new System.EventHandler(this.btHuy_Click);
            // 
            // Sound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 174);
            this.Controls.Add(this.btHuy);
            this.Controls.Add(this.btHoanTat);
            this.Controls.Add(this.btChon);
            this.Controls.Add(this.tbSound);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sound";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuông hết giờ";
            this.Load += new System.EventHandler(this.Sound_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSound;
        private System.Windows.Forms.Button btChon;
        private System.Windows.Forms.Button btHoanTat;
        private System.Windows.Forms.Button btHuy;

    }
}