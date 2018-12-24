USE kinship_world0
GO

DROP PROCEDURE IF EXISTS procedure_new_village
GO

CREATE PROCEDURE procedure_new_village 
	@village_name nvarchar(32),
	@owner_id int,
	@x int,
	@y int,
	@z int,
	@new_identity INT = NULL OUTPUT
AS
	INSERT INTO world0_villages (village_name, village_owner_id, village_coord_x, village_coord_y, village_coord_z)
	VALUES (@village_name, @owner_id, @x, @y, @z)

	SET @new_identity = SCOPE_IDENTITY();
	
	INSERT INTO world0_village_buildings (village_id, headquarters, rally_point, barracks, stables, workshop, woodcutter, quarry, iron_mine, farm, palace, warehouse, barn, bank, market, houses, monastery, academy, university, wall, tower)
	VALUES (@new_identity, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)

	INSERT INTO world0_village_resources (village_id, wood, stone, iron, food, gold)
	VALUES (@new_identity, 200, 200, 200, 200, 0)
	
	INSERT INTO world0_village_troops (village_id, milita, swordsman, champion, spearman, pikeman, halberdier, warrior, axeman, berserker, slinger, archer, crossbowman, scout, eagle_scout, pathfinder, light_cavalry, knight, hussar, heavy_cavalry, cataphract, paladin, cavalry_archer, chariot_archer, battering_ram, siege_ram, mangonel, catapult, trebuchet, ballista, onager, scorpio)
	VALUES (@new_identity, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
	
	INSERT INTO world0_village_research (village_id, headquarters_crane, rally_point_calculation, barracks_armory, stables_ranch, workshop_arsenal, woodcutter_forager, woodcutter_sawmill, quarry_stonecutter, quarry_masonry, iron_mine_blacksmith, iron_mine_forge, farm_mill, farm_bakery, palace_tax, palace_statue, warehouse_stockpile, barn_granary, bank_vault, market_trade_cart, market_caravan, market_bazaar, houses_cranny, monastery_shrine, monastery_temple, academy_recruiter, academy_training_camp, university_school, university_library, wall_traps, wall_garrison, wall_mound, tower_watch, tower_patrol)
	VALUES (@new_identity, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)

GO