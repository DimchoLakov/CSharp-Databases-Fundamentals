/*** Problem 1.	Find Names of All Employees by First Name ***/
USE SoftUni

SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'SA%'

/*** Problem 2. Find Names of All employees by Last Name ***/
SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%ei%'

/*** Problem 3.	Find First Names of All Employees  ***/
SELECT FirstName
FROM Employees
WHERE DepartmentID IN(3,10)
AND DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005

/*** Problem 4.	Find All Employees Except Engineers ***/
SELECT FirstName, LastName
FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'

/*** Problem 5.	Find Towns with Name Length ***/
SELECT [Name]
FROM Towns
WHERE LEN([Name]) BETWEEN 5 AND 6
ORDER BY [Name] ASC

/*** Problem 6. Find Towns Starting With ***/
SELECT *
FROM Towns
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name] ASC

/*** Problem 7.	Find Towns Not Starting With ***/
SELECT *
FROM Towns
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name] ASC

/*** Problem 8.	Create View Employees Hired After 2000 Year ***/
GO
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
FROM Employees
WHERE DATEPART(YEAR, HireDate) > 2000

/*** Problem 9.	Length of Last Name ***/
GO
SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5

/*** Problem 10. Countries Holding ‘A’ 3 or More Times ***/
USE [Geography]

SELECT CountryName, IsoCode
FROM Countries
WHERE CountryName LIKE '%A%A%A%'
ORDER BY IsoCode ASC

/*** Problem 11. Mix of Peak and River Names ***/
SELECT Peaks.PeakName, Rivers.RiverName, LOWER(CONCAT(LEFT(PeakName, LEN(PeakName) - 1), RiverName)) AS Mix
FROM Peaks
	JOIN Rivers
		ON RIGHT(Peaks.PeakName, 1) = LEFT(Rivers.RiverName, 1)
ORDER BY Mix ASC

/*** Problem 12. Games from 2011 and 2012 year ***/
USE Diablo

SELECT TOP (50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start]
FROM Games
WHERE DATEPART(YEAR, [Start]) BETWEEN 2011 AND 2012
ORDER BY [Start], [Name]

/*** Problem 13. User Email Providers ***/
SELECT Username, RIGHT(Email, LEN(Email) - CHARINDEX('@', Email)) AS [EmailProvider]
FROM Users
ORDER BY EmailProvider ASC, Username ASC

/*** Problem 14. Get Users with IPAdress Like Pattern ***/
SELECT Username, IpAddress
FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username ASC

/*** Problem 15. Show All Games with Duration and Part of the Day ***/
USE Diablo

SELECT
	[Name] AS [Game],

	(
		CASE
			WHEN DATEPART(HOUR, [Start]) BETWEEN 0 AND 11 THEN 'Morning'
			WHEN DATEPART(HOUR, [Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
			WHEN DATEPART(HOUR, [Start]) BETWEEN 18 AND 23 THEN 'Evening'
		END 
	) 
	AS [Part of the Day],
	 
	(
		CASE
			WHEN Duration <= 3 THEN 'Extra Short'
			WHEN Duration BETWEEN 4 AND 6 THEN  'Short'
			WHEN Duration > 6 THEN 'Long'
			ELSE 'Extra Long'
		END
	)
	AS [Duration]
FROM Games
ORDER BY Game ASC, [Duration] ASC, [Part of the Day] ASC

/*** Problem 16. Orders Table ***/
USE Orders

SELECT ProductName, OrderDate, DATEADD(DAY, 3, OrderDate) AS [Pay Due], DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders

/*** Problem 17. People Table ***/
CREATE TABLE People
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	Birthdate DATETIME2 NOT NULL
)

INSERT INTO People([Name], Birthdate)
VALUES
('Gosho', '2000-12-07 00:00:00.000'),
('Pesho', '1992-09-10 00:00:00.000'),
('Sasho', '1910-09-19 00:00:00.000'),
('Stamat', '2008-11-15 00:00:00.000'),
('Prakash', '2010-01-06 00:00:00.000')

SELECT 
[Name],
DATEDIFF(YEAR, Birthdate, GETDATE()) AS [Age in Years],
DATEDIFF(MONTH, Birthdate, GETDATE()) AS [Age in Months],
DATEDIFF(DAY, Birthdate, GETDATE()) AS [Age in Days],
DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Minutes]
FROM People