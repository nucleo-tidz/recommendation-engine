IF NOT EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = Object_id(N'dbo.Customer') AND type = 'U'
		)
BEGIN
	CREATE TABLE dbo.Customer
	(
	Email NVARCHAR(50) NOT NULL,
	Name NVARCHAR(120) NOT NULL,
	
	  CONSTRAINT PK_Customer
            PRIMARY KEY
            (
                Email 
            )
	)
END
GO