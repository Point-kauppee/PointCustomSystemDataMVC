CREATE TABLE [dbo].[TreatmentReport] (
    [TreatmentReport_id]  INT             IDENTITY (1, 1) NOT NULL,
    [TreatmentReportText] NVARCHAR (3000) NULL,
    [TreatmentDate]       DATETIME        NULL,
    [TreatmentTime]       DATETIME        NULL,
    [User_id]             INT             NULL,
    [Customer_id]         INT             NULL,
    [Student_id]          INT             NULL,
    [Personnel_id]        INT             NULL,
    [Reservation_id]      INT             NULL,
    [Treatment_id]        INT             NULL,
    PRIMARY KEY CLUSTERED ([TreatmentReport_id] ASC),
    CONSTRAINT [FK_TreatmentReport_ToTable_5] FOREIGN KEY ([Treatment_id]) REFERENCES [dbo].[Treatment] ([Treatment_id]),
    CONSTRAINT [FK_TreatmentReport_ToTable_3] FOREIGN KEY ([Personnel_id]) REFERENCES [dbo].[Personnel] ([Personnel_id]),
    CONSTRAINT [FK_TreatmentReport_ToTable] FOREIGN KEY ([User_id]) REFERENCES [dbo].[User] ([User_id]),
    CONSTRAINT [FK_TreatmentReport_ToTable_4] FOREIGN KEY ([Reservation_id]) REFERENCES [dbo].[Reservation] ([Reservation_id]),
    CONSTRAINT [FK_TreatmentReport_ToTable_2] FOREIGN KEY ([Student_id]) REFERENCES [dbo].[Studentx] ([Student_id]),
    CONSTRAINT [FK_TreatmentReport_ToTable_1] FOREIGN KEY ([Customer_id]) REFERENCES [dbo].[Customer] ([Customer_id])
);

