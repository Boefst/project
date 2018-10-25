USE kinship
GO

DROP TABLE IF EXISTS kinship_accounts
GO

CREATE TABLE kinship_accounts(
	user_id int IDENTITY(1,1) NOT NULL,
	account_name nvarchar(32) NOT NULL UNIQUE,
	account_email nvarchar(32) NOT NULL UNIQUE,
	account_password nvarchar(128) NOT NULL,
	account_created datetime NOT NULL,
	CONSTRAINT PK_kinship_accounts PRIMARY KEY CLUSTERED (user_id)
)
GO