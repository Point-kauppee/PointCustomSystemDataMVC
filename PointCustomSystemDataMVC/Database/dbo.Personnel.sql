CREATE TABLE [dbo].[Personnel] (
    [Personnel_id]		INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]			NVARCHAR (50)  NULL,
    [LastName]			NVARCHAR (50)  NULL,
    [Identity]			NVARCHAR (20)  NULL,
    [Notes]				NVARCHAR (300) NULL,
    [Email]				NVARCHAR (100) NULL,
	[CreatedAt]			DATETIME		NULL,
	[LastModifiedAt]	DATETIME		NULL,
	[DeletedAt]			DATETIME		NULL,
	[Active]			BIT				NULL,
    [Information]		NVARCHAR (2000) NULL,
    PRIMARY KEY CLUSTERED ([Personnel_id] ASC)
);

