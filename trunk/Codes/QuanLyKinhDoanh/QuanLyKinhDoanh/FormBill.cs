using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKinhDoanh
{
    public partial class FormBill : Form
    {
        public FormBill()
        {
            InitializeComponent();
        }

        private void FormBill_Load(object sender, EventArgs e)
        {
            
        }

        private List<string> SplitName(string name)
        {
            List<string> result = new List<string>();

            if (name.Length >= 20)
            {
                string[] arr = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string temp = string.Empty;

                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        temp += arr[i] + " ";

                        if (temp.Length >= 15)
                        {
                            result.Add(temp);
                            temp = string.Empty;
                        }
                    }

                    if (!string.IsNullOrEmpty(temp))
                    {
                        result.Add(temp);
                    }
                }
                else
                {
                    result.Add(temp);
                }
            }

            return result;
        }

        private void CreateBill(ListView lvSource)
        {
            foreach (ListViewItem lviSource in lvSource.Items)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(result[0]);
                lvThongTin.Items.Add(lvi);

                if (result.Count > 1)
                {
                    for (int i = 1; i < result.Count; i++)
                    {
                        ListViewItem lviEx = new ListViewItem();

                        lviEx.SubItems.Add(string.Empty);
                        lviEx.SubItems.Add(string.Empty);
                        lviEx.SubItems.Add(result[i]);

                        lvThongTin.Items.Add(lviEx);
                    }
                }
            }
        }
    }
}
