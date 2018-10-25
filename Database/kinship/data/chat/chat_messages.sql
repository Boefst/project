USE kinship
GO

INSERT INTO kinship_chat_messages (sender_id, receiver_id, message_text, sent_time)
VALUES
	(1, 2, N'Herro my friend!', CURRENT_TIMESTAMP),
	(2, 1, N'Waddup!', CURRENT_TIMESTAMP)
GO