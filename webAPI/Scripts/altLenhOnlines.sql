if exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = 'HouseBill'
      and is_nullable = 1
)
begin
    print N'LenhOnlines.HouseBill is currently nullable. Please ensure no NULL values exist before changing to NOT NULL manually.';
end
go

if exists (
    select 1
    from sys.foreign_keys
    where name = N'FK_LenhOnlines_DanhMucNguoiSuDung'
      and parent_object_id = object_id(N'dbo.LenhOnlines')
)
begin
    alter table dbo.LenhOnlines drop constraint FK_LenhOnlines_DanhMucNguoiSuDung;
end
go

if exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'IDUser'
)
and not exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'IDDanhMucKhachHangDoiLenh'
)
begin
    if exists (
        select 1
        from sys.foreign_keys
        where name = N'FK_LenhOnlines_DanhMucKhachHangDoiLenh'
          and parent_object_id = object_id(N'dbo.LenhOnlines')
    )
    begin
        alter table dbo.LenhOnlines drop constraint FK_LenhOnlines_DanhMucKhachHangDoiLenh;
    end

    exec sp_rename N'dbo.LenhOnlines.IDUser', N'IDDanhMucKhachHangDoiLenh', N'COLUMN';
end
go

if exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'idx_LenhOnlines_IDUser'
)
begin
    exec sp_rename N'dbo.LenhOnlines.idx_LenhOnlines_IDUser', N'idx_LenhOnlines_IDDanhMucKhachHangDoiLenh', N'INDEX';
end
go

if not exists (
    select 1
    from sys.foreign_keys
    where name = N'FK_LenhOnlines_DanhMucKhachHangDoiLenh'
      and parent_object_id = object_id(N'dbo.LenhOnlines')
)
begin
    alter table dbo.LenhOnlines
    add constraint FK_LenhOnlines_DanhMucKhachHangDoiLenh
        foreign key (IDDanhMucKhachHangDoiLenh) references dbo.DanhMucKhachHangDoiLenh(ID);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'idx_LenhOnlines_IDDanhMucKhachHangDoiLenh'
)
begin
    create index idx_LenhOnlines_IDDanhMucKhachHangDoiLenh
        on dbo.LenhOnlines(IDDanhMucKhachHangDoiLenh);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'UX_LenhOnlines_HouseBill'
)
begin
    create unique index UX_LenhOnlines_HouseBill
        on dbo.LenhOnlines(HouseBill)
        where HouseBill is not null;
end
go

if not exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'TrangThai'
)
begin
    alter table dbo.LenhOnlines
    add TrangThai int not null constraint DF_LenhOnlines_TrangThai default(0);
end
go

if not exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'SoThuTuLenh'
)
begin
    alter table dbo.LenhOnlines
    add SoThuTuLenh bigint null;
end
go

;with OrderedRows as
(
    select
        ID,
        row_number() over (order by ID) as NewSoThuTuLenh
    from dbo.LenhOnlines
)
update lo
set SoThuTuLenh = isnull(lo.SoThuTuLenh, o.NewSoThuTuLenh)
from dbo.LenhOnlines lo
inner join OrderedRows o on o.ID = lo.ID;
go

if exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'SoThuTuLenh'
      and is_nullable = 1
)
begin
    alter table dbo.LenhOnlines alter column SoThuTuLenh bigint not null;
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.LenhOnlines')
      and name = N'UX_LenhOnlines_SoThuTuLenh'
)
begin
    create unique index UX_LenhOnlines_SoThuTuLenh
        on dbo.LenhOnlines(SoThuTuLenh);
end
go
