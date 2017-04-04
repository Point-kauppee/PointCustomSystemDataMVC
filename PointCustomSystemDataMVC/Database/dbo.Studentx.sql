CREATE TABLE [dbo].[Studentx] (
    [Student_id]        INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]         NVARCHAR (50)   NULL,
    [LastName]          NVARCHAR (50)   NULL,
    [Identity]          NVARCHAR (20)   NULL,
    [Address]           NVARCHAR (100)  NULL,
    [Notes]             NVARCHAR (300)  NULL,
    [Email]             NVARCHAR (100)  NULL,
    [EnrollmentDateIN]  DATETIME        NULL,
    [EnrollmentDateOUT] DATETIME        NULL,
    [EnrollmentDateOFF] DATETIME        NULL,
    [CreatedAt]         DATETIME        NULL,
    [LastModifiedAt]    DATETIME        NULL,
    [DeletedAt]         DATETIME        NULL,
    [Active]            BIT             NULL,
    [Information]       NVARCHAR (2000) NULL,
    [StudentGroup_id]   INT             NULL,
    PRIMARY KEY CLUSTERED ([Student_id] ASC),
    CONSTRAINT [FK_Studentx_StudentGroup] FOREIGN KEY ([StudentGroup_id]) REFERENCES [dbo].[StudentGroup] ([StudentGroup_id])
);

