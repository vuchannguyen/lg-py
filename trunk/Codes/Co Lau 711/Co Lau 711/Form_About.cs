using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace About
{
    partial class Form_About : Form
    {
        public Form_About()
        {
            InitializeComponent();

            this.ShowDialog();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_About_Load(object sender, EventArgs e)
        {

        }
    }
}
