IF OBJECT_ID(N'dbo.tblLenhOnlines', N'U') IS NULL AND OBJECT_ID(N'dbo.LenhOnlines', N'U') IS NOT NULL
BEGIN
    EXEC sp_rename N'dbo.LenhOnlines', N'tblLenhOnlines', N'OBJECT';
END
GO

IF OBJECT_ID(N'dbo.tblLenhOnlines', N'U') IS NULL
BEGIN
    RETURN;
END
GO

IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'IDUser'
)
AND NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'IDDanhMucKhachHangDoiLenh'
)
BEGIN
    EXEC sp_rename N'dbo.tblLenhOnlines.IDUser', N'IDDanhMucKhachHangDoiLenh', N'COLUMN';
END
GO

IF EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_tblLenhOnlines_DanhMucNguoiSuDung'
      AND parent_object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlines
    DROP CONSTRAINT FK_tblLenhOnlines_DanhMucNguoiSuDung;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_tblLenhOnlines_DanhMucKhachHangDoiLenh'
      AND parent_object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlines
    ADD CONSTRAINT FK_tblLenhOnlines_DanhMucKhachHangDoiLenh
        FOREIGN KEY (IDDanhMucKhachHangDoiLenh) REFERENCES dbo.DanhMucKhachHangDoiLenh(ID);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'idx_tblLenhOnlines_IDDanhMucKhachHangDoiLenh'
)
BEGIN
    CREATE INDEX idx_tblLenhOnlines_IDDanhMucKhachHangDoiLenh
        ON dbo.tblLenhOnlines(IDDanhMucKhachHangDoiLenh);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'UX_tblLenhOnlines_HouseBill'
)
BEGIN
    CREATE UNIQUE INDEX UX_tblLenhOnlines_HouseBill
        ON dbo.tblLenhOnlines(HouseBill)
        WHERE HouseBill IS NOT NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'TrangThai'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlines
    ADD TrangThai INT NOT NULL CONSTRAINT DF_tblLenhOnlines_TrangThai DEFAULT(0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'SoThuTuLenh'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlines
    ADD SoThuTuLenh BIGINT NULL;
END
GO

;WITH OrderedRows AS
(
    SELECT
        ID,
        ROW_NUMBER() OVER (ORDER BY ID) AS NewSoThuTuLenh
    FROM dbo.tblLenhOnlines
)
UPDATE lo
SET SoThuTuLenh = ISNULL(lo.SoThuTuLenh, o.NewSoThuTuLenh)
FROM dbo.tblLenhOnlines lo
INNER JOIN OrderedRows o ON o.ID = lo.ID;
GO

IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'SoThuTuLenh'
      AND is_nullable = 1
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlines ALTER COLUMN SoThuTuLenh BIGINT NOT NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlines')
      AND name = N'UX_tblLenhOnlines_SoThuTuLenh'
)
BEGIN
    CREATE UNIQUE INDEX UX_tblLenhOnlines_SoThuTuLenh
        ON dbo.tblLenhOnlines(SoThuTuLenh);
END
GO
