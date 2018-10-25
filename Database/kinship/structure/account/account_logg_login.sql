USE kinship
GO

DROP TABLE IF EXISTS kinship_accounts_login_log
GO

CREATE TABLE kinship_accounts_login_log(
	log_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	ip_address nvarchar(32) NOT NULL,
	login_time datetime NOT NULL,
	login_status nvarchar(32) NOT NULL,
	CONSTRAINT PK_kinship_accounts_login_log PRIMARY KEY CLUSTERED (log_id),
	CONSTRAINT FK_kinship_accounts_login_log FOREIGN KEY (user_id) REFERENCES kinship_accounts (user_id)
)
GO