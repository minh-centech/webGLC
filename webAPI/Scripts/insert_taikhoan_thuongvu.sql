INSERT INTO DanhMucKhachHangDoiLenh(
    [IDDanhMucDonVi],
    [IDDanhMucLoaiDoiTuong],
    [Email],
    [Ten],
    [SoDienThoai],
    [Password],
    [PartnerGUID],
    [KichHoat],
    [IDDanhMucNguoiSuDungCreate],
    [CreateDate],
    [LoaiTaiKhoan],
    [IsActive]
)
VALUES (
    1,                                 -- IDDanhMucDonVi (thay bằng ID đơn vị thực tế)
    1,                                 -- IDDanhMucLoaiDoiTuong (thay bằng ID loại đối tượng thực tế)
    N'thuongvu@glc.vn',           -- Email tài khoản
    N'Thương Vụ',       -- Tên khách hàng
    '0000000000',                      -- Số điện thoại
    'Password_Ma_Hoa_O_Day',           -- Mật khẩu (thường là chuỗi đã mã hóa hash)
    NEWID(),                           -- PartnerGUID (tự động tạo chuỗi GUID mới)
    1,                                 -- KichHoat (1: Đã kích hoạt, 0: Chưa)
    1,                                 -- IDDanhMucNguoiSuDungCreate (ID người tạo)
    GETDATE(),                         -- CreateDate (lấy thời gian hiện tại)
    3,                                 -- LoaiTaiKhoan (3 chính là Thương vụ bạn vừa thêm)
    1                                  -- IsActive (1: Đang hoạt động)
);
GO