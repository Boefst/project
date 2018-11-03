USE kinship_admin
GO

INSERT INTO admin_accounts (account_name, account_email, account_password, account_created)
VALUES (N'Admin1', N'admin@admin.com', N'admin123', CURRENT_TIMESTAMP)
GO