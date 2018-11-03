USE kinship
GO

UPDATE kinship_support_tickets
SET ticket_status = N'Answered'
WHERE ticket_id = 1;
GO