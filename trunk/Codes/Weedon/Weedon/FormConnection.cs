using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using Library;
using System.IO;

namespace Weedon
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

            SQLConnection.windowsAuthentication = true;
            pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_DISABLE);
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
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK);
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_DISABLE);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbServerName.Text) && 
                    !string.IsNullOrEmpty(tbUserName.Text) && 
                    !string.IsNullOrEmpty(tbPassword.Text))
                {
                    pbOk.Enabled = true;
                    pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK);
                }
                else
                {
                    pbOk.Enabled = false;
                    pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_DISABLE);
                }
            }
        }

        private void ConnectDB()
        {
            SQLConnection.serverName = tbServerName.Text;
            SQLConnection.userName = tbUserName.Text;
            SQLConnection.password = tbPassword.Text;

            if (!SQLConnection.CreateSQlConnection())
            {
                CommonFunc.SetLabel(lbError, lbError.Location.Y, this.Size, "Không thể kết nối đến server!");

                tbServerName.Focus();
                tbServerName.SelectAll();
            }
            else
            {
                FormMain.isConnected = true;
                WriteConfiguration();

                this.Close();
            }
        }

        private void WriteConfiguration()
        {
            StreamWriter swWriter = null;

            try
            {
                swWriter = new StreamWriter(Constant.DEFAULT_DATABASE_CONFIGURATION_FILE_NAME);

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
                swWriter.WriteLine(CryptoFunction.Crypto.EncryptDBPassText(tbPassword.Text));
                swWriter.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (swWriter != null)
                {
                    swWriter.Close();
                }
            }
        }

        private void pbOk_Click(object sender, EventArgs e)
        {
            ConnectDB();
        }

        private void pbOk_MouseEnter(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_MOUSEOVER);
        }

        private void pbOk_MouseLeave(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK);
        }

        private void rbWindowsAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Text = string.Empty;
            tbUserName.Enabled = false;

            tbPassword.Text = string.Empty;
            tbPassword.Enabled = false;

            SQLConnection.windowsAuthentication = true;

            ValidateInput();
        }

        private void rbSQLServerAu_CheckedChanged(object sender, EventArgs e)
        {
            tbUserName.Enabled = true;
            tbPassword.Enabled = true;

            SQLConnection.windowsAuthentication = false;

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
    }
}
