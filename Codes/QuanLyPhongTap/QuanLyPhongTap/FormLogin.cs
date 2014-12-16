using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using Model;
using Controller;
using CryptoFunction;

namespace QuanLyPhongTap
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
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

        private bool Login()
        {
            bool res = false;

            try
            {
                res = UserImp.Login(tbUserName.Text, tbPassword.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return res;
        }

        private void ValidateInput()
        {
            if (!String.IsNullOrEmpty(tbUserName.Text))
            {
                btLogin.Enabled = true;
            }
            else
            {
                btLogin.Enabled = false;
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

        private void btLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
