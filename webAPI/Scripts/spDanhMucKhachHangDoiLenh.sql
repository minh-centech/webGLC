	---------------DANH MỤC ĐỐI TƯỢNG THANH TOÁN
alter procedure List_DanhMucKhachHangDoiLenh
	@ID						bigint = null,
	@IDDanhMucDonVi			bigint,
	@IDDanhMucLoaiDoiTuong	bigint
as
begin
	set nocount on;
	select	a.ID, 
			a.IDDanhMucDonVi, 
			a.IDDanhMucLoaiDoiTuong, 
			a.LoaiTaiKhoan,
			a.IsActive,
			a.Email, 
			a.Ten, 
			a.SoDienThoai,
			a.[Password],
			a.[Password] PasswordConfirm,
			a.PartnerGUID,
			a.MaKichHoat,
			a.ThoiGianTaoMaKichHoat,
			a.KichHoat,
			a.IDDanhMucNguoiSuDungCreate, UserCreate.Ma MaDanhMucNguoiSuDungCreate, 
			a.CreateDate, 
			a.IDDanhMucNguoiSuDungEdit, UserEdit.Ma MaDanhMucNguoiSuDungEdit, 
			a.EditDate 
		from DanhMucKhachHangDoiLenh a 
			left join DanhMucNguoiSuDung UserCreate on a.IDDanhMucNguoiSuDungCreate = UserCreate.ID
			left join DanhMucNguoiSuDung UserEdit on a.IDDanhMucNguoiSuDungEdit = UserEdit.ID
	where 
		a.IDDanhMucDonVi = @IDDanhMucDonVi 
		and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong 
		and case when @ID is not null then a.ID else 0 end = ISNULL(@ID, 0) 
	order by a.Email;
end
go
------------
alter procedure List_DanhMucKhachHangDoiLenh_Login
	@ID						bigint = null output,
	@IDDanhMucDonVi			bigint,
	@IDDanhMucLoaiDoiTuong	bigint,
	@Email					nvarchar(128) = null,
	@Password				nvarchar(256) = null
as
begin
	set nocount on;

	declare @ErrMsg nvarchar(max), @countID int;

	--raiserror(N'Hệ thống tạm khóa để bảo trì!', 16, 1);
	--return;

	if @Email is null or len(ltrim(rtrim(@Email))) = 0 or len(ltrim(rtrim(@Email))) > 128
	begin
		raiserror(N'Email không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));
	if @countID = 0 
	begin
		set @ErrMsg = N'Email không tồn tại!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	--select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email)) and KichHoat = 1;
	--if @countID = 0 
	--begin
	--	set @ErrMsg = N'Email chưa được kích hoạt!';
	--	raiserror(@ErrMsg, 16, 1);
	--	return;
	--end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email)) and IsActive = 1;
	if @countID = 0
	begin
		set @ErrMsg = N'Tài khoản đã bị khóa hoặc ngừng hoạt động!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;


	declare @OriginalPassword nvarchar(256);
	select @OriginalPassword = [Password] from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));

	if convert(varbinary, @Password) = convert(varbinary, @OriginalPassword)
	begin
		--select	@ID = a.ID from DanhMucKhachHangDoiLenh a where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong and Email = ltrim(rtrim(@Email));
		select	* from DanhMucKhachHangDoiLenh a where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong and Email = ltrim(rtrim(@Email)) and a.IsActive = 1;
	end
	else
	begin
		set @ErrMsg = N'Email hoặc password không đúng!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;
end;
go

------------
alter procedure Insert_DanhMucKhachHangDoiLenh
	@ID							bigint out,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@LoaiTaiKhoan				tinyint = 1,
	@IsActive					bit = 0,
	@Email						nvarchar(128) = null,
	@Ten						nvarchar(255) = null,
	@SoDienThoai				nvarchar(128) = null,
	@Password					nvarchar(256) = null,
	@PasswordConfirm			nvarchar(256) = null,
	@PartnerGUID				nvarchar(36) = null,
	@MaKichHoat					nvarchar(6) = null,
	@ThoiGianTaoMaKichHoat		datetime = null,
	@IDDanhMucNguoiSuDungCreate	bigint,
	@CreateDate					datetime = null out
as
begin
	set nocount on;
	
	declare @ErrMsg nvarchar(max), @countID int;

	set @Email = dbo.ChuanHoaChuoi(@Email);
	set @Ten = dbo.ChuanHoaChuoi(@Ten);
	set @SoDienThoai = dbo.ChuanHoaChuoi(@SoDienThoai);

	if @LoaiTaiKhoan not in (0, 1, 2)
	begin
		raiserror(N'LoaiTaiKhoan chỉ nhận các giá trị 0-admin, 1-cá nhân, 2-doanh nghiệp!', 16, 1);
		return;
	end;

	if @Email is null or len(ltrim(rtrim(@Email))) = 0 or len(ltrim(rtrim(@Email))) > 128
	begin
		raiserror(N'Email không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));
	if @countID > 0 
	begin
		set @ErrMsg = N'Email ' + ltrim(rtrim(@Email)) + N' đã được đăng ký!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	if @Ten is null or len(ltrim(rtrim(@Ten))) = 0 or len(ltrim(rtrim(@Ten))) > 255
	begin
		raiserror(N'Tên không được bỏ trống hoặc dài hơn 255 ký tự!', 16, 1);
		return;
	end;

	if @SoDienThoai is null or len(ltrim(rtrim(@SoDienThoai))) = 0 or len(ltrim(rtrim(@SoDienThoai))) > 128
	begin
		raiserror(N'Số điện thoại không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where SoDienThoai = ltrim(rtrim(@SoDienThoai));
	if @countID > 0 
	begin
		set @ErrMsg = N'Số điện thoại ' + ltrim(rtrim(@SoDienThoai)) + N' đã được đăng ký!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	if @Password is null or len(@Password) = 0 or len(@Password) > 64
	begin
		raiserror(N'Password không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end;

	if @PasswordConfirm is null or len(@PasswordConfirm) = 0 or len(@PasswordConfirm) > 64
	begin
		raiserror(N'Password xác nhận không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end;

	if cast(@Password as varbinary(max)) <> cast(@PasswordConfirm as varbinary(max))
	begin
		raiserror(N'Password và xác nhận password không khớp nhau!', 16, 1);
		return;
	end;

	if @PartnerGUID is null or len(@PartnerGUID) = 0 or len(@PartnerGUID) > 36
	begin
		raiserror(N'PartnerGUID không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end;

	begin tran
	begin try
		exec Insert_DanhMucDoiTuong @ID = @ID out, @IDDanhMucDonVi = @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong, @Ma = null, @Ten = null, @IDDanhMucNguoiSuDungCreate = @IDDanhMucNguoiSuDungCreate, @CreateDate = @CreateDate out;
		insert DanhMucKhachHangDoiLenh 
		(
			ID, 
			IDDanhMucDonVi, 
			IDDanhMucLoaiDoiTuong, 
			LoaiTaiKhoan,
			IsActive,
			Email, 
			Ten, 
			SoDienThoai, 
			[Password], 
			PartnerGUID,
			KichHoat,
			MaKichHoat,
			ThoiGianTaoMaKichHoat,
			IDDanhMucNguoiSuDungCreate, 
			CreateDate
		) 
		values 
		(
			@ID, 
			@IDDanhMucDonVi, 
			@IDDanhMucLoaiDoiTuong, 
			@LoaiTaiKhoan,
			@IsActive,
			ltrim(rtrim(@Email)), 
			ltrim(rtrim(@Ten)), 
			ltrim(rtrim(@SoDienThoai)), 
			@Password,
			@PartnerGUID,
			0,
			@MaKichHoat,
			@ThoiGianTaoMaKichHoat,
			@IDDanhMucNguoiSuDungCreate, 
			@CreateDate
		);
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go
------------
alter procedure Update_DanhMucKhachHangDoiLenh
	@ID							bigint,
	@IDDanhMucDonVi				bigint,
	@IDDanhMucLoaiDoiTuong		bigint,
	@LoaiTaiKhoan				tinyint,
	@IsActive					bit,
	@Email						nvarchar(128) = null,
	@Ten						nvarchar(255) = null,
	@SoDienThoai				nvarchar(128) = null,
	@KichHoat					bit,
	@IDDanhMucNguoiSuDungEdit	bigint,
	@EditDate					datetime = null out
as
begin
	declare @ErrMsg nvarchar(max), @countID int;

	set @Email = dbo.ChuanHoaChuoi(@Email);
	set @Ten = dbo.ChuanHoaChuoi(@Ten);
	set @SoDienThoai = dbo.ChuanHoaChuoi(@SoDienThoai);

	if @LoaiTaiKhoan not in (0, 1, 2)
	begin
		raiserror(N'LoaiTaiKhoan chỉ nhận các giá trị 0-admin, 1-cá nhân, 2-doanh nghiệp!', 16, 1);
		return;
	end;

	if @Email is null or len(ltrim(rtrim(@Email))) = 0 or len(ltrim(rtrim(@Email))) > 128
	begin
		raiserror(N'Email không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email)) and ID <> @ID;
	if @countID > 0 
	begin
		set @ErrMsg = N'Email ' + ltrim(rtrim(@Email)) + N' đã được đăng ký!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	if @Ten is null or len(ltrim(rtrim(@Ten))) = 0 or len(ltrim(rtrim(@Ten))) > 255
	begin
		raiserror(N'Tên không được bỏ trống hoặc dài hơn 255 ký tự!', 16, 1);
		return;
	end;

	if @SoDienThoai is null or len(ltrim(rtrim(@SoDienThoai))) = 0 or len(ltrim(rtrim(@SoDienThoai))) > 128
	begin
		raiserror(N'Số điện thoại không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where SoDienThoai = ltrim(rtrim(@SoDienThoai)) and ID <> @ID;
	if @countID > 0 
	begin
		set @ErrMsg = N'Số điện thoại ' + ltrim(rtrim(@SoDienThoai)) + N' đã được đăng ký!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	begin tran
	begin try
		exec Update_DanhMucDoiTuong @ID = @ID, @IDDanhMucDonVi = @IDDanhMucDonVi, @IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong, @Ma = null, @Ten = null, @IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit, @EditDate = @EditDate out;

		update DanhMucKhachHangDoiLenh set
			LoaiTaiKhoan = @LoaiTaiKhoan,
			IsActive = @IsActive,
			Email = @Email,
			Ten = @Ten,
			SoDienThoai = @SoDienThoai,
			KichHoat = @KichHoat,
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where ID = @ID;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go
------------
alter procedure Update_DanhMucKhachHangDoiLenh_KichHoat
	@IDctAccessID				bigint,
	@PartnerGUID				nvarchar(36),
	@IDDanhMucNguoiSuDungEdit	bigint
as
begin
	set nocount on;

	declare @Err int, @ErrMsg nvarchar(max), @countID int;
	declare @NgayCapNhat datetime, @ValidFrom datetime, @ValidTo datetime;

	set @NgayCapNhat = getdate();

	if @IDctAccessID is null
	begin
		raiserror(N'Mã truy cập không hợp lệ!', 16, 1);
		return;
	end;

	select @countID = count(ID) from ctAccessID where ID = @IDctAccessID;
	if @countID = 0
	begin
		raiserror(N'Mã truy cập không tồn tại!', 16, 1);
		return;
	end;

	select @ValidFrom = ValidFrom, @ValidTo = ValidTo from ctAccessID where ID = @IDctAccessID;
	if @NgayCapNhat < @ValidFrom or @NgayCapNhat > @ValidTo
	begin
		raiserror(N'Mã truy cập đã hết hiệu lực!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where PartnerGUID = @PartnerGUID;
	if @countID = 0 
	begin
		set @ErrMsg = N'PartnerGUID không tồn tại!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	begin tran
	begin try
		update DanhMucKhachHangDoiLenh set
			KichHoat = 1,
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @NgayCapNhat
		where PartnerGUID = @PartnerGUID;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go
------------
alter procedure Update_DanhMucKhachHangDoiLenh_Password
	@Email						nvarchar(128),
	@OldPassword				nvarchar(256),
	@NewPassword				nvarchar(256),
	@NewPasswordConfirm			nvarchar(256),
	@IDDanhMucNguoiSuDungEdit	bigint,
	@EditDate					datetime = null out
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max), @countID int;

	if @Email is null or len(ltrim(rtrim(@Email))) = 0 or len(ltrim(rtrim(@Email))) > 128
	begin
		raiserror(N'Email không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));
	if @countID = 0 
	begin
		raiserror(N'Email không tồn tại!', 16, 1);
		return;
	end;

	--select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email)) and KichHoat = 1;
	--if @countID = 0 
	--begin
	--	raiserror(N'Email chưa được kích hoạt!', 16, 1);
	--	return;
	--end;

	if @OldPassword is null or len(@OldPassword) > 64
	begin
		raiserror(N'Password cũ không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end;

	if @NewPassword is null or len(@NewPassword) > 64
	begin
		raiserror(N'Password mới không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end;

	if cast(@NewPassword as varbinary(max)) <> cast(@NewPasswordConfirm as varbinary(max))
	begin
		raiserror(N'Password mới không khớp nhau!', 16, 1);
		return;
	end;
	
	declare @OriginalPassword nvarchar(512);
	select @OriginalPassword = [Password] from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));

	if convert(varbinary, @OldPassword) <> convert(varbinary, @OriginalPassword)
	begin
		set @ErrMsg = N'Email hoặc password không đúng!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	begin tran
	begin try
		update DanhMucKhachHangDoiLenh set
			Password = @NewPassword,
			IDDanhMucNguoiSuDungEdit = @IDDanhMucNguoiSuDungEdit,
			EditDate = @EditDate
		where Email = @Email;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE();
		raiserror(@ErrMsg, 16, 1);
	end catch;
end
go
------------
alter procedure Delete_DanhMucKhachHangDoiLenh
	@ID			bigint
as
begin
	set nocount on;
	declare @ErrMsg nvarchar(max);
	begin tran
	begin try
		delete DanhMucKhachHangDoiLenhRecoverPasswordLog where IDDanhMucKhachHangDoiLenh = @ID;
		delete DanhMucKhachHangDoiLenh	where ID = @ID;
		exec Delete_DanhMucDoiTuong @ID;
	commit tran
	end try
	begin catch
		if @@TRANCOUNT > 0 rollback tran;
		select @ErrMsg = ERROR_MESSAGE()
		raiserror(@ErrMsg, 16, 1)
	end catch
end
go
------------
alter procedure Get_DanhMucKhachHangDoiLenh_PartnerGUIDByEmail
	@IDDanhMucDonVi			bigint,
	@IDDanhMucLoaiDoiTuong	bigint,
	@Email					nvarchar(128),
	@PartnerGUID			nvarchar(36) = null out
as
begin
	set nocount on;

	declare @ErrMsg nvarchar(max), @countID int;

	if @Email is null or len(ltrim(rtrim(@Email))) = 0 or len(ltrim(rtrim(@Email))) > 128
	begin
		raiserror(N'Email không được bỏ trống hoặc dài hơn 128 ký tự!', 16, 1);
		return;
	end;

	select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email));
	if @countID = 0 
	begin
		set @ErrMsg = N'Email không tồn tại!';
		raiserror(@ErrMsg, 16, 1);
		return;
	end;

	--select @countID = count(ID) from DanhMucKhachHangDoiLenh where Email = ltrim(rtrim(@Email)) and KichHoat = 1;
	--if @countID = 0 
	--begin
	--	set @ErrMsg = N'Email chưa được kích hoạt!';
	--	raiserror(@ErrMsg, 16, 1);
	--	return;
	--end;

	select	@PartnerGUID = a.PartnerGUID from DanhMucKhachHangDoiLenh a where a.IDDanhMucDonVi = @IDDanhMucDonVi and a.IDDanhMucLoaiDoiTuong = @IDDanhMucLoaiDoiTuong and Email = ltrim(rtrim(@Email));
end;
go

-------------------- Kiểm tra xác nhận tài khoản
alter procedure Update_DanhMucKhachHangDoiLenh_KichHoatAccount (@ID bigint, @MaKichHoat nvarchar(6)) 

AS
BEGIN
	SET NOCOUNT ON;

    -- Kiểm tra ID có tồn tại
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE ID = @ID)
    BEGIN
        RAISERROR ('Tài khoản không tồn tại', 16, 1);
        RETURN;
    END

	  -- Kiểm tra mã kích hoạt có đúng
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE ID = @ID AND MaKichHoat = @MaKichHoat)
    BEGIN
       RAISERROR ('Mã xác nhận không tồn tại', 16, 1);
        RETURN;
    END


    -- Kiểm tra xem ID có tồn tại trong bảng không
   -- Kiểm tra thời gian tạo mã kích hoạt
    DECLARE @ThoiGianTaoMaKichHoat DATETIME;
    SELECT @ThoiGianTaoMaKichHoat = ThoiGianTaoMaKichHoat FROM DanhMucKhachHangDoiLenh WHERE ID = @ID;

    IF DATEADD(minute, 15, @ThoiGianTaoMaKichHoat) > GETDATE()
    BEGIN
        -- Nếu tồn tại, cập nhật KichHoat = 0
        UPDATE dbo.DanhMucKhachHangDoiLenh SET KichHoat = 1 WHERE ID = @ID;
    END
    ELSE
    BEGIN
        -- Nếu không tồn tại, trả về lỗi
        RAISERROR ('Mã xác nhận đã hết hạn hoặc không tồn tại', 16, 1);
    END
END;
go
----------------Kích hoạt tài khoản
ALTER PROCEDURE Get_DanhMucKhachHangDoiLenh_MaKichHoatByEmail (
    @Email NVARCHAR(128),
    @MaKichHoatMoi NVARCHAR(6), -- Tham số mới: Mã kích hoạt cần gán
    @ID BIGINT = NULL OUTPUT,
    @MaKichHoat NVARCHAR(6) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra email có tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE Email = @Email)
    BEGIN
        SET @MaKichHoat = NULL;
        RAISERROR ('Email chưa được đăng ký!', 16, 1);
        RETURN;
    END

    -- Kiểm tra trạng thái kích hoạt
    IF EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE Email = @Email AND KichHoat = 1)
    BEGIN
        SET @MaKichHoat = NULL;
        RAISERROR ('Email đã được kích hoạt!', 16, 1);
        RETURN;
    END
	 DECLARE @ThoiGianTaoMaKichHoat DATETIME;
    -- Nếu email tồn tại và chưa được kích hoạt, cập nhật MaKichHoat và lấy ID
    UPDATE DanhMucKhachHangDoiLenh
    SET MaKichHoat = @MaKichHoatMoi,
	ThoiGianTaoMaKichHoat = GETDATE()
   
    WHERE Email = @Email;

	 
   

    -- Lấy ID và MaKichHoat sau khi cập nhật
    SELECT
        @ID = ID,
        @MaKichHoat = MaKichHoat
    FROM DanhMucKhachHangDoiLenh
    WHERE Email = @Email;
END;
go
--------Quên mật khẩu
alter PROCEDURE Get_DanhMucKhachHangDoiLenh_MaXacNhanMatKhau (
    @Email NVARCHAR(128),
    @MaXacNhanMoi NVARCHAR(6), -- Tham số mới: Mã kích hoạt cần gán
    @ID BIGINT = NULL OUTPUT,
    @MaXacNhan NVARCHAR(6) = NULL OUTPUT



)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra email có tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE Email = @Email)
    BEGIN
        SET @MaXacNhan = NULL;
        RAISERROR ('Email chưa được đăng ký!', 16, 1);
        RETURN;
    END

    -- Kiểm tra trạng thái kích hoạt
   
	 DECLARE @ThoiGianTaoMaKichHoat DATETIME;
    -- Nếu email tồn tại và chưa được kích hoạt, cập nhật MaKichHoat và lấy ID
    UPDATE DanhMucKhachHangDoiLenh
    SET MaXacNhan = @MaXacNhanMoi,
	ThoiGianTaoMaKichHoat = GETDATE()
   
    WHERE Email = @Email;

	 
   

    -- Lấy ID và MaKichHoat sau khi cập nhật
    SELECT
        @ID = ID,
        @MaXacNhan = MaXacNhan
    FROM DanhMucKhachHangDoiLenh
    WHERE Email = @Email;
END;

GO
--- xác nhận đổi mật khẩu
alter PROCEDURE  Update_DanhMucKhachHangDoiLenh_XacNhanDoiMatKhau
    @ID BIGINT,
    @MaXacNhan NVARCHAR(6) = null,
    @MatKhau NVARCHAR(256) = null,
    @XacNhanMatKhau NVARCHAR(256) =null
AS
BEGIN
    -- Kiểm tra xem ID có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE ID = @ID)
    BEGIN
        RAISERROR('ID không tồn tại.', 16, 1)
        RETURN
    END

    -- Kiểm tra mã xác nhận
    IF NOT EXISTS (SELECT 1 FROM DanhMucKhachHangDoiLenh WHERE ID = @ID AND MaXacNhan = @MaXacNhan)
    BEGIN
        RAISERROR('Mã xác nhận không đúng.', 16, 1)
        RETURN
    END

    -- Kiểm tra mật khẩu và xác nhận mật khẩu có khớp nhau không
    IF @MatKhau <> @XacNhanMatKhau
    BEGIN
        RAISERROR('Mật khẩu và xác nhận mật khẩu không khớp.', 16, 1)
        RETURN
    END

    -- Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
   if @MatKhau is null or len(@MatKhau) > 64
	begin
		raiserror(N'Mật khẩu không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end; 
	 if @XacNhanMatKhau is null or len(@XacNhanMatKhau) > 64
	begin
		raiserror(N'Mật khẩu không được bỏ trống hoặc dài hơn 64 ký tự!', 16, 1);
		return;
	end; 
	if @MaXacNhan is null or len(@MaXacNhan) > 6
	begin
		raiserror(N'Mã xác nhận không được bỏ trống hoặc dài hơn 6 ký tự!', 16, 1);
		return;
	end;

    -- Cập nhật mật khẩu người dùng
    UPDATE DanhMucKhachHangDoiLenh
    SET Password = @MatKhau,
        MaXacNhan = NULL, -- Xóa mã xác nhận sau khi sử dụng
        ThoiGianTaoMaKichHoat = NULL, -- Xóa thời gian tạo mã xác nhận
        EditDate = GETDATE()
    WHERE ID = @ID

   
END
GO
