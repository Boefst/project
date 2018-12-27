USE kinship_world0
GO

DROP TABLE IF EXISTS world0_village_resources
GO

CREATE TABLE world0_village_resources(
	village_id int NOT NULL,
	wood int NOT NULL,
	stone int NOT NULL,
	iron int NOT NULL,
	food int NOT NULL,
	gold int NOT NULL,
	CONSTRAINT PK_world0_village_resources PRIMARY KEY CLUSTERED (village_id),
	CONSTRAINT FK_world0_village_resources FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO