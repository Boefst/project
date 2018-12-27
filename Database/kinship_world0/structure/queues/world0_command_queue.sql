USE kinship_world0
GO

DROP TABLE IF EXISTS world0_command_queue
GO

CREATE TABLE world0_command_queue(
	queue_id int IDENTITY(1,1) NOT NULL,
	sender_id int NOT NULL,
	target_id int NOT NULL,
	command_type nvarchar(32) NOT NULL,
	troops nvarchar(512) NOT NULL,
	time_start datetime NOT NULL,
	time_end datetime NOT NULL,
	CONSTRAINT PK_world0_command_queue PRIMARY KEY CLUSTERED (queue_id),
	CONSTRAINT FK_world0_command_origin_queue FOREIGN KEY (sender_id) REFERENCES world0_villages (village_id),
	CONSTRAINT FK_world0_command_destination_queue FOREIGN KEY (target_id) REFERENCES world0_villages (village_id)
)
GO