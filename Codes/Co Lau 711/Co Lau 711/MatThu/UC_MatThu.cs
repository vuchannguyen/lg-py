using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;

namespace Co_Lau_711
{
    public partial class UC_MatThu : UserControl
    {
        private UC_CountDownTimer uc_cdt;

        public UC_MatThu()
        {
            InitializeComponent();
        }

        private void UC_MatThu_Load(object sender, EventArgs e)
        {
            lbError.Text = "";

            CreateTimer();
        }

        private void btHoanTat_Click(object sender, EventArgs e)
        {
            if (tbLoiGiai.Text == "COWF LAU 711")
            {
                tbLoiGiai.Enabled = false;
                btHoanTat.Enabled = false;
                tbLoiGiai.UseSystemPasswordChar = true;

                Form_ThongDiep frm = new Form_ThongDiep();
                frm.ShowDialog();

                this.Dispose();
            }
            else
            {
                SubFunction.SetError(lbError, lbError.Location.Y, gbLoiGiai.Size, "Lời giải không chính xác! Vui lòng thử lại.");
            }
        }

        private void tbLoiGiai_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = "";
        }

        private void CreateTimer()
        {
            gbTime.Controls.Clear();
            uc_cdt = new UC_CountDownTimer(0, 5, 0);
            uc_cdt.EnabledChanged += new EventHandler(uc_cdt_EnabledChanged);
            uc_cdt.Location = SubFunction.SetCenterLocation(gbTime.Size, uc_cdt.Size);
            gbTime.Controls.Add(uc_cdt);
        }

        private void uc_cdt_EnabledChanged(object sender, EventArgs e)
        {
            if (!uc_cdt.BTimeCounting)
            {
                SubFunction.SetError(lbError, lbError.Location.Y, gbLoiGiai.Size, "Hết thời gian!");

                gbTime.Enabled = false;
                tbLoiGiai.Enabled = false;
                btHoanTat.Enabled = false;
            }
        }

        private void tbLoiGiai_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbLoiGiai_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                //if (tbLoiGiai.Text == "")
            }
        }

        private void TestLoiGiai(string sLoiGiai)
        { 
            
        }
    }
}
