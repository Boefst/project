USE kinship
GO

DROP TABLE IF EXISTS kinship_account_profile
GO

CREATE TABLE kinship_account_profile(
	user_id int NOT NULL,
	profile_text nvarchar(256),
	CONSTRAINT PK_kinship_account_profile PRIMARY KEY CLUSTERED (user_id),     
	CONSTRAINT FK_kinship_account_profile FOREIGN KEY (user_id) REFERENCES kinship_accounts (user_id)
)
GO