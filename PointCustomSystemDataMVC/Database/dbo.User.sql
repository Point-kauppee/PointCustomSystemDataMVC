CREATE TABLE [dbo].[User] (
    [User_id]			INT           IDENTITY (1, 1) NOT NULL,
    [UserIdentity]		NVARCHAR (50) NOT NULL,
    [Password]			NVARCHAR (50) NOT NULL,
    [Personnel_id]		INT           NULL,
    [Student_id]		INT           NULL,
    [Customer_id]		INT           NULL,
	[CreatedAt]			DATETIME		NULL,
	[LastModifiedAt]	DATETIME		NULL,
	[DeletedAt]			DATETIME		NULL,
	[Active]			BIT				NULL,
    [Information]		NVARCHAR (2000) NULL,
    PRIMARY KEY CLUSTERED ([User_id] ASC),
    CONSTRAINT [FK_User_ToTable] FOREIGN KEY ([Personnel_id]) REFERENCES [dbo].[Personnel] ([Personnel_id]),
    CONSTRAINT [FK_User_ToTable_1] FOREIGN KEY ([Student_id]) REFERENCES [dbo].[Studentx] ([Student_id]),
    CONSTRAINT [FK_User_ToTable_2] FOREIGN KEY ([Customer_id]) REFERENCES [dbo].[Customer] ([Customer_id])
);

