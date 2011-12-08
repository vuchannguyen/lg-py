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
    public partial class UC_TraCuu : UserControl
    {
        private List<HoSo> list_dto;
        private string sMa;
        private int iRows;
        private int iTotalPage;
        private int iRowsPerPage;

        public UC_TraCuu()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbTitle.Image = Image.FromFile(@"Resources\icon_tracuu.png");
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                pbTraCuu.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_TraCuu_Load(object sender, EventArgs e)
        {
            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "TRA";
            lbTitle.Text = "TRA CỨU";

            LoadPic();

            pnQuanLy.Size = new System.Drawing.Size(770, 485);
            pnQuanLy.Location = SubFunction.SetWidthCenter(this.Size, pnQuanLy.Size, pnQuanLy.Top);

            iRows = 10;
        }

        private void setRowsPerPage()
        {
            int n = list_dto.Count - ((int.Parse(lbPage.Text) - 1) * iRows);
            if (n < iRows)
            {
                iRowsPerPage = list_dto.Count;
            }
            else
            {
                iRowsPerPage = int.Parse(lbPage.Text) * iRows;
            }
        }

        private void setTotalPage()
        {
            if (list_dto.Count % iRows == 0)
            {
                iTotalPage = list_dto.Count / iRows;
            }
            else
            {
                iTotalPage = (list_dto.Count / iRows) + 1;
            }
        }

        private void refreshListView()
        {
            tbSearch.Text = "";
            list_dto = HoSo_BUS.LayDSHoSo();
            if (list_dto.Count > 0)
            {
                sMa = list_dto[list_dto.Count - 1].Ma;
            }

            setTotalPage();
            lbTotalPage.Text = iTotalPage.ToString() + " Trang";
            SubFunction.SetError(lbPage, lbPage.Top, pnPage.Size, "1");
            refreshListView(1);

            pbOk.Enabled = false;
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView(string sTen)
        {
            list_dto = HoSo_BUS.TraCuuDSHoSoTheoTen(sTen);

            setTotalPage();
            lbTotalPage.Text = iTotalPage.ToString() + " Trang";
            SubFunction.SetError(lbPage, lbPage.Top, pnPage.Size, "1");
            refreshListView(1);
        }

        private void refreshListView(int Page)
        {
            SubFunction.ClearlvItem(lvThongTin);
            setRowsPerPage();
            for (int i = (int.Parse(lbPage.Text) - 1) * iRows; i < iRowsPerPage; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto[i].Ma;
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto[i].HoTen);
                lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_dto[i].MaNhomTrachVu).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto[i].Nganh);
                lvi.SubItems.Add(list_dto[i].DonVi);

                lvThongTin.Items.Add(lvi);
            }
        }



        #region Tra cuu trong Listview
        private void pbOk_Click(object sender, EventArgs e)
        {
            refreshListView(tbSearch.Text);
        }

        private void pbOk_MouseEnter(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_selected.png");
        }

        private void pbOk_MouseLeave(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
        }



        private void disableButtonPage(int iPage)
        {
            if (int.Parse(lbPage.Text) == 1)
            {
                pbBackPage.Enabled = false;
                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back_disable.png");
            }
            else
            {
                pbBackPage.Enabled = true;
                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
            }

            if (int.Parse(lbPage.Text) == iTotalPage)
            {
                pbNextPage.Enabled = false;
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next_disable.png");
            }
            else
            {
                pbNextPage.Enabled = true;
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
        }

        private void pbBackPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage.Text) > 1)
            {
                lbPage.Text = (int.Parse(lbPage.Text) - 1).ToString();
            }
        }

        private void pbBackPage_MouseEnter(object sender, EventArgs e)
        {
            pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back_selected.png");
        }

        private void pbBackPage_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage.Text));
        }



        private void pbNextPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage.Text) < iTotalPage)
            {
                lbPage.Text = (int.Parse(lbPage.Text) + 1).ToString();
            }
        }

        private void pbNextPage_MouseEnter(object sender, EventArgs e)
        {
            pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next_selected.png");
        }

        private void pbNextPage_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage.Text));
        }
        


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text.Length > 0)
            {
                pbOk.Enabled = true;
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
            }
            else
            {
                pbOk.Enabled = false;
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                refreshListView();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                pbOk_Click(sender, e);
            }
        }

        private void lvThongTin_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin, e);
        }

        private void lbPage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pbBackPage.Enabled = false;
            pbNextPage.Enabled = false;

            tbPage.Visible = true;
            tbPage.Text = "";
            tbPage.Focus();
        }

        private void lbPage_TextChanged(object sender, EventArgs e)
        {
            refreshListView(int.Parse(lbPage.Text));

            disableButtonPage(int.Parse(lbPage.Text));
        }

        private void tbPage_LostFocus(object sender, EventArgs e)
        {
            tbPage.Visible = false;

            pbBackPage.Enabled = true;
            pbNextPage.Enabled = true;
        }

        private void tbPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbPage.Text.Length > 0)
                {
                    if (SubFunction.TestInt(tbPage.Text))
                    {
                        if (int.Parse(tbPage.Text) <= iTotalPage)
                        {
                            tbPage.Visible = false;
                            lbPage.Text = tbPage.Text;
                        }
                        else
                        {
                            Form_Notice frm_Notice = new Form_Notice("Không có trang này!", "Vui lòng kiểm tra lại.", false);
                        }
                    }
                    else
                    {
                        Form_Notice frm_Notice = new Form_Notice("Số trang không hợp lệ!", "Tắt bộ gõ dấu Tiếng Việt.", false);
                    }
                }
                else
                {
                    tbPage.Visible = false;
                }
            }
        }
        #endregion



        private void pbSearch_Click(object sender, EventArgs e)
        {
            chbLocTheoKetQua.Checked = false;

            //Nguyen test
            //ket qua tim duoc tra ve list_dto da duoc khai bao, sau do goi ham refreshListview()
            list_dto = Search_Function.SearchWithOneCriteria(tbSearch1.Text, HoSo_BUS.LayDSHoSo());
        }

        private void pbSearch_MouseEnter(object sender, EventArgs e)
        {
            pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_selected.png");
        }

        private void pbSearch_MouseLeave(object sender, EventArgs e)
        {
            pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
        }



        private void TestSearch1()
        {
            if (tbSearch1.Text.Length > 0)
            {
                pbSearch.Enabled = true;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");

                chbSearch2.Enabled = true;
            }
            else
            {
                pbSearch.Enabled = false;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                chbSearch2.Enabled = false;
            }
        }

        private void TestSearch2()
        {
            if (tbSearch1.Text.Length > 0 && tbSearch2.Text.Length > 0)
            {
                pbSearch.Enabled = true;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");

                chbSearch3.Enabled = true;
            }
            else
            {
                pbSearch.Enabled = false;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                chbSearch3.Enabled = false;
            }
        }

        private void TestSearch3()
        {
            if (tbSearch1.Text.Length > 0 && tbSearch2.Text.Length > 0 && tbSearch3.Text.Length > 0)
            {
                pbSearch.Enabled = true;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");

                chbSearch4.Enabled = true;
            }
            else
            {
                pbSearch.Enabled = false;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                chbSearch4.Enabled = false;
            }
        }

        private void TestSearch4()
        {
            if (tbSearch1.Text.Length > 0 && tbSearch2.Text.Length > 0 && tbSearch3.Text.Length > 0 && tbSearch4.Text.Length > 0)
            {
                pbSearch.Enabled = true;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
            }
            else
            {
                pbSearch.Enabled = false;
                pbSearch.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
            }
        }

        private void TestSearch()
        {
            if (tbSearch1.Enabled)
            {
                TestSearch1();
            }

            if (tbSearch2.Enabled)
            {
                TestSearch2();
            }

            if (tbSearch3.Enabled)
            {
                TestSearch3();
            }

            if (tbSearch4.Enabled)
            {
                TestSearch4();
            }
        }

        private void tbSearch1_TextChanged(object sender, EventArgs e)
        {
            TestSearch();
        }

        private void chbSearch2_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSearch2.Checked)
            {
                tbSearch2.Enabled = true;

                tbSearch1.Enabled = false;
            }
            else
            {
                tbSearch2.Text = "";
                tbSearch2.Enabled = false;


                tbSearch1.Enabled = true;
            }

            TestSearch();
        }

        private void tbSearch2_TextChanged(object sender, EventArgs e)
        {
            TestSearch();
        }

        private void chbSearch3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSearch3.Checked)
            {
                tbSearch3.Enabled = true;

                tbSearch2.Enabled = false;
                chbSearch2.Enabled = false;
            }
            else
            {
                tbSearch3.Text = "";
                tbSearch3.Enabled = false;

                tbSearch2.Enabled = true;
                chbSearch2.Enabled = true;
            }

            TestSearch();
        }

        private void tbSearch3_TextChanged(object sender, EventArgs e)
        {
            TestSearch();
        }

        private void chbSearch4_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSearch4.Checked)
            {
                tbSearch4.Enabled = true;

                tbSearch3.Enabled = false;
                chbSearch3.Enabled = false;
            }
            else
            {
                tbSearch4.Text = "";
                tbSearch4.Enabled = false;

                tbSearch3.Enabled = true;
                chbSearch3.Enabled = true;
            }

            TestSearch();
        }

        private void tbSearch4_TextChanged(object sender, EventArgs e)
        {
            TestSearch();
        }



        private void chbLocTheoKetQua_CheckedChanged(object sender, EventArgs e)
        {
            if (chbLocTheoKetQua.Checked)
            {
                cbNganh.Enabled = true;
                cbNhomTrachVu.Enabled = true;
                cbTrachVu.Enabled = true;
                cbID.Enabled = true;
            }
            else
            {
                cbNganh.SelectedIndex = -1;
                cbNhomTrachVu.SelectedIndex = -1;
                cbTrachVu.SelectedIndex = -1;
                cbID.SelectedIndex = -1;

                cbNganh.Enabled = false;
                cbNhomTrachVu.Enabled = false;
                cbTrachVu.Enabled = false;
                cbID.Enabled = false;
            }
        }

        private void cbNganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nguyen test
        }

        private void cbNhomTrachVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nguyen test
        }

        private void cbTrachVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nguyen test
        }

        private void cbID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nguyen test
        }
    }
}
