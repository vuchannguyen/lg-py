using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Function;
using DAO;
using BUS;

namespace VNSC
{
    public partial class UC_ListViewEx : UserControl
    {
        private int m_ListViewWidth;

        public int ListViewWidth
        {
            get { return m_ListViewWidth; }
            set { m_ListViewWidth = value; }
        }

        private int m_ListViewHeight;

        public int ListViewHeight
        {
            get { return m_ListViewHeight; }
            set { m_ListViewHeight = value; }
        }

        private ListViewEx m_lvEx;

        public ListViewEx LvEx
        {
            get { return m_lvEx; }
            set { m_lvEx = value; }
        }

        private List<HoSo> list_dto;
        private int iColumnCount;



        public UC_ListViewEx()
        {
            InitializeComponent();

            m_ListViewWidth = 760;
            m_ListViewHeight = 438;
        }

        public UC_ListViewEx(List<HoSo> list_HS)
        {
            InitializeComponent();

            m_ListViewWidth = 760;
            m_ListViewHeight = 438;

            list_dto = list_HS;
        }

        private void UC_ListViewEx_Load(object sender, EventArgs e)
        {
            listViewEx.Width = m_ListViewWidth;
            listViewEx.Height = m_ListViewHeight;

            //listViewEx.ColumnWidthChanged +=new ColumnWidthChangedEventHandler(listViewEx_ColumnWidthChanged);
            LoadListControlData();

            HideColumn();

            iColumnCount = listViewEx.Columns.Count;
            listViewEx.ColumnWidthChanged += new ColumnWidthChangedEventHandler(listViewEx_ColumnWidthChanged);

            refreshListView();

            m_lvEx = listViewEx;
        }

        private void LoadListControlData()
        {
            listViewEx.Columns.Add("Mã", 10, HorizontalAlignment.Left); //0
            listViewEx.Columns.Add("STT", 50, HorizontalAlignment.Left); //1
            listViewEx.Columns.Add("Họ tên", 100, HorizontalAlignment.Left); //2
            listViewEx.Columns.Add("Nhóm trách vụ", 100, HorizontalAlignment.Center); //3
            listViewEx.Columns.Add("Trách vụ", 100, HorizontalAlignment.Center); //4

            listViewEx.Columns.Add("Ngày cập nhật", 100, HorizontalAlignment.Center); //5
            listViewEx.Columns.Add("Ngày sinh", 100, HorizontalAlignment.Center); //6
            listViewEx.Columns.Add("Giới tính", 100, HorizontalAlignment.Center); //7
            listViewEx.Columns.Add("Quê quán", 100, HorizontalAlignment.Left); //8
            listViewEx.Columns.Add("Trình độ học vấn", 100, HorizontalAlignment.Left); //9
            listViewEx.Columns.Add("Tôn giáo", 100, HorizontalAlignment.Left); //10
            listViewEx.Columns.Add("Địa chỉ", 100, HorizontalAlignment.Left); //11
            listViewEx.Columns.Add("Điện thoại liên lạc", 100, HorizontalAlignment.Center); //12
            listViewEx.Columns.Add("Email", 100, HorizontalAlignment.Left); //13

            listViewEx.Columns.Add("Ngành", 100, HorizontalAlignment.Center); //14
            listViewEx.Columns.Add("Đơn vị", 100, HorizontalAlignment.Center); //15

            listViewEx.Columns.Add("Liên đoàn", 100, HorizontalAlignment.Left); //16
            listViewEx.Columns.Add("Đạo", 100, HorizontalAlignment.Left); //17
            listViewEx.Columns.Add("Châu", 100, HorizontalAlignment.Left); //18
            listViewEx.Columns.Add("Ngày tuyên hứa", 100, HorizontalAlignment.Center); //19
            listViewEx.Columns.Add("Trưởng nhận lời hứa", 100, HorizontalAlignment.Left); //20
            listViewEx.Columns.Add("Trách vụ tại đơn vị", 100, HorizontalAlignment.Left); //21
            listViewEx.Columns.Add("Trách vụ ngoài đơn vị", 100, HorizontalAlignment.Left); //22
            listViewEx.Columns.Add("Tên Rừng", 100, HorizontalAlignment.Left); //23
            listViewEx.Columns.Add("Ghi chú", 100, HorizontalAlignment.Left); //24

            listViewEx.Columns.Add("Nghề nghiệp", 100, HorizontalAlignment.Left); //25
            listViewEx.Columns.Add("Sở trường", 100, HorizontalAlignment.Left); //26
        }

        private void HideColumn()
        {
            // Let us hide columns initally
            listViewEx.Columns[0].Visible = false;
            listViewEx.Columns[5].Visible = false;
            listViewEx.Columns[6].Visible = false;
            listViewEx.Columns[7].Visible = false;
            listViewEx.Columns[8].Visible = false;
            listViewEx.Columns[9].Visible = false;
            listViewEx.Columns[10].Visible = false;
            listViewEx.Columns[11].Visible = false;
            listViewEx.Columns[12].Visible = false;
            listViewEx.Columns[13].Visible = false;
            listViewEx.Columns[16].Visible = false;
            listViewEx.Columns[17].Visible = false;
            listViewEx.Columns[18].Visible = false;
            listViewEx.Columns[19].Visible = false;
            listViewEx.Columns[20].Visible = false;
            listViewEx.Columns[21].Visible = false;
            listViewEx.Columns[22].Visible = false;
            listViewEx.Columns[23].Visible = false;
            listViewEx.Columns[24].Visible = false;
            listViewEx.Columns[25].Visible = false;
            listViewEx.Columns[26].Visible = false;

            // We will avoid removing the first column by the user.
            // Dont provide the menu to remove simple...
            listViewEx.Columns[0].ColumnMenuItem.Visible = false;
            listViewEx.Columns[2].ColumnMenuItem.Visible = false;
            //listViewEx.Columns[26].ColumnMenuItem.Visible = false;
            //listViewEx.Columns[4].ColumnMenuItem.Visible = false;
            //listViewEx.Columns[14].ColumnMenuItem.Visible = false;
            //listViewEx.Columns[15].ColumnMenuItem.Visible = false;
        }

        private void refreshListView()
        {
            SubFunction.ClearlvItem(listViewEx);

            for (int i = 0; i < list_dto.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //if (listViewEx.Columns[0].Visible)
                //{
                //    lvi.Text = list_dto[i].Ma;
                //}

                if (listViewEx.Columns[1].Visible)
                {
                    lvi.Text = SubFunction.setSTT(i + 1);
                    //lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                }

                if (listViewEx.Columns[2].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].HoTen);
                }

                if (listViewEx.Columns[3].Visible)
                {
                    lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_dto[i].MaNhomTrachVu).Ten);
                }

                if (listViewEx.Columns[4].Visible)
                {
                    lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto[i].MaTrachVu).Ten);
                }



                if (listViewEx.Columns[5].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].NgayCapNhat);
                }

                if (listViewEx.Columns[6].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].NgaySinh.ToString());
                }

                if (listViewEx.Columns[7].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].GioiTinh);
                }

                if (listViewEx.Columns[8].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].QueQuan);
                }

                if (listViewEx.Columns[9].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TrinhDoHocVan);
                }

                if (listViewEx.Columns[10].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TonGiao);
                }

                if (listViewEx.Columns[11].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].DiaChi);
                }

                if (listViewEx.Columns[12].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].DienThoaiLienLac);
                }

                if (listViewEx.Columns[13].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].Email);
                }



                if (listViewEx.Columns[14].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].Nganh);
                }

                if (listViewEx.Columns[15].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].DonVi);
                }

                if (listViewEx.Columns[16].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].LienDoan);
                }

                if (listViewEx.Columns[17].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].Dao);
                }

                if (listViewEx.Columns[18].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].Chau);
                }

                if (listViewEx.Columns[19].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].NgayTuyenHua.ToString());
                }

                if (listViewEx.Columns[20].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TruongNhanLoiHua);
                }

                if (listViewEx.Columns[21].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TrachVuTaiDonVi);
                }

                if (listViewEx.Columns[22].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TrachVuNgoaiDonVi);
                }

                if (listViewEx.Columns[23].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].TenRung);
                }

                if (listViewEx.Columns[24].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].GhiChu);
                }



                if (listViewEx.Columns[25].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].NgheNghiep);
                }

                if (listViewEx.Columns[26].Visible)
                {
                    lvi.SubItems.Add(list_dto[i].SoTruong);
                }

                listViewEx.Items.Add(lvi);
            }
        }

        private void listViewEx_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (listViewEx.Columns.Count != iColumnCount)
            {
                iColumnCount = listViewEx.Columns.Count;
                refreshListView();

                m_lvEx = listViewEx;
            }
        }
    }
}
