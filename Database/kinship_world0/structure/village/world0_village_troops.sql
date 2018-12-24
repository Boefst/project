USE kinship_world0
GO

DROP TABLE IF EXISTS world0_village_troops
GO

CREATE TABLE world0_village_troops(
	village_id int NOT NULL,
	milita int NOT NULL,
	swordsman int NOT NULL,
	champion int NOT NULL,
	spearman int NOT NULL,
	pikeman int NOT NULL,
	halberdier int NOT NULL,
	warrior int NOT NULL,
	axeman int NOT NULL,
	berserker int NOT NULL,
	slinger int NOT NULL,
	archer int NOT NULL,
	crossbowman int NOT NULL,
	scout int NOT NULL,
	eagle_scout int NOT NULL,
	pathfinder int NOT NULL,
	light_cavalry int NOT NULL,
	knight int NOT NULL,
	hussar int NOT NULL,
	heavy_cavalry int NOT NULL,
	cataphract int NOT NULL,
	paladin int NOT NULL,
	cavalry_archer int NOT NULL,
	chariot_archer int NOT NULL,
	battering_ram int NOT NULL,
	siege_ram int NOT NULL,
	mangonel int NOT NULL,
	catapult int NOT NULL,
	trebuchet int NOT NULL,
	ballista int NOT NULL,
	onager int NOT NULL,
	scorpio int NOT NULL,
	CONSTRAINT PK_world0_village_troops PRIMARY KEY CLUSTERED (village_id),
	CONSTRAINT FK_world0_village_troops FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO