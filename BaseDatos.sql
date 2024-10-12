CREATE DATABASE BankingSystem;
GO

USE BankingSystem;
GO

CREATE TABLE Person (
    Per_Id INT PRIMARY KEY IDENTITY(1,1),
    Per_Name NVARCHAR(100) NOT NULL,
    Per_Gender VARCHAR(10) NOT NULL,
    Per_Age SMALLINT NOT NULL,
    Per_IdentityNumber VARCHAR(50) NOT NULL UNIQUE,
    Per_Address VARCHAR(200),
    Per_PhoneNumber VARCHAR(20)
);

CREATE TABLE Customer (
    Cus_Id INT PRIMARY KEY IDENTITY(1,1), 
    Cus_Password VARCHAR(100) NOT NULL,
    Cus_State SMALLINT NOT NULL DEFAULT 1,
	Cus_PersonId INT,             
    CONSTRAINT FK_Customer_Person FOREIGN KEY (Cus_PersonId) REFERENCES Person(Per_Id)
);

CREATE TABLE Account (
	Acc_Number VARCHAR(100) PRIMARY KEY,
	Acc_CustomerIdentity VARCHAR(50) NOT NULL,
	Acc_Type SMALLINT NOT NULL,
	Acc_Balance MONEY NOT NULL DEFAULT 0,
	Acc_State SMALLINT NOT NULL DEFAULT 1
);

CREATE TABLE Movements (
	Mov_Id INT PRIMARY KEY IDENTITY(1,1),
	Mov_AccountNumber VARCHAR(100) NOT NULL,
	Mov_Date DATETIME NOT NULL,
	Mov_Type SMALLINT NOT NULL,
	Mov_Amount MONEY NOT NULL,
	Mov_Balance MONEY NOT NULL,
	CONSTRAINT FK_Account_Movements FOREIGN KEY (Mov_AccountNumber) REFERENCES Account(Acc_Number)
);

CREATE INDEX IX_Customer_PersonId ON Customer(Cus_PersonId);
CREATE INDEX IX_Movements_AccountNumber ON Movements(Mov_AccountNumber);

