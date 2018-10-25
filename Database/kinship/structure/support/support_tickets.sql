USE kinship
GO

DROP TABLE IF EXISTS kinship_support_tickets
GO

CREATE TABLE kinship_support_tickets(
	ticket_id int IDENTITY(1,1) NOT NULL,
	ticket_title nvarchar(64) NOT NULL,
	ticket_text nvarchar(256) NOT NULL,
	sender_id int NOT NULL,
	sent_time datetime NOT NULL,
	CONSTRAINT PK_kinship_support_tickets PRIMARY KEY CLUSTERED (ticket_id),
	CONSTRAINT FK_kinship_support_tickets FOREIGN KEY (sender_id) REFERENCES kinship_accounts (user_id)
)
GO