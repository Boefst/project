USE kinship
GO

INSERT INTO kinship_forum_messages (thread_id, message_text, sender_id, is_admin, sent_time, deleted)
VALUES
	(1, N'one', 1, 0, CURRENT_TIMESTAMP, 0),
	(1, N'two', 1, 0, CURRENT_TIMESTAMP, 0),
	(1, N'three', 1, 1, CURRENT_TIMESTAMP, 0),
	(1, N'four', 1, 0, CURRENT_TIMESTAMP, 0),
	(1, N'five', 1, 0, CURRENT_TIMESTAMP, 0)
GO