USE kinship_world0
GO

DROP TABLE IF EXISTS world0_building_queue
GO

CREATE TABLE world0_building_queue(
	queue_id int IDENTITY(1,1) NOT NULL,
	village_id int NOT NULL,
	building nvarchar(32) NOT NULL,
	change int NOT NULL,
	time_start datetime NOT NULL,
	time_end datetime NOT NULL,
	CONSTRAINT PK_world0_building_queue PRIMARY KEY CLUSTERED (queue_id),
	CONSTRAINT FK_world0_building_queue FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO