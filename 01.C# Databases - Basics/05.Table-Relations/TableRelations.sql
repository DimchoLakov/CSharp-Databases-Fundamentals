CREATE DATABASE TableRelations

USE TableRelations

/*** Problem 1.	One-To-One Relationship ***/
CREATE TABLE Persons
(
	PersonID INT IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(32) NOT NULL,
	Salary DECIMAL(15,2) NOT NULL
)

CREATE TABLE Passports
(
	PassportID INT IDENTITY(101, 1) NOT NULL,
	PassportNumber VARCHAR(32) NOT NULL
)

ALTER TABLE Persons
	ADD PRIMARY KEY(PersonID)

ALTER TABLE Passports
	ADD PRIMARY KEY(PassportID)

ALTER TABLE Persons
	ADD PassportID INT NOT NULL FOREIGN KEY REFERENCES Passports(PassportID)

INSERT INTO Passports(PassportNumber)
VALUES
('N34FG21B'),
('K65LO4R7'),
('ZE657QP2')

INSERT INTO Persons(FirstName, Salary, PassportID)
VALUES
('Roberto', 43300, 102),
('Tom', 56100, 103),
('Yana', 60200, 101)

/*** Problem 2.	One-To-Many Relationship ***/
CREATE TABLE Manufacturers
(
	ManufacturerID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(32) NOT NULL,
	EstablishedOn DATE NOT NULL
)

ALTER TABLE Manufacturers
	ADD PRIMARY KEY(ManufacturerID)

CREATE TABLE Models
(
	ModelID INT IDENTITY(101, 1) NOT NULL,
	[Name] NVARCHAR(32) NOT NULL,
	ManufacturerID INT NOT NULL
)

ALTER TABLE Models
	ADD PRIMARY KEY(ModelID)

ALTER TABLE Models
	ADD FOREIGN KEY(ManufacturerID) REFERENCES Manufacturers(ManufacturerID)

INSERT INTO Manufacturers([Name], EstablishedOn)
VALUES
('BMW', '07/03/1916'),
('Tesla', '01/01/2003'),
('Lada', '01/05/1966')

INSERT INTO Models([Name], ManufacturerID)
VALUES
('X1', 1),
('i6', 1),
('Model S', 2),
('Model X', 2),
('Model 3', 2),
('Nova', 3)

--SELECT Ma.[Name], COUNT(Mo.[Name]) AS [Models Count]
--FROM Manufacturers AS Ma
--JOIN Models AS Mo
--	ON Ma.ManufacturerID = Mo.ManufacturerID
--GROUP BY Ma.[Name]
--ORDER BY [Models Count] DESC

/*** Problem 3.	Many-To-Many Relationship ***/
CREATE TABLE Students
(
	StudentID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(32) NOT NULL
)

CREATE TABLE Exams
(
	ExamID INT IDENTITY(101, 1) NOT NULL,
	[Name] NVARCHAR(32) NOT NULL
)

ALTER TABLE Students
	ADD PRIMARY KEY(StudentID)

ALTER TABLE Exams
	ADD PRIMARY KEY(ExamID)

CREATE TABLE StudentsExams
(
	StudentID INT NOT NULL,
	ExamID INT NOT NULL
)

ALTER TABLE StudentsExams
	ADD PRIMARY KEY(StudentID, ExamID),
	CONSTRAINT FK_StudentsExams_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
	CONSTRAINT FK_StudentsExams_Exams FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
		
INSERT INTO Students([Name])
VALUES
('Mila'),
('Toni'),
('Ron')

INSERT INTO Exams([Name])
VALUES
('SrpingMVC'),
('Neo4j'),
('Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
VALUES
(1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)

/*** Problem 4.	Self-Referencing ***/
CREATE TABLE Teachers
(
	TeacherID INT IDENTITY(101, 1) NOT NULL
	CONSTRAINT PK_TeacherID PRIMARY KEY,

	[Name] NVARCHAR(32) NOT NULL,

	ManagerID INT
	CONSTRAINT FK_ManagerID_TeacherID FOREIGN KEY REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers([Name], ManagerID)
VALUES
('John', NULL),
('Maya', 106),
('Silvia', 106),
('Ted', 105),
('Mark', 101),
('Greta', 101)

/*** Problem 5.	Online Store Database ***/
CREATE DATABASE OnlineStore

USE OnlineStore

CREATE TABLE Cities
(
	CityID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,

	CONSTRAINT PK_Cities PRIMARY KEY(CityID)
)

CREATE TABLE Customers
(
	CustomerID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Birthday DATE,
	CityID INT NOT NULL,

	CONSTRAINT PK_Customers PRIMARY KEY(CustomerID),
	CONSTRAINT FK_Customers_Cities FOREIGN KEY(CityID) REFERENCES Cities(CityID)
)

CREATE TABLE Orders
(
	OrderID INT IDENTITY NOT NULL,
	CustomerID INT NOT NULL,

	CONSTRAINT PK_Orders PRIMARY KEY(OrderID),
	CONSTRAINT FK_Orders_Customers FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes
(
	ItemTypeID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,

	CONSTRAINT PK_ItemTypes PRIMARY KEY(ItemTypeID)
)

CREATE TABLE Items
(
	ItemID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	ItemTypeID INT NOT NULL,

	CONSTRAINT PK_Items PRIMARY KEY(ItemID),
	CONSTRAINT FK_Items_ItemTypes FOREIGN KEY(ItemTypeID) REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems
(
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,

	CONSTRAINT PK_OrderItems PRIMARY KEY(OrderID, ItemID),
	CONSTRAINT FK_OrderItems_Orders FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_OrderItems_Items FOREIGN KEY(ItemID) REFERENCES Items(ItemID)
)

/*** Problem 6.	University Database ***/
CREATE DATABASE University

USE University

CREATE TABLE Majors
(
	MajorID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,

	CONSTRAINT PK_Majors PRIMARY KEY(MajorID)
)

CREATE TABLE Students
(
	StudentID INT IDENTITY NOT NULL,
	StudentNumber INT NOT NULL,
	StudentName NVARCHAR(50) NOT NULL,
	MajorID INT NOT NULL,

	CONSTRAINT PK_Students PRIMARY KEY(StudentID),
	CONSTRAINT FK_Students_Majors FOREIGN KEY(MajorID) REFERENCES Majors(MajorID)
)

CREATE TABLE Payments
(
	PaymentID INT IDENTITY NOT NULL,
	PaymentDate DATETIME2 NOT NULL,
	PaymentAmount DECIMAL(15,2) NOT NULL,
	StudentID INT NOT NULL,

	CONSTRAINT PK_Payments PRIMARY KEY(PaymentID),
	CONSTRAINT FK_Payments_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID)
)

CREATE TABLE Subjects
(
	SubjectID INT IDENTITY NOT NULL,
	SubjectName NVARCHAR(50) NOT NULL,

	CONSTRAINT PK_Subjects PRIMARY KEY(SubjectID)
)

CREATE TABLE Agenda
(
	StudentID INT NOT NULL,
	SubjectID INT NOT NULL,

	CONSTRAINT PK_Agenda PRIMARY KEY(StudentID, SubjectID),
	CONSTRAINT FK_Agenda_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
	CONSTRAINT FK_Agenda_Subjects FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
)

/*** Problem 9.	*Peaks in Rila ***/
USE [Geography]

SELECT m.MountainRange, p.PeakName, p.Elevation
FROM Peaks AS p
JOIN Mountains AS m
	ON p.MountainId = m.Id
WHERE m.MountainRange = 'Rila'
ORDER BY p.Elevation DESC