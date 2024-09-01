DROP TYPE IF EXISTS [dbo].[Product_DataType];
CREATE TYPE [dbo].[Product_DataType] AS TABLE (
 Code nvarchar(50) NOT NULL,
 Name nvarchar(120) NOT NULL,  
 Description NVARCHAR(MAX) NOT NULL
);