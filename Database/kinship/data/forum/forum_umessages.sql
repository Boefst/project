USE kinship
GO

INSERT INTO kinship_forum_messages (thread_id, message_text, sender_id, sent_time)
VALUES
	(1, N'ett', 1, CURRENT_TIMESTAMP)
GO