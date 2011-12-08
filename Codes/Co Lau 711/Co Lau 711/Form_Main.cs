using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using About;
using Function;

namespace Co_Lau_711
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {

        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult bClose = MessageBox.Show("Bạn có thật sự muốn thoát?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (bClose == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult bClose = MessageBox.Show("Bạn đã sẵn sàng chưa?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (bClose == DialogResult.OK)
            {
                pnGame.Controls.Clear();

                NewGame();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_About frm = new Form_About();
        }

        private void NewGame()
        {
            int n = SubFunction.RandomIntNumber(1, 4);
            //int n = 3;

            switch (n)
            {
                case 1:
                    {
                        UC_DoanSo uc = new UC_DoanSo();
                        uc.Location = SubFunction.SetCenterLocation(pnGame.Size, uc.Size);

                        pnGame.Controls.Add(uc);

                        break;
                    }
                case 2:
                    {
                        UC_DoanHinh uc = new UC_DoanHinh();
                        uc.Location = SubFunction.SetCenterLocation(pnGame.Size, uc.Size);

                        pnGame.Controls.Add(uc);

                        break;
                    }
                case 3:
                    {
                        UC_MatThu uc = new UC_MatThu();
                        uc.Location = SubFunction.SetCenterLocation(pnGame.Size, uc.Size);

                        pnGame.Controls.Add(uc);

                        break;
                    }
            }
        }
    }
}
