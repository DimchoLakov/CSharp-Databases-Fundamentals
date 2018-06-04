/*** 2.	Find All Information About Departments ***/
SELECT * 
FROM Departments

/*** 3.	Find all Department Names ***/
SELECT [Name] 
FROM Departments

/*** 4. Find Salary of Each Employee ***/
SELECT FirstName, LastName, Salary 
FROM Employees

/*** 5.	Find Full Name of Each Employee ***/
SELECT Firstname, MiddleName, LastName
FROM Employees

/*** 6.	Find Email Address of Each Employee ***/
SELECT FirstName + '.' + LastName + '@softuni.bg' AS [Full Email Address]
FROM Employees

/*** 7.	Find All Different Employee’s Salaries ***/
SELECT DISTINCT Salary
FROM Employees

/*** 8.	Find all Information About Employees ***/
SELECT *
FROM Employees
WHERE JobTitle = 'Sales Representative'

/*** 9.	Find Names of All Employees by Salary in Range ***/
SELECT FirstName, LastName, JobTitle
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000

/*** 10. Find Names of All Employees ***/
SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name]
FROM Employees
WHERE Salary IN (25000, 14000, 12500, 23600)
--WHERE Salary = 25000 OR Salary = 14000 OR Salary = 12500 OR Salary = 23600

/*** 11. Find All Employees Without Manager ***/
SELECT FirstName, LastName
FROM Employees
WHERE ManagerID IS NULL

/*** 12. Find All Employees with Salary More Than 50000 ***/
SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC

/*** 13. Find 5 Best Paid Employees ***/
SELECT TOP (5) FirstName, LastName
FROM Employees
ORDER BY Salary DESC

/*** 14. Find All Employees Except Marketing ***/
SELECT FirstName, LastName
FROM Employees
WHERE DepartmentID != 4

/*** 15. Sort Employees Table ***/
SELECT *
FROM Employees
ORDER BY Salary DESC, FirstName ASC, LastName DESC, MiddleName ASC

/*** 16. Create View Employees with Salaries ***/
GO
CREATE VIEW V_EmployeesSalaries AS
SELECT FirstName, LastName, Salary
FROM Employees

/*** 17. Create View Employees with Job Titles ***/
GO
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name], JobTitle
FROM Employees

/*** 18. Distinct Job Titles ***/
GO
SELECT DISTINCT JobTitle
FROM Employees

/*** 19. Find First 10 Started Projects ***/
SELECT TOP (10) *
FROM Projects
WHERE StartDate IS NOT NULL
ORDER BY StartDate ASC, [Name] ASC

/*** 20. Last 7 Hired Employees ***/
SELECT TOP (7) FirstName, LastName, HireDate
FROM Employees
ORDER BY HireDate DESC

/*** 21. Increase Salaries ***/
BACKUP DATABASE SoftUni
TO DISK = 'D:\softuni-exercise-backup.bak'

DECLARE @engineeringDepId INT

SELECT TOP (1) @engineeringDepId = DepartmentID 
FROM Departments
WHERE [Name] = 'Engineering'

DECLARE @designToolDepId INT

SELECT TOP (1) @designToolDepId = DepartmentID
FROM Departments
WHERE [Name] = 'Tool Design'

DECLARE @marketingDepId INT

SELECT TOP (1) @marketingDepId = DepartmentID
FROM Departments
WHERE [Name] = 'Marketing'

DECLARE @infoServicesDepId INT

SELECT TOP (1) @infoServicesDepId = DepartmentID
FROM Departments
WHERE [Name] = 'Information Services'

UPDATE Employees
	SET Salary += Salary * 0.12
WHERE DepartmentID IN(@engineeringDepId, @designToolDepId, @marketingDepId, @infoServicesDepId)

SELECT Salary
FROM Employees

USE master
DROP DATABASE SoftUni

RESTORE DATABASE SoftUni
FROM DISK = 'D:\softuni-exercise-backup.bak'

/*** 22. All Mountain Peaks ***/
USE [Geography]

SELECT PeakName
FROM Peaks
ORDER BY PeakName ASC

/*** 23. Biggest Countries by Population ***/
SELECT TOP (30) CountryName, [Population]
FROM Countries
WHERE ContinentCode = 'EU'
ORDER BY [Population] DESC, CountryName ASC

/*** 24. *Countries and Currency (Euro / Not Euro) ***/
SELECT CountryName, CountryCode, Currency =
CASE
	WHEN CurrencyCode = 'EUR' THEN 'Euro'
	ELSE 'Not Euro'
END
FROM Countries
ORDER BY CountryName ASC

--SELECT CountryName, CountryCode,
--(
--	CASE
--		WHEN CurrencyCode = 'EUR' THEN 'Euro'
--		ELSE 'Not Euro'
--	END
--) 
--	AS [Currency]
--FROM Countries
--ORDER BY CountryName ASC

/*** 25. All Diablo Characters ***/
USE Diablo

SELECT [Name]
FROM Characters
ORDER BY [Name] ASC