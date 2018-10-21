USE [kinship]
GO

DROP TABLE IF EXISTS [dbo].[kinship_sessions]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[kinship_sessions](
	[user_id] [int] PRIMARY KEY NOT NULL,
	[client_id] [nvarchar](256) NOT NULL,
	[client_secret] [nvarchar](256) NOT NULL,
	[ip_adress] [nvarchar](32) NOT NULL
)
GO