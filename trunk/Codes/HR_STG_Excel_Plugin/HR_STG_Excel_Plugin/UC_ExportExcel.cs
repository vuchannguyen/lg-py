using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DTO;

namespace HR_STG_Excel_Plugin
{
    public partial class UC_ExportExcel : UserControl
    {
        public static  UserControl uc;

        public static bool pluginStatus = true;

        private UC_ListViewEx uc_ListViewEx;
        private List<HoSo_DTO> list_dto;
        private string sList_HoSo;

        public UC_ExportExcel()
        {
            InitializeComponent();
        }

        //public UC_ExportExcel(List<HoSo_DTO> list_HS)
        //{
        //    InitializeComponent();

        //    list_dto = list_HS;
        //}

        public UC_ExportExcel(string sContent)
        {
            InitializeComponent();

            sList_HoSo = sContent;
        }

        private void LoadPic()
        {
            try
            {
                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");

                pbTitle.Image = Image.FromFile(@"Resources\Export\export_title.png");
            }
            catch
            {
                this.Dispose();
                MessageBox.Show("Kiểm tra thư mục Resource!");
            }
        }

        private void UC_ExportExcel_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnQuanLy.Location = SubFunction.SetWidthCenter(this.Size, pnQuanLy.Size, 100);

            uc_ListViewEx = new UC_ListViewEx(sList_HoSo);
            pnQuanLy.Controls.Add(uc_ListViewEx);
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            string sPath = File_Function.SaveDialog("Excel File", "xls");

            if (sPath != null)
            {
                if (Office_Function.ExportListViewData2Excel07("HR-STG", sPath, uc_ListViewEx.LvEx))
                {
                    pbHuy_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Không thể xuất dữ liệu!");
                }
            }
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
