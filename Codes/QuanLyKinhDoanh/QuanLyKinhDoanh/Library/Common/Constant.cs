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

        public const int DEFAULT_TOTAL_DECAL = 84;
        public const int DEFAULT_SPACE_WIDTH = 7;
        public const int DEFAULT_SPACE_HEIGHT = 8;
        public const int DEFAULT_TOP_FIRST_DECAL = 17;
        public const int DEFAULT_LEFT_FIRST_DECAL = 10;
        public const int DEFAULT_NUMS_DECAL_ROWS = 12;
        public const int DEFAULT_NUMS_DECAL_COLUMNS = 7;

        public static Size DEFAULT_SIZE_DECAL = new Size(79, 53);
        public static Size DEFAULT_SIZE_LISTVIEWEX_EXPORT = new Size(770, 485);

        public static DateTime DEFAULT_DATE = new DateTime(2000, 1, 1);
        public const string DEFAULT_DATE_FORMAT = "dd/MM/yyyy";
        public const string DEFAULT_DATE_TIME_FORMAT = "dd/MM/yyyy - hh:mm tt";

        public const string DEFAULT_FORMAT_ID_PRODUCT = "0000";
        public const string DEFAULT_FORMAT_ID_BILL = "00000";
        public const string DEFAULT_FORMAT_MONEY = "#" + Constant.SYMBOL_LINK_MONEY + "###";
        public const string DEFAULT_MONEY_SUBFIX = "Đ";

        public const string DEFAULT_PASSWORD = "admin";

        public static string SORT_ASCENDING = "asc";
        public static string SORT_DESCENDING = "desc";

        public static Color COLOR_NORMAL = Color.Gray;
        public static Color COLOR_MOUSEOVER = Color.Orange;
        public static Color COLOR_DISABLE = Color.LightSlateGray;
        public static Color COLOR_IN_USE = Color.Red;
        public static Color COLOR_CHOOSEN_PRICE = Color.LightBlue;

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
        public const string SYMBOL_LINK_STRING = " - ";
        public const string SYMBOL_LINK_MONEY = ",";
        public const string SYMBOL_DISCOUNT = "%";

        public const int ID_GROUP_KHACH_THUONG = 1;
        public const int ID_GROUP_KHTT = 2;
        public const int ID_GROUP_KHND = 3;
        public const int ID_GROUP_KHACH_SI = 4;
        public const int ID_GROUP_VIP = 5;

        public const int DEFAULT_DISCOUNT_GROUP_KHACH_THUONG = 0;
        public const int DEFAULT_DISCOUNT_GROUP_KHTT = 10;
        public const int DEFAULT_DISCOUNT_GROUP_VIP = 5;
        public const int DEFAULT_DISCOUNT_GROUP_KHACH_SI = 5;

        public const int DEFAULT_AGE_KHACH_HANG = 18;

        public const int ID_TYPE_MUA = 1;
        public const int ID_TYPE_BAN = 2;
        public const int ID_TYPE_THU = 3;
        public const int ID_TYPE_CHI = 4;

        public const string PREFIX_MUA = "M";
        public const string PREFIX_BAN = "B";
        public const string PREFIX_THU = "T";
        public const string PREFIX_CHI = "C";

        public const int ID_STATUS_DONE = 1;
        public const int ID_STATUS_DEBT = 2;
        public const string STATUS_DONE = "Trả hết";
        public const string STATUS_DEBT = "Nợ";
        public const string DEFAULT_MONEY_STATUS_DONE = "Tiền hồi lại:";
        public const string DEFAULT_MONEY_STATUS_DEBT = "Tiền còn lại:";

        public const string DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME = "Excel File";
        public const string DEFAULT_EXPORT_EXCEL_FILE_TYPE = "xls";
        public const string DEFAULT_EXPORT_EXCEL_DATE_FORMAT = "ddMMyyyy";

        public const string CAPTION_CONFIRM = "CONFIRM";
        public const string CAPTION_WARNING = "WARNING";
        public const string CAPTION_ERROR = "ERROR";

        public const string MESSAGE_NEW_LINE = "\r\n";
        public const string MESSAGE_EXIT = "Thoát?";
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

        public const string MESSAGE_CONFIRM = "Hoàn tất?";
        public const string MESSAGE_CONFIRM_DELETE_ALL_PRICE = "Xóa hết tất cả các ô?";
        public const string MESSAGE_CONFIRM_EXPORT = "Xuất dữ liệu ra file Excel?";

        public const string MESSAGE_ERROR = "Có lỗi!";
        public const string MESSAGE_ERROR_NULL_DATA = "Dữ liệu không tồn tại!";
        public const string MESSAGE_ERROR_MISSING_RESOURCE = "Vui lòng kiểm tra thư mục Resource!";
        public const string MESSAGE_ERROR_MISSING_DATA = "Vui lòng nhập dữ liệu {0} trước!";
        public const string MESSAGE_ERROR_MISSING_MONEY = "Sản phẩm chưa có giá bán!";
        public const string MESSAGE_ERROR_DELETE_DATA = "Vui lòng kiểm tra dữ liệu có đang sử dụng hay không!";
        public const string MESSAGE_ERROR_DOB = "Vui lòng kiểm tra ngày sinh!" + MESSAGE_NEW_LINE + "(Từ {0} tuổi trở lên)";
        public const string MESSAGE_ERROR_EXPORT_EXCEL = "Không thể xuất dữ liệu!\nVui lòng thử lại.";
        public const string MESSAGE_ERROR_EXPORT_EXCEL_NULL_DATA = "Không có dữ liệu để xuất!";

        public const string MESSAGE_SUCCESS_EXPORT_EXCEL = "Dữ liệu đã xuất thành công.";

        public const string SEARCH_USER_TIP = "Họ tên - Tên đăng nhập - Email";
        public const string SEARCH_KHACHHANG_TIP = "Mã - Họ tên - Điện thoại - Email";
        public const string SEARCH_SANPHAMGROUP_TIP = "Mã - Tên - Mô tả";
        public const string SEARCH_SANPHAM_TIP = "Mã - Tên - Mô tả";
        public const string SEARCH_XUATXU_TIP = "Tên - Địa chỉ - Email";
        public const string SEARCH_NHAPKHO_TIP = "Mã nhập - Mã SP - Tên SP";
        public const string SEARCH_THU_TIP = "Mã Hóa đơn - Lý do";
    }
}
