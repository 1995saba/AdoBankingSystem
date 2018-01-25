
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE CreateNewBankClient(
	@Id VARCHAR(50),
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Email VARCHAR(50),
	@PasswordHash VARCHAR(50),
	@CreatedTime DATETIME,
	@EntityStatus INT
)
AS
BEGIN
	INSERT INTO dbo.BankClients
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[PasswordHash]
           ,[CreatedTime]
           ,[EntityStatus])
     VALUES
			(@Id, @FirstName, @LastName, @Email, @PasswordHash, @CreatedTime, @EntityStatus)
END
GO