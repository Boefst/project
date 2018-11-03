USE kinship
GO

INSERT INTO kinship_support_tickets (ticket_title, ticket_category, ticket_world, sender_id, create_time, ticket_status)
VALUES
	(N'halp me', N'Question', N'world0', 1, CURRENT_TIMESTAMP, N'Unanswered'),
	(N'dis player cheating', N'Report Player', N'world0', 2, CURRENT_TIMESTAMP, N'Unanswered'),
	(N'troops bug', N'Bug', N'world0', 1, CURRENT_TIMESTAMP, N'Unanswered'),
	(N'halp me', N'Question', N'world0', 1, CURRENT_TIMESTAMP, N'Unanswered'),
	(N'dis player cheating', N'Report Player', N'world0', 3, CURRENT_TIMESTAMP, N'Unanswered'),
	(N'troops bug', N'Bug', N'world0', 2, CURRENT_TIMESTAMP, N'Unanswered')
GO