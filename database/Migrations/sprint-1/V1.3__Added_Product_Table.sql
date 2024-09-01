IF NOT EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = Object_id(N'dbo.Product') AND type = 'U'
		)
BEGIN
	CREATE TABLE dbo.Product
	(
	Code NVARCHAR(50) NOT NULL,
	Name NVARCHAR(120) NOT NULL,
	Description NVARCHAR(MAX) NOT NULL,
	  CONSTRAINT PK_Product
            PRIMARY KEY
            (
                Code 
            )
	)
END
GO