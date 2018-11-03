USE kinship
GO

DROP PROCEDURE IF EXISTS procedure_create_ticket
GO

CREATE PROCEDURE procedure_create_ticket
	@title nvarchar(64),
	@category nvarchar(32),
	@world nvarchar(32),
	@senderID int,
	@message nvarchar(512),
	@isAdmin binary,
	@new_identity INT = NULL OUTPUT
AS
	INSERT INTO kinship_support_tickets (ticket_title, ticket_category, ticket_world, sender_id, create_time, ticket_status)
	VALUES (@title, @category, @world, @senderID, CURRENT_TIMESTAMP, N'NEW')

	SET @new_identity = SCOPE_IDENTITY();
	
	INSERT INTO kinship_support_messages (ticket_id, ticket_message, sender_id, admin, sent_time)
	VALUES (@new_identity, @message, @senderID, @isAdmin, CURRENT_TIMESTAMP)

GO