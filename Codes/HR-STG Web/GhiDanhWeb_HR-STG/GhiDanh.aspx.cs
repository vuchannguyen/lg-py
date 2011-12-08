using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO;
using BUS;
using System.IO;
using System.Xml;
using CryptoFunction;
using System.Net.Mail;
using System.Net;

namespace GhiDanhWeb_HR_STG
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private List<string> list_IDV;
        private List<string> list_NhomTrachVu;
        private List<string> list_TrachVu;



        protected void Page_Load(object sender, EventArgs e)
        {
            New();

            if (!IsPostBack)
            {
                if (SetSuKienPath(@"Resources/CoLau711.cfg"))
                {
                    AddIDV(ddlIDV);
                }

                if (SetNTV_TVPath(@"Resources/NTV_TV.cfg"))
                {
                    if (AddNhomTrachVu(ddlNhomTrachVu))
                    {
                        LayDSTrachVuTheoMaNhomTrachVu_DropDownList(ddlTrachVu, list_NhomTrachVu[0]);
                    }
                }
            }
            else
            {
                List<IDV_DTO> list_Temp = IDV_BUS.LayDSIDV();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_IDV.Add(list_Temp[i].Ma);
                }

                List<NhomTrachVu_DTO> list_Temp1 = NhomTrachVu_BUS.LayDSNhomTrachVu();
                for (int i = 0; i < list_Temp1.Count; i++)
                {
                    list_NhomTrachVu.Add(list_Temp1[i].Ma);
                }

                List<TrachVu_DTO> list_Temp2 = TrachVu_BUS.LayDSTrachVu();
                for (int i = 0; i < list_Temp2.Count; i++)
                {
                    list_TrachVu.Add(list_Temp2[i].Ma);
                }
            }
        }

        private void New()
        {
            list_IDV = new List<string>();
            list_NhomTrachVu = new List<string>();
            list_TrachVu = new List<string>();
        }

        private bool SetSuKienPath(string sPath)
        {
            sPath = Server.MapPath(sPath);

            if (File.Exists(sPath))
            {
                XMLConnection_BUS.SetSuKienPath(sPath);
            }
            else
            {
                Response.Write("Vui lòng kiểm tra tập tin config trong Resoruces!");

                return false;
            }

            return true;
        }

        private bool SetNTV_TVPath(string sPath)
        {
            sPath = Server.MapPath(sPath);

            if (File.Exists(sPath))
            {
                XMLConnection_BUS.SetNTV_TVPath(sPath);
            }
            else
            {
                Response.Write("Vui lòng kiểm tra tập tin config trong Resoruces!");

                return false;
            }

            return true;
        }

        private bool AddIDV(DropDownList ddl)
        {
            List<IDV_DTO> list_Temp = IDV_BUS.LayDSIDV();

            if (list_Temp.Count > 0)
            {
                list_IDV.Clear();
                ddl.Items.Clear();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_IDV.Add(list_Temp[i].IDV);
                    ddlIDV.Items.Add(list_Temp[i].DienGiai);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool AddNhomTrachVu(DropDownList ddl)
        {
            List<NhomTrachVu_DTO> list_Temp = NhomTrachVu_BUS.LayDSNhomTrachVu();

            if (list_Temp.Count > 0)
            {
                list_NhomTrachVu.Clear();
                ddl.Items.Clear();

                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_NhomTrachVu.Add(list_Temp[i].Ma);
                    ddl.Items.Add(list_Temp[i].Ten);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool LayDSTrachVuTheoMaNhomTrachVu_DropDownList(DropDownList ddl, string sMaNhomTrachVu)
        {
            XmlNodeList list_Temp = XMLConnection_BUS.CreateXmlDocNTV_TV("TrachVu");

            if (list_Temp != null)
            {
                list_TrachVu.Clear();
                ddl.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (list_Temp[i]["MaNhomTrachVu"].InnerText == sMaNhomTrachVu)
                    {
                        list_TrachVu.Add(list_Temp[i].Attributes["Ma"].InnerText);
                        ddl.Items.Add(list_Temp[i]["Ten"].InnerText);
                    }
                }
            }
            else
            {
                Response.Write("Vui lòng kiểm tra tập tin config trong Resoruces!");

                return false;
            }

            return true;
        }

        protected void ddlNhomTrachVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayDSTrachVuTheoMaNhomTrachVu_DropDownList(ddlTrachVu, list_NhomTrachVu[ddlNhomTrachVu.SelectedIndex]);
        }

        protected void btHoanTat_Click(object sender, EventArgs e)
        {
            if (EncryptAndSendMail())
            {
                Response.Write("Đã gửi hồ sơ về Ban Tổ Chức.");
            }
            else
            {
                Response.Write("Có lỗi! Vui lòng gửi lại.");
            }
        }



        #region Encrypt and Send Email
        private HoSo_DTO NewHoSo()
        {
            HoSo_DTO dto = new HoSo_DTO();

            dto.Ma = 1;
            dto.NgayCapNhat = "";

            //dto.Avatar = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(pbAvatar.Image));

            dto.MaIDV = int.Parse(list_IDV[ddlIDV.SelectedIndex]);
            dto.MaNhomTrachVu = list_NhomTrachVu[ddlNhomTrachVu.SelectedIndex];
            dto.MaTrachVu = list_TrachVu[ddlTrachVu.SelectedIndex];

            dto.HoTen = tbHoTen.Text;
            //dto.NgaySinh = DateTime.Parse(lbNgaySinh.Text);

            if (rbNam.Checked)
            {
                dto.GioiTinh = rbNam.Text;
            }

            if (rbNu.Checked)
            {
                dto.GioiTinh = rbNu.Text;
            }

            dto.QueQuan = tbQueQuan.Text;
            dto.TrinhDoHocVan = tbTrinhDoHocVan.Text;
            dto.TonGiao = tbTonGiao.Text;
            dto.DiaChi = tbDiaChi.Text;
            dto.DienThoaiLienLac = tbDienThoaiLienLac.Text;
            dto.Email = tbEmail.Text;

            if (rbAu.Checked)
            {
                dto.Nganh = rbAu.Text;
            }

            if (rbThieu.Checked)
            {
                dto.Nganh = rbThieu.Text;
            }

            if (rbKha.Checked)
            {
                dto.Nganh = rbKha.Text;
            }

            if (rbTrang.Checked)
            {
                dto.Nganh = rbTrang.Text;
            }

            if (rbKhac.Checked)
            {
                dto.Nganh = rbKhac.Text;
            }

            dto.DonVi = tbDonVi.Text;
            dto.LienDoan = tbLienDoan.Text;
            dto.Dao = tbDao.Text;
            dto.Chau = tbChau.Text;
            //dto.NgayTuyenHua = dtpNgayTuyenHua.Value;
            dto.TruongNhanLoiHua = tbTruongNhanLoiHua.Text;
            dto.TrachVuTaiDonVi = tbTrachVuTaiDonVi.Text;
            dto.TrachVuNgoaiDonVi = tbTrachVuNgoaiDonVi.Text;
            dto.TenRung = tbTenRung.Text;
            dto.GhiChu = tbGhiChu.Text;

            dto.NgheNghiep = tbNgheNghiep.Text;
            if (chbNutDay.Checked) //1
            {
                dto.NutDay = 1;
            }
            else
            {
                dto.NutDay = 0;
            }

            if (chbPhuongHuong.Checked) //2
            {
                dto.PhuongHuong = 1;
            }
            else
            {
                dto.PhuongHuong = 0;
            }

            if (chbCuuThuong.Checked) //3
            {
                dto.CuuThuong = 1;
            }
            else
            {
                dto.CuuThuong = 0;
            }

            if (chbTruyenTin.Checked) //4
            {
                dto.TruyenTin = 1;
            }
            else
            {
                dto.TruyenTin = 0;
            }

            if (chbTroChoi.Checked) //5
            {
                dto.TroChoi = 1;
            }
            else
            {
                dto.TroChoi = 0;
            }

            if (chbLuaTrai.Checked) //6
            {
                dto.LuaTrai = 1;
            }
            else
            {
                dto.LuaTrai = 0;
            }

            dto.SoTruong = tbSoTruong.Text;

            return dto;
        }

        private bool EncryptData(HoSo_DTO dto, string sPath)
        {
            try
            {
                XmlDocument XmlDoc = XMLConnection_BUS.SelectXmlDocConfig();
                string sContent = XmlDoc.FirstChild.OuterXml.Split(new char[] { '>' })[0] + ">";

                sContent += "<HS>"; //bat dau ghi thong tin Ho so
                sContent += HoSo_BUS.Insert2String(dto);
                sContent += "</HS>";

                sContent += "<HL>"; //bat dau ghi thong tin Huan luyen
                //Khong co HL trong web
                sContent += "</HL>";

                sContent += "<HS_HL>"; //bat dau ghi thong tin Huan luyen
                //Khong co HS_HL trong web
                sContent += "</HS_HL>";

                sContent += "</HR-STG>";

                if (!Crypto.EncryptData(sContent, sPath))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool SendMail(string sAddress, string sName, string sAttachPath)
        {
            try
            {
                MailMessage msgMail = new MailMessage();

                msgMail.To.Add(sAddress);
                msgMail.From = new MailAddress("ghidanh.colau711@gmail.com");
                msgMail.Subject = "Hồ sơ " + sName;
                msgMail.Body = "Hồ sơ " + sName;
                msgMail.Attachments.Add(new Attachment(sAttachPath));

                // SMTP Details
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                NetworkCredential SMTPUserInfo = new NetworkCredential("ghidanh.colau711@gmail.com", "hoicolau711");
                smtpClient.Credentials = SMTPUserInfo;

                smtpClient.Send(msgMail);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool EncryptAndSendMail()
        {
            HoSo_DTO dto = NewHoSo();
            string sPath = Path.GetTempPath() + dto.HoTen + ".hrd";

            if (EncryptData(dto, sPath))
            {
                if (!SendMail("ghidanh.colau711@gmail.com", dto.HoTen, sPath))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}