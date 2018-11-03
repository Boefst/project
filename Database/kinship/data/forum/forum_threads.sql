USE kinship
GO

INSERT INTO kinship_forum_threads (category_id, title, creator_id, create_time, deleted)
VALUES
	(1, N'Counting thread', 1, CURRENT_TIMESTAMP, 0)
GO