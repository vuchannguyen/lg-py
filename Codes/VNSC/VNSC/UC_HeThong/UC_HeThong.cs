using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;

namespace VNSC
{
    public partial class UC_HeThong : UserControl
    {
        private UC_Plugin uc_Plugin;

        public UC_HeThong()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbTitle.Image = Image.FromFile(@"Resources\HeThong\title_system.png");

                pbDatabase.Image = Image.FromFile(@"Resources\HeThong\system_dbmanager.png");
                pbUser.Image = Image.FromFile(@"Resources\HeThong\system_usermanager.png");
                pbPluginManager.Image = Image.FromFile(@"Resources\HeThong\system_pluginmanager.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_HeThong_Load(object sender, EventArgs e)
        {
            LoadPic();

            this.Visible = false;
            pnSelect.Location = SubFunction.SetWidthCenter(this.Size, pnSelect.Size, pnSelect.Top);
        }

        private void NewControls(int i)
        {
            //if (i == 0)
            //{
            //    uc_SuKien = new UC_SuKien();
            //    this.Controls.Add(uc_SuKien);
            //}

            //if (i == 1)
            //{
            //    uc_LoaiHinh = new UC_LoaiHinh();
            //    this.Controls.Add(uc_LoaiHinh);
            //}

            if (i == 2)
            {
                uc_Plugin = new UC_Plugin();
                this.Controls.Add(uc_Plugin);
            }
        }

        private void pbDatabase_Click(object sender, EventArgs e)
        {

        }

        private void pbDatabase_MouseEnter(object sender, EventArgs e)
        {
            pbDatabase.Image = Image.FromFile(@"Resources\HeThong\system_dbmanager_mouseover.png");
            lbDatabase.ForeColor = Color.Orange;
        }

        private void pbDatabase_MouseLeave(object sender, EventArgs e)
        {
            pbDatabase.Image = Image.FromFile(@"Resources\HeThong\system_dbmanager.png");
            lbDatabase.ForeColor = Color.Gray;
        }

        private void pbUser_Click(object sender, EventArgs e)
        {

        }

        private void pbUser_MouseEnter(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(@"Resources\HeThong\system_usermanager_mouseover.png");
            lbUser.ForeColor = Color.Orange;
        }

        private void pbUser_MouseLeave(object sender, EventArgs e)
        {
            pbUser.Image = Image.FromFile(@"Resources\HeThong\system_usermanager.png");
            lbUser.ForeColor = Color.Gray;
        }

        private void pbPluginManager_Click(object sender, EventArgs e)
        {
            pnTitle.Visible = false;
            pnSelect.Visible = false;

            NewControls(2);

            if (!uc_Plugin.Visible)
            {
                pnTitle.Visible = true;
                pnSelect.Visible = true;
            }
        }

        private void pbPluginManager_MouseEnter(object sender, EventArgs e)
        {
            pbPluginManager.Image = Image.FromFile(@"Resources\HeThong\system_pluginmanager_mouseover.png");
            lbPluginManager.ForeColor = Color.Orange;
        }

        private void pbPluginManager_MouseLeave(object sender, EventArgs e)
        {
            pbPluginManager.Image = Image.FromFile(@"Resources\HeThong\system_pluginmanager.png");
            lbPluginManager.ForeColor = Color.Gray;
        }
    }
}
