USE kinship_admin
GO

DROP TABLE IF EXISTS admin_accounts
GO

CREATE TABLE admin_accounts(
	user_id int IDENTITY(1,1) NOT NULL,
	account_name nvarchar(32) NOT NULL UNIQUE,
	account_email nvarchar(32) NOT NULL UNIQUE,
	account_password nvarchar(128) NOT NULL,
	account_created datetime NOT NULL,
	CONSTRAINT PK_admin_accounts PRIMARY KEY CLUSTERED (user_id)
)
GO