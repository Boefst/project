USE kinship_world0
GO

DROP TABLE IF EXISTS world0_research_queue
GO

CREATE TABLE world0_research_queue(
	queue_id int IDENTITY(1,1) NOT NULL,
	village_id int NOT NULL,
	research nvarchar(32) NOT NULL,
	change nvarchar(8) NOT NULL,
	time_start datetime NOT NULL,
	time_end datetime NOT NULL,
	CONSTRAINT PK_world0_research_queue PRIMARY KEY CLUSTERED (queue_id),
	CONSTRAINT FK_world0_research_queue FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO