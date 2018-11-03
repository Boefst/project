USE kinship
GO

INSERT INTO kinship_support_messages (ticket_id, ticket_message, sender_id, admin, sent_time)
VALUES
	(1, N'How do I do dis?', 1, 0, CURRENT_TIMESTAMP),
	(2, N'This player is cheating ban him plz', 2, 0, CURRENT_TIMESTAMP),
	(3, N'I can not see my troops', 3, 0, CURRENT_TIMESTAMP),
	(1, N'You do dis by doing dis', 1, 1, CURRENT_TIMESTAMP),
	(2, N'I will ban', 1, 1, CURRENT_TIMESTAMP),
	(3, N'Now you can', 1, 1, CURRENT_TIMESTAMP),
	(1, N'How do I do dis?', 1, 0, CURRENT_TIMESTAMP),
	(5, N'This player is cheating ban him plz', 2, 0, CURRENT_TIMESTAMP),
	(6, N'I can not see my troops', 3, 0, CURRENT_TIMESTAMP),
	(1, N'You do dis by doing dis', 1, 1, CURRENT_TIMESTAMP),
	(5, N'I will ban', 1, 1, CURRENT_TIMESTAMP),
	(6, N'Now you can', 1, 1, CURRENT_TIMESTAMP)
GO