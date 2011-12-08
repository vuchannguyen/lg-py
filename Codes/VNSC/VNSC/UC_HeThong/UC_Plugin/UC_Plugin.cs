using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Function;

namespace VNSC
{
    public partial class UC_Plugin : UserControl
    {
        PluginInterface.IPlugin plugin;

        public UC_Plugin()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbTitle.Image = Image.FromFile(@"Resources\HeThong\title_plugin.png");

                pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
                pbInstall.Image = Image.FromFile(@"Resources\HeThong\button_installplugin.png");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_Plugin_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnMain.Location = new Point(10, 50);
            pnMain.Size = new System.Drawing.Size(780, 550);

            pnEdit.Location = new Point(10, 50);
            pnEdit.Size = new System.Drawing.Size(780, 550);

            refreshListView();
        }

        private void refreshListView()
        {
            SubFunction.ClearlvItem(lvThongTin);

            for (int i = 0; i < Form_Main.list_Plugin.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = SubFunction.setSTT(i + 1);
                lvi.SubItems.Add(Form_Main.list_Plugin[i].GetPluginName());

                if (Form_Main.list_Plugin[i].GetPluginStatus())
                {
                    lvi.SubItems.Add("Enable");
                }
                else
                {
                    lvi.SubItems.Add("Disable");
                }

                lvi.SubItems.Add(Form_Main.list_Plugin[i].GetPluginType());
                lvi.SubItems.Add("Hồ sơ cá nhân"); //Danh sách các control được sử dụng plugin
                lvi.SubItems.Add(Form_Main.list_Plugin[i].GetPluginID());

                lvThongTin.Items.Add(lvi);
            }

            pbXoa.Enabled = false;
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
        }

        private bool TestPluginInstalled(string sName)
        {
            string sTest = Application.StartupPath + @"\Addin\" + sName;

            if (File.Exists(sTest))
            {
                Form_Notice frm = new Form_Notice("Plugin đang được sử dụng!", false);

                return false;
            }

            return true;
        }

        private void pbInstall_Click(object sender, EventArgs e)
        {
            if (tbPath.TextLength > 0)
            {
                string[] s = tbPath.Text.Split(new string[] { @"\" }, StringSplitOptions.None);

                if (TestPluginInstalled(s[s.Length - 1]))
                {
                    File.Copy(tbPath.Text, @"Addin\" + s[s.Length - 1], true);

                    tbPath.Text = "";

                    Form_Main.LoadPlugin();

                    refreshListView();
                }
            }
            else
            {
                Form_Notice frm_Notice = new Form_Notice("Vui lòng chọn Plugin!", false);
            }
        }

        private void pbInstall_MouseEnter(object sender, EventArgs e)
        {
            pbInstall.Image = Image.FromFile(@"Resources\HeThong\button_installplugin_mouseover.png");
        }

        private void pbInstall_MouseLeave(object sender, EventArgs e)
        {
            pbInstall.Image = Image.FromFile(@"Resources\HeThong\button_installplugin.png");
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            Form_Confirm frm_Confirm = new Form_Confirm("Đồng ý xóa " + lvThongTin.SelectedItems.Count + " dữ liệu?");

            if (frm_Confirm.Yes)
            {
                string sPath = Application.StartupPath + @"\Addin\" + Form_Main.list_Plugin[lvThongTin.SelectedIndices[0]].GetPluginFullName() + ".addin";
                Form_Main.list_Plugin.RemoveAt(lvThongTin.SelectedIndices[0]);

                if (File.Exists(sPath))
                {
                    try
                    {
                        File.Delete(sPath);

                        refreshListView();
                    }
                    catch
                    {
                        Form_Notice frm = new Form_Notice("Không thể xóa Plugin!", "Vui lòng thử lại.", false);
                    }
                }
            }
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_selected.png");
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            int iPlugin = lvThongTin.SelectedIndices[0];

            lbID.Text = Form_Main.list_Plugin[iPlugin].GetPluginID();
            lbPluginName.Text = Form_Main.list_Plugin[iPlugin].GetPluginName();
            lbPluginType.Text = Form_Main.list_Plugin[iPlugin].GetPluginType();

            if (Form_Main.list_Plugin[iPlugin].GetPluginStatus())
            {
                rbEnable.Checked = true;
            }
            else
            {
                rbDisable.Checked = true;
            }

            lbDescription.Text = Form_Main.list_Plugin[iPlugin].GetPluginDescription();

            pnMain.Visible = false;
            pnEdit.Visible = true;

            lbTitle.Text = "MANAGER" + lbPluginName.Text;
        }

        private void pbSua_MouseEnter(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_selected.png");
        }

        private void pbSua_MouseLeave(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
        }

        private void pbBrowse_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.OpenDialog("Plugin file", "addin");

            if (sPath != null)
            {
                tbPath.Text = sPath;
                tbPath.Select();
            }
        }

        private void pbBrowse_MouseEnter(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse_selected.png");
        }

        private void pbBrowse_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
        }

        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin.SelectedItems.Count > 0)
            {
                if (lvThongTin.SelectedItems.Count == 1)
                {
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
                }
                else
                {
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
                }

                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
            }
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            Form_Notice frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);

            if (frm_Notice.Yes)
            {
                pnMain.Visible = true;
                pnEdit.Visible = false;

                lbTitle.Text = "MANAGER";
                //lbSelect.Text = "";

                refreshListView();

                lvThongTin.SelectedItems.Clear();
            }
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (rbEnable.Checked)
            {
                Form_Main.list_Plugin[lvThongTin.SelectedIndices[0]].SetPluginStatus(true);
            }
            else
            {
                Form_Main.list_Plugin[lvThongTin.SelectedIndices[0]].SetPluginStatus(false);
            }

            pnMain.Visible = true;
            pnEdit.Visible = false;

            lbTitle.Text = "MANAGER";
            //lbSelect.Text = "";

            refreshListView();

            lvThongTin.SelectedItems.Clear();
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }
    }
}
