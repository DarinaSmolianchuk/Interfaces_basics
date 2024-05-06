CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ПІБ] NVARCHAR(100) NOT NULL, 
    [Підрозділ] INT NOT NULL, 
    [Посада] NVARCHAR(30) NOT NULL, 
    [Телефон] VARCHAR(16) NOT NULL
)
