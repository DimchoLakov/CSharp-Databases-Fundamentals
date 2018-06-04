/*** Problem 1. Create Database Minions ***/
CREATE DATABASE Minions

--USE Minions

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

/*** Problem 10. Add Check Constraint ***/
ALTER TABLE Users
	ADD CONSTRAINT CHK_PasswordLength CHECK(DATALENGTH([Password]) >= 5)

/*** Problem 11. Set Default Value of a Field ***/
ALTER TABLE Users
	ADD CONSTRAINT DF_LastLoginTime DEFAULT(GETDATE()) FOR LastLoginTime

/*** Problem 12. Set Unique Field ***/
ALTER TABLE Users
	DROP CONSTRAINT PK_Users

ALTER TABLE Users
	ADD CONSTRAINT PK_Users PRIMARY KEY (Id)

ALTER TABLE Users
	ADD CONSTRAINT UQ_Users UNIQUE(Username)

ALTER TABLE Users
	ADD CONSTRAINT CHK_UsernameLength CHECK(DATALENGTH(Username) >= 3)

/*** Problem 13. Movies Database ***/
CREATE DATABASE Movies

--USE Movies

CREATE TABLE Directors
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	DirectorName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	GenreName NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Title NVARCHAR(35) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyRightYear DATETIME2 NOT NULL,
	[Length] INT NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(3,1),
	Notes NVARCHAR(MAX)
)

INSERT INTO Genres(GenreName)
VALUES
('Action,'),
('Comedy'),
('Sci-Fi'),
('Adventure'),
('Fantasy')

INSERT INTO Directors(DirectorName)
VALUES
('Anthony Hopkins'),
('Jerry Bruckheimer'),
('Batman'),
('Kiro Kirov'),
('Bai Shile')

INSERT INTO Categories(CategoryName)
VALUES
('First Class'),
('Second Class'),
('Third Class'),
('Fourth Class'),
('Fifth Class')

INSERT INTO Movies(Title, DirectorId, CopyRightYear, [Length], GenreId, CategoryId)
VALUES
('The Flash', 5, CONVERT(datetime2, '25-10-2014', 103), 0.4, 5, 1),
('Prison Break', 4, CONVERT(datetime2, '25-10-2005', 103), 1.2, 1, 1),
('The Vampire Diaries', 2, CONVERT(datetime2, '25-10-2011', 103), 0.45, 1, 2),
('Once Upon a Time', 3, CONVERT(datetime2, '25-10-2015', 103), 0.46, 5, 3),
('Legends of Tomorrow', 1, CONVERT(datetime2, '25-10-2016', 103), 0.48, 5, 4)

/*** Problem 14. Car Rental Database ***/
CREATE DATABASE CarRental

--USE CarRental

CREATE TABLE Categories
(
	Id INT IDENTITY NOT NULL,
	CategoryName NVARCHAR(30) NOT NULL,
	DailyRate DECIMAL (8,2) NOT NULL,
	WeeklyRate DECIMAL (8,2) NOT NULL,
	MonthlyRate DECIMAL (8,2) NOT NULL,
	WeekendRate DECIMAL (8,2)
)

ALTER TABLE Categories
	ADD PRIMARY KEY (Id)
	
INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate)
VALUES
('AllTrack', 2.5, 10, 4),
('OffRoad', 3.5, 11, 5),
('Commercial', 4.5, 12, 6)

CREATE TABLE Cars
(
	Id INT IDENTITY NOT NULL,
	PlateNumber NVARCHAR(10) NOT NULL,
	Manufacturer NVARCHAR(20) NOT NULL,
	Model NVARCHAR(15) NOT NULL,
	CarYear DATE NOT NULL,
	CategoryId INT NOT NULL,
	Doors INT NOT NULL,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(100) NOT NULL,
	Available BIT DEFAULT(1) NOT NULL
)

ALTER TABLE Cars
	ADD PRIMARY KEY (Id)

ALTER TABLE Cars
	ADD CONSTRAINT CHK_PictureSize CHECK(DATALENGTH(Picture) <= 1024 * 1024 * 5)

ALTER TABLE Cars
	ADD FOREIGN KEY (CategoryId) REFERENCES Categories(Id)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES
('AB1001', 'Merc', '320', CONVERT(date, '25-01-2000', 103), 1, 4, 7812421, 'New', 1),
('CD1001', 'Au', 'A4', CONVERT(date, '25-02-2000', 103), 2, 5, 7346, 'As New', 0),
('EF1001', 'BM', '516i', CONVERT(date, '25-03-2000', 103), 3, 7, 7812421, 'New', 1)

CREATE TABLE Employees
(
	Id INT IDENTITY NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Title NVARCHAR(10) NOT NULL,
	Notes NVARCHAR(MAX)
)

ALTER TABLE Employees
	ADD PRIMARY KEY (Id)

INSERT INTO Employees(FirstName, LastName, Title)
VALUES
('Jack', 'Sparrow', 'Mr'),
('Dominic', 'Torreto', 'Mr'),
('Brian', 'O''Connor', 'Mr')

CREATE TABLE Customers
(
	Id INT IDENTITY NOT NULL,
	DriverLicenseNumber NVARCHAR(25) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	City NVARCHAR(20) NOT NULL,
	ZIPCode NVARCHAR(10) NOT NULL,
	Notes NVARCHAR(MAX)
)

ALTER TABLE Customers
	ADD PRIMARY KEY (Id)

INSERT INTO Customers(DriverLicenseNumber, FullName, [Address], City, ZIPCode)
VALUES
('A1200', 'H.R.', 'Dura Street', 'Dundee', 'DD1'),
('B1300', 'T.V.', 'Eliza Street', 'Glasgow', 'GL1'),
('C1400', 'J.K.', 'Bell Street', 'Aberdeen', 'AB1')

CREATE TABLE RentalOrders
(
	Id INT IDENTITY NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel DECIMAL(5,2) NOT NULL,	
	KilometrageStart DECIMAL(8,2) NOT NULL,
	KilometrageEnd DECIMAL(8,2) NOT NULL,
	TotalKilometrage AS KilometrageEnd - KilometrageStart,
	StartDate DATETIME2 NOT NULL,
	EndDate DATETIME2 NOT NULL,
	--TotalDays INT,
	TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
	RateApplied DECIMAL(8,2) NOT NULL,
	TaxRate DECIMAL(8,2) NOT NULL,
	OrderStatus BIT DEFAULT(0) NOT NULL,
	Notes NVARCHAR(MAX)
)

ALTER TABLE RentalOrders
	ADD PRIMARY KEY (Id)

--ALTER TABLE RentalOrders
--	ADD CONSTRAINT CHK_TotalDays CHECK(DATEDIFF(DAY, StartDate, EndDate) = TotalDays)

INSERT INTO RentalOrders
(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate, RateApplied, TaxRate, OrderStatus)
VALUES
(1, 1, 1, 20, 0, 100, CONVERT(datetime2, '20-07-2002', 103), CONVERT(datetime2, '20-10-2002', 103), 10, 20, 1),
(2, 2, 2, 30, 50, 200, CONVERT(datetime2, '10-08-2002', 103), CONVERT(datetime2, '10-12-2002', 103), 12, 22, 1),
(3, 3, 3, 40, 80, 300, CONVERT(datetime2, '15-03-2002', 103), CONVERT(datetime2, '15-04-2002', 103), 14, 24, 0)

/*** Problem 15. Hotel Database ***/
CREATE DATABASE Hotel

--USE Hotel

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(15) NOT NULL,
	LastName NVARCHAR(15) NOT NULL,
	Title NVARCHAR(10) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees(FirstName, LastName, Title)
VALUES
('Jack', 'Sparrow', 'Captain'),
('Harry', 'Potter', 'Magician'),
('Barry', 'Allen', 'Speedster')

CREATE TABLE Customers
(
	AccountNumber INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	PhoneNumber INT NOT NULL,
	EmergencyName NVARCHAR(40) NOT NULL,
	EmergencyNumber INT NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Customers(AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber)
VALUES
(1001, 'Drow', 'Ranger', 0885, 'Traxex', 08855),
(1002, 'Jugger', 'Naut', 0886, 'Yurnero', 08856),
(1003, 'Ursa', 'Bear', 0887, 'Ulf ''Saar', 08857)

CREATE TABLE RoomStatus
(
	RoomStatus NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO RoomStatus(RoomStatus)
VALUES
('Dirty'),
('Almost Clean'),
('Spotless')

CREATE TABLE RoomTypes
(
	RoomType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO RoomTypes(RoomType)
VALUES
('Small'),
('Standard'),
('Big')

CREATE TABLE BedTypes
(
	BedType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO BedTypes(BedType)
VALUES
('Single'),
('Double'),
('King Size')

CREATE TABLE Rooms
(
	RoomNumber INT PRIMARY KEY IDENTITY NOT NULL,
	RoomType NVARCHAR(50) NOT NULL,
	BedType NVARCHAR(50) NOT NULL,
	Rate DECIMAL(2,1) NOT NULL,
	RoomStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Rooms(RoomType, BedType, Rate, RoomStatus)
VALUES
('Small', 'Single', 5, 'Dirty'),
('Medium', 'Double', 6, 'Almost Clean'),
('Big', 'King Size', 7, 'Spotless')

CREATE TABLE Payments
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	EmployeeId INT NOT NULL,
	PaymentDate DATE NOT NULL,
	AccountNumber INT NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL(10, 2) NOT NULL,
	TaxRate DECIMAL(10, 2) NOT NULL,
	TaxAmount DECIMAL(10, 2) /*NOT NULL,*/,
	PaymentTotal DECIMAL(10, 2) NOT NULL,
	Notes NVARCHAR(MAX)
)

ALTER TABLE Payments
	ADD CONSTRAINT CHK_TotalDays CHECK(DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied) = TotalDays)

ALTER TABLE Payments
	ADD CONSTRAINT CHK_TaxAmount CHECK(TaxAmount = TotalDays * TaxRate)
	
INSERT INTO Payments
(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, PaymentTotal)
VALUES
(1, CONVERT(datetime2, '30-04-2010', 103), 1, CONVERT(datetime2, '20-04-2010', 103), CONVERT(datetime2, '25-04-2010', 103), 5, 20, 10, 200),
(2, CONVERT(datetime2, '30-04-2011', 103), 2, CONVERT(datetime2, '15-04-2011', 103), CONVERT(datetime2, '20-04-2011', 103), 5, 30, 20, 300),
(3, CONVERT(datetime2, '30-04-2012', 103), 3, CONVERT(datetime2, '10-04-2012', 103), CONVERT(datetime2, '15-04-2012', 103), 5, 40, 30, 500)

CREATE TABLE Occupancies
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	EmployeeId INT NOT NULL,
	DateOccupied DATETIME2 NOT NULL,
	AccountNumber INT NOT NULL,
	RoomNumber INT NOT NULL,
	RateApplied DECIMAL(15,2),
	PhoneCharge DECIMAL(15,3),
	Notes NVARCHAR(50)
)

INSERT INTO Occupancies(EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge)
VALUES
(1, CONVERT(datetime2, '20-07-2014', 103) , 1, 2, 200, 20),
(2, CONVERT(datetime2, '20-08-2014', 103) , 2, 3, 300, 30),
(3, CONVERT(datetime2, '20-09-2014', 103) , 3, 4, 400, 40)

/*** Problem 16. Create SoftUni Database ***/
CREATE DATABASE SoftUni

--USE SoftUni

CREATE TABLE Towns
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Addresses
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	AddressText NVARCHAR(100) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	JobTitle NVARCHAR(35) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATETIME2 NOT NULL,
	Salary DECIMAL(15,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL
)

/*** Problem 17. Backup Database ***/
BACKUP DATABASE SoftUni
TO DISK = 'D:\softuni-backup.bak'

DROP DATABASE SoftUni

RESTORE DATABASE SoftUni
FROM DISK = 'D:\softuni-backup.bak'

USE SoftUni

/*** Problem 18. Basic Insert ***/
INSERT INTO Towns([Name])
VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas'),
('Pleven')

INSERT INTO Addresses(AddressText, TownId)
VALUES
('First Street', 1),
('Second Street', 2),
('Third Street', 3),
('Fourth Street', 4),
('Fifth Street', 5)

INSERT INTO Departments([Name])
VALUES
('Software Development'),
('Engineering'),
('Quality Assurance'),
('Sales'),
('Marketing')

INSERT INTO Employees(FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)
VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 1, CONVERT(datetime2, '01/02/2013', 103), 3500, 1),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 2, CONVERT(datetime2, '02/03/2004', 103), 4000, 2),
('Maria', 'Petrova', 'Ivanova', 'Intern', 3, CONVERT(datetime2, '28/08/2016', 103), 525.25, 3),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 4, CONVERT(datetime2, '09/12/2007', 103), 3000, 4),
('Peter', 'Pan', 'Pan', 'Intern', 5, CONVERT(datetime2, '28/08/2016', 103), 599.88, 5)

/*** Problem 19. Basic Select All Fields ***/
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

/*** Problem 20. Basic Select All Fields and Order Them ***/
SELECT * FROM Towns
ORDER BY [Name]

SELECT * FROM Departments
ORDER BY [Name]

SELECT * FROM Employees
ORDER BY Salary DESC

/*** Problem 21. Basic Select Some Fields ***/
SELECT [Name] FROM Towns
ORDER BY [Name]

SELECT [Name] FROM Departments
ORDER BY [Name]

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC

/*** 22. Increase Employees Salary ***/
UPDATE Employees
	SET Salary *= 1.1

SELECT Salary FROM Employees

/*** 23. Decrease Tax Rate ***/
--USE Hotel

UPDATE Payments
	SET TaxRate -= TaxRate * 0.03

SELECT TaxRate FROM Payments

/*** 24. Delete All Records ***/
TRUNCATE TABLE Occupancies