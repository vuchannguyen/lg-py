using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Function;
using System.IO;

namespace PlayStationKD
{
    public partial class Sound : Form
    {
        private string sFileName = @"Sound\Sound.sou";

        public Sound()
        {
            InitializeComponent();
        }

        private void Sound_Load(object sender, EventArgs e)
        {
            if (File.Exists(sFileName))
            {
                StreamReader sr = new StreamReader(sFileName);

                tbSound.Text = sr.ReadLine();

                sr.Close();
            }
        }

        private void btChon_Click(object sender, EventArgs e)
        {
            string sSound = File_Function.OpenDialog("Chuông", "wav");

            if (sSound != null)
            {
                tbSound.Text = sSound;
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btHoanTat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đồng ý thay đổi?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);

                    FileStream fs = File.Create(sFileName);
                    fs.Close();

                    StreamWriter sw = new StreamWriter(sFileName, true);
                    sw.WriteLine(tbSound.Text);

                    sw.Close();
                }

                this.Close();
            }
        }
    }
}
