USE kinship
GO

DROP TABLE IF EXISTS kinship_forum_messages
GO

CREATE TABLE kinship_forum_messages(
	message_id int IDENTITY(1,1) NOT NULL,
	thread_id int NOT NULL,
	message_text nvarchar(256),
	sender_id int NOT NULL,
	sent_time datetime NOT NULL,
	CONSTRAINT PK_kinship_forum_messages PRIMARY KEY CLUSTERED (message_id),     
	CONSTRAINT FK_kinship_forum_messages_thread FOREIGN KEY (thread_id) REFERENCES kinship_forum_threads (thread_id),
	CONSTRAINT FK_kinship_forum_messages_user FOREIGN KEY (sender_id) REFERENCES kinship_accounts (user_id)
)
GO