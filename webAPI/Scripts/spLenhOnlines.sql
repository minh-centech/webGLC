if object_id(N'dbo.List_tblLenhOnlines', N'P') is null
	exec('create procedure dbo.List_tblLenhOnlines as begin set nocount on; end');
go

ALTER PROCEDURE dbo.List_tblLenhOnlines
	@ID							BIGINT = NULL,
	@IDDanhMucKhachHangDoiLenh	BIGINT = NULL,
	@TuNgay						DATETIME = NULL,
	@DenNgay					DATETIME = NULL,
	@HouseBill					NVARCHAR(100) = NULL,
	@SoCont						NVARCHAR(50) = NULL,
	@MaSoThue					NVARCHAR(50) = NULL,
	@Page						INT = 1,
	@PageSize					INT = 10,
	@TrangThaiThanhToanBNG		INT = -1 -- -1: Tất cả, 1: Đã thanh toán, 0: Chưa thanh toán
AS
BEGIN
	SET NOCOUNT ON;

	SET @HouseBill = dbo.ChuanHoaChuoi(@HouseBill);
	SET @SoCont = dbo.ChuanHoaChuoi(@SoCont);
	SET @MaSoThue = dbo.ChuanHoaChuoi(@MaSoThue);
	SET @Page = ISNULL(NULLIF(@Page, 0), 1);
	SET @PageSize = ISNULL(NULLIF(@PageSize, 0), 10);

	IF @Page < 1 SET @Page = 1;
	IF @PageSize < 1 SET @PageSize = 10;

	-- 1. Thống kê biên nhận theo từng Detail ID
	WITH ReceiptSummary AS (
		SELECT 
			IDctLenhNhapKhoHangNhapKhauChiTiet,
			SUM(CASE WHEN DaThanhToan = 1 THEN 1 ELSE 0 END) AS SoBienNhanDaThanhToan,
			SUM(CASE WHEN DaThanhToan = 0 THEN 1 ELSE 0 END) AS SoBienNhanChuaThanhToan,
			MAX(
				CASE 
					WHEN IDctLenhXuatKhoHangNhapKhau IS NOT NULL 
					THEN CAST(DaThanhToan AS INT) 
					ELSE 0 
				END
			) AS BienNhanThanhToanGoc
		FROM ctBienNhanThanhToanHangNhapKhauTemp WITH (NOLOCK)
		WHERE IDctLenhNhapKhoHangNhapKhauChiTiet IS NOT NULL
		GROUP BY IDctLenhNhapKhoHangNhapKhauChiTiet
	),
	-- 2. Lọc danh sách Lệnh Online theo đầy đủ điều kiện (bao gồm TrangThaiThanhToanBNG)
	Filtered AS (
		SELECT
			lo.ID,
			lo.SoThuTuLenh,
			lo.HoVaTen,
			lo.SoDienThoai,
			lo.SoCMND,
			lo.SoXe,
			lo.MaSoThue,
			lo.TenCongTy,
			lo.DiaChi,
			lo.Email,
			lo.HouseBill,
			lo.NgayLamLenh,
			lo.SoCont,
			lo.NgayLayHang,
			lo.SoToKhai,
			lo.TrangThai,
			lo.IDDanhMucKhachHangDoiLenh,
			kh.Email AS EmailNguoiTao,
			kh.Ten AS TenNguoiTao,
			lo.IDctLenhNhapKhoHangNhapKhauChiTiet,
			ISNULL(CAST(lo.HoanThanh AS INT), 0) AS IsHoanThanh,
			lo.CreateDate,
			lo.EditDate,
			ISNULL(rc.SoBienNhanDaThanhToan, 0) AS SoBienNhanDaThanhToan,
			ISNULL(rc.SoBienNhanChuaThanhToan, 0) AS SoBienNhanChuaThanhToan,
			ISNULL(rc.BienNhanThanhToanGoc, 0) AS BienNhanThanhToanGoc
		FROM tblLenhOnlines lo WITH (NOLOCK)
		LEFT JOIN DanhMucKhachHangDoiLenh kh WITH (NOLOCK) ON kh.ID = lo.IDDanhMucKhachHangDoiLenh
		LEFT JOIN ReceiptSummary rc ON rc.IDctLenhNhapKhoHangNhapKhauChiTiet = lo.IDctLenhNhapKhoHangNhapKhauChiTiet
		WHERE (@ID IS NULL OR lo.ID = @ID)
			AND (@IDDanhMucKhachHangDoiLenh IS NULL OR lo.IDDanhMucKhachHangDoiLenh = @IDDanhMucKhachHangDoiLenh)
			AND (@TuNgay IS NULL OR CONVERT(DATE, lo.NgayLamLenh) >= CONVERT(DATE, @TuNgay))
			AND (@DenNgay IS NULL OR CONVERT(DATE, lo.NgayLamLenh) <= CONVERT(DATE, @DenNgay))
			AND (ISNULL(@HouseBill, '') = '' OR lo.HouseBill = @HouseBill)
			AND (ISNULL(@SoCont, '') = '' OR lo.SoCont = @SoCont)
			AND (ISNULL(@MaSoThue, '') = '' OR lo.MaSoThue = @MaSoThue)
			-- Lọc theo Trạng thái biên nhận gốc ngay tại đây
			AND (@TrangThaiThanhToanBNG = -1 OR ISNULL(rc.BienNhanThanhToanGoc, 0) = @TrangThaiThanhToanBNG)
	)
	-- 3. Phân trang và trả kết quả chính xác
	SELECT
		*,
		COUNT(1) OVER() AS TotalCount
	FROM Filtered
	ORDER BY CASE WHEN TrangThai = 0 THEN 0 ELSE 1 END, NgayLamLenh DESC, ID DESC
	OFFSET (@Page - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END
GO

if object_id(N'dbo.Insert_tblLenhOnlines', N'P') is null
	exec('create procedure dbo.Insert_tblLenhOnlines as begin set nocount on; end');
go

alter procedure dbo.Insert_tblLenhOnlines
	@ID				bigint = null out,
	@SoThuTuLenh	bigint = null out,
	@HoVaTen		nvarchar(255),
	@SoDienThoai	nvarchar(50) = null,
	@SoCMND			nvarchar(50) = null,
	@SoXe			nvarchar(50) = null,
	@MaSoThue		nvarchar(50) = null,
	@TenCongTy		nvarchar(255) = null,
	@DiaChi			nvarchar(500) = null,
	@Email			nvarchar(255) = null,
	@HouseBill		nvarchar(100) = null,
	@NgayLamLenh	datetime = null out,
	@SoCont			nvarchar(50) = null,
	@NgayLayHang	datetime = null,
	@SoToKhai		nvarchar(100) = null,
	@TrangThai		int = 0,
	@HoanThanh		bit = 0,
	@IDDanhMucKhachHangDoiLenh	bigint,
	@CreateDate		datetime = null out
as
begin
	set nocount on;

	declare @ErrMsg nvarchar(max);

	set @HoVaTen = dbo.ChuanHoaChuoi(@HoVaTen);
	set @SoDienThoai = dbo.ChuanHoaChuoi(@SoDienThoai);
	set @SoCMND = dbo.ChuanHoaChuoi(@SoCMND);
	set @SoXe = dbo.ChuanHoaChuoi(@SoXe);
	set @MaSoThue = dbo.ChuanHoaChuoi(@MaSoThue);
	set @TenCongTy = dbo.ChuanHoaChuoi(@TenCongTy);
	set @DiaChi = dbo.ChuanHoaChuoi(@DiaChi);
	set @Email = dbo.ChuanHoaChuoi(@Email);
	set @HouseBill = dbo.ChuanHoaChuoi(@HouseBill);
	set @SoCont = dbo.ChuanHoaChuoi(@SoCont);
	set @SoToKhai = dbo.ChuanHoaChuoi(@SoToKhai);
	set @TrangThai = isnull(@TrangThai, 0);

	if @HoVaTen is null or len(ltrim(rtrim(@HoVaTen))) = 0 or len(ltrim(rtrim(@HoVaTen))) > 255
	begin
		raiserror(N'HoVaTen khong duoc bo trong hoac dai hon 255 ky tu!', 16, 1);
		return;
	end;

	if @IDDanhMucKhachHangDoiLenh is null
	begin
		raiserror(N'IDDanhMucKhachHangDoiLenh khong duoc bo trong!', 16, 1);
		return;
	end;

	if @HouseBill is null or len(ltrim(rtrim(@HouseBill))) = 0 or len(ltrim(rtrim(@HouseBill))) > 100
	begin
		raiserror(N'HouseBill khong duoc bo trong hoac dai hon 100 ky tu!', 16, 1);
		return;
	end;

	if not exists (select 1 from DanhMucKhachHangDoiLenh where ID = @IDDanhMucKhachHangDoiLenh)
	begin
		raiserror(N'IDDanhMucKhachHangDoiLenh khong ton tai!', 16, 1);
		return;
	end;

	if @TrangThai not between 0 and 5
	begin
		raiserror(N'TrangThai khong hop le!', 16, 1);
		return;
	end;

	if exists (select 1 from tblLenhOnlines where HouseBill = @HouseBill)
	begin
		raiserror(N'HouseBill đã tồn tại trong hệ thống! Vui lòng tạo lệnh với mã khác', 16, 1);
		return;
	end;

	begin tran
	begin try
		select @ID = isnull(max(ID), 0) + 1 from tblLenhOnlines with (updlock, holdlock);
		select @SoThuTuLenh = isnull(max(SoThuTuLenh), 0) + 1 from tblLenhOnlines with (updlock, holdlock);
		set @NgayLamLenh = getdate();
		set @CreateDate = @NgayLamLenh;

		insert into tblLenhOnlines
		(
			ID,
			SoThuTuLenh,
			HoVaTen,
			SoDienThoai,
			SoCMND,
			SoXe,
			MaSoThue,
			TenCongTy,
			DiaChi,
			Email,
			HouseBill,
			NgayLamLenh,
			SoCont,
			NgayLayHang,
			SoToKhai,
			TrangThai,
			IDDanhMucKhachHangDoiLenh,
			CreateDate
		)
		values
		(
			@ID,
			@SoThuTuLenh,
			@HoVaTen,
			@SoDienThoai,
			@SoCMND,
			@SoXe,
			@MaSoThue,
			@TenCongTy,
			@DiaChi,
			@Email,
			@HouseBill,
			@NgayLamLenh,
			@SoCont,
			@NgayLayHang,
			@SoToKhai,
			@TrangThai,
			@IDDanhMucKhachHangDoiLenh,
			@CreateDate
		);

		commit tran;
	end try
	begin catch
		if @@trancount > 0 rollback tran;
		select @ErrMsg = error_message();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go

if object_id(N'dbo.Update_tblLenhOnlines', N'P') is null
	exec('create procedure dbo.Update_tblLenhOnlines as begin set nocount on; end');
go

alter procedure dbo.Update_tblLenhOnlines
	@ID				bigint,
	@HoVaTen		nvarchar(255) = null,
	@SoDienThoai	nvarchar(50) = null,
	@SoCMND			nvarchar(50) = null,
	@SoXe			nvarchar(50) = null,
	@MaSoThue		nvarchar(50) = null,
	@TenCongTy		nvarchar(255) = null,
	@DiaChi			nvarchar(500) = null,
	@Email			nvarchar(255) = null,
	@HouseBill		nvarchar(100) = null,
	@SoCont			nvarchar(50) = null,
	@NgayLayHang	datetime = null,
	@SoToKhai		nvarchar(100) = null,
	@TrangThai		int = null,
	@HoanThanh		bit = null,
	@IDDanhMucKhachHangDoiLenh	bigint = null,
	@EditDate		datetime = null out
as
begin
	set nocount on;

	declare @ErrMsg nvarchar(max);
	declare @CurrentHoVaTen nvarchar(255);
	declare @CurrentSoDienThoai nvarchar(50);
	declare @CurrentSoCMND nvarchar(50);
	declare @CurrentSoXe nvarchar(50);
	declare @CurrentMaSoThue nvarchar(50);
	declare @CurrentTenCongTy nvarchar(255);
	declare @CurrentDiaChi nvarchar(500);
	declare @CurrentEmail nvarchar(255);
	declare @CurrentHouseBill nvarchar(100);
	declare @CurrentSoCont nvarchar(50);
	declare @CurrentNgayLayHang datetime;
	declare @CurrentSoToKhai nvarchar(100);
	declare @CurrentTrangThai int;
	declare @CurrentIDDanhMucKhachHangDoiLenh bigint;

	select
		@CurrentHoVaTen = HoVaTen,
		@CurrentSoDienThoai = SoDienThoai,
		@CurrentSoCMND = SoCMND,
		@CurrentSoXe = SoXe,
		@CurrentMaSoThue = MaSoThue,
		@CurrentTenCongTy = TenCongTy,
		@CurrentDiaChi = DiaChi,
		@CurrentEmail = Email,
		@CurrentHouseBill = HouseBill,
		@CurrentSoCont = SoCont,
		@CurrentNgayLayHang = NgayLayHang,
		@CurrentSoToKhai = SoToKhai,
		@CurrentTrangThai = TrangThai,
		@CurrentIDDanhMucKhachHangDoiLenh = IDDanhMucKhachHangDoiLenh
	from tblLenhOnlines
	where ID = @ID;

	if @CurrentHoVaTen is null
	begin
		raiserror(N'Ban ghi khong ton tai!', 16, 1);
		return;
	end;

	set @HoVaTen = dbo.ChuanHoaChuoi(@HoVaTen);
	set @SoDienThoai = dbo.ChuanHoaChuoi(@SoDienThoai);
	set @SoCMND = dbo.ChuanHoaChuoi(@SoCMND);
	set @SoXe = dbo.ChuanHoaChuoi(@SoXe);
	set @MaSoThue = dbo.ChuanHoaChuoi(@MaSoThue);
	set @TenCongTy = dbo.ChuanHoaChuoi(@TenCongTy);
	set @DiaChi = dbo.ChuanHoaChuoi(@DiaChi);
	set @Email = dbo.ChuanHoaChuoi(@Email);
	set @HouseBill = dbo.ChuanHoaChuoi(@HouseBill);
	set @SoCont = dbo.ChuanHoaChuoi(@SoCont);
	set @SoToKhai = dbo.ChuanHoaChuoi(@SoToKhai);
	set @HoVaTen = isnull(@HoVaTen, @CurrentHoVaTen);
	set @SoDienThoai = isnull(@SoDienThoai, @CurrentSoDienThoai);
	set @SoCMND = isnull(@SoCMND, @CurrentSoCMND);
	set @SoXe = isnull(@SoXe, @CurrentSoXe);
	set @MaSoThue = isnull(@MaSoThue, @CurrentMaSoThue);
	set @TenCongTy = isnull(@TenCongTy, @CurrentTenCongTy);
	set @DiaChi = isnull(@DiaChi, @CurrentDiaChi);
	set @Email = isnull(@Email, @CurrentEmail);
	set @HouseBill = isnull(@HouseBill, @CurrentHouseBill);
	set @SoCont = isnull(@SoCont, @CurrentSoCont);
	set @NgayLayHang = isnull(@NgayLayHang, @CurrentNgayLayHang);
	set @SoToKhai = isnull(@SoToKhai, @CurrentSoToKhai);
	set @TrangThai = isnull(@TrangThai, @CurrentTrangThai);
	set @IDDanhMucKhachHangDoiLenh = isnull(@IDDanhMucKhachHangDoiLenh, @CurrentIDDanhMucKhachHangDoiLenh);

	if @HoVaTen is null or len(ltrim(rtrim(@HoVaTen))) = 0 or len(ltrim(rtrim(@HoVaTen))) > 255
	begin
		raiserror(N'HoVaTen khong duoc bo trong hoac dai hon 255 ky tu!', 16, 1);
		return;
	end;

	if @IDDanhMucKhachHangDoiLenh is null
	begin
		raiserror(N'IDDanhMucKhachHangDoiLenh khong duoc bo trong!', 16, 1);
		return;
	end;

	if @HouseBill is null or len(ltrim(rtrim(@HouseBill))) = 0 or len(ltrim(rtrim(@HouseBill))) > 100
	begin
		raiserror(N'HouseBill khong duoc bo trong hoac dai hon 100 ky tu!', 16, 1);
		return;
	end;

	if not exists (select 1 from DanhMucKhachHangDoiLenh where ID = @IDDanhMucKhachHangDoiLenh)
	begin
		raiserror(N'IDDanhMucKhachHangDoiLenh khong ton tai!', 16, 1);
		return;
	end;

	if @TrangThai not between 0 and 5
	begin
		raiserror(N'TrangThai khong hop le!', 16, 1);
		return;
	end;

	if exists (select 1 from tblLenhOnlines where HouseBill = @HouseBill and ID <> @ID)
	begin
		raiserror(N'HouseBill đã tồn tại trong hệ thống! Vui lòng tạo lệnh với mã khác!', 16, 1);
		return;
	end;

	begin tran
	begin try
		set @EditDate = getdate();

		update tblLenhOnlines
		set
			HoVaTen = @HoVaTen,
			SoDienThoai = @SoDienThoai,
			SoCMND = @SoCMND,
			SoXe = @SoXe,
			MaSoThue = @MaSoThue,
			TenCongTy = @TenCongTy,
			DiaChi = @DiaChi,
			Email = @Email,
			HouseBill = @HouseBill,
			SoCont = @SoCont,
			NgayLayHang = @NgayLayHang,
			SoToKhai = @SoToKhai,
			TrangThai = @TrangThai,
			IDDanhMucKhachHangDoiLenh = @IDDanhMucKhachHangDoiLenh,
			EditDate = @EditDate
		where ID = @ID;

		commit tran;
	end try
	begin catch
		if @@trancount > 0 rollback tran;
		select @ErrMsg = error_message();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go

if object_id(N'dbo.Delete_tblLenhOnlines', N'P') is null
	exec('create procedure dbo.Delete_tblLenhOnlines as begin set nocount on; end');
go

alter procedure dbo.Delete_tblLenhOnlines
	@ID bigint
as
begin
	set nocount on;

	if not exists (select 1 from tblLenhOnlines where ID = @ID)
	begin
		raiserror(N'Ban ghi khong ton tai!', 16, 1);
		return;
	end;

	delete from tblLenhOnlines where ID = @ID;
end
go


--xuất Excel thống kê chi tiết một Lệnh online có thể gắn với nhiều Biên nhận, hàm này sẽ dùng LEFT JOIN trực tiếp giữa tblLenhOnlines và ctBienNhanThanhToanHangNhapKhauTemp để liệt kê đầy đủ các dòng biên nhận kèm các thông tin hóa đơn
-- Version này chỉ trả ra lệnh gốc không trả biên nhận gia hạn
ALTER PROCEDURE dbo.ExportExcel_V1_tblLenhOnlines
	@TuNgay                 DATETIME = NULL,
	@DenNgay                DATETIME = NULL,
	@TrangThaiThanhToanBNG  INT = -1 -- -1: Tất cả, 1: Đã thanh toán, 0: Chưa thanh toán
AS
BEGIN
	SET NOCOUNT ON;

	-- 1. Chuẩn hóa khoảng thời gian
	DECLARE @FromDate DATETIME = CASE WHEN @TuNgay IS NOT NULL THEN CONVERT(DATETIME, CONVERT(DATE, @TuNgay)) ELSE NULL END;
	DECLARE @ToDate   DATETIME = CASE WHEN @DenNgay IS NOT NULL THEN DATEADD(SECOND, 86399, CONVERT(DATETIME, CONVERT(DATE, @DenNgay))) ELSE NULL END;

	-- 2. Truy vấn lấy dữ liệu xuất Excel
	SELECT
		-- Thông tin Lệnh Online
		lo.ID AS IDLenh,
		lo.SoThuTuLenh,
		lo.NgayLamLenh,
		lo.HoVaTen,
		lo.SoDienThoai,
		lo.SoCMND,
		lo.SoXe,
		lo.MaSoThue,
		lo.TenCongTy,
		lo.DiaChi,
		lo.Email,
		lo.HouseBill,
		lo.SoCont,
		lo.NgayLayHang,
		lo.SoToKhai,
		CASE 
			WHEN lo.TrangThai = 0 THEN N'Mới tạo'
			WHEN lo.TrangThai = 1 THEN N'Đã duyệt'
			WHEN lo.TrangThai = 2 THEN N'Đã hủy'
			ELSE N'Khác'
		END AS TenTrangThaiLenh,
		kh.Ten AS TenNguoiTao,

		-- 🟢 TRẠNG THÁI BIÊN NHẬN GỐC (Tính chính xác theo từng dòng Biên nhận)
		CASE 
			WHEN bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL THEN CAST(ISNULL(bn.DaThanhToan, 0) AS INT)
			ELSE 0 
		END AS BienNhanThanhToanGoc,

		CASE 
			WHEN bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL AND ISNULL(bn.DaThanhToan, 0) = 1 THEN N'Đã thanh toán'
			ELSE N'Chưa thanh toán'
		END AS TenTrangThaiThanhToanBNG,

		-- Thông tin chi tiết Biên nhận & Hóa đơn
		bn.ID AS IDBienNhan,
		bn.SoHoaDon,
		bn.NgayHoaDon,
		bn.DaThanhToan AS TrangThaiThanhToanBienNhan,
		bn.MauHoaDon,
		bn.KyHieuHoaDon,
		bn.Fkey,
		ISNULL(bn.TienHang, 0) AS TienHang,
		ISNULL(bn.TienThue, 0) AS TienThue,
		ISNULL(bn.TongTien, 0) AS TongTien

	FROM tblLenhOnlines lo WITH (NOLOCK)
	
	-- JOIN lấy thông tin Người tạo
	LEFT JOIN DanhMucKhachHangDoiLenh kh WITH (NOLOCK) 
		ON kh.ID = lo.IDDanhMucKhachHangDoiLenh

	-- LEFT JOIN danh sách chi tiết các Biên nhận liên quan
	LEFT JOIN ctBienNhanThanhToanHangNhapKhauTemp bn WITH (NOLOCK) 
		ON bn.IDctLenhNhapKhoHangNhapKhauChiTiet = lo.IDctLenhNhapKhoHangNhapKhauChiTiet

	WHERE 
		-- Lọc theo Khoảng thời gian Ngày Làm Lệnh
		(@FromDate IS NULL OR lo.NgayLamLenh >= @FromDate)
		AND (@ToDate IS NULL OR lo.NgayLamLenh <= @ToDate)
		
		-- Lọc theo Trạng thái thanh toán biên nhận gốc
		AND (
			@TrangThaiThanhToanBNG = -1 
			OR (
				-- Kiểm tra điều kiện Biên nhận gốc trực tiếp tại WHERE
				CASE 
					WHEN bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL THEN CAST(ISNULL(bn.DaThanhToan, 0) AS INT)
					ELSE 0 
				END = @TrangThaiThanhToanBNG
			)
		)

	ORDER BY 
		lo.NgayLamLenh DESC, 
		lo.ID DESC, 
		bn.ID ASC;
END
GO

--V2 trả cả gốc và gia hạn đã thanh toán
ALTER PROCEDURE dbo.ExportExcel_tblLenhOnlines
	@TuNgay                 DATETIME = NULL,
	@DenNgay                DATETIME = NULL,
	@TrangThaiThanhToanBNG  INT = -1 -- -1: Tất cả, 1: Đã thanh toán, 0: Chưa thanh toán
AS
BEGIN
	SET NOCOUNT ON;

	-- 1. Chuẩn hóa khoảng thời gian
	DECLARE @FromDate DATETIME = CASE WHEN @TuNgay IS NOT NULL THEN CONVERT(DATETIME, CONVERT(DATE, @TuNgay)) ELSE NULL END;
	DECLARE @ToDate   DATETIME = CASE WHEN @DenNgay IS NOT NULL THEN DATEADD(SECOND, 86399, CONVERT(DATETIME, CONVERT(DATE, @DenNgay))) ELSE NULL END;

	-- 2. Truy vấn lấy dữ liệu
	SELECT
		-- Thông tin Lệnh Online
		lo.ID AS IDLenh,
		lo.SoThuTuLenh,
		lo.NgayLamLenh,
		lo.HoVaTen,
		lo.SoDienThoai,
		lo.SoCMND,
		lo.SoXe,
		lo.MaSoThue,
		lo.TenCongTy,
		lo.DiaChi,
		lo.Email,
		lo.HouseBill,
		lo.SoCont,
		lo.NgayLayHang,
		lo.SoToKhai,
		CASE 
			WHEN lo.TrangThai = 0 THEN N'Mới tạo'
			WHEN lo.TrangThai = 1 THEN N'Đã duyệt'
			WHEN lo.TrangThai = 2 THEN N'Đã hủy'
			ELSE N'Khác'
		END AS TenTrangThaiLenh,
		kh.Ten AS TenNguoiTao,

		-- 🟢 Cột phân loại 1/0
		CASE 
			WHEN bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL AND ISNULL(bn.DaThanhToan, 0) = 1 THEN 1
			ELSE 0 
		END AS BienNhanThanhToanGoc,

		-- 🟢 SỬA TÊN TRẠNG THÁI HIỂN THỊ CHUẨN XÁC VÀ RÕ RÀNG
		CASE 
			WHEN bn.ID IS NULL THEN N'Chưa có biên nhận'
			WHEN bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL THEN N'BN Gốc - Đã thanh toán'
			ELSE N'BN Gia hạn - Đã thanh toán'
		END AS TenTrangThaiThanhToanBNG,

		-- Thông tin chi tiết Biên nhận & Hóa đơn
		bn.ID AS IDBienNhan,
		bn.SoHoaDon,
		bn.NgayHoaDon,
		bn.DaThanhToan AS TrangThaiThanhToanBienNhan,
		bn.MauHoaDon,
		bn.KyHieuHoaDon,
		bn.Fkey,
		ISNULL(bn.TienHang, 0) AS TienHang,
		ISNULL(bn.TienThue, 0) AS TienThue,
		ISNULL(bn.TongTien, 0) AS TongTien

	FROM tblLenhOnlines lo WITH (NOLOCK)
	
	LEFT JOIN DanhMucKhachHangDoiLenh kh WITH (NOLOCK) 
		ON kh.ID = lo.IDDanhMucKhachHangDoiLenh

	-- CHỈ JOIN CÁC BIÊN NHẬN ĐÃ THANH TOÁN (DaThanhToan = 1)
	LEFT JOIN ctBienNhanThanhToanHangNhapKhauTemp bn WITH (NOLOCK) 
		ON bn.IDctLenhNhapKhoHangNhapKhauChiTiet = lo.IDctLenhNhapKhoHangNhapKhauChiTiet
	   AND ISNULL(bn.DaThanhToan, 0) = 1

	WHERE 
		-- Lọc theo Khoảng thời gian Ngày Làm Lệnh
		(@FromDate IS NULL OR lo.NgayLamLenh >= @FromDate)
		AND (@ToDate IS NULL OR lo.NgayLamLenh <= @ToDate)
		
		-- Lọc theo Trạng thái BNG
		AND (
			@TrangThaiThanhToanBNG = -1 
			OR EXISTS (
				SELECT 1 
				FROM ctBienNhanThanhToanHangNhapKhauTemp sub_bn WITH (NOLOCK)
				WHERE sub_bn.IDctLenhNhapKhoHangNhapKhauChiTiet = lo.IDctLenhNhapKhoHangNhapKhauChiTiet
				  AND sub_bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL 
				  AND ISNULL(sub_bn.DaThanhToan, 0) = @TrangThaiThanhToanBNG
			)
			OR (
				@TrangThaiThanhToanBNG = 0 
				AND NOT EXISTS (
					SELECT 1 
					FROM ctBienNhanThanhToanHangNhapKhauTemp sub_bn WITH (NOLOCK)
					WHERE sub_bn.IDctLenhNhapKhoHangNhapKhauChiTiet = lo.IDctLenhNhapKhoHangNhapKhauChiTiet
					  AND sub_bn.IDctLenhXuatKhoHangNhapKhau IS NOT NULL
					  AND ISNULL(sub_bn.DaThanhToan, 0) = 1
				)
			)
		)

	ORDER BY 
		lo.NgayLamLenh DESC, 
		lo.ID DESC, 
		bn.ID ASC;
END
GO
