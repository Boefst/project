USE kinship
GO

DROP PROCEDURE IF EXISTS procedure_login
GO

CREATE PROCEDURE procedure_login 
	@username nvarchar(32),
	@password nvarchar(128),
	@client_id nvarchar(256),
	@client_secret nvarchar(256),
	@ip_address nvarchar(32)
AS
	DECLARE @userID int
	
	SET @userID =
	(
		SELECT user_id FROM kinship_accounts WHERE account_name = @username AND account_password = @password COLLATE SQL_Latin1_General_CP1_CS_AS
	)
	
	IF (@userID IS NULL)
	BEGIN
		SET @userID =
		(
			SELECT user_id FROM kinship_accounts WHERE account_name = @username
		)
		IF (@userID IS NULL)
		BEGIN
			SELECT -1 AS user_id, N'Username not found' AS status 
		END
		ELSE
		BEGIN
			INSERT INTO kinship_accounts_login_log (user_id, ip_address, login_time, login_status)
			VALUES
				(@userID, @ip_address, CURRENT_TIMESTAMP, N'Failed')

			SELECT -1 AS user_id, N'Password incorrect' AS status
		END
	END
	ELSE
	BEGIN
		IF EXISTS (SELECT user_id FROM kinship_sessions WHERE user_id = @userID)
		BEGIN
			UPDATE kinship_sessions
			SET client_id = @client_id, client_secret = @client_secret, ip_address = @ip_address
			WHERE user_id = @userID
		END
		ELSE
		BEGIN
			INSERT INTO kinship_sessions (user_id, client_id, client_secret, ip_address)
			VALUES
				(@userID, @client_id, @client_secret, @ip_address)
		END

		INSERT INTO kinship_accounts_login_log (user_id, ip_address, login_time, login_status)
		VALUES
			(@userID, @ip_address, CURRENT_TIMESTAMP, N'Success')

		SELECT @userID AS user_id, N'Success' AS status
	END
GO