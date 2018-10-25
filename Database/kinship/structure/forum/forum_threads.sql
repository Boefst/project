USE kinship
GO

DROP TABLE IF EXISTS kinship_forum_threads
GO

CREATE TABLE kinship_forum_threads(
	thread_id int IDENTITY(1,1) NOT NULL,
	category_id int NOT NULL,
	title nvarchar(32) NOT NULL,
	creator_id int NOT NULL,
	create_time datetime NOT NULL,
	CONSTRAINT PK_kinship_forum_threads PRIMARY KEY CLUSTERED (thread_id),     
	CONSTRAINT FK_kinship_forum_threads_category FOREIGN KEY (category_id) REFERENCES kinship_forum_categories (category_id),
	CONSTRAINT FK_kinship_forum_threads_user FOREIGN KEY (creator_id) REFERENCES kinship_accounts (user_id)
)
GO