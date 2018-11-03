USE kinship
GO

DROP PROCEDURE IF EXISTS procedure_create_forum_thread
GO

CREATE PROCEDURE procedure_create_forum_thread
	@category int,
	@title nvarchar(64),
	@creator int,
	@message nvarchar(512),
	@isAdmin binary,
	@new_identity INT = NULL OUTPUT
AS
	INSERT INTO kinship_forum_threads (category_id, title, creator_id, create_time, deleted)
	VALUES (@category, @title, @creator, CURRENT_TIMESTAMP, 0)

	SET @new_identity = SCOPE_IDENTITY();
	
	INSERT INTO kinship_forum_messages (thread_id, message_text, sender_id, is_admin, sent_time, deleted)
	VALUES (@new_identity, @message, @creator, @isAdmin, CURRENT_TIMESTAMP, 0)

GO