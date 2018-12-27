USE kinship_world0
GO

DROP TABLE IF EXISTS world0_village_research
GO

CREATE TABLE world0_village_research(
	village_id int NOT NULL,
	headquarters_crane bit NOT NULL,
	rally_point_calculation bit NOT NULL,
	barracks_armory bit NOT NULL,
	stables_ranch bit NOT NULL,
	workshop_arsenal bit NOT NULL,
	woodcutter_forager bit NOT NULL,
	woodcutter_sawmill bit NOT NULL,
	quarry_stonecutter bit NOT NULL,
	quarry_masonry bit NOT NULL,
	iron_mine_blacksmith bit NOT NULL,
	iron_mine_forge bit NOT NULL,
	farm_mill bit NOT NULL,
	farm_bakery bit NOT NULL,
	palace_tax bit NOT NULL,
	palace_statue bit NOT NULL,
	warehouse_stockpile bit NOT NULL,
	barn_granary bit NOT NULL,
	bank_vault bit NOT NULL,
	market_trade_cart bit NOT NULL,
	market_caravan bit NOT NULL,
	market_bazaar bit NOT NULL,
	houses_cranny bit NOT NULL,
	monastery_shrine bit NOT NULL,
	monastery_temple bit NOT NULL,
	academy_recruiter bit NOT NULL,
	academy_training_camp bit NOT NULL,
	university_school bit NOT NULL,
	university_library bit NOT NULL,
	wall_traps bit NOT NULL,
	wall_garrison bit NOT NULL,
	wall_mound bit NOT NULL,
	tower_watch bit NOT NULL,
	tower_patrol bit NOT NULL,
	CONSTRAINT PK_world0_village_research PRIMARY KEY CLUSTERED (village_id),
	CONSTRAINT FK_world0_village_research FOREIGN KEY (village_id) REFERENCES world0_villages (village_id)
)
GO