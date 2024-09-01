IF NOT EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = Object_id(N'dbo.Order') AND type = 'U'
		)
BEGIN
	CREATE TABLE [dbo].[Order]
	(
	  Id INT NOT NULL IDENTITY(1,1),
	  ProductCode NVARCHAR(50),
	  CONSTRAINT PK_Order
            PRIMARY KEY
            (
                Id 
            )
	)
END
GO