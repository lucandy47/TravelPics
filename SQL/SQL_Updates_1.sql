CREATE TABLE Users(
	UserId bigint IDENTITY(1,1) NOT NULL,
	Username nvarchar(MAX) NOT NULL,
	FirstName nvarchar(250) NOT NULL,
	LastName nvarchar(250) NOT NULL,
	Email nvarchar(250) NOT NULL,
	Password nvarchar(MAX) NOT NULL,
	Phone nvarchar(30) NULL,
	CONSTRAINT PK_User PRIMARY KEY CLUSTERED (UserId ASC)
)
GO