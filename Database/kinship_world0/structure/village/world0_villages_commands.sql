USE kinship_world0
GO

DROP TABLE IF EXISTS world0_village_commands
GO

CREATE TABLE world0_village_commands(
	command_id int IDENTITY(1,1) NOT NULL,
	sender_village_id int NOT NULL,
	target_village_id int NOT NULL,
	troops nvarchar(256) NOT NULL,
	command_type nvarchar(16) NOT NULL,
	send_time datetime NOT NULL,
	arrival_time datetime NOT NULL,
	CONSTRAINT PK_world0_village_commands PRIMARY KEY CLUSTERED (command_id),
	CONSTRAINT FK_world0_village_commands_origin FOREIGN KEY (sender_village_id) REFERENCES world0_villages (village_id),
	CONSTRAINT FK_world0_village_commands_target FOREIGN KEY (target_village_id) REFERENCES world0_villages (village_id)
)
GO