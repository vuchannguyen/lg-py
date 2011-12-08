using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;
using System.IO;

namespace VNSC
{
    public partial class Form_DB_Definition : Form
    {
        public Form_DB_Definition()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                this.BackgroundImage = Image.FromFile(@"Resources\database_def.jpg");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }
        private void Form_DB_Definition_Load(object sender, EventArgs e)
        {
            LoadPic();

            lbError.Text = "";

            SQL_Connection.bWindowsAuthentication = true;
        }

        private void pbOk_Click(object sender, EventArgs e)
        {
            //
            SubFunction.SetError(lbError, lbError.Location.Y, this.Size, "Vui lòng đợi kết nối đến server.");

            string sPath = @"SQL.con";
            SQL_Connection.sServerName = tbServerName.Text;
            SQL_Connection.sUserName = tbUserName.Text;
            SQL_Connection.sPassword = tbPassword.Text;

            if (SQL_Connection.CreateSQlConnection() == null)
            {
                SubFunction.SetError(lbError, lbError.Location.Y, this.Size, "Không thể kết nối đến server!");

                tbServerName.Focus();
                tbServerName.SelectAll();
            }
            else
            {
                StreamWriter swWriter = new StreamWriter(sPath);
                if (rbWindowsAu.Checked)
                {
                    swWriter.WriteLine(rbWindowsAu.Text);
                }
                else
                {
                    swWriter.WriteLine(rbSQLServerAu.Text);
                }
                swWriter.WriteLine(tbServerName.Text);
                swWriter.WriteLine(tbUserName.Text);
                swWriter.WriteLine(tbPassword.Text);
                swWriter.Flush();
                swWriter.Close();

                Form_Main.bDB_Def = true;

                this.Close();
            }
        }

        private void pbOk_MouseEnter(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_selected.png");
        }

        private void pbOk_MouseLeave(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
        }

        private void tbServerName_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (rbWindowsAu.Checked)
            {
                if (tbServerName.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
            else
            {
                if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (rbWindowsAu.Checked)
            {
                if (tbServerName.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
            else
            {
                if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (rbWindowsAu.Checked)
            {
                if (tbServerName.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
            else
            {
                if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
                    lbError.Text = "";
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                }
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit_mouseover.png");
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");
        }

        private void rbWindowsAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Text = "";
            tbUserName.Enabled = false;

            tbPassword.Text = "";
            tbPassword.Enabled = false;

            SQL_Connection.bWindowsAuthentication = true;

            tbServerName_TextChanged(sender, e);
        }

        private void rbSQLServerAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Enabled = true;
            tbPassword.Enabled = true;

            SQL_Connection.bWindowsAuthentication = false;

            tbServerName_TextChanged(sender, e);
        }

        private void Form_DB_Definition_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void Form_DB_Definition_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                pbOk_Click(sender, e);
            }
        }

        private void tbServerName_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbServerName_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                pbOk_Click(sender, e);
            }
            else
            {
                if (rbWindowsAu.Checked)
                {
                    if (SubFunction.tbPass(e) && tbServerName.Text.Length > 0)
                    {
                        pbOk_Click(sender, e);
                    }
                }
                else
                {
                    SubFunction.NextFocus(tbUserName, e);
                }
            }
        }

        private void tbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                if (SubFunction.tbPass(e))
                {
                    pbOk_Click(sender, e);
                }
            }
            else
            {
                SubFunction.NextFocus(tbPassword, e);
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbServerName.Text.Length > 0 && tbUserName.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                if (SubFunction.tbPass(e))
                {
                    pbOk_Click(sender, e);
                }
            }
            else
            {
                SubFunction.NextFocus(tbServerName, e);
            }
        }
    }
}
