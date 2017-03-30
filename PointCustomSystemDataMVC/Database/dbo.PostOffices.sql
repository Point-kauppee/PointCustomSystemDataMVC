CREATE TABLE [dbo].[PostOffices] (
    [Post_id]            INT          IDENTITY (1, 1) NOT NULL,
    [PostalCode]         VARCHAR (5)  NULL,
    [PostOffice]         VARCHAR (50) NULL,
    [Personnel_id]       INT          NULL,
    [Customer_id]        INT          NULL,
    [Student_id]         INT          NULL,
    [TreatmentOffice_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Post_id] ASC),
    CONSTRAINT [FK_PostOffices_ToTable] FOREIGN KEY ([Personnel_id]) REFERENCES [dbo].[Personnel] ([Personnel_id]),
    CONSTRAINT [FK_PostOffices_ToTable_4] FOREIGN KEY ([Student_id]) REFERENCES [dbo].[Studentx] ([Student_id]),
    CONSTRAINT [FK_PostOffices_ToTable_2] FOREIGN KEY ([Customer_id]) REFERENCES [dbo].[Customer] ([Customer_id]),
    CONSTRAINT [FK_PostOffices_ToTable_6] FOREIGN KEY ([TreatmentOffice_id]) REFERENCES [dbo].[TreatmentOffice] ([TreatmentOffice_id])
);

