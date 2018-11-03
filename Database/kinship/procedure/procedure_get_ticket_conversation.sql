USE kinship
GO

DROP PROCEDURE IF EXISTS procedure_get_ticket_conversation
GO

CREATE PROCEDURE procedure_get_ticket_conversation
	@id int
AS

	SELECT *
	FROM 
		(SELECT message_id, ticket_message, Acc.account_name, sent_time FROM kinship_support_messages AS Mes INNER JOIN kinship_accounts AS Acc ON Mes.sender_id = Acc.user_id WHERE ticket_id = @id AND admin = 0) t1
	UNION
		(SELECT message_id, ticket_message, Acc.account_name, sent_time FROM kinship_support_messages AS Mes INNER JOIN [kinship_admin].[dbo].[admin_accounts] AS Acc ON Mes.sender_id = Acc.user_id WHERE ticket_id = @id AND admin = 1)
	ORDER BY message_id ASC

GO