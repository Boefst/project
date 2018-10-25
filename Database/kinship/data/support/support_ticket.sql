USE kinship
GO

INSERT INTO kinship_support_tickets (ticket_title, ticket_text, sender_id, sent_time)
VALUES
	(N'Help', N'I need help', 1, CURRENT_TIMESTAMP)
GO