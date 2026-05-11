UPDATE DanhMucKhachHangDoiLenh
SET [Password] = N'tiZ/DEci13zrkFXiWFTzCQ==', ---123654789
    EditDate = GETDATE()
WHERE Email = N'admin@glc.vn'
  AND PartnerGUID = N'77E17C7F-2AD9-40EC-9916-C66525450449';



--Cap nhat tai khoan admin sau khi dang ky mot tai khoản thường chạy lệnh sau--
UPDATE [dbo].[DanhMucKhachHangDoiLenh]
SET 
    [Password] = N'tiZ/DEci13zrkFXiWFTzCQ==', -- Mật khẩu: 123654789
    [KichHoat] = 1,                          -- Kích hoạt tài khoản (cột cũ)
    [IsActive] = 1,                          -- Kích hoạt tài khoản (cột mới thêm)
    [LoaiTaiKhoan]=0,
    [PartnerGUID] = N'77E17C7F-2AD9-40EC-9916-C66525450449',
    [EditDate] = GETDATE()
WHERE [Email] = N'admin@glc.vn'
 -- AND [PartnerGUID] = N'c8d822c3-74aa-418b-85dd-676dc6ea10c8';

-- Kiểm tra xem có dòng nào được cập nhật không
IF @@ROWCOUNT = 0
    PRINT N'Không tìm thấy bản ghi thỏa mãn điều kiện Email và PartnerGUID.';
ELSE
    PRINT N'Cập nhật mật khẩu và kích hoạt tài khoản thành công!';
GO