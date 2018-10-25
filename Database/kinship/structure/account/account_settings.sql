USE kinship
GO

DROP TABLE IF EXISTS kinship_account_settings
GO

CREATE TABLE kinship_account_settings(
	user_id int NOT NULL,
	setting binary,
	CONSTRAINT PK_kinship_account_settings PRIMARY KEY CLUSTERED (user_id),     
	CONSTRAINT FK_kinship_account_settings FOREIGN KEY (user_id) REFERENCES kinship_accounts (user_id)
)
GO