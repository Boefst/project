USE [kinship_world0]
GO

DROP TABLE IF EXISTS [dbo].[world0_villages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[world0_villages](
	[village_id] [int] IDENTITY(1,1) NOT NULL,
	[village_name] [nvarchar](16) NOT NULL,
	[village_points] [int],
	[village_xcoord] [int] NOT NULL,
	[village_ycoord] [int] NOT NULL,
	[village_owner_id] [int],
 CONSTRAINT [PK_world0_villages] PRIMARY KEY CLUSTERED 
(
	[village_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO