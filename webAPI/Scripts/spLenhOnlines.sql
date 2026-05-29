if object_id(N'dbo.List_tblLenhOnlines', N'P') is null
	exec('create procedure dbo.List_tblLenhOnlines as begin set nocount on; end');
go

alter procedure dbo.List_tblLenhOnlines
	@ID		bigint = null,
	@IDDanhMucKhachHangDoiLenh	bigint = null,
	@TuNgay	datetime = null,
	@DenNgay	datetime = null,
	@HouseBill	nvarchar(100) = null,
	@SoCont		nvarchar(50) = null,
	@MaSoThue	nvarchar(50) = null,
	@Page		int = 1,
	@PageSize	int = 10
as
begin
	set nocount on;

	set @HouseBill = dbo.ChuanHoaChuoi(@HouseBill);
	set @SoCont = dbo.ChuanHoaChuoi(@SoCont);
	set @MaSoThue = dbo.ChuanHoaChuoi(@MaSoThue);
	set @Page = isnull(nullif(@Page, 0), 1);
	set @PageSize = isnull(nullif(@PageSize, 0), 10);

	if @Page < 1 set @Page = 1;
	if @PageSize < 1 set @PageSize = 10;

	with Filtered as
	(
		select
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
			lo.IDctLenhNhapKhoHangNhapKhauChiTiet,
			ct.ID as ChiTietId,
			ct.TrangThaiThanhToan,
			case
				when isnull(ct.IsHoanThanh, 0) = 1 then 1
				else 0
			end as IsHoanThanh,
			ct.LinkTaiHoaDon,
			ct.DuongDanFileHoaDon,
			lo.CreateDate,
			lo.EditDate
		from tblLenhOnlines lo
		left join tblLenhOnlineChiTiet ct on ct.IDLenhOnline = lo.ID
		where
			(@ID is null or lo.ID = @ID)
			and (@IDDanhMucKhachHangDoiLenh is null or lo.IDDanhMucKhachHangDoiLenh = @IDDanhMucKhachHangDoiLenh)
			and (@TuNgay is null or convert(date, lo.NgayLamLenh) >= convert(date, @TuNgay))
			and (@DenNgay is null or convert(date, lo.NgayLamLenh) <= convert(date, @DenNgay))
			and (@HouseBill is null or lo.HouseBill like N'%' + @HouseBill + N'%')
			and (@SoCont is null or lo.SoCont like N'%' + @SoCont + N'%')
			and (@MaSoThue is null or lo.MaSoThue like N'%' + @MaSoThue + N'%')
	)
	select
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
		IDctLenhNhapKhoHangNhapKhauChiTiet,
		ChiTietId,
		TrangThaiThanhToan,
		IsHoanThanh,
		LinkTaiHoaDon,
		DuongDanFileHoaDon,
		CreateDate,
		EditDate,
		count(1) over() as TotalCount
	from Filtered
	order by case when TrangThai = 0 then 0 else 1 end, NgayLamLenh desc, ID desc
	offset (@Page - 1) * @PageSize rows
	fetch next @PageSize rows only;
end
go

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
