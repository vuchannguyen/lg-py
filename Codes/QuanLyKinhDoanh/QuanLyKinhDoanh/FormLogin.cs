using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DTO;
using BUS;
using CryptoFunction;

namespace QuanLyKinhDoanh
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbTitle.Image = Image.FromFile(ConstantResource.MAIN_LOGIN);
                pbLogin.Image = Image.FromFile(ConstantResource.MAIN_ICON_KEY);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            LoadResource();

            lbError.Text = string.Empty;

            this.BackColor = Color.White;

            ValidateInput();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // look for the expected key
            if (keyData == Keys.Enter)
            {
                Login();

                // eat the message to prevent it from being passed on
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbUserName.Text) &&
                !string.IsNullOrEmpty(tbPassword.Text)
                )
            {
                pbLogin.Enabled = true;
                pbLogin.Image = Image.FromFile(ConstantResource.MAIN_ICON_KEY);
            }
            else
            {
                pbLogin.Enabled = false;
                pbLogin.Image = Image.FromFile(ConstantResource.MAIN_ICON_KEY_DISABLE);
            }
        }

        private void Login()
        {
            FormMain.user = UserBus.GetByUserName(tbUserName.Text);

            if (FormMain.user != null)
            {
                string s = Crypto.EncryptText(tbPassword.Text);

                if (FormMain.user.Password == Crypto.EncryptText(tbPassword.Text))
                {
                    this.Dispose();
                }
                else
                {
                    lbError.Text = Constant.MESSAGE_LOGIN_WRONG_PASS;
                }
            }
            else
            {
                lbError.Text = Constant.MESSAGE_LOGIN_WRONG_USERNAME;
            }
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = string.Empty;

            ValidateInput();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            lbError.Text = string.Empty;

            ValidateInput();
        }

        private void pbLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void pbLogin_MouseEnter(object sender, EventArgs e)
        {
            pbLogin.Image = Image.FromFile(ConstantResource.MAIN_ICON_KEY_MOUSEOVER);
        }

        private void pbLogin_MouseLeave(object sender, EventArgs e)
        {
            pbLogin.Image = Image.FromFile(ConstantResource.MAIN_ICON_KEY);
        }
    }
}
