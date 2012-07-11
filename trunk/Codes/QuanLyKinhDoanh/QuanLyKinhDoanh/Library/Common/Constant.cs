using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library
{
    class Constant
    {
        public static List<string> listFolderAvatar = new List<string>() { "Store", "SP" };
        public const int DEFAULT_AVATAR_WIDTH = 100;
        public const int DEFAULT_AVATAR_HEIGHT = 100;

        public const int DEFAULT_AVATAR_MAX_WIDTH = 300;
        public const int DEFAULT_AVATAR_MAX_HEIGHT = 300;

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

        public const int DEFAULT_WARNING_DAYS_DOB = 7;
        public const int DEFAULT_WARNING_DAYS_EXPIRED = 7;

        public const int DEFAULT_STATUS_USED_DATE_BEFORE = 0;
        public const int DEFAULT_STATUS_USED_DATE_NEAR = 1;
        public const int DEFAULT_STATUS_USED_DATE_END = 2;

        public static Size DEFAULT_SIZE_DECAL = new Size(79, 53);
        public static Size DEFAULT_SIZE_LISTVIEWEX_EXPORT = new Size(770, 485);

        public const string DEFAULT_TITLE_ADD = "THÊM";
        public const string DEFAULT_TITLE_EDIT = "SỬA";

        public const string DEFAULT_WELCOME_DATE_TIME_FORMAT = "Hôm nay là {0}, {1}";

        public static DateTime DEFAULT_DATE = new DateTime(2000, 1, 1);
        public const string DEFAULT_DATE_FORMAT = "dd/MM/yyyy";
        public const string DEFAULT_DATE_TIME_FORMAT = "dd/MM/yyyy - HH:mm";
        public const string DEFAULT_DATE_TIME_AVATAR_FORMAT = "ddMMyyyyHHmmss";
        public const string DEFAULT_TYPE_DAY = "Ngày";
        public const string DEFAULT_TYPE_MONTH = "Tháng";
        public const string DEFAULT_TYPE_YEAR = "Năm";

        public const string DEFAULT_FORMAT_ID_PRODUCT = "0000";
        public const string DEFAULT_FORMAT_ID_BILL = "00000";
        public const string DEFAULT_FORMAT_MONEY = "#" + Constant.SYMBOL_LINK_MONEY + "###";
        public const string DEFAULT_MONEY_SUBFIX = "Đ";

        public const string DEFAULT_FIRST_VALUE_COMBOBOX = "Tất cả";

        public const string DEFAULT_PASSWORD = "admin";

        public static string IS_SOLD = "Is Sold";

        public static string SORT_ASCENDING = "asc";
        public static string SORT_DESCENDING = "desc";

        public static Color COLOR_NORMAL = Color.Gray;
        public static Color COLOR_MOUSEOVER = Color.Orange;
        public static Color COLOR_DISABLE = Color.LightSlateGray;
        public static Color COLOR_IN_USE = Color.Red;
        public static Color COLOR_CHOOSEN_PRICE = Color.LightBlue;

        public const string TOOLTIP_USER = "Quản lý User";
        public const string TOOLTIP_KHACHHANG = "Quản lý Khách hàng";
        public const string TOOLTIP_SANPHAM = "Quản lý Sản phẩm";
        public const string TOOLTIP_KHOHANG = "Quản lý Kho hàng";
        public const string TOOLTIP_THUCHI = "Quản lý Thu - Chi";
        public const string TOOLTIP_THANHTOAN = "Thanh toán hóa đơn bán hàng";

        /// <summary>
        /// Ho ten, Gioi tinh, Nhom, Ten dang nhap, CMND, Dien thoai, Email
        /// </summary>
        public const string TOOLTIP_DETAIL_USER = "Họ tên: {0}\n" +
            "Giới tính: {1}\n" +
            "Nhóm: {2}\n" +
            "Tên đăng nhập: {3}\n" +
            "CMND: {4}\n" +
            "Điện thoại: {5}\n" +
            "Email: {6}";

        /// <summary>
        /// Ho ten, Gioi tinh, Ngay sinh, CMND, Dia chi, Dien thoai, DTDD, Email
        /// </summary>
        public const string TOOLTIP_DETAIL_KHACHHANG = "Họ tên: {0}\n" +
            "Giới tính: {1}\n" +
            "Ngày sinh: {2}\n" +
            "CMND: {3}\n" +
            "Địa chỉ: {4}\n" +
            "Điện thoại: {5}\n" +
            "ĐTDĐ: {6}\n" +
            "Email: {7}";

        /// <summary>
        /// Ten, Dia chi, Dien Thoai, Fax, Email
        /// </summary>
        public const string TOOLTIP_DETAIL_XUATXU = "Tên: {0}\n" +
            "Địa chỉ: {1}\n" +
            "Điện thoại: {2}\n" +
            "Fax: {3}\n" +
            "Email: {4}\n";

        public const string TOOLTIP_MUA_THEM = "Nhập danh mục sản phẩm có thể bán";
        public const string TOOLTIP_MUA_THEM_SAN_PHAM = "Thêm sản phẩm mới vào danh mục";
        public const string TOOLTIP_BAN_THEM = "Nhập sản phẩm vào kho hàng để bán";

        public const string TOOLTIP_TRA_SP_CONGNO = "Trả SP và hủy hóa đơn";
        public const string TOOLTIP_XUAT_KHO = "Xuất SP tồn trong kho";

        public const string DEFAULT_STATUS_SP_ALL = "Tất cả";
        public const string DEFAULT_STATUS_SP_NOT_ZERO = "Còn";
        public const string DEFAULT_STATUS_SP_ZERO = "Hết";

        public const string PAGE_TEXT = " Trang";
        public const string SEPERATE_STRING = ", ";
        public const string SYMBOL_LINK_STRING = " - ";
        public const string SYMBOL_LINK_PATH = "_";
        public const string SYMBOL_LINK_MONEY = ",";
        public const string SYMBOL_DISCOUNT = "%";

        public const string DEFAULT_AVATAR_EXT = ".jpg";

        public const int ID_GROUP_ADMIN = 1;
        public const int ID_GROUP_USER = 2;

        public const int ID_GROUP_KHACH_THUONG = 1;
        public const int ID_GROUP_KHTT = 2;
        public const int ID_GROUP_TVND = 3;
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
        public const int ID_TYPE_MUA_CHI = 5;
        public const int ID_TYPE_BAN_THU = 6;

        public const string PREFIX_MUA = "M";
        public const string PREFIX_BAN = "B";
        public const string PREFIX_THU = "T";
        public const string PREFIX_CHI = "C";

        public const int ID_STATUS_DONE = 1;
        public const int ID_STATUS_DEBT = 2;
        public const string STATUS_DONE = "Trả hết";
        public const string STATUS_DEBT = "Nợ";
        public const string DEFAULT_MONEY_STATUS_DONE = "Hồi lại:";
        public const string DEFAULT_MONEY_STATUS_DEBT = "Còn lại:";

        public const string DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME = "Excel File";
        public const string DEFAULT_EXPORT_EXCEL_FILE_TYPE = "xls";
        public const string DEFAULT_EXPORT_EXCEL_DATE_FORMAT = "ddMMyyyy";

        public const string CAPTION_CONFIRM = "CONFIRM";
        public const string CAPTION_WARNING = "WARNING";
        public const string CAPTION_ERROR = "ERROR";

        public const string MESSAGE_NEW_LINE = "\r\n";
        public const string MESSAGE_EXIT = "Thoát?";
        public const string MESSAGE_EXIT_APP = "Thoát chương trình?";
        public const string MESSAGE_CONTINUE = "Bạn có muốn tiếp tục?";

        public const string MESSAGE_SEND_BACK_NOTE = "Trả SP: {0}";
        public const string MESSAGE_SEND_BACK_CONFIRM = "Hoàn trả sản phẩm?";
        public const string MESSAGE_SEND_BACK_SUCCESS = "Sản phẩm đã được hoàn trả vào kho.";
        public const string MESSAGE_SEND_BACK_ERROR = "Quá trình hoàn trả thất bại!";

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
        public const string MESSAGE_CONFIRM_SELF_DESTRUCTION = "Hủy tài khoản và thoát chương trình?";
        public const string MESSAGE_CONFIRM_PAY_DEBT = "Thanh toán hóa đơn?";

        public const string MESSAGE_ERROR = "Có lỗi!";
        public const string MESSAGE_ERROR_NULL_DATA = "Dữ liệu không tồn tại!";
        public const string MESSAGE_ERROR_MISSING_RESOURCE = "Vui lòng kiểm tra thư mục Resource!";

        /// <summary>
        /// Vui lòng nhập dữ liệu {0} trước! 
        /// </summary>
        public const string MESSAGE_ERROR_MISSING_DATA = "Vui lòng nhập dữ liệu {0} trước!";

        public const string MESSAGE_ERROR_MISSING_MONEY = "Sản phẩm chưa có giá bán!";
        public const string MESSAGE_ERROR_DELETE_DATA = "Vui lòng kiểm tra dữ liệu có đang sử dụng hay không!";
        public const string MESSAGE_ERROR_DOB = "Vui lòng kiểm tra ngày sinh!" + MESSAGE_NEW_LINE + "(Từ {0} tuổi trở lên)";
        public const string MESSAGE_ERROR_EXPORT_EXCEL = "Không thể xuất dữ liệu!\nVui lòng thử lại.";
        public const string MESSAGE_ERROR_EXPORT_EXCEL_NULL_DATA = "Không có dữ liệu để xuất!";
        public const string MESSAGE_ERROR_EDIT_DATA = "Không thể thay đổi dữ liệu này!";

        public const string MESSAGE_ERROR_INSERT_HD_CHI = "Quá trình tạo hóa đơn Chi thất bại!";

        public const string MESSAGE_SUCCESS_EXPORT_EXCEL = "Dữ liệu đã xuất thành công.";
        public const string MESSAGE_PAY_ALL_DEBT = "Hóa đơn đã được thanh toán hết.";

        public const string MESSAGE_LOGIN_WRONG_USERNAME = "Tài khoản không tồn tai!";
        public const string MESSAGE_LOGIN_WRONG_PASS = "Mật khẩu không chính xác!";

        public const string MESSAGE_ERROR_TICH_LUY = "Không thể cập nhật Tích lũy của KH!";
        public const string MESSAGE_ERROR_CONFIRM_PASS = "Mật khẩu mới và xác nhận chưa trùng nhau!";
        public const string MESSAGE_ERROR_VERIFY_OLD_PASS = "Mật khẩu cũ không chính xác!";
        public const string MESSAGE_ERROR_DO_NOT_HAVE_PERMISSION = "Tài khoản không có quyền!";
        public const string MESSAGE_ERROR_EDIT_PROFILE_ADMIN = "Không thể thay đổi thông tin Admin khác!";
        public const string MESSAGE_ERROR_SELF_DESTRUCTION = "Vui lòng sử dụng chức năng \"Hủy tài khoản\"!";

        public const string MESSAGE_ERROR_OPEN_AVATAR = "Không thể mở hình!";
        public const string MESSAGE_ERROR_TOO_SMALL_AVATAR = "Kích thước ảnh quá nhỏ!";
        public const string MESSAGE_ERROR_SAVE_AVATAR = "Không lưu được hình! Vui lòng thử lại.";

        public const string SEARCH_USER_TIP = "Họ tên - Tên đăng nhập - Email";
        public const string SEARCH_KHACHHANG_TIP = "Mã KH - Họ tên - Điện thoại - Email";
        public const string SEARCH_SANPHAMGROUP_TIP = "Mã nhóm SP - Tên - Mô tả";
        public const string SEARCH_SANPHAM_TIP = "Mã SP - Tên - Xuất xứ - Mô tả";
        public const string SEARCH_XUATXU_TIP = "Tên - Địa chỉ - Email";
        public const string SEARCH_NHAPKHO_TIP = "Mã HĐ - Mã SP - Tên SP";
        public const string SEARCH_THU_TIP = "Mã HĐ - Mã KH - Tên KH - Ghi chú";
        public const string SEARCH_CHI_TIP = "Mã HĐ - Mã KH - Tên KH - Ghi chú";
        public const string SEARCH_CONGNO_TIP = "Mã HĐ - Mã KH - Tên KH - Ghi chú";
    }
}
