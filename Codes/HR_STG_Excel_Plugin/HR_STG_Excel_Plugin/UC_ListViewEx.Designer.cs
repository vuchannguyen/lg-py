namespace HR_STG_Excel_Plugin
{
    partial class UC_ListViewEx
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
            this.listViewEx = new HR_STG_Excel_Plugin.ListViewEx();
            this.SuspendLayout();
            // 
            // listViewEx
            // 
            this.listViewEx.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewEx.FullRowSelect = true;
            this.listViewEx.GridLines = true;
            this.listViewEx.Location = new System.Drawing.Point(3, 3);
            this.listViewEx.Name = "listViewEx";
            this.listViewEx.Size = new System.Drawing.Size(521, 293);
            this.listViewEx.TabIndex = 0;
            this.listViewEx.UseCompatibleStateImageBehavior = false;
            this.listViewEx.View = System.Windows.Forms.View.Details;
            // 
            // UC_ListViewEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewEx);
            this.Name = "UC_ListViewEx";
            this.Size = new System.Drawing.Size(770, 485);
            this.Load += new System.EventHandler(this.UC_ListViewEx_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewEx listViewEx;
    }
}
