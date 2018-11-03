USE kinship
GO

DROP TABLE IF EXISTS kinship_support_messages
GO

CREATE TABLE kinship_support_messages(
	message_id int IDENTITY(1,1) NOT NULL,
	ticket_id int NOT NULL,
	ticket_message nvarchar(256) NOT NULL,
	sender_id int NOT NULL,
	admin binary,
	sent_time datetime NOT NULL,
	CONSTRAINT PK_kinship_support_messages PRIMARY KEY CLUSTERED (message_id),
	CONSTRAINT FK_kinship_support_messages FOREIGN KEY (ticket_id) REFERENCES kinship_support_tickets (ticket_id)
)
GO