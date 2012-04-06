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

        public const int DEFAULT_MAX_PERCENT_PROFIT = 999;

        public const string DEFAULT_FORMAT_ID_PRODUCT = "0000";
        public const string DEFAULT_FORMAT_ID_BILL = "00000";

        public const string DEFAULT_PASSWORD = "admin";

        public static string SORT_ASCENDING = "asc";
        public static string SORT_DESCENDING = "desc";

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
        public const string LINK_SYMBOL_STRING = " - ";
        public const string LINK_SYMBOL_MONEY = ",";

        public const int ID_GROUP_KH_MUA_LE = 1;
        public const int ID_GROUP_KH_MUA_SI = 2;
        public const int ID_GROUP_KH_VIP = 3;

        public const int ID_TYPE_MUA = 1;
        public const int ID_TYPE_BAN = 2;
        public const int ID_TYPE_THU = 3;
        public const int ID_TYPE_CHI = 4;

        public const string PREFIX_MUA = "M";
        public const string PREFIX_BAN = "B";
        public const string PREFIX_THU = "T";
        public const string PREFIX_CHI = "C";

        public const string STATUS_DONE = "Đã thanh toán";
        public const string STATUS_NOTHING = "Chưa thanh toán";

        public const string CAPTION_CONFIRM = "CONFIRM";
        public const string CAPTION_WARNING = "WARNING";
        public const string CAPTION_ERROR = "ERROR";

        public const string MESSAGE_NEW_LINE = "\n";
        public const string MESSAGE_EXIT = "Bạn có muốn thoát?";
        public const string MESSAGE_CONTINUE = "Bạn có muốn tiếp tục?";

        public const string MESSAGE_DELETE_CONFIRM = "Xóa dữ liệu đã chọn?";
        public const string MESSAGE_DELETE_SUCCESS = "{0} đã xóa thành công.";
        public const string MESSAGE_DELETE_ERROR = "{0} xóa thất bại!";

        public const string MESSAGE_INSERT_SUCCESS = "{0} đã nhập thành công";
        public const string MESSAGE_INSERT_ERROR = "Quá trình nhập dữ liệu mới thất bại!";
        public const string MESSAGE_INSERT_ERROR_DUPLICATE = "Quá trình nhập dữ liệu mới thất bại!" + MESSAGE_NEW_LINE + "Vui lòng kiểm tra \"{0}\" đã tồn tại.";

        public const string MESSAGE_UPDATE_CONFIRM = "Cập nhật dữ liệu?";
        public const string MESSAGE_UPDATE_SUCCESS = "{0} đã cập nhật thành công.";
        public const string MESSAGE_UPDATE_ERROR = "{0} cập nhật dữ liệu thất bại!";

        public const string MESSAGE_ERROR = "Có lỗi!";
        public const string MESSAGE_ERROR_MISSING_DATA = "Vui lòng nhập dữ liệu {0} trước!";

        public const string SEARCH_USER_TIP = "Họ tên - Tên đăng nhập - Email";
        public const string SEARCH_KHACHHANG_TIP = "Họ tên - Email";
        public const string SEARCH_SANPHAMGROUP_TIP = "Mã - Tên - Mô tả";
        public const string SEARCH_SANPHAM_TIP = "Mã - Tên - Mô tả";
        public const string SEARCH_NHAPKHO_TIP = "Mã nhập - Mã SP - Tên SP";
    }
}
