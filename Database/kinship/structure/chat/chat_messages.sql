USE kinship
GO

DROP TABLE IF EXISTS kinship_chat_messages
GO

CREATE TABLE kinship_chat_messages(
	message_id int IDENTITY(1,1) NOT NULL,
	sender_id int NOT NULL,
	receiver_id int NOT NULL,
	message_text nvarchar(256) NOT NULL,
	sent_time datetime NOT NULL,
	CONSTRAINT PK_kinship_chat_messages PRIMARY KEY CLUSTERED (message_id),     
	CONSTRAINT FK_kinship_chat_messages_sender FOREIGN KEY (sender_id) REFERENCES kinship_accounts (user_id),
	CONSTRAINT FK_kinship_chat_messages_receiver FOREIGN KEY (receiver_id) REFERENCES kinship_accounts (user_id)
)
GO