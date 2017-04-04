CREATE TABLE [dbo].[StudentGroup] (
    [StudentGroup_id]  INT            IDENTITY (1, 1) NOT NULL,
    [StudentGroupName] NVARCHAR (100) NULL,
    [Active]           BIT            NULL,
    [CreatedAt]        DATETIME       NULL,
    [LastModifiedAt]   DATETIME       NULL,
    [DeletedAt]        DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([StudentGroup_id] ASC)
);

