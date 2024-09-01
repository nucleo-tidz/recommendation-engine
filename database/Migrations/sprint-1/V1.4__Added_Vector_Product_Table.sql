IF NOT EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = Object_id(N'dbo.ProductVector') AND type = 'U'
		)
BEGIN
	CREATE TABLE [dbo].[ProductVector]
	(
	  Id INT NOT NULL IDENTITY(1,1),
	  ProductCode NVARCHAR(50) NOT NULL,
	  ProductVector NVARCHAR(MAX),
	  CONSTRAINT PK_ProductVector
            PRIMARY KEY
            (
                Id 
            )
	)
END
GO