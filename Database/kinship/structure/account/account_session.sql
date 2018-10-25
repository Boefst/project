USE kinship
GO

DROP TABLE IF EXISTS kinship_sessions
GO

CREATE TABLE kinship_sessions(
	user_id int NOT NULL,
	client_id nvarchar(256) NOT NULL,
	client_secret nvarchar(256) NOT NULL,
	ip_address nvarchar(32) NOT NULL,
	CONSTRAINT PK_kinship_sessions PRIMARY KEY CLUSTERED (user_id),     
	CONSTRAINT FK_kinship_sessions FOREIGN KEY (user_id) REFERENCES kinship_accounts (user_id)
)
GO