using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using WinSCP;
using CrcSharp;

namespace cenCommon
{
    public static class LoaiManHinh
    {
        //Hàng nhập khẩu
        public const string IDKeHoachKhaiThacHangNhapKhau = "KTN";
        public const string NameKeHoachKhaiThacHangNhapKhau = "Kế hoạch khai thác hàng nhập khẩu";

        public const string IDLenhNhapKhoHangNhapKhau = "LNN";
        public const string NameLenhNhapKhoHangNhapKhau = "Lệnh nhập kho hàng nhập khẩu";
        public const string FileImportLenhNhapKhoHangNhapKhau = "ctLenhNhapKhoHangNhapKhau.xlsx";

        public const string IDPalletIDHangNhapKhau = "PIN";
        public const string NamePalletIDHangNhapKhau = "Pallet ID hàng nhập khẩu";

        public const string IDNhapPalletHangNhapKhau = "NPN";
        public const string NameNhapPalletHangNhapKhau = "Nhập pallet hàng nhập khẩu";

        public const string IDXacNhanHoanThanhNhapKhoHangNhapKhau = "XNN";
        public const string NameXacNhanHoanThanhNhapKhoHangNhapKhau = "Xác nhận hoàn thành nhập kho hàng nhập khẩu";

        public const string IDDaoChuyenHangNhapKhau = "DCN";
        public const string NameDaoChuyenHangNhapKhau = "Đảo chuyển hàng nhập khẩu";

        public const string IDDonGiaThuThemHangNhapKhau = "DGN";
        public const string NameDonGiaThuThemHangNhapKhau = "Đơn giá thu thêm hàng nhập khẩu";
        public const string FileImportDonGiaThuThemHangNhapKhau = "ctDonGiaThuThemHangNhapKhau.xlsx";

        public const string IDKhoaVanDonHangNhapKhau = "KBN";
        public const string NameKhoaVanDonHangNhapKhau = "Khoá vận đơn hàng nhập khẩu";

        public const string IDLenhXuatKhoHangNhapKhau = "LXN";
        public const string NameLenhXuatKhoHangNhapKhau = "Lệnh xuất kho hàng nhập khẩu";

        public const string IDBienNhanThanhToanHangNhapKhau = "BTN";
        public const string NameBienNhanThanhToanHangNhapKhau = "Biên nhận thanh toán cước hàng nhập khẩu";

        public const string IDXuatPalletHangNhapKhau = "XPN";
        public const string NameXuatPalletHangNhapKhau = "Xuất pallet hàng nhập khẩu";

        public const string IDXacNhanHoanThanhXuatKhoHangNhapKhau = "XXN";
        public const string NameXacNhanHoanThanhXuatKhoHangNhapKhau = "Xác nhận hoàn thành xuất kho hàng nhập khẩu";

        public const string IDHoaDonGTGT = "HDN";
        public const string NameHoaDonGTGT = "Hoá đơn GTGT";

        //Hàng xuất khẩu
        public const string IDKeHoachNhapKhoHangXuatKhau = "KNX";
        public const string NameKeHoachNhapKhoHangXuatKhau = "Kế hoạch nhập kho hàng xuất khẩu";
        public const string FileImportKeHoachNhapKhoHangXuatKhau = "ctKeHoachNhapKhoHangXuatKhau.xlsx";

        public const string IDLenhNhapKhoHangXuatKhau = "LNX";
        public const string NameLenhNhapKhoHangXuatKhau = "Lệnh nhập kho hàng xuất khẩu";
        public const string FileImportLenhNhapKhoHangXuatKhau = "ctLenhNhapKhoHangXuatKhau.xlsx";

        public const string IDNhapPalletHangXuatKhau = "NPX";
        public const string NameNhapPalletHangXuatKhau = "Nhập pallet hàng xuất khẩu";

        public const string IDXacNhanHoanThanhNhapKhoHangXuatKhau = "XNX";
        public const string NameXacNhanHoanThanhNhapKhoHangXuatKhau = "Xác nhận hoàn thành nhập kho hàng xuất khẩu";

        public const string IDDaoChuyenHangXuatKhau = "DCX";
        public const string NameDaoChuyenHangXuatKhau = "Đảo chuyển hàng xuất khẩu";

        public const string IDXuLyTinhTrangHangXuatKhau = "XTX";
        public const string NameXuLyTinhTrangHangXuatKhau = "Xử lý tình trạng hàng xuất khẩu";

        public const string IDDongHangXuatKhau = "DHX";
        public const string NameDongHangXuatKhau = "Đóng hàng xuất khẩu";
        public const string FileImportKeHoachDongHangXuatKhau = "ctKeHoachDongHangXuatKhau_NhieuContainer.xlsx";

        public const string IDContainerHangXuatKhauVaoKho = "CXV";
        public const string NameContainerHangXuatKhauVaoKho = "Container hàng xuất khẩu vào kho";

        public const string IDDongPalletHangXuatKhau = "DPX";
        public const string NameDongPalletHangXuatKhau = "Đóng pallet hàng xuất khẩu";

        public const string IDXacNhanHoanThanhDongHangXuatKhau = "XDX";
        public const string NameXacNhanHoanThanhDongHangXuatKhau = "Xác nhận hoàn thành đóng hàng xuất khẩu";

        public const string IDContainerHangXuatKhauRaKho = "CXR";
        public const string NameContainerHangXuatKhauRaKho = "Container hàng xuất khẩu ra kho";

        public const string IDLenhRutHangXuatKhau = "RHX";
        public const string NameLenhRutHangXuatKhau = "Lệnh rút hàng xuất khẩu";

        public const string IDRutPalletHangXuatKhau = "RPX";
        public const string NameRutPalletHangXuatKhau = "Rút pallet hàng xuất khẩu";

        public const string IDXacNhanHoanThanhRutHangXuatKhau = "XRX";
        public const string NameXacNhanHoanThanhRutHangXuatKhau = "Xác nhận hoàn thành rút hàng xuất khẩu";

        public const string IDTinhTonHangXuatKhau = "THX";
        public const string NameTinhTonHangXuatKhau = "Tịnh hàng xuất khẩu";
        //Thương vụ, kế toán
        public const string IDctChungTuKeToanSoDuDauKy = "301";
        public const string NameChungTuKeToanSoDuDauKy = "Số dư đầu kỳ";
        public const string IDctChungTuKeToanSoDuKhachHangDauKy = "302";
        public const string NameChungTuKeToanSoDuKhachHangDauKy = "Số dư khách hàng đầu kỳ";

        public const string IDctChungTuKeToanPhieuThuTienMat = "311";
        public const string NameChungTuKeToanPhieuThuTienMat = "Phiếu thu tiền mặt";
        public const string IDctChungTuKeToanPhieuThuTienGuiNganHang = "312";
        public const string NameChungTuKeToanPhieuThuTienGuiNganHang = "Phiếu thu ngân hàng";
        public const string IDctChungTuKeToanPhieuDoanhThu = "313";
        public const string NameChungTuKeToanPhieuDoanhThu = "Phiếu doanh thu";

        public const string IDctChungTuKeToanPhieuChiTienMat = "321";
        public const string NameChungTuKeToanPhieuChiTienMat = "Phiếu chi tiền mặt";
        public const string IDctChungTuKeToanPhieuChiTienGuiNganHang = "322";
        public const string NameChungTuKeToanPhieuChiTienGuiNganHang = "Phiếu chi ngân hàng";

        public const string IDctChungTuKeToanDeNghiThanhToanChiPhiHangNhapKhau = "323";
        public const string NameChungTuKeToanDeNghiThanhToanChiPhiHangNhapKhau = "Đề nghị thanh toán chi phí hàng nhập khẩu";
        public const string IDctChungTuKeToanDeNghiThanhToanChiPhiHangXuatKhau = "324";
        public const string NameChungTuKeToanDeNghiThanhToanChiPhiHangXuatKhau = "Đề nghị thanh toán chi phí hàng xuất khẩu";

        public const string IDctChungTuKeToan = "351";
        public const string NameChungTuKeToan = "Phiếu kế toán";
        public const string IDctChungTuKeToanKhauHaoTaiSanCoDinh = "352";
        public const string NameChungTuKeToanKhauHaoTaiSanCoDinh = "Phiếu khấu hao tài sản cố định";
        public const string IDctChungTuKeToanPhieuChiPhiHangNhapKhau = "361";
        public const string NameChungTuKeToanPhieuChiPhiHangNhapKhau = "Phiếu chi phí hàng nhập khẩu";
        public const string IDctChungTuKeToanPhieuChiPhiHangXuatKhau = "362";
        public const string NameChungTuKeToanPhieuChiPhiHangXuatKhau = "Phiếu chi phí hàng xuất khẩu";
        public const string IDctChungTuKeToanPhanBoChiPhi = "363";
        public const string NameChungTuKeToanPhanBoChiPhi = "Phiếu phân bổ chi phí";
        public const string IDctChungTuKeToanHoaDonMuaVao = "364";
        public const string NameChungTuKeToanHoaDonMuaVao = "Hóa đơn hàng hóa, dịch vụ mua vào";
        public const string IDctChungTuKeToanPhieuChiPhiChung = "365";
        public const string NameChungTuKeToanPhieuChiPhiChung = "Phiếu chi phí chung";
        //Cổng
        public const string IDXeVao = "701";
        public const string NameXeVao = "Xe vào";
        public const string IDXeRa = "751";
        public const string NameXeRa = "Xe ra";
        //Tác nghiệp VASSCM
        public const string IDeimSoDinhDanhHangHoa = "566-8";
        public const string NameeimSoDinhDanhHangHoa = "Khai báo số định danh hàng hóa nhập khẩu";
        public const string IDeimContainerNhapKho = "366-8";
        public const string NameeimContainerNhapKho = "Khai báo container hàng nhập khẩu vào kho";

        public const string IDeimContainerRutHang = "217-8";
        public const string NameeimContainerRutHang = "Khai báo container rút hàng";

        public const string IDeimHangKienNhapKho = "266-8";
        public const string NameeimHangKienNhapKho = "Khai báo hàng kiện nhập khẩu vào kho";

        public const string IDeimContainerXuatKho = "465-8";
        public const string NameeimContainerXuatKho = "Khai báo container rỗng ra kho";

        public const string IDeimContainerSuaNhapKho = "366-5";
        public const string NameeimContainerSuaNhapKho = "Khai báo sửa thông tin container hàng nhập khẩu vào kho";
        public const string IDeimContainerHuyNhapKho = "366-1";
        public const string NameeimContainerHuyNhapKho = "Khai báo hủy thông tin container hàng nhập khẩu vào kho";

        public const string IDeimHangKienSuaNhapKho = "266-5";
        public const string NameeimHangKienSuaNhapKho = "Khai báo sửa thông tin hàng kiện nhập khẩu vào kho";
        public const string IDeimHangKienHuyNhapKho = "266-1";
        public const string NameeimHangKienHuyNhapKho = "Khai báo hủy thông tin hàng kiện nhập khẩu vào kho";

        public const string IDeimThongTinVanDonDuDieuKienQuaKVGS = "223-8";
        public const string NameeimThongTinVanDonDuDieuKienQuaKVGS = "Thông tin vận đơn đủ điều kiện qua KVGS";

        public const string IDeimHangKienXuatKho = "321-8";
        public const string NameeimHangKienXuatKho = "Khai báo hàng kiện nhập khẩu xuất kho";

        public const string IDeexHangKienNhapKho = "266-8-2";
        public const string NameeexHangKienNhapKho = "Khai báo hàng kiện xuất khẩu nhập kho";

        public const string IDeexHangKienSuaNhapKho = "266-5-2";
        public const string NameeexHangKienSuaNhapKho = "Khai báo sửa thông tin hàng kiện xuất khẩu nhập kho";
        public const string IDeexHangKienHuyNhapKho = "266-1-2";
        public const string NameeexHangKienHuyNhapKho = "Khai báo hủy thông tin hàng kiện xuất khẩu nhập kho";

        public const string IDeexContainerNhapKho = "366-8-2";
        public const string NameeexContainerNhapKho = "Khai báo container rỗng hàng xuất khẩu nhập kho";


        public const string IDeexContainerDongHang = "227-8";
        public const string NameeexContainerDongHang = "Khai báo container hàng xuất khẩu đóng hàng";

        public const string IDeexContainerXuatKho = "365-8";
        public const string NameeexContainerXuatKho = "Khai báo container hàng xuất khẩu xuất kho";
        public const string IDeexHangKienGhepToKhaiNhanh = "509";
        public const string NameeexHangKienGhepToKhaiNhanh = "Khai báo ghép tờ khai nhánh hàng kiện xuất khẩu";

        public const string IDeexContainerSuaNhapKho = "502-2";
        public const string NameeexContainerSuaNhapKho = "Khai báo sửa thông tin container rỗng hàng xuất khẩu nhập kho";
        public const string IDeexContainerHuyNhapKho = "502-3";
        public const string NameeexContainerHuyNhapKho = "Khai báo hủy thông tin container rỗng hàng xuất khẩu nhập kho";

        public const string IDeexContainerSuaDongHang = "503-2";
        public const string NameeexContainerSuaDongHang = "Khai báo sửa thông tin container hàng xuất khẩu đóng hàng";
        public const string IDeexContainerHuyDongHang = "503-3";
        public const string NameeexContainerHuyDongHang = "Khai báo hủy thông tin container hàng xuất khẩu đóng hàng";

        public const string IDToKhaiGetIn = "601";
        public const string NameToKhaiGetIn = "GetIn Tờ khai";
        public const string IDToKhaiDuDieuKienQuaKVGS = "602";
        public const string NameToKhaiDuDieuKienQuaKVGS = "Thông tin tờ khai đủ điều kiện qua KVGS";
        public const string IDToKhaiGetOut = "603";
        public const string NameToKhaiGetOut = "GetOut tờ khai";

        public const string IDThongTinChuKyDienTu = "604";
        public const string NameThongTinChuKyDienTu = "Lấy thông tin chữ kí điện tử";
    }
    public static class ThamSoHeThong
    {
        //THAM SỐ HỆ THỐNG
        //Tham số loại đối tượng
        public const String MaThamSoLoaiDoiTuongNhomHangTau = "MaLoaiDoiTuong_NhomHangTau";
        public const String MaThamSoLoaiDoiTuongHangTau = "MaLoaiDoiTuong_HangTau";

        public const String MaThamSoLoaiDoiTuongTaiKhoanKeToan = "MaLoaiDoiTuong_TaiKhoanKeToan";

        public const String MaThamSoLoaiDoiTuongTaiKhoanNganHang = "MaLoaiDoiTuong_TaiKhoanNganHang";

        public const String MaThamSoLoaiDoiTuongNhomKhachHang = "MaLoaiDoiTuong_NhomKhachHang";
        public const String MaThamSoLoaiDoiTuongKhachHang = "MaLoaiDoiTuong_KhachHang";

        public const String MaThamSoLoaiDoiTuongNhomDaiLy = "MaLoaiDoiTuong_NhomDaiLy";
        public const String MaThamSoLoaiDoiTuongDaiLy = "MaLoaiDoiTuong_DaiLy";

        public const String MaThamSoLoaiDoiTuongNhomCang = "MaLoaiDoiTuong_NhomCang";
        public const String MaThamSoLoaiDoiTuongCang = "MaLoaiDoiTuong_Cang";

        public const String MaThamSoLoaiDoiTuongNhomBaiContainer = "MaLoaiDoiTuong_NhomBaiContainer";
        public const String MaThamSoLoaiDoiTuongBaiContainer = "MaLoaiDoiTuong_BaiContainer";

        public const String MaThamSoLoaiDoiTuongNhomLoaiContainer = "MaLoaiDoiTuong_NhomLoaiContainer";
        public const String MaThamSoLoaiDoiTuongLoaiContainer = "MaLoaiDoiTuong_LoaiContainer";

        public const String MaThamSoLoaiDoiTuongDonViVanTai = "MaLoaiDoiTuong_DonViVanTai";

        public const String MaThamSoLoaiDoiTuongNhomHangHoa = "MaLoaiDoiTuong_NhomHangHoa";
        public const String MaThamSoLoaiDoiTuongDonViTinh = "MaLoaiDoiTuong_DonViTinh";
        public const String MaThamSoLoaiDoiTuongTinhTrangHangHoa = "MaLoaiDoiTuong_TinhTrangHangHoa";

        public const String MaThamSoLoaiDoiTuongCuaLamHang = "MaLoaiDoiTuong_CuaLamHang";
        public const String MaThamSoLoaiDoiTuongNhomViTriKho = "MaLoaiDoiTuong_NhomViTriKho";
        public const String MaThamSoLoaiDoiTuongViTriKho = "MaLoaiDoiTuong_ViTriKho";
        public const String MaThamSoLoaiDoiTuongThuKho = "MaLoaiDoiTuong_ThuKho";
        public const String MaThamSoLoaiDoiTuongCongNhanBocXep = "MaLoaiDoiTuong_CongNhanBocXep";
        public const String MaThamSoLoaiDoiTuongXeNang = "MaLoaiDoiTuong_XeNang";

        public const String MaThamSoLoaiDoiTuongThueSuat = "MaLoaiDoiTuong_ThueSuat";
        public const String MaThamSoLoaiDoiTuongLoaiCuoc = "MaLoaiDoiTuong_LoaiCuoc";
        public const String MaThamSoLoaiDoiTuongTienTe = "MaLoaiDoiTuong_TienTe";
        public const String MaThamSoLoaiDoiTuongCuoc = "MaLoaiDoiTuong_Cuoc";
        public const String MaThamSoLoaiDoiTuongChiPhi = "MaLoaiDoiTuong_ChiPhi";
        public const String MaThamSoLoaiDoiTuongTyGia = "MaLoaiDoiTuong_TyGia";

        public const String MaThamSoLoaiDoiTuongKhachHangDoiLenh = "MaLoaiDoiTuong_KhachHangDoiLenh";

        public const String MaThamSoLoaiDoiTuongSize = "MaLoaiDoiTuong_Size";

        public const String MaThamSoLoaiDoiTuongMayInLPN = "MaLoaiDoiTuong_MayInLPN";
        //Danh sách tài khoản kế toán theo các loại phiếu
        public const String MaThamSoKeToanDanhSachTaiKhoanKeToanTienMat = "MaThamSoKeToan_DanhSachTaiKhoanTienMat";
        public const String MaThamSoKeToanDanhSachTaiKhoanKeToanTienGuiNganHang = "MaThamSoKeToan_DanhSachTaiKhoanTienGuiNganHang";
        public const String MaThamSoKeToanTaiKhoanTienMatMacDinh = "MaThamSoKeToan_TaiKhoanTienMatMacDinh";
        //Tham số danh mục chứng từ
        //Hàng nhập khẩu
        public const string MaThamSoChungTuKeHoachKhaiThacHangNhapKhau = "MaChungTu_KeHoachKhaiThacHangNhapKhau";
        public const string MaThamSoChungTuLenhNhapKhoHangNhapKhau = "MaChungTu_LenhNhapKhoHangNhapKhau";
        public const string MaThamSoChungTuPalletIDHangNhapKhau = "MaChungTu_PalletIDHangNhapKhau";
        public const string MaThamSoChungTuNhapPalletHangNhapKhau = "MaChungTu_NhapPalletHangNhapKhau";
        public const string MaThamSoChungTuXacNhanHoanThanhNhapKhoHangNhapKhau = "MaChungTu_XacNhanHoanThanhNhapKhoHangNhapKhau";
        public const string MaThamSoChungTuDaoChuyenHangNhapKhau = "MaChungTu_DaoChuyenHangNhapKhau";
        public const string MaThamSoChungTuLenhXuatKhoHangNhapKhau = "MaChungTu_LenhXuatKhoHangNhapKhau";
        public const string MaThamSoChungTuBienNhanThanhToanHangNhapKhau = "MaChungTu_BienNhanThanhToanHangNhapKhau";
        public const string MaThamSoChungTuXacNhanHoanThanhXuatKhoHangNhapKhau = "MaChungTu_XacNhanHoanThanhXuatKhoHangNhapKhau";
        public const string MaThamSoChungTuXuatPalletHangNhapKhau = "MaChungTu_XuatPalletHangNhapKhau";

        public const string MaThamSoChungTuHoaDonHangNhapKhau = "MaChungTu_HoaDonHangNhapKhau";

        public const string MaThamSoChungTuHangNhapKhauPhieuKiemHoa = "MaChungTu_HangNhapKhauPhieuKiemHoa";

        public const string MaThamSoChungTuHangNhapKhauYeuCauGiaoHang = "MaChungTu_HangNhapKhauYeuCauGiaoHang";
        public const string MaThamSoChungTuHangNhapKhauLenhGiaoHang = "MaChungTu_HangNhapKhauLenhGiaoHang";

        public const string MaThamSoChungTuHangNhapKhauPhieuDoanhThu = "MaChungTu_HangNhapKhauPhieuDoanhThu";
        public const string MaThamSoChungTuHangNhapKhauPhieuThuHoDaiLy = "MaChungTu_HangNhapKhauPhieuThuHoDaiLy";
        public const string MaThamSoChungTuHangNhapKhauPhieuXuat = "MaChungTu_HangNhapKhauPhieuXuat";

        public const string MaThamSoChungTuHangNhapKhauPhieuSanLuongThuDaiLy = "MaChungTu_HangNhapKhauPhieuSanLuongThuDaiLy";
        public const string MaThamSoChungTuHangNhapKhauPhieuSanLuongTraDaiLy = "MaChungTu_HangNhapKhauPhieuSanLuongTraDaiLy";
        //Hàng xuất khẩu
        public const string MaThamSoChungTuKeHoachNhapKhoHangXuatKhau = "MaChungTu_KeHoachNhapKhoHangXuatKhau";
        public const string MaThamSoChungTuLenhNhapKhoHangXuatKhau = "MaChungTu_LenhNhapKhoHangXuatKhau";
        public const string MaThamSoChungTuNhapPalletHangXuatKhau = "MaChungTu_NhapPalletHangXuatKhau";
        public const string MaThamSoChungTuDaoChuyenHangXuatKhau = "MaChungTu_DaoChuyenHangXuatKhau";
        public const string MaThamSoChungTuXacNhanHoanThanhNhapKhoHangXuatKhau = "MaChungTu_XacNhanHoanThanhNhapKhoHangXuatKhau";
        public const string MaThamSoChungTuXuLyTinhTrangHangXuatKhau = "MaChungTu_XuLyTinhTrangHangXuatKhau";
        public const string MaThamSoChungTuDongHangXuatKhau = "MaChungTu_DongHangXuatKhau";
        public const string MaThamSoChungTuContainerHangXuatKhauVaoKho = "MaChungTu_ContainerHangXuatKhauVaoKho";
        public const string MaThamSoChungTuDongPalletHangXuatKhau = "MaChungTu_DongPalletHangXuatKhau";
        public const string MaThamSoChungTuXacNhanHoanThanhDongHangXuatKhau = "MaChungTu_XacNhanHoanThanhDongHangXuatKhau";
        public const string MaThamSoChungTuLenhRutHangXuatKhau = "MaChungTu_LenhRutHangXuatKhau";
        public const string MaThamSoChungTuRutPalletHangXuatKhau = "MaChungTu_RutPalletHangXuatKhau";
        public const string MaThamSoChungTuXacNhanHoanThanhRutHangXuatKhau = "MaChungTu_XacNhanHoanThanhRutHangXuatKhau";

        public const string MaThamSoChungTuHangXuatKhauPhieuDoanhThu = "MaChungTu_HangXuatKhauPhieuDoanhThu";
        public const string MaThamSoChungTuHangXuatKhauXuatKho = "MaChungTu_HangXuatKhauXuatKho";
        public const string MaThamSoChungTuHangXuatKhauContainerRaKho = "MaChungTu_HangXuatKhauContainerRaKho";

        public const string MaThamSoChungTuTinhTonHangXuatKhau = "MaChungTu_TinhTonHangXuatKhau";

        public const string MaThamSoChungTuHangXuatKhauPhieuSanLuongPhiTongHop = "MaChungTu_HangXuatKhauPhieuSanLuongPhiTongHop";
        public const string MaThamSoChungTuHangXuatKhauPhieuSanLuongPhiLuuKho = "MaChungTu_HangXuatKhauPhieuSanLuongPhiLuuKho";
        public const string MaThamSoChungTuHangXuatKhauPhieuSanLuongPhiKhaiThacNgoaiGio = "MaChungTu_HangXuatKhauPhieuSanLuongPhiKhaiThacNgoaiGio";
        public const string MaThamSoChungTuHangXuatKhauPhieuSanLuongPhiTraHang = "MaChungTu_HangXuatKhauPhieuSanLuongPhiTraHang";
        public const string MaThamSoChungTuHangXuatKhauPhieuSanLuongPhiToKhai = "MaChungTu_HangXuatKhauPhieuSanLuongPhiToKhai";
        //Kế toán
        public const string MaThamSoChungTuKeToanSoDuDauKy = "MaChungTu_KeToanSoDuDauKy";
        public const string MaThamSoChungTuKeToanSoDuKhachHangDauKy = "MaChungTu_KeToanSoDuKhachHangDauKy";

        public const string MaThamSoChungTuKeToanPhieuThuTienMat = "MaChungTu_KeToanPhieuThuTienMat";
        public const string MaThamSoChungTuKeToanPhieuThuNganHang = "MaChungTu_KeToanPhieuThuNganHang";
        public const string MaThamSoChungTuKeToanPhieuDoanhThu = "MaChungTu_KeToanPhieuDoanhThu";

        public const string MaThamSoChungTuKeToanPhieuChiTienMat = "MaChungTu_KeToanPhieuChiTienMat";
        public const string MaThamSoChungTuKeToanPhieuChiNganHang = "MaChungTu_KeToanPhieuChiNganHang";

        public const string MaThamSoChungTuKeToanDeNghiThanhToanChiPhiHangNhapKhau = "MaChungTu_KeToanDeNghiThanhToanChiPhiHangNhapKhau";
        public const string MaThamSoChungTuKeToanDeNghiThanhToanChiPhiHangXuatKhau = "MaChungTu_KeToanDeNghiThanhToanChiPhiHangXuatKhau";

        public const string MaThamSoChungTuKeToanPhieuKeToan = "MaChungTu_KeToanPhieuKeToan";
        public const string MaThamSoChungTuKeToanKhauHaoTaiSanCoDinh = "MaChungTu_KeToanKhauHaoTaiSanCoDinh";

        public const string MaThamSoChungTuKeToanChiPhiHangNhapKhau = "MaChungTu_KeToanPhieuChiPhiHangNhapKhau";
        public const string MaThamSoChungTuKeToanChiPhiHangXuatKhau = "MaChungTu_KeToanPhieuChiPhiHangXuatKhau";

        public const string MaThamSoChungTuKeToanPhanBoChiPhi = "MaChungTu_KeToanPhanBoChiPhi";

        public const string MaThamSoChungTuKeToanHoaDonMuaVao = "MaChungTu_KeToanHoaDonMuaVao";

        public const string MaThamSoChungTuKeToanChiPhiChung = "MaChungTu_KeToanChiPhiChung";
        //Cổng
        public const string MaThamSoChungTuXeVao = "MaChungTu_XeVao";
        public const string MaThamSoChungTuXeRa = "MaChungTu_XeRa";
        //Chứng từ eCargo
        public const string MaThamSoChungTu_eimSoDinhDanhHangHoa = "MaChungTu_eimSoDinhDanhHangHoa";

        public const string MaThamSoChungTu_eimContainerNhapKho = "MaChungTu_eimContainerNhapKho";
        public const string MaThamSoChungTu_eimContainerRutHang = "MaChungTu_eimContainerRutHang";
        public const string MaThamSoChungTu_eimHangKienNhapKho = "MaChungTu_eimHangKienNhapKho";
        public const string MaThamSoChungTu_eimThongTinVanDonDuDieuKienQuaKVGS = "MaChungTu_eimThongTinVanDonDuDieuKienQuaKVGS";
        public const string MaThamSoChungTu_eimHangKienXuatKho = "MaChungTu_eimHangKienXuatKho";
        public const string MaThamSoChungTu_eimContainerXuatKho = "MaChungTu_eimContainerXuatKho";

        public const string MaThamSoChungTu_eimContainerSuaNhapKho = "MaChungTu_eimSuaThongTinContainerNhapKho";
        public const string MaThamSoChungTu_eimContainerHuyNhapKho = "MaChungTu_eimContainerHuyNhapKho";
        public const string MaThamSoChungTu_eimHangKienSuaNhapKho = "MaChungTu_eimHangKienSuaNhapKho";
        public const string MaThamSoChungTu_eimHuyThongTinHangKienNhapKho = "MaChungTu_eimHuyThongTinHangKienNhapKho";

        public const string MaThamSoChungTu_eexHangKienNhapKho = "MaChungTu_eexHangKienNhapKho";
        public const string MaThamSoChungTu_eexHangKienGhepToKhaiNhanh = "MaChungTu_eexHangKienGhepToKhaiNhanh";
        public const string MaThamSoChungTu_eexContainerNhapKho = "MaChungTu_eexContainerNhapKho";
        public const string MaThamSoChungTu_eexContainerDongHang = "MaChungTu_eexContainerDongHang";
        public const string MaThamSoChungTu_eexContainerXuatKho = "MaChungTu_eexContainerXuatKho";

        public const string MaThamSoChungTu_eexHangKienSuaNhapKho = "MaChungTu_eexHangKienSuaNhapKho";
        public const string MaThamSoChungTu_eexHangKienHuyNhapKho = "MaChungTu_eexHangKienHuyNhapKho";
        public const string MaThamSoChungTu_eexContainerSuaNhapKho = "MaChungTu_eexContainerSuaNhapKho";
        public const string MaThamSoChungTu_eexContainerHuyNhapKho = "MaChungTu_eexContainerHuyNhapKho";
        public const string MaThamSoChungTu_eexContainerSuaDongHang = "MaChungTu_eexContainerSuaDongHang";
        public const string MaThamSoChungTu_eexContainerHuyDongHang = "MaChungTu_eexContainerHuyDongHang";

        public const string MaThamSoChungTuToKhaiGetIn = "MaChungTu_ToKhaiGetIn";
        public const string MaThamSoChungTuToKhaMaThamSoChungTuuDieuKienQuaKVGS = "MaChungTu_ToKhaMaThamSoChungTuuDieuKienQuaKVGS";
        public const string MaThamSoChungTuToKhaiGetOut = "MaChungTu_ToKhaiGetOut";

        public const string MaThamSoChungTuSuaThongTinGetIn = "MaChungTu_SuaThongTinGetIn";
        public const string MaThamSoChungTuContainerHuyThongTinGetIn = "MaChungTu_ContainerHuyThongTinGetIn";
        public const string MaThamSoChungTuHangKienHuyThongTinGetIn = "MaChungTu_HangKienHuyThongTinGetIn";
        public const string MaThamSoChungTuThongTinChuKyDienTu = "MaChungTu_ThongTinChuKyDienTu";
        //Mã tham số mã đối tượng
        public const string MaThamSoDoiTuongPhiQuanLy = "MaDoiTuong_PhiQuanLy";
        //Tham số kết nối eCargo
        public const String eCargoFileCert = "eCargo_FileCert";
        public const String eCargoPublicKey = "eCargo_PublicKey";
        public const String eCargoMaKho = "eCargo_MaKho";
        public const String eCargoMaSoDoanhNghiep = "eCargo_MaSoDoanhNghiep";
        public const String eCargoTenDoanhNghiep = "eCargo_TenDoanhNghiep";
        public const String eCargoMaChiCucHaiQuan = "eCargo_MaChiCucHaiQuan";
        public const String eCargoTenChiCucHaiQuan = "eCargo_TenChiCucHaiQuan";
        public const String eCargoTenUngDungHaiQuan = "eCargo_TenUngDungHaiQuan";
        public const String eCargoTenUngDung = "eCargo_TenUngDung";
        public const String eCargoUserName = "eCargo_UserName";
        public const String eCargoPasword = "eCargo_Password";
        //Tham số phát hành hóa đơn điện tử VNPT
        public const String MaThamSoHoaDonDienTuVNPT_PublishService = "MaThamSoHoaDonDienTuVNPT_PublishService";
        public const String MaThamSoHoaDonDienTuVNPT_BusinessService = "MaThamSoHoaDonDienTuVNPT_BusinessService";
        public const String MaThamSoHoaDonDienTuVNPT_PortalService = "MaThamSoHoaDonDienTuVNPT_PortalService";
        public const String MaThamSoHoaDonDienTuVNPT_AdminAccountName = "MaThamSoHoaDonDienTuVNPT_AdminAccountName";
        public const String MaThamSoHoaDonDienTuVNPT_AdminAccountPassword = "MaThamSoHoaDonDienTuVNPT_AdminAccountPassword";
        public const String MaThamSoHoaDonDienTuVNPT_AccountName = "MaThamSoHoaDonDienTuVNPT_AccountName";
        public const String MaThamSoHoaDonDienTuVNPT_AccountPassword = "MaThamSoHoaDonDienTuVNPT_AccountPassword";
        public const String MaThamSoHoaDonDienTuVNPT_InvoicePattern = "MaThamSoHoaDonDienTuVNPT_InvoicePattern";
        public const String MaThamSoHoaDonDienTuVNPT_InvoiceSerial = "MaThamSoHoaDonDienTuVNPT_InvoiceSerial";

        public const String MaThamSoHoaDonDienTuVNPT_importAndPublishInv = "MaThamSoHoaDonDienTuVNPT_importAndPublishInv";
        public const String MaThamSoHoaDonDienTuVNPT_replaceInvoice = "MaThamSoHoaDonDienTuVNPT_replaceInvoice";
        public const String MaThamSoHoaDonDienTuVNPT_adjustInvoice = "MaThamSoHoaDonDienTuVNPT_adjustInvoice";
        public const String MaThamSoHoaDonDienTuVNPT_cancelInv = "MaThamSoHoaDonDienTuVNPT_cancelInv";
        public const String MaThamSoHoaDonDienTuVNPT_updateCustomer = "MaThamSoHoaDonDienTuVNPT_updateCustomer";
        public const String MaThamSoHoaDonDienTuVNPT_downloadInvFkeyNoPay = "MaThamSoHoaDonDienTuVNPT_downloadInvFkeyNoPay";
        public const String MaThamSoHoaDonDienTuVNPT_convertForStoreFkey = "MaThamSoHoaDonDienTuVNPT_convertForStoreFkey";

       
    }
    public static class GlobalVariables
    {
        public const string CFSPassword = "-={~CFS.S3+Viet+Nam~}=-";
        //Trạng thái trao đổi dữ liệu eCargo
        public const String functionGui = "8";
        public const String functionChoXuLy = "12";
        public const String functionHoiPhanHoi = "13";
        public const String functionKhongHopLe = "27";
        public const String functionCapSoTiepNhan = "29";
        public const String functionTraDuLieu = "32";
        //Cấu trúc của msg eCargo
        public const String _xmlStatementBegin = "<function>";
        public const String _xmlStatementEnd = "</function>";
        public const String _xmlContentBegin = "<content>";
        public const String _xmlContentEnd = "</content>";
        //Loại container
        public const String contType_Kho = "1";
        public const String contTypeName_Kho = "Container khô";
        public const String contType_HangRoi = "2";
        public const String contTypeName_HangRoi = "Container hàng rời";
        public const String contType_ChuyenDung = "3";
        public const String contTypeName_ChuyenDung = "Container chuyên dụng";
        public const String contType_Lanh = "4";
        public const String contTypeName_Lanh = "Container lạnh";
        public const String contType_MoNoc = "5";
        public const String contTypeName_MoNoc = "Container mở nóc (opentop)";
        public const String contType_MatBang = "6";
        public const String contTypeName_MatBang = "Container mặt bằng";
        public const String contType_Bon = "7";
        public const String contTypeName_Bon = "Container bồn";
        //Kích thước container
        public const String contSize_20Dry = "1";
        public const String contSizeName_20Dry = "20 dry";
        public const String contSize_20OpenTop = "2";
        public const String contSizeName_20OpenTop = "20 opentop";
        public const String contSize_20Refrigerated = "3";
        public const String contSizeName_20Refrigerated = "20 refrigerated";
        public const String contSize_20FlatRack = "4";
        public const String contSizeName_20FlatRack = "20 flat rack";
        public const String contSize_40Dry = "5";
        public const String contSizeName_40Dry = "40 dry";
        public const String contSize_40HighCube = "6";
        public const String contSizeName_40HighCube = "40 high cube";
        public const String contSize_40OpenTop = "7";
        public const String contSizeName_40OpenTop = "40 open top";
        public const String contSize_40Refrigerated = "8";
        public const String contSizeName_40Refrigerated = "40 refrigerated";
        public const String contSize_40HighCubeRefrigerated = "9";
        public const String contSizeName_40HighCubeRefrigerated = "40 high cube refrigerated";
        public const String contSize_40FlatRack = "10";
        public const String contSizeName_40FlatRack = "40 flat rack";
        public const String contSize_20HighCube = "11";
        public const String contSizeName_20HighCube = "20 high cube";
        public const String contSize_45Dry = "12";
        public const String contSizeName_45Dry = "45 dry";
        public const String contSize_20Tank = "13";
        public const String contSizeName_20Tank = "20 tank";
        public const String contSize_40Tank = "14";
        public const String contSizeName_40Tank = "40 tank";
        public const String contSize_20Wide = "15";
        public const String contSizeName_20Wide = "20 wide";
        public const String contSize_40Wide = "16";
        public const String contSizeName_40Wide = "40 wide";
        //
        public const String LoaiHangTatCa = "0";
        public const String LoaiHangNhapKhau = "1";
        public const String LoaiHangXuatKhau = "2";
        //
        public const String LoaiHoaDonNhap = "0";
        public const String TenLoaiHoaDonNhap = "Hóa đơn nháp";
        public const String LoaiHoaDonGoc = "1";
        public const String TenLoaiHoaDonGoc = "Hóa đơn gốc";
        public const String LoaiHoaDonDieuChinhTangTien = "2";
        public const String TenLoaiHoaDonDieuChinhTangTien = "Hóa đơn điều chỉnh tăng tiền";
        public const String LoaiHoaDonDieuChinhGiamTien = "3";
        public const String TenLoaiHoaDonDieuChinhGiamTien = "Hóa đơn điều chỉnh giảm tiền";
        public const String LoaiHoaDonDieuChinhThongTin = "4";
        public const String TenLoaiHoaDonDieuChinhThongTin = "Hóa đơn điều chỉnh thông tin";
        public const String LoaiHoaDonThayThe = "5";
        public const String TenLoaiHoaDonThayThe = "Hóa đơn thay thế";
        public const String LoaiHoaDonBiDieuChinh = "6";
        public const String TenLoaiHoaDonBiDieuChinh = "Hóa đơn bị điều chỉnh";
        public const String LoaiHoaDonBiThayThe = "7";
        public const String TenLoaiHoaDonBiThayThe = "Hóa đơn bị thay thế";
        public const String LoaiHoaDonHuy = "8";
        public const String TenLoaiHoaDonHuy = "Hóa đơn huỷ";


        public const String LoaiBienNhanThanhToanTamThu = "0";
        public const String TenLoaiBienNhanThanhToanTamThu = "Biên nhận thanh toán tạm thu";
        public const String LoaiBienNhanThanhToanGoc = "1";
        public const String TenLoaiBienNhanThanhToanGoc = "Biên nhận thanh toán gốc";
        public const String LoaiBienNhanThanhToanDieuChinhTangTien = "2";
        public const String TenLoaiBienNhanThanhToanDieuChinhTangTien = "Biên nhận thanh toán điều chỉnh tăng tiền";
        public const String LoaiBienNhanThanhToanDieuChinhGiamTien = "3";
        public const String TenLoaiBienNhanThanhToanDieuChinhGiamTien = "Biên nhận thanh toán điều chỉnh giảm tiền";
        public const String LoaiBienNhanThanhToanThayThe = "4";
        public const String TenLoaiBienNhanThanhToanThayThe = "Biên nhận thanh toán thay thế";
        public const String LoaiBienNhanThanhToanBiDieuChinh = "5";
        public const String TenLoaiBienNhanThanhToanBiDieuChinh = "Biên nhận thanh toán bị điều chỉnh";
        public const String LoaiBienNhanThanhToanBiThayThe = "6";
        public const String TenLoaiBienNhanThanhToanBiThayThe = "Biên nhận thanh toán bị thay thế";
        public const String LoaiBienNhanThanhToanHuy = "7";
        public const String TenLoaiBienNhanThanhToanHuy = "Biên nhận thanh toán huỷ";



        public const String LoaiHangHoaDichVu_HangHoaDichVu = "1";
        public const String TenLoaiHangHoaDichVu_HangHoaDichVu = "Hàng hóa, dịch vụ";
        public const String LoaiHangHoaDichVu_GhiChu = "4";
        public const String TenLoaiHangHoaDichVu_GhiChu = "Ghi chú";
        //
        public const String LoaiDoanhThuCont = "0";
        public const String LoaiDoanhThuVanDon = "1";
        public const String LoaiDoanhThuDienGiai = "2";
        public const String LoaiDoanhThuHangHoaDichVu = "3";
        //
        public const String KieuThuNgay = "1";
        public const String KieuThuSau = "2";
        //
        public const String HinhThucThanhToanTienMatChuyenKhoan = "1";
        public const String TenHinhThucThanhToanTienMatChuyenKhoan = "TM/CK";
        public const String HinhThucThanhToanTienMat = "2";
        public const String TenHinhThucThanhToanTienMat = "TM";
        public const String HinhThucThanhToanChuyenKhoan = "3";
        public const String TenHinhThucThanhToanChuyenKhoan = "CK";

        //
        public const String KieuHoaDonChiTietCuoc = "1";
        public const String KieuHoaDonTongHopCuocTheoBillCont = "2";
        public const String KieuHoaDonTongHopCuocTheoMaCuoc = "3";
        public const String KieuHoaDonTongHopCuocTheoLoaiCuoc = "4";
        //
        public const String LoaiChiPhiDungRiengChiuThueDuDieuKienKhauTru = "1";
        public const String TenLoaiChiPhiDungRiengChiuThueDuDieuKienKhauTru = "1.Hàng hóa, dịch vụ dùng riêng cho SXKD chịu thuế GTGT và sử dụng cho các hoạt động cung cấp hàng hóa, dịch vụ không kê khai, nộp thuế GTGT đủ điều kiện khấu trừ thuế:";
        public const String LoaiChiPhiKhongDuDieuKienKhauTru = "2";
        public const String TenLoaiChiPhiKhongDuDieuKienKhauTru = "2.Hàng hóa, dịch vụ không đủ điều kiện khấu trừ thuế:";
        public const String LoaiChiPhiDungChungDuDieuKienKhauTru = "3";
        public const String TenLoaiChiPhiDungChungDuDieuKienKhauTru = "3.Hàng hóa, dịch vụ dùng chung cho SXKD chịu thuế và không chịu thuế đủ điều kiện khấu trừ thuế:";
        public const String LoaiChiPhiDuAnDauTuDuDieuKienKhauTru = "4";
        public const String TenLoaiChiPhiDuAnDauTuDuDieuKienKhauTru = "4.Hàng hóa, dịch vụ dùng cho dự án đầu tư đủ điều kiện khấu trừ thuế:";
        public const String LoaiChiPhiKhongPhaiTongHopTrenToKhai01 = "5";
        public const String TenLoaiChiPhiKhongPhaiTongHopTrenToKhai01 = "5.Hàng hóa, dịch vụ không phải tổng hợp trên tờ khai 01/GTGT";
        //
        public const String TrangThaiChungTuThue = "1";
        public const String TenTrangThaiChungTuThue = "Tax";
        public const String TrangThaiChungTuNoiBo = "2";
        public const String TenTrangThaiChungTuNoiBo = "Internal";
        //
        public const String QRCodeThanhToanDynamic1 = "00020101021138540010A00000072701240006970436011010340105680208QRIBFTTA530370454";
        public const String QRCodeThanhToanDynamic2 = "5802VN62";
        public const String QRCodeThanhToanDynamic3 = "6304";
    }

    public class FTP
    {
        public static void downloadFiles(string host, string username, string password, string serverFolder, string localFolder)
        {
            // Setup session options
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = host,
                UserName = username,
                Password = password,
            };

            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);

                // Download files
                session.GetFiles($"/{serverFolder}/*", localFolder);

                session.Close();
            }
        }
        public static void deleteFile(string host, string username, string password, string serverFolder, string filename)
        {
            // Setup session options
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = host,
                UserName = username,
                Password = password,
            };

            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);

                // Download files
                session.RemoveFile($"/{serverFolder}/{filename}");
                session.Close();
            }
        }
    }
    public class cenCommon
    {
        public static string CreateDynamicQRCode(string QRCodeDynamicPart1, string QRCodeDynamicPart2, string QRCodeDynamicPart3, string NoiDungThanhToan, string SoTien)
        {
            string QRCodeDynamic = QRCodeDynamicPart1;
            string lenSoTien = SoTien.Length.ToString("0#");
            string lenNoiDungThanhToan1 = (("08" + NoiDungThanhToan).Length + 2).ToString("0#");
            string lenNoiDungThanhToan2 = (NoiDungThanhToan).Length.ToString("0#");
            QRCodeDynamic += lenSoTien + SoTien.ToString() + QRCodeDynamicPart2 + lenNoiDungThanhToan1 + "08" + lenNoiDungThanhToan2 + NoiDungThanhToan + QRCodeDynamicPart3;
            var data = Encoding.ASCII.GetBytes(QRCodeDynamic);
            var crc16 = new Crc(new CrcParameters(16, 0x1021, 0xffff, 0x0000, false, false));
            ulong result = crc16.CalculateAsNumeric(data);
            string CRC = result.ToString("X4");
            QRCodeDynamic += CRC;
            return QRCodeDynamic;
        }

    }
}
