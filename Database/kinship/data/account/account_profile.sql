USE kinship
GO

UPDATE kinship_account_profile
SET profile_text = N'New profile text'
WHERE user_id = 1

GO