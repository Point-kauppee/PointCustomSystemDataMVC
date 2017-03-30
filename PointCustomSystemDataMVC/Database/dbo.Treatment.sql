CREATE TABLE [dbo].[Treatment] (
    [Treatment_id]   INT           IDENTITY (1, 1) NOT NULL,
    [TreatmentName]  NVARCHAR (20) NULL,
    [TreatmentTime]  VARCHAR (20)  NULL,
    [TreatmentPrice] VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Treatment_id] ASC)
);

