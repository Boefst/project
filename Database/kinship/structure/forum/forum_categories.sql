USE kinship
GO

DROP TABLE IF EXISTS kinship_forum_categories
GO

CREATE TABLE kinship_forum_categories(
	category_id int IDENTITY(1,1) NOT NULL,
	title nvarchar(32) NOT NULL,
	CONSTRAINT PK_kinship_forum_categories PRIMARY KEY CLUSTERED (category_id)
)
GO