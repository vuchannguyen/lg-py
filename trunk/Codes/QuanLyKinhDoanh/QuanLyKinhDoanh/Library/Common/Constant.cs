using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library
{
    class Constant
    {
        public const int TOP_HEIGHT_DEFAULT = 150;
        public const int BOT_HEIGHT_DEFAULT = 20;

        public static Color COLOR_NORMAL = Color.Gray;
        public static Color COLOR_MOUSEOVER = Color.Orange;
        public static Color COLOR_DISABLE = Color.LightSlateGray;
        public static Color COLOR_IN_USE = Color.Orange;

        public const string TOOLTIP_USER = "Quản lý thông tin User";
        public const string TOOLTIP_KHACHHANG = "Quản lý thông tin Khách hàng";
        public const string TOOLTIP_SANPHAM = "Quản lý Sản Phẩm và Nhóm Sản Phẩm";
        public const string TOOLTIP_MUABAN = "Quản lý giao dịch trong kho hàng";
        public const string TOOLTIP_KHOHANG = "Tra cứu danh mục sản phẩm đang bán";
        public const string TOOLTIP_THUCHI = "Quản lý thu nhập hàng ngày";
        public const string TOOLTIP_THANHTOAN = "Tính hóa đơn cho khách hàng";

        public const string TOOLTIP_MUA_THEM = "Nhập danh mục sản phẩm có thể bán";
        public const string TOOLTIP_MUA_THEM_SAN_PHAM = "Thêm sản phẩm mới vào danh mục";
        public const string TOOLTIP_BAN_THEM = "Nhập sản phẩm vào kho hàng để bán";
    }
}
