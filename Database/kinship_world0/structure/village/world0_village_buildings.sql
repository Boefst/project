USE kinship_world0
GO

DROP TABLE IF EXISTS world0_village_buildings
GO

CREATE TABLE world0_village_buildings(
	village_id int NOT NULL,
	headquarters int NOT NULL,
	rally_point int NOT NULL,
	barracks int NOT NULL,
	stables int NOT NULL,
	workshop int NOT NULL,
	woodcutter int NOT NULL,
	quarry int NOT NULL,
	iron_mine int NOT NULL,
	farm int NOT NULL,
	palace int NOT NULL,
	warehouse int NOT NULL,
	barn int NOT NULL,
	bank int NOT NULL,
	market int NOT NULL,
	houses int NOT NULL,
	monastery int NOT NULL,
	academy int NOT NULL,
	university int NOT NULL,
	wall int NOT NULL,
	tower int NOT NULL,
	CONSTRAINT PK_world0_village_buildings PRIMARY KEY CLUSTERED (village_id),
	CONSTRAINT FK_world0_village_buildings FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO