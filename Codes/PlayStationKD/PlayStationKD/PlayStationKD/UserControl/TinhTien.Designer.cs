namespace PlayStationKD
{
    partial class TinhTien
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
            this.timerChuyenMay = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerChuyenMay
            // 
            this.timerChuyenMay.Interval = 1000;
            // 
            // TinhTien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TinhTien";
            this.Size = new System.Drawing.Size(730, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerChuyenMay;


    }
}
