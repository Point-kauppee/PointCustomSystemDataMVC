CREATE TABLE [dbo].[TreatmentOffice] (
    [TreatmentOffice_id]  INT             IDENTITY (1, 1) NOT NULL,
    [TreatmentOfficeName] NVARCHAR (200)  NULL,
    [Address]             NVARCHAR (100)  NULL,
    [Note]                NVARCHAR (1000) NULL,
    [MapPlace]            NVARCHAR(MAX) NULL,
    PRIMARY KEY CLUSTERED ([TreatmentOffice_id] ASC)
);

