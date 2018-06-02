/*** Problem 1. Create Database Minions ***/
CREATE DATABASE Minions

USE Minions

/*** Problem 2. Create Tables Minions and Towns ***/
CREATE TABLE Minions (
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Age INT
)

CREATE TABLE Towns (
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

/*** Problem 3.	Alter Minions Table ***/
ALTER TABLE Minions
	ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

/*** Problem 4.	Insert Records in Both Tables ***/
INSERT INTO Towns (Id, [Name])
VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

INSERT INTO Minions (Id, [Name], Age, TownId)
VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

/*** Problem 5.	Truncate Table Minions ***/
TRUNCATE TABLE Minions

/*** Problem 6.	Drop All Tables ***/
DROP TABLE Minions

DROP TABLE Towns

/*** Problem 7.	Create Table People ***/
CREATE TABLE People(
	Id INT UNIQUE IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(3,2),
	[Weight] DECIMAL(5,2),
	Gender CHAR(1) CHECK(Gender = 'm' OR Gender = 'f') NOT NULL,
	Birthdate DATETIME2 NOT NULL,
	Biography NVARCHAR(MAX)
)

ALTER TABLE People
	ADD PRIMARY KEY (Id)

INSERT INTO People([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES
('Ivan', 322331, 1.67, 80.32, 'm', '01-01-2000', 'Ivan''s biography'),
('Petar', 34534534, 1.68, 82.32, 'm', '01-02-2000', 'Petar''s biography'),
('Georgi', 57456456, 1.69, 83.32, 'm', '01-03-2000', 'Georgi''s biography'),
('Gosho', 67867867, 1.70, 84.32, 'm', '01-04-2000', 'Gosho''s biography'),
('Pesho', 890890890, 1.71, 85.32, 'm', '01-05-2000', 'Pesho''s biography')

/*** Problem 8.	Create Table Users ***/
CREATE TABLE Users(
	Id BIGINT UNIQUE IDENTITY,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	LastLoginTime DATETIME2,
	IsDeleted BIT
)

ALTER TABLE Users
	ADD PRIMARY KEY(Id)

ALTER TABLE Users
	ADD CONSTRAINT CHK_ProfilePictureSize CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024)

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
VALUES
('Ivan', HASHBYTES('SHA1', '1213'), 123216, CONVERT(datetime, '25-01-2000', 103), 0),
('Gosho', '12345', 123215, '01-25-2000', 0),
('Pesho', '12356', 123214, '01-03-2000', 0),
('Petar', '12367', 123218, '01-04-2000', 0),
('Georgi', '12378', 123219, '01-05-2000', 0)

/*** Problem 9.	Change Primary Key ***/
ALTER TABLE Users
	DROP CONSTRAINT PK__Users__3214EC075A5691CF

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Id, Username)

/*** Problem 10.	Add Check Constraint ***/
ALTER TABLE Users
	ADD CONSTRAINT CHK_PasswordLength CHECK(DATALENGTH([Password]) >= 5)

/*** Problem 11.	Set Default Value of a Field ***/
ALTER TABLE Users
	ADD CONSTRAINT DF_LastLoginTime DEFAULT(GETDATE()) FOR LastLoginTime

/*** Problem 12.	Set Unique Field ***/
ALTER TABLE Users
	DROP CONSTRAINT PK_Users

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Id)

ALTER TABLE Users
	ADD CONSTRAINT UQ_Users UNIQUE(Username)

ALTER TABLE Users
	ADD CONSTRAINT CHK_UsernameLength CHECK(DATALENGTH(Username) >= 3)
