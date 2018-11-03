USE kinship
GO

DROP TABLE IF EXISTS kinship_support_tickets
GO

CREATE TABLE kinship_support_tickets(
	ticket_id int IDENTITY(1,1) NOT NULL,
	ticket_title nvarchar(64) NOT NULL,
	ticket_category nvarchar(64) NOT NULL,
	ticket_world nvarchar(64) NOT NULL,
	sender_id int NOT NULL,
	create_time datetime NOT NULL,
	ticket_status nvarchar(32) NOT NULL,
	rating int,
	CONSTRAINT PK_kinship_support_tickets PRIMARY KEY CLUSTERED (ticket_id),
	CONSTRAINT FK_kinship_support_tickets FOREIGN KEY (sender_id) REFERENCES kinship_accounts (user_id)
)
GO