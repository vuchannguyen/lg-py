using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;
using CryptoFunction;
using DTO;
using DAO;
using BUS;
using System.Xml;

namespace HR_STG_for_Client
{
    public partial class UC_HoSoThamDu : UserControl
    {
        private List<HoSo_DTO> list_dto;
        private List<int> list_dto_HSTD;

        public static string sIDV;

        public UC_HoSoThamDu()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien_title.png");

                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
                pbHoSoCaNhan.Image = Image.FromFile(@"Resources\SuKien\icon_hosothamdu_hscn.png");
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");

                pbHuy_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");

                pnHoanTat.BackgroundImage = Image.FromFile(@"Resources\General\taskbar_bg.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void UC_HoSoThamDu_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnHoSoThamDu.Location = SubFunction.SetWidthCenter(this.Size, pnHoSoThamDu.Size, 34);
            gbHoanTat.Location = SubFunction.SetCenterLocation(this.Size, gbHoanTat.Size);

            lvThongTin_HSCN.LostFocus += new EventHandler(lvThongTin_HSCN_LostFocus);

            list_dto = new List<HoSo_DTO>();
            list_dto_HSTD = new List<int>();

            refreshListView();
        }

        private void lvThongTin_HSCN_LostFocus(object sender, EventArgs e)
        {
            lvThongTin_HSCN.SelectedItems.Clear();

            if (!lvThongTin_HSTD.Focused)
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }

        private void refreshListView()
        {
            list_dto = HoSo_BUS.LayDSHoSo();

            SubFunction.ClearlvItem(lvThongTin_HSCN);

            for (int i = 0; i < list_dto.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto[i].Ma.ToString();
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto[i].HoTen);
                lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_dto[i].MaNhomTrachVu).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto[i].Nganh);
                lvi.SubItems.Add(list_dto[i].DonVi);

                lvThongTin_HSCN.Items.Add(lvi);
            }
        }

        private void Add_HSTD(int iMa)
        {
            HoSo_DTO dto_Temp = HoSo_BUS.TraCuuHoSoTheoMa(iMa);

            ListViewItem lvi = new ListViewItem();
            lvi.Text = dto_Temp.Ma.ToString();
            lvi.SubItems.Add(SubFunction.setSTT(lvThongTin_HSTD.Items.Count + 1));
            lvi.SubItems.Add(dto_Temp.HoTen);
            lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_Temp.MaNhomTrachVu).Ten);
            lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(dto_Temp.MaTrachVu).Ten);
            lvi.SubItems.Add(dto_Temp.Nganh);
            lvi.SubItems.Add(dto_Temp.DonVi);

            lvThongTin_HSTD.Items.Add(lvi);
        }

        private void Delete_HSTD(ListViewItem lvi)
        {
            list_dto_HSTD.Remove(int.Parse(lvi.SubItems[0].Text));
            lvThongTin_HSTD.Items.Remove(lvi);
        }

        private void pbHuy_HSTD_Click(object sender, EventArgs e)
        {
            Form_Notice frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                this.Dispose();
            }
        }

        private void pbHuy_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbHuy_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbHuy_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbHoanTat_HSTD_Click(object sender, EventArgs e)
        {
            pnHoSoThamDu.Visible = false;
            gbHoanTat.Visible = true;
        }

        private void pbHoanTat_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }

        private void lvThongTin_HSTD_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin_HSTD, e);
        }

        private void lvThongTin_HSCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin_HSCN.SelectedItems.Count > 0)
            {
                pbTransfer.Enabled = true;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow.png");
            }
            else
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }

        private void lvThongTin_HSCN_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin_HSCN, e);
        }

        private void pbTransfer_Click(object sender, EventArgs e)
        {
            if (lvThongTin_HSCN.Focused)
            {
                for (int i = 0; i < lvThongTin_HSCN.SelectedItems.Count; i++)
                {
                    if (KiemTraHoSoCaNhanBiTrungHoSoThamDu(int.Parse(lvThongTin_HSCN.SelectedItems[i].SubItems[0].Text)))
                    {
                        //
                    }
                    else
                    {
                        list_dto_HSTD.Add(int.Parse(lvThongTin_HSCN.SelectedItems[i].SubItems[0].Text));
                        Add_HSTD(list_dto_HSTD[list_dto_HSTD.Count - 1]);

                        pbHoanTat_HSTD.Enabled = true;
                        pbHoanTat_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
                    }
                }
            }
        }

        private bool KiemTraHoSoCaNhanBiTrungHoSoThamDu(int iMa)
        {
            for (int i = 0; i < lvThongTin_HSTD.Items.Count; i++)
            {
                if (int.Parse(lvThongTin_HSTD.Items[i].SubItems[0].Text) == iMa)
                {
                    string[] sTen = lvThongTin_HSTD.Items[i].SubItems[2].Text.Split();
                    Form_Notice frm = new Form_Notice("Kiểm tra Hồ sơ " + sTen[sTen.Length - 1] + " bị trùng!", false);

                    return true;
                }
            }

            return false;
        }

       

        private void pbHuy_Click(object sender, EventArgs e)
        {
            pnHoSoThamDu.Visible = true;
            gbHoanTat.Visible = false;
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
            XmlDocument XmlDoc = XMLConnection.SelectXmlDocConfig();
            string sContent = XmlDoc.FirstChild.OuterXml.Split(new char[] {'>'})[0] + ">";

            sContent += "<HS>"; //bat dau ghi thong tin Ho so
            foreach (int iMa in list_dto_HSTD)
            {
                sContent += HoSo_BUS.Insert2String(HoSo_BUS.TraCuuHoSoTheoMa(iMa), sIDV);
            }
            sContent += "</HS>";

            sContent += "<HL>"; //bat dau ghi thong tin Huan luyen
            foreach (int iMa in list_dto_HSTD)
            {
                List<HoSo_HuanLuyen_DTO> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(iMa);

                foreach (HoSo_HuanLuyen_DTO dto_Temp in list_HoSo_HuanLuyen)
                {
                    sContent += HuanLuyen_BUS.Insert2String(HuanLuyen_BUS.TraCuuHuanLuyenTheoMa(dto_Temp.MaHuanLuyen));
                }
            }
            sContent += "</HL>";

            sContent += "<HS_HL>"; //bat dau ghi thong tin Huan luyen
            foreach (int iMa in list_dto_HSTD)
            {
                List<HoSo_HuanLuyen_DTO> list_HoSo_HuanLuyen = HoSo_HuanLuyen_BUS.TraCuuDSHuanLuyenTheoMaHoSo(iMa);

                foreach (HoSo_HuanLuyen_DTO dto_Temp in list_HoSo_HuanLuyen)
                {
                    sContent += HoSo_HuanLuyen_BUS.Insert2String(dto_Temp);
                }
            }
            sContent += "</HS_HL>";

            sContent += "</HR-STG>";

            if (Crypto.EncryptData(sContent, tbSave.Text))
            {
                //Form_Notice frm = new Form_Notice("Tạo file thành công.", false);

                this.Dispose();
            }
            else
            {
                Form_Notice frm = new Form_Notice("Tạo file thất bại!", "Vui lòng thử lại.", false);
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

        private void btBrowse_Click(object sender, EventArgs e)
        {
            tbSave.Text = File_Function.SaveDialog("HR data ", "hrd");

            if (tbSave.Text.Length > 0)
            {
                tbSave.Select();

                pbHoanTat.Enabled = true;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");
            }
        }

        private void lvThongTin_HSTD_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control)
            //{
            //    switch (e.KeyCode)
            //    {
            //        case Keys.C:
            //            {
            //                edit_item.Copy();
            //                break;
            //            }
            //        case Keys.X:
            //            {
            //                edit_item.Cut();
            //                break;
            //            }
            //        case Keys.V:
            //            {
            //                edit_item.Paste(str);
            //                flag = true;
            //                break;
            //            }
            //        case Keys.Left:
            //            {
            //                Back();
            //                break;
            //            }
            //        case Keys.Right:
            //            {
            //                Forward();
            //                break;
            //            }
            //        default: return;
            //    }
            //}

            //if (e.Shift)
            //{
            //    if (e.KeyCode == Keys.Delete)
            //    {
            //        edit_item.DeletePermanently();
            //        flag = true;
            //    }
            //}
            //else
            //{
            //    if (e.KeyCode == Keys.Delete)
            //    {
            //        edit_item.DeleteToRecycleBin();
            //        flag = true;
            //    }
            //}

            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                while (lvThongTin_HSTD.SelectedItems.Count > 0)
                {
                    Delete_HSTD(lvThongTin_HSTD.SelectedItems[0]);
                }

                if (lvThongTin_HSTD.Items.Count > 0)
                {
                    for (int i = 0; i < lvThongTin_HSTD.Items.Count; i++)
                    {
                        lvThongTin_HSTD.Items[i].SubItems[1].Text = SubFunction.setSTT(i + 1);
                    }
                }
                else
                {
                    pbHoanTat_HSTD.Enabled = false;
                    pbHoanTat_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");
                }
            }

            //switch (e.KeyCode)
            //{
            //    case Keys.F2:
            //        {
            //            edit_item.Rename();
            //            break;
            //        }
            //    case Keys.F3:
            //        {
            //            Sign();
            //            break;
            //        }
            //    case Keys.F4:
            //        {
            //            Check();
            //            break;
            //        }
            //    case Keys.F5:
            //        {
            //            Refresh();
            //            break;
            //        }
            //    case Keys.F6:
            //        {
            //            Encrypt();
            //            break;
            //        }
            //    case Keys.F7:
            //        {
            //            Decrypt();
            //            break;
            //        }
            //}
        }
    }
}
