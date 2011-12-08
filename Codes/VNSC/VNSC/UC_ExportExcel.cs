using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;
using BUS;

namespace VNSC
{
    public partial class UC_ExportExcel : UserControl
    {
        UC_ListViewEx uc_ListViewEx;
        private List<HoSo> list_dto;

        public UC_ExportExcel()
        {
            InitializeComponent();
        }

        public UC_ExportExcel(List<HoSo> list_HS)
        {
            InitializeComponent();

            list_dto = list_HS;
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
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_ExportExcel_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnQuanLy.Location = SubFunction.SetWidthCenter(this.Size, pnQuanLy.Size, 100);

            uc_ListViewEx = new UC_ListViewEx(list_dto);
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
                    Form_Notice frm = new Form_Notice("Không thể xuất dữ liệu!", false);
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
