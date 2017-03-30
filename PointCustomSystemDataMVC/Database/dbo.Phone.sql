CREATE TABLE [dbo].[Phone] (
    [Phone_id]           INT           IDENTITY (1, 1) NOT NULL,
    [PhoneNum_1]         NVARCHAR (20) NULL,
    [PhoneNum_2]         NVARCHAR (20) NULL,
    [PhoneNum_3]         NVARCHAR (20) NULL,
    [Personnel_id]       INT           NULL,
    [Customer_id]        INT           NULL,
    [Student_id]         INT           NULL,
    [TreatmentOffice_id] INT           NULL,
    PRIMARY KEY CLUSTERED ([Phone_id] ASC),
    CONSTRAINT [FK_Phone_ToTable] FOREIGN KEY ([Personnel_id]) REFERENCES [dbo].[Personnel] ([Personnel_id]),
    CONSTRAINT [FK_Phone_ToTable_1] FOREIGN KEY ([Student_id]) REFERENCES [dbo].[Studentx] ([Student_id]),
    CONSTRAINT [FK_Phone_ToTable_3] FOREIGN KEY ([Customer_id]) REFERENCES [dbo].[Customer] ([Customer_id]),
    CONSTRAINT [FK_Phone_ToTable_2] FOREIGN KEY ([TreatmentOffice_id]) REFERENCES [dbo].[TreatmentOffice] ([TreatmentOffice_id])
);

