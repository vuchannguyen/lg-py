﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh
{
    public partial class UcChi : UserControl
    {
        public UcChi()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

                pbTraCuu.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEARCH);
                pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
                pbTotalPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);

                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcChi_Load(object sender, EventArgs e)
        {
            LoadResource();

            this.BringToFront();
        }
    }
}
