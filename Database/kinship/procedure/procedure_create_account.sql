USE kinship
GO

DROP PROCEDURE IF EXISTS procedure_create_account
GO

CREATE PROCEDURE procedure_create_account 
	@name nvarchar(32),
	@email  nvarchar(64),
	@password nvarchar(128),
	@new_identity INT = NULL OUTPUT
AS
	INSERT INTO kinship_accounts (account_name, account_email, account_password, account_created)
	VALUES (@name, @email, @password, CURRENT_TIMESTAMP)

	SET @new_identity = SCOPE_IDENTITY();
	
	INSERT INTO kinship_account_profile (user_id, profile_text)
	VALUES (@new_identity, N'Add profile text...')

	INSERT INTO kinship_account_settings (user_id, setting)
	VALUES (@new_identity, 0)
GO