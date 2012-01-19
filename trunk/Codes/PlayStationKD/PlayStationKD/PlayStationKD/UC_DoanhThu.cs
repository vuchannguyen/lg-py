using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;
using System.IO;

namespace PlayStationKD
{
    public partial class UC_DoanhThu : UserControl
    {
        private bool bEnter = false;
        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        public UC_DoanhThu()
        {
            InitializeComponent();
        }

        private void UC_DoanhThu_Load(object sender, EventArgs e)
        {
            this.Size = new Size(680, 600);

            pnDoanhThu_Ngay.Location = new Point(2, 3);

            dateTimePickerDT.Value = DateTime.Now;
            showDoanhThu_Ngay(dateTimePickerDT.Value.ToString("ddMMyyyy") + ".txt");
        }



        #region SubFunctions
        private Point SetLocation(Size size)
        {
            return (new Point((this.Width - size.Width) / 2, (this.Height - 30 - size.Height) / 2));
        }

        private void SortColumn(ListView lv, ColumnClickEventArgs e)
        {
            lv.ListViewItemSorter = lvwColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            lv.Sort();
        }

        private void ClearlvItem(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                lv.Items.Clear();
            }
        }

        private void MuteEnterPress(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bEnter = false;
            }
        }

        private String setSTT(int i)
        {
            if (i < 10)
            {
                return "00" + i.ToString();
            }
            else
            {
                if (i >= 10 && i <= 99)
                {
                    return "0" + i.ToString();
                }

                return i.ToString();
            }
        }
        #endregion



        private void showDoanhThu_Ngay(String sFileName)
        {
            try
            {
                string sFolderYear = sFileName.Substring(4, 4);
                string sFolderMonth = Path.Combine(sFolderYear, sFileName.Substring(2, 2));
                sFileName = Path.Combine(sFolderMonth, sFileName);

                lbTongTienDT.Text = "Tổng tiền:   " + Save.getTongTien(sFileName).ToString();
                List<String> lsContent = Save.GetContent(sFileName);

                lvThuChiDT.Items.Clear();
                for (int i = 1; i < lsContent.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = setSTT(i);
                    String sTemp;
                    int iTemp;

                    if (int.TryParse(lsContent[i].Substring(0, 2), out iTemp))
                    {
                        iTemp = 8;
                    }
                    else
                    {
                        iTemp = 7;
                    }

                    lvi.SubItems.Add(lsContent[i].Substring(0, iTemp));
                    iTemp += 3;

                    if (lsContent[i].EndsWith("(CHI)"))
                    {
                        lvi.SubItems.Add("");
                        sTemp = lsContent[i].Remove(lsContent[i].Length - 5);
                        lvi.SubItems.Add(sTemp.Substring(iTemp));
                    }
                    else
                    {
                        sTemp = lsContent[i].Remove(lsContent[i].Length - 5);
                        lvi.SubItems.Add(sTemp.Substring(iTemp));
                        lvi.SubItems.Add("");
                    }

                    lvThuChiDT.Items.Add(lvi);
                }
            }
            catch
            {
                lbTongTienDT.Text = "Tổng tiền:   0";
                lvThuChiDT.Items.Clear();
            }
        }



        private List<string> setNgayTrongThang(int sThang, int sNam)
        {
            List<string> list = new List<string>();
            int iSoNgay = 0;

            if (sThang == 2)
            {
                if ((sNam % 4 == 0 && sNam % 100 != 0) || (sNam % 400 == 0))
                {
                    iSoNgay = 29;
                }
                else
                {
                    iSoNgay = 28;
                }
            }
            else
            {
                if (sThang == 4 || sThang == 6 || sThang == 9 || sThang == 11)
                {
                    iSoNgay = 30;
                }
                else
                {
                    iSoNgay = 31;
                }
            }

            for (int i = 1; i <= iSoNgay; i++)
            {
                if (i < 10)
                {
                    list.Add("0" + i.ToString());
                }
                else
                {
                    list.Add(i.ToString());
                }
            }


            return list;
        }



        private void showDoanhThu_Thang(string sThang, string sNam)
        {
            try
            {
                long lTongThu = 0;

                List<string> list_Ngay = setNgayTrongThang(int.Parse(sThang), int.Parse(sNam));
                string sFolderYear = sNam.ToString();
                string sFolderMonth = Path.Combine(sFolderYear, sThang.ToString());

                lvThuChiDT.Items.Clear();
                for (int i = 0; i < list_Ngay.Count; i++)
                {
                    long lTongChiNgay = 0;
                    string sFileName = list_Ngay[i] + sThang + sNam + ".txt";
                    sFileName = Path.Combine(sFolderMonth, sFileName);

                    ListViewItem lvi = new ListViewItem();

                    if (!File.Exists(sFileName))
                    {
                        lvi.Text = setSTT(i + 1);
                        lvi.SubItems.Add(list_Ngay[i] + "/" + sThang + "/" + sNam);
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("0");

                        lvThuChiDT.Items.Add(lvi);

                        continue;
                    }

                    List<String> lsContent = Save.GetContent(sFileName);

                    for (int j = 1; j < lsContent.Count; j++)
                    {
                        string sTemp;
                        int iTemp;

                        if (int.TryParse(lsContent[j].Substring(0, 2), out iTemp))
                        {
                            iTemp = 8;
                        }
                        else
                        {
                            iTemp = 7;
                        }

                        iTemp += 3;

                        if (lsContent[j].EndsWith("(CHI)"))
                        {
                            sTemp = lsContent[j].Remove(lsContent[j].Length - 5);
                            lTongChiNgay += long.Parse(sTemp.Substring(iTemp, 8));
                        }
                    }

                    lTongThu += Save.getTongTien(sFileName);

                    lvi.Text = setSTT(i + 1);
                    lvi.SubItems.Add(list_Ngay[i] + "/" + sThang + "/" + sNam);
                    lvi.SubItems.Add(Save.getTongTien(sFileName).ToString());
                    lvi.SubItems.Add(lTongChiNgay.ToString());

                    lvThuChiDT.Items.Add(lvi);
                }

                lbTongTienDT.Text = "Tổng tiền:   " + lTongThu.ToString();
            }
            catch
            {
                lbTongTienDT.Text = "Tổng tiền:   0";
                lvThuChiDT.Items.Clear();
            }
        }



        private long getTongThu_Thang(string sThang, string sNam)
        {
            try
            {
                long lTongThu = 0;

                List<string> list_Ngay = setNgayTrongThang(int.Parse(sThang), int.Parse(sNam));
                string sFolderYear = sNam.ToString();
                string sFolderMonth = Path.Combine(sFolderYear, sThang.ToString());

                for (int i = 0; i < list_Ngay.Count; i++)
                {
                    string sFileName = list_Ngay[i] + sThang + sNam + ".txt";
                    sFileName = Path.Combine(sFolderMonth, sFileName);

                    lTongThu += Save.getTongTien(sFileName);
                }

                return lTongThu;
            }
            catch
            {
                return 0;
            }
        }

        private long getTongChi_Thang(string sThang, string sNam)
        {
            try
            {
                long lTongChi = 0;

                List<string> list_Ngay = setNgayTrongThang(int.Parse(sThang), int.Parse(sNam));
                string sFolderYear = sNam.ToString();
                string sFolderMonth = Path.Combine(sFolderYear, sThang.ToString());

                for (int i = 0; i < list_Ngay.Count; i++)
                {
                    long lTongChiNgay = 0;
                    string sFileName = list_Ngay[i] + sThang + sNam + ".txt";
                    sFileName = Path.Combine(sFolderMonth, sFileName);

                    if (!File.Exists(sFileName))
                    {
                        continue;
                    }

                    List<String> lsContent = Save.GetContent(sFileName);

                    for (int j = 1; j < lsContent.Count; j++)
                    {
                        string sTemp;
                        int iTemp;

                        if (int.TryParse(lsContent[j].Substring(0, 2), out iTemp))
                        {
                            iTemp = 8;
                        }
                        else
                        {
                            iTemp = 7;
                        }

                        iTemp += 3;

                        if (lsContent[j].EndsWith("(CHI)"))
                        {
                            sTemp = lsContent[j].Remove(lsContent[j].Length - 5);
                            lTongChiNgay += long.Parse(sTemp.Substring(iTemp, 8));
                        }
                    }

                    lTongChi += lTongChiNgay;
                }

                return lTongChi;
            }
            catch
            {
                return 0;
            }
        }

        private void showDoanhThu_Nam(string sNam)
        {
            try
            {
                long lTongThu = 0;

                string sFolderYear = sNam.ToString();
                
                lvThuChiDT.Items.Clear();

                for (int i = 1; i < 13; i++)
                {
                    string sThang = "";

                    if (i < 10)
                    {
                        sThang = "0" + i.ToString();
                    }
                    else
                    {
                        sThang = i.ToString();
                    }

                    string sFolderMonth = Path.Combine(sFolderYear, "0" + i.ToString());

                    long lTongThuThang = getTongThu_Thang(sThang, sNam);
                    long lTongChiThang = getTongChi_Thang(sThang, sNam);

                    lTongThu += lTongThuThang;

                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = setSTT(i);
                    lvi.SubItems.Add(sThang + "/" + sNam);
                    lvi.SubItems.Add(lTongThuThang.ToString());
                    lvi.SubItems.Add(lTongChiThang.ToString());

                    lvThuChiDT.Items.Add(lvi);
                }

                lbTongTienDT.Text = "Tổng tiền:   " + lTongThu.ToString();
            }
            catch
            {
                lbTongTienDT.Text = "Tổng tiền:   0";
                lvThuChiDT.Items.Clear();
            }
        }



        private void lvThuChiDT_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvThuChiDT, e);
        }

        private void dateTimePickerDT_ValueChanged(object sender, EventArgs e)
        {
            if (rbNgay.Checked)
            {
                showDoanhThu_Ngay(dateTimePickerDT.Value.ToString("ddMMyyyy") + ".txt");
            }

            if (rbThang.Checked)
            {
                showDoanhThu_Thang(dateTimePickerDT.Value.ToString("MM"), dateTimePickerDT.Value.ToString("yyyy"));
            }

            if (rbNam.Checked)
            {
                showDoanhThu_Nam(dateTimePickerDT.Value.ToString("yyyy"));
            }
        }

        private void rbNgay_CheckedChanged(object sender, EventArgs e)
        {
            clhGiờ.Text = "Giờ";

            showDoanhThu_Ngay(dateTimePickerDT.Value.ToString("ddMMyyyy") + ".txt");
        }

        private void rbThang_CheckedChanged(object sender, EventArgs e)
        {
            clhGiờ.Text = "Ngày";

            showDoanhThu_Thang(dateTimePickerDT.Value.ToString("MM"), dateTimePickerDT.Value.ToString("yyyy"));
        }

        private void rbNam_CheckedChanged(object sender, EventArgs e)
        {
            clhGiờ.Text = "Tháng";

            showDoanhThu_Nam(dateTimePickerDT.Value.ToString("yyyy"));
        }

        private void UC_DoanhThu_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                dateTimePickerDT_ValueChanged(sender, e);
            }
        }
    }
}
