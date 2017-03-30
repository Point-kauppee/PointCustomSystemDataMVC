CREATE TABLE [dbo].[StudentGroup] (
    [StudentGroup_id]  INT            IDENTITY (1, 1) NOT NULL,
    [StudentGroupName] NVARCHAR (100) NULL,
    [Active]           BIT            NULL,
    [CreatedAt]        DATETIME       NULL,
    [LastModifiedAt]   DATETIME       NULL,
    [DeletedAt]        DATETIME       NULL,
    [User_id]          INT            NULL,
    [Student_id]       INT            NULL,
    PRIMARY KEY CLUSTERED ([StudentGroup_id] ASC),
    CONSTRAINT [FK_StudentGroup_Studentx] FOREIGN KEY ([Student_id]) REFERENCES [dbo].[Studentx] ([Student_id]),
    CONSTRAINT [FK_StudentGroup_User] FOREIGN KEY ([User_id]) REFERENCES [dbo].[User] ([User_id])
);

