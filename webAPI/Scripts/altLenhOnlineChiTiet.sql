IF OBJECT_ID(N'dbo.tblLenhOnlineChiTiet', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblLenhOnlineChiTiet
    (
        ID BIGINT NOT NULL,
        IDLenhOnline BIGINT NOT NULL,

        PhiLuuKho DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiLuuKho DEFAULT (0),
        PhiGiaoNhan DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiGiaoNhan DEFAULT (0),
        PhiBocXep DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiBocXep DEFAULT (0),
        VAT DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_VAT DEFAULT (0),

        TrangThaiThanhToan TINYINT NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_TrangThaiThanhToan DEFAULT (0),
        TrangThaiThongQuan TINYINT NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_TrangThaiThongQuan DEFAULT (0),

        ThuKho NVARCHAR(255) NULL,
        Forwarder NVARCHAR(255) NULL,
        TenTau NVARCHAR(255) NULL,
        ChuHang NVARCHAR(255) NULL,

        SoKien INT NULL,
        SoChuyen NVARCHAR(100) NULL,
        SoHouseBill NVARCHAR(100) NULL,
        NgayTauCap DATETIME NULL,

        TrongLuong DECIMAL(18,3) NULL,
        SoCont NVARCHAR(50) NULL,
        SoKhoi DECIMAL(18,3) NULL,

        LinkTaiHoaDon NVARCHAR(500) NULL,
        DuongDanFileHoaDon NVARCHAR(500) NULL,

        CreateDate DATETIME NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_CreateDate DEFAULT (GETDATE()),
        EditDate DATETIME NULL,

        CONSTRAINT PK_tblLenhOnlineChiTiet PRIMARY KEY (ID),
        CONSTRAINT UQ_tblLenhOnlineChiTiet_IDLenhOnline UNIQUE (IDLenhOnline),
        CONSTRAINT FK_tblLenhOnlineChiTiet_tblLenhOnlines
            FOREIGN KEY (IDLenhOnline) REFERENCES dbo.tblLenhOnlines(ID) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'PhiLuuKho'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD PhiLuuKho DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiLuuKho DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'PhiGiaoNhan'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD PhiGiaoNhan DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiGiaoNhan DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'PhiBocXep'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD PhiBocXep DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_PhiBocXep DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'VAT'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD VAT DECIMAL(18,2) NULL CONSTRAINT DF_tblLenhOnlineChiTiet_VAT DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'TrangThaiThanhToan'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD TrangThaiThanhToan TINYINT NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_TrangThaiThanhToan DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'TrangThaiThongQuan'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD TrangThaiThongQuan TINYINT NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_TrangThaiThongQuan DEFAULT (0);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'ThuKho'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD ThuKho NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'Forwarder'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD Forwarder NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'TenTau'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD TenTau NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'ChuHang'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD ChuHang NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'SoKien'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD SoKien INT NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'SoChuyen'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD SoChuyen NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'SoHouseBill'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD SoHouseBill NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'NgayTauCap'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD NgayTauCap DATETIME NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'TrongLuong'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD TrongLuong DECIMAL(18,3) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'SoCont'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD SoCont NVARCHAR(50) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'SoKhoi'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD SoKhoi DECIMAL(18,3) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'LinkTaiHoaDon'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD LinkTaiHoaDon NVARCHAR(500) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'DuongDanFileHoaDon'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD DuongDanFileHoaDon NVARCHAR(500) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'CreateDate'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD CreateDate DATETIME NOT NULL CONSTRAINT DF_tblLenhOnlineChiTiet_CreateDate DEFAULT (GETDATE());
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'EditDate'
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD EditDate DATETIME NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'IX_tblLenhOnlineChiTiet_IDLenhOnline'
)
BEGIN
    CREATE INDEX IX_tblLenhOnlineChiTiet_IDLenhOnline
        ON dbo.tblLenhOnlineChiTiet(IDLenhOnline);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'UQ_tblLenhOnlineChiTiet_IDLenhOnline'
)
BEGIN
    CREATE UNIQUE INDEX UQ_tblLenhOnlineChiTiet_IDLenhOnline
        ON dbo.tblLenhOnlineChiTiet(IDLenhOnline);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_tblLenhOnlineChiTiet_LenhOnlines'
      AND parent_object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
)
BEGIN
    ALTER TABLE dbo.tblLenhOnlineChiTiet
    ADD CONSTRAINT FK_tblLenhOnlineChiTiet_tblLenhOnlines
        FOREIGN KEY (IDLenhOnline) REFERENCES dbo.tblLenhOnlines(ID) ON DELETE CASCADE;
END
GO
