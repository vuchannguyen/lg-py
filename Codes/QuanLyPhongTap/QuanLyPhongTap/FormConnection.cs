using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Library;
using System.IO;
using Controller.Common;

namespace QuanLyPhongTap
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            lbError.Text = "";

            if (CommonFunction.AutoConnect())
            {
                this.Hide();
                FormLogin frm = new FormLogin();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                SqlConnection.WindowsAuthentication = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // look for the expected key
            if (keyData == Keys.Enter)
            {
                ConnectDB();

                // eat the message to prevent it from being passed on
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ValidateInput()
        {
            lbError.Text = "";

            if (rbWindowsAu.Checked)
            {
                if (!string.IsNullOrEmpty(tbServerName.Text))
                {
                    btConnect.Enabled = true;
                }
                else
                {
                    btConnect.Enabled = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbServerName.Text) && 
                    !string.IsNullOrEmpty(tbUserName.Text) && 
                    !string.IsNullOrEmpty(tbPassword.Text))
                {
                    btConnect.Enabled = true;
                }
                else
                {
                    btConnect.Enabled = false;
                }
            }
        }

        private void ConnectDB()
        {
            SqlConnection.ServerName = tbServerName.Text;
            SqlConnection.UserName = tbUserName.Text;
            SqlConnection.Password = tbPassword.Text;

            if (!SqlConnection.NewConnection())
            {
                CommonFunc.SetLabel(lbError, lbError.Location.Y, this.Size, "Không thể kết nối đến server!");
                tbServerName.Focus();
                tbServerName.SelectAll();
            }
            else
            {
                this.Hide();
                WriteConfiguration();
                FormLogin frm = new FormLogin();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void WriteConfiguration()
        {
            try
            {
                if (rbWindowsAu.Checked)
                {
                    CommonFunction.WriteConfiguration(true, tbServerName.Text, tbUserName.Text, tbPassword.Text);
                }
                else
                {
                    CommonFunction.WriteConfiguration(false, tbServerName.Text, tbUserName.Text, tbPassword.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbWindowsAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Text = string.Empty;
            tbUserName.Enabled = false;
            tbPassword.Text = string.Empty;
            tbPassword.Enabled = false;
            SqlConnection.WindowsAuthentication = true;
            ValidateInput();
        }

        private void rbSQLServerAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Enabled = true;
            tbPassword.Enabled = true;
            SqlConnection.WindowsAuthentication = false;
            ValidateInput();
        }

        private void tbServerName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            ConnectDB();
        }
    }
}
