if object_id(N'dbo.Upsert_tblLenhOnlineChiTiet', N'P') is null
	exec('create procedure dbo.Upsert_tblLenhOnlineChiTiet as begin set nocount on; end');
go

if object_id(N'dbo.tblLenhOnlineChiTiet', N'U') is not null
and col_length(N'dbo.tblLenhOnlineChiTiet', N'IsHoanThanh') is not null
and not exists
(
	select 1
	from sys.default_constraints dc
	inner join sys.columns c on c.default_object_id = dc.object_id
	inner join sys.tables t on t.object_id = c.object_id
	where t.object_id = object_id(N'dbo.tblLenhOnlineChiTiet')
	  and c.name = N'IsHoanThanh'
)
begin
	alter table dbo.tblLenhOnlineChiTiet
	add constraint DF_tblLenhOnlineChiTiet_IsHoanThanh default (0) for IsHoanThanh;
end
go

alter procedure dbo.Upsert_tblLenhOnlineChiTiet
	@ID						bigint = null out,
	@IDLenhOnline			bigint,
	@PhiLuuKho				decimal(18,2) = 0,
	@PhiGiaoNhan			decimal(18,2) = 0,
	@PhiBocXep				decimal(18,2) = 0,
	@VAT					decimal(18,2) = 0,
	@TrangThaiThanhToan		tinyint = 0,
	@TrangThaiThongQuan		tinyint = 0,
	@ThuKho					nvarchar(255) = null,
	@Forwarder				nvarchar(255) = null,
	@TenTau					nvarchar(255) = null,
	@ChuHang				nvarchar(255) = null,
	@SoKien					int = null,
	@SoChuyen				nvarchar(100) = null,
	@SoHouseBill			nvarchar(100) = null,
	@NgayTauCap				datetime = null,
	@TrongLuong				decimal(18,3) = null,
	@SoCont					nvarchar(50) = null,
	@SoKhoi					decimal(18,3) = null,
	@LinkTaiHoaDon			nvarchar(500) = null,
	@DuongDanFileHoaDon		nvarchar(500) = null,
	@IsHoanThanh			bit = 0,
	@CreateDate				datetime = null out,
	@EditDate				datetime = null out
as
begin
	set nocount on;

	declare @Now datetime = getdate();
	declare @ExistingId bigint;

	set @ThuKho = dbo.ChuanHoaChuoi(@ThuKho);
	set @Forwarder = dbo.ChuanHoaChuoi(@Forwarder);
	set @TenTau = dbo.ChuanHoaChuoi(@TenTau);
	set @ChuHang = dbo.ChuanHoaChuoi(@ChuHang);
	set @SoChuyen = dbo.ChuanHoaChuoi(@SoChuyen);
	set @SoHouseBill = dbo.ChuanHoaChuoi(@SoHouseBill);
	set @SoCont = dbo.ChuanHoaChuoi(@SoCont);
	set @LinkTaiHoaDon = dbo.ChuanHoaChuoi(@LinkTaiHoaDon);
	set @DuongDanFileHoaDon = dbo.ChuanHoaChuoi(@DuongDanFileHoaDon);

	if @IDLenhOnline is null or @IDLenhOnline <= 0
	begin
		raiserror(N'IDLenhOnline khong hop le!', 16, 1);
		return;
	end;

	if not exists (select 1 from tblLenhOnlines where ID = @IDLenhOnline)
	begin
		raiserror(N'Lenh online khong ton tai!', 16, 1);
		return;
	end;

	begin tran
	begin try
		select @ExistingId = ID
		from tblLenhOnlineChiTiet with (updlock, holdlock)
		where IDLenhOnline = @IDLenhOnline;

		if @ExistingId is null
		begin
			select @ExistingId = isnull(max(ID), 0) + 1
			from tblLenhOnlineChiTiet with (updlock, holdlock);

			insert into tblLenhOnlineChiTiet
			(
				ID,
				IDLenhOnline,
				PhiLuuKho,
				PhiGiaoNhan,
				PhiBocXep,
				VAT,
				TrangThaiThanhToan,
				TrangThaiThongQuan,
				ThuKho,
				Forwarder,
				TenTau,
				ChuHang,
				SoKien,
				SoChuyen,
				SoHouseBill,
				NgayTauCap,
				TrongLuong,
				SoCont,
				SoKhoi,
				LinkTaiHoaDon,
				DuongDanFileHoaDon,
				IsHoanThanh,
				CreateDate,
				EditDate
			)
			values
			(
				@ExistingId,
				@IDLenhOnline,
				@PhiLuuKho,
				@PhiGiaoNhan,
				@PhiBocXep,
				@VAT,
				@TrangThaiThanhToan,
				@TrangThaiThongQuan,
				@ThuKho,
				@Forwarder,
				@TenTau,
				@ChuHang,
				@SoKien,
				@SoChuyen,
				@SoHouseBill,
				@NgayTauCap,
				@TrongLuong,
				@SoCont,
				@SoKhoi,
				@LinkTaiHoaDon,
				@DuongDanFileHoaDon,
				@IsHoanThanh,
				@Now,
				null
			);

			set @CreateDate = @Now;
			set @EditDate = null;
			set @ID = @ExistingId;
		end
		else
		begin
			update tblLenhOnlineChiTiet
			set
				PhiLuuKho = @PhiLuuKho,
				PhiGiaoNhan = @PhiGiaoNhan,
				PhiBocXep = @PhiBocXep,
				VAT = @VAT,
				TrangThaiThanhToan = @TrangThaiThanhToan,
				TrangThaiThongQuan = @TrangThaiThongQuan,
				ThuKho = @ThuKho,
				Forwarder = @Forwarder,
				TenTau = @TenTau,
				ChuHang = @ChuHang,
				SoKien = @SoKien,
				SoChuyen = @SoChuyen,
				SoHouseBill = @SoHouseBill,
				NgayTauCap = @NgayTauCap,
				TrongLuong = @TrongLuong,
				SoCont = @SoCont,
				SoKhoi = @SoKhoi,
				LinkTaiHoaDon = @LinkTaiHoaDon,
				DuongDanFileHoaDon = @DuongDanFileHoaDon,
				IsHoanThanh = @IsHoanThanh,
				EditDate = @Now
			where IDLenhOnline = @IDLenhOnline;

			set @CreateDate = null;
			set @EditDate = @Now;
			set @ID = @ExistingId;
		end

		commit tran;
	end try
	begin catch
		if @@trancount > 0 rollback tran;
		throw;
	end catch
end
go
