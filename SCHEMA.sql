USE AdoBankingSystem;

CREATE TABLE BankClients
(
	Id varchar(50) NOT NULL PRIMARY KEY,
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	PasswordHash varchar(50) NOT NULL,
	CreatedTime DATETIME NOT NULL,
	EntityStatus INT NOT NULL
);

CREATE TABLE BankManagers
(
	Id VARCHAR(50) NOT NULL PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	PasswordHash VARCHAR(50) NOT NULL,
	CreatedTime DATETIME NOT NULL,
	EntityStatus INT NOT NULL
);

CREATE TABLE Transactions
(
	Id VARCHAR(50) NOT NULL PRIMARY KEY,
	SenderId VARCHAR(50) NOT NULL,
	ReceiverId VARCHAR(50) NOT NULL,
	TransactionType INT NOT NULL,
	TransactionAmount DECIMAL NOT NULL,
	TransactionTime DATETIME NOT NULL,
	CreatedTime DATETIME NOT NULL,
	EntityStatus INT NOT NULL
);

CREATE TABLE CurrentSessions
(
	Id VARCHAR(50) NOT NULL PRIMARY KEY,
	UserId VARCHAR(50) NOT NULL,
	SignInTime DATETIME NOT NULL,
	LastOperationTime DATETIME NOT NULL,
	CreatedTime DATETIME NOT NULL,
	EntityStatus INT NOT NULL
);
