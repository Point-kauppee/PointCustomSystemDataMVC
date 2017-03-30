CREATE TABLE [dbo].[TreatmentPlace] (
    [TreatmentPlace_id]    INT           IDENTITY (1, 1) NOT NULL,
    [TreatmentPlaceName]   NVARCHAR (50) NULL,
    [TreatmentPlaceNumber] NVARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([TreatmentPlace_id] ASC)
);

