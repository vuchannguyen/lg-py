using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using PluginInterface;
using DTO;
using DAO;

namespace VNSC
{
    public partial class Form_CongCuHoTro : Form
    {
        private List<HoSo> list_dto;

        public Form_CongCuHoTro()
        {
            InitializeComponent();
        }

        public Form_CongCuHoTro(List<IPlugin> list_Plugin, List<HoSo> list)
        {
            InitializeComponent();

            LoadPic();

            list_dto = list;

            if (list_Plugin.Count > 0 && list_Plugin[0].GetPluginStatus() && list_dto != null)
            {
                pnCongCuHoTro.Visible = true;
                //pbExcel.Enabled = true;
            }
            else
            {
                pnCongCuHoTro.Visible = false;
                //pbExcel.Enabled = false;
            }
        }

        private void LoadPic()
        {
            try
            {
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbTitle.Image = Image.FromFile(@"Resources\CongCuHoTro\icon_plugin_function.png");
                pbExcel.Image = Image.FromFile(@"Resources\CongCuHoTro\icon_excel.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void Form_CongCuHoTro_Load(object sender, EventArgs e)
        {
            
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Form_Main.bExport = false;
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

        private HoSo_DTO ConvertHoSo2HoSo_DTO(HoSo dto)
        {
            HoSo_DTO dto_Temp = new HoSo_DTO();

            dto_Temp.Ma = int.Parse(dto.Ma.Substring(0, 4));
            dto_Temp.NgayCapNhat = dto.NgayCapNhat;

            dto_Temp.MaIDV = (int)dto.MaIDV;
            dto_Temp.MaNhomTrachVu = dto.MaNhomTrachVu;
            dto_Temp.MaTrachVu = dto.MaTrachVu;
            dto_Temp.HoTen = dto.HoTen;
            dto_Temp.NgaySinh = (DateTime)dto.NgaySinh;
            dto_Temp.GioiTinh = dto.GioiTinh;
            dto_Temp.QueQuan = dto.QueQuan;
            dto_Temp.TrinhDoHocVan = dto.TrinhDoHocVan;
            dto_Temp.TonGiao = dto.TonGiao;
            dto_Temp.DiaChi = dto.DiaChi;
            dto_Temp.DienThoaiLienLac = dto.DienThoaiLienLac;
            dto_Temp.Email = dto.Email;

            dto_Temp.Nganh = dto.Nganh;
            dto_Temp.DonVi = dto.DonVi;
            dto_Temp.LienDoan = dto.LienDoan;
            dto_Temp.Dao = dto.Dao;
            dto_Temp.Chau = dto.Chau;
            dto_Temp.NgayTuyenHua = (DateTime)dto.NgayTuyenHua;
            dto_Temp.TruongNhanLoiHua = dto.TruongNhanLoiHua;
            dto_Temp.TrachVuTaiDonVi = dto.TrachVuTaiDonVi;
            dto_Temp.TrachVuNgoaiDonVi = dto.TrachVuNgoaiDonVi;
            dto_Temp.TenRung = dto.TenRung;
            dto_Temp.GhiChu = dto.GhiChu;

            dto_Temp.NgheNghiep = dto.NgheNghiep;
            dto_Temp.NutDay = (int)dto.NutDay;
            dto_Temp.PhuongHuong = (int)dto.PhuongHuong;
            dto_Temp.CuuThuong = (int)dto.CuuThuong;
            dto_Temp.TruyenTin = (int)dto.TruyenTin;
            dto_Temp.TroChoi = (int)dto.TroChoi;
            dto_Temp.LuaTrai = (int)dto.LuaTrai;
            dto_Temp.SoTruong = dto.SoTruong;

            return dto_Temp;
        }

        private void pbExcel_Click(object sender, EventArgs e)
        {
            //string sContent = "";

            //foreach (HoSo dto in list_dto)
            //{
            //    HoSo_DTO dto_Temp = ConvertHoSo2HoSo_DTO(dto);

            //    sContent += HoSo_DAO.Insert2String(dto_Temp);
            //}

            //UserControl uc_ExportExcel = Plugin_Function.GetPluginUC(Form_Main.list_Plugin[0], sContent);
            ////UC_ExportExcel uc_ExportExcel = new UC_ExportExcel(new List<HoSo>());
            //this.Controls.Add(uc_ExportExcel);
            //uc_ExportExcel.BringToFront();

            Form_Main.bExport = true;
            this.Close();
        }

        private void pbExcel_MouseEnter(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(@"Resources\CongCuHoTro\icon_excel_mouseover.png");
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(@"Resources\CongCuHoTro\icon_excel.png");
        }
    }
}
