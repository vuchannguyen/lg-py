<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GhiDanh.aspx.cs" Inherits="GhiDanhWeb_HR_STG.WebForm1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    <link href="Styles/GhiDanh.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="DatetimePickerControl/css/jquery-ui-1.8.12.custom.css" />
    <script type="text/javascript" src="DatetimePickerControl/js/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="DatetimePickerControl/js/jquery-ui-1.8.12.custom.min.js"></script>
    <script type="text/jscript">
        $(function () {
            $("#dtpNgaySinh").datepicker({ dateFormat: 'dd-mm-yy' });
            $("#dtpNgayTuyenHua").datepicker({ dateFormat: 'dd-mm-yy' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titlenote">
        * GHI DANH TRỰC TUYẾN là một phần của phần mềm QUẢN LÝ NHÂN SỰ HR-STG. Mã nguồn
        và phát triển bới STG-Software.
        <br />
        <a class="event">Sự kiện: Cờ lau 711</a>
        <br />
        Hoạt động Ghi danh trực tuyến bắt đầu ngày 30/05/2011, kết thúc ngày 15/07/2011.
    </div>
    <table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="headerText">
                    <a class="titlenote">(*) là phần không thể bỏ trống</a><br />
                    Phần 1 - Lý lịch cá nhân
                </div>
                <div>
                    <p>
                        (Ảnh đại diện)</p>
                    <p class="center">
                        IDV:
                        <asp:DropDownList ID="ddlIDV" runat="server">
                        </asp:DropDownList>
                        &nbsp;Nhóm trách vụ:
                        <asp:DropDownList ID="ddlNhomTrachVu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNhomTrachVu_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;Trách vụ:
                        <asp:DropDownList ID="ddlTrachVu" runat="server">
                        </asp:DropDownList>
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Họ và tên:
                            </td>
                            <td>
                                <asp:TextBox ID="tbHoTen" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p class="center">
                        Ngày sinh:
                        <input type="text" id="dtpNgaySinh" />
                        <asp:Label ID="lbNgaySinh" runat="server" Text=""></asp:Label>
                    </p>
                    <p class="center">
                        <asp:RadioButton ID="rbNam" runat="server" Text="Nam" GroupName="GioiTinh" Checked="True" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbNu" runat="server" Text="Nữ" GroupName="GioiTinh" />
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Quê quán:
                            </td>
                            <td>
                                <asp:TextBox ID="tbQueQuan" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Trình độ học vấn:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTrinhDoHocVan" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Tôn giáo:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTonGiao" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Địa chỉ:
                            </td>
                            <td>
                                <asp:TextBox ID="tbDiaChi" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Điện thoại liên lạc:
                            </td>
                            <td>
                                <asp:TextBox ID="tbDienThoaiLienLac" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Email:
                            </td>
                            <td>
                                <asp:TextBox ID="tbEmail" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p class="center">
                        Kích cỡ áo:
                        <asp:RadioButton ID="rbNho" runat="server" Text="Nhỏ (S)" GroupName="Size" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbTrung" runat="server" Text="Trung (M)" GroupName="Size" Checked="True" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbLon" runat="server" Text="Lớn (L)" GroupName="Size" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbXL" runat="server" Text="(XL)" GroupName="Size" />
                    </p>
                    <p>
                        <span class="headerText">Phần 2 - Lý lịch Hướng đạo</span></p>
                    <p class="center">
                        Ngành đang sinh hoạt:
                        <asp:RadioButton ID="rbAu" runat="server" Text="Ấu" GroupName="Nganh" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbThieu" runat="server" Text="Thiếu" GroupName="Nganh" Checked="True" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbKha" runat="server" Text="Kha" GroupName="Nganh" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbTrang" runat="server" Text="Tráng" GroupName="Nganh" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbKhac" runat="server" Text="Khác" GroupName="Nganh" />
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Đơn vị:
                            </td>
                            <td>
                                <asp:TextBox ID="tbDonVi" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Liên đoàn:
                            </td>
                            <td>
                                <asp:TextBox ID="tbLienDoan" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Đạo:
                            </td>
                            <td>
                                <asp:TextBox ID="tbDao" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Châu:
                            </td>
                            <td>
                                <asp:TextBox ID="tbChau" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p class="center">
                        Ngày tuyên hứa:
                        <input type="text" id="dtpNgayTuyenHua" />
                        <asp:Label ID="lbNgayTuyenHua" runat="server" Text=""></asp:Label>
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Trưởng nhận lời hứa:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTruongNhanLoiHua" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Trách vụ kiêm nhiệm tại đơn vị:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTrachVuTaiDonVi" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Trách vụ kiêm nhiệm ngoài đơn vị:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTrachVuNgoaiDonVi" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Tên Rừng:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTenRung" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuContent">
                                Ghi chú:
                            </td>
                            <td>
                                <asp:TextBox ID="tbGhiChu" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p>
                        &nbsp;</p>
                    <p>
                        <span class="headerText">Phần 3 - Nghề nghiệp - Kỹ năng</span></p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Nghề nghiệp:
                            </td>
                            <td>
                                <asp:TextBox ID="tbNgheNghiep" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p>
                        Chuyên môn Hướng Đạo:
                    </p>
                    <p class="center">
                        <asp:CheckBox ID="chbNutDay" runat="server" Text="Nút dây" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chbPhuongHuong" runat="server" Text="Phương hướng" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chbCuuThuong" runat="server" Text="Cứu thương" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chbTruyenTin" runat="server" Text="Truyền tin" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chbTroChoi" runat="server" Text="Trò chơi" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chbLuaTrai" runat="server" Text="Lửa trại" />
                    </p>
                    <table width="100%">
                        <tr>
                            <td class="menuContent">
                                Sở trường:
                            </td>
                            <td>
                                <asp:TextBox ID="tbSoTruong" runat="server" Width="400px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <p class="headerText">
                        Vui lòng kiểm tra các thông tin vừa nhập..<br />
                        Sau đó nhấn [Hoàn tất], hồ sơ sẽ được chuyển trực tiếp đến Ban Tổ Chức.</p>
                    <div class="center">
                        <asp:Button ID="btHoanTat" runat="server" Text="Hoàn tất" OnClick="btHoanTat_Click" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
