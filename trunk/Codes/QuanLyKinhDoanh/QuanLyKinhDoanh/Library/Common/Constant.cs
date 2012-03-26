using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library
{
    class Constant
    {
        public const int DEFAULT_TOP_HEIGHT = 150;
        public const int DEFAULT_BOT_HEIGHT = 20;

        public const int DEFAULT_ROW = 20;

        public const string DEFAULT_PASSWORD = "admin";

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

        public const string PAGE_TEXT = " Trang";
        public const string SEPERATE_STRING = ", ";

        public const string CAPTION_CONFIRM = "CONFIRM";
        public const string CAPTION_WARNING = "WARNING";
        public const string CAPTION_ERROR = "ERROR";

        public const string MESSAGE_EXIT = "Bạn có muốn thoát?";
        public const string MESSAGE_CONTINUE = "Bạn có muốn tiếp tục?";

        public const string MESSAGE_DELETE_CONFIRM = "Xóa dữ liệu đã chọn?";
        public const string MESSAGE_DELETE_SUCCESS = "{0} đã xóa thành công";
        public const string MESSAGE_DELETE_ERROR = "{0} xóa thất bại!";

        public const string MESSAGE_INSERT_SUCCESS = "{0} đã nhập thành công";
        public const string MESSAGE_INSERT_ERROR = "Quá trình nhập dữ liệu mới thất bại!";

        public const string MESSAGE_UPDATE_CONFIRM = "Cập nhật dữ liệu?";
        public const string MESSAGE_UPDATE_SUCCESS = "{0} đã cập nhật thành công";
        public const string MESSAGE_UPDATE_ERROR = "{0} cập nhật dữ liệu thất bại!";

        public const string MESSAGE_ERROR = "Có lỗi!";
        public const string MESSAGE_ERROR_MISSING_DATA = "Vui lòng nhập dữ liệu {0} trước!";

        public const string SEARCH_USER_TIP = "Họ tên - Tên đăng nhập - Email";
        public const string SEARCH_SANPHAMGROUP_TIP = "Mã - Tên - Mô tả";
        public const string SEARCH_SANPHAM_TIP = "Mã - Tên - Mô tả";
    }
}
