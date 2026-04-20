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
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.tblLenhOnlineChiTiet')
      AND name = N'IX_tblLenhOnlineChiTiet_IDLenhOnline'
)
BEGIN
    CREATE INDEX IX_tblLenhOnlineChiTiet_IDLenhOnline
        ON dbo.tblLenhOnlineChiTiet(IDLenhOnline);
END
GO
