USE kinship
GO

DROP TABLE IF EXISTS kinship_help_suggestions
GO

CREATE TABLE kinship_help_suggestions(
	suggestion_id int IDENTITY(1,1) NOT NULL,
	page_identifier nvarchar(32) NOT NULL,
	header_identifier nvarchar(32) NOT NULL,
	suggestion_text nvarchar(256) NOT NULL,
	sender_id int NOT NULL,
	sent_time datetime NOT NULL,
	CONSTRAINT PK_kinship_help_suggestions PRIMARY KEY CLUSTERED (suggestion_id),
	CONSTRAINT FK_kinship_help_suggestions FOREIGN KEY (sender_id) REFERENCES kinship_accounts (user_id)
)
GO