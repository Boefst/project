USE kinship_world0
GO

DROP TABLE IF EXISTS world0_villages
GO

CREATE TABLE world0_villages(
	village_id int IDENTITY(1,1) NOT NULL,
	village_name nvarchar(32) NOT NULL,
	village_coord_x int NOT NULL,
	village_coord_y int NOT NULL,
	village_coord_z int NOT NULL,
	village_owner_id int NOT NULL,
	CONSTRAINT PK_world0_villages PRIMARY KEY CLUSTERED (village_id)
)
GO