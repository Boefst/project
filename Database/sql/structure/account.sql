USE [kinship]
GO

DROP TABLE IF EXISTS [dbo].[kinship_accounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[kinship_accounts](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[account_name] [nvarchar](32) NOT NULL,
	[account_email] [nvarchar](32) NOT NULL,
	[account_password] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_kinship_accounts] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO