/*** Section I. Functions and Procedures
	 Part 1. Queries for SoftUni Database
	 Problem 1. Employees with Salary Above 35000 ***/

CREATE OR ALTER PROCEDURE usp_GetEmployeesSalaryAbove35000
	AS
BEGIN
		SELECT e.FirstName AS [First Name],
			   e.LastName AS [Last Name]
		FROM Employees AS e
		WHERE e.Salary >= 35000
END

GO

EXECUTE usp_GetEmployeesSalaryAbove35000

/*** Problem 2. Employees with Salary Above Number ***/
GO

CREATE OR ALTER PROCEDURE usp_GetEmployeesSalaryAboveNumber(@salaryInput DECIMAL(18, 4))
AS
BEGIN
	SELECT e.FirstName AS [First Name],
		   e.LastName AS [Last Name]
	FROM Employees AS e
	WHERE e.Salary >= @salaryInput
END

EXECUTE usp_GetEmployeesSalaryAboveNumber 48100

/*** Problem 3. Town Names Starting With ***/
GO

CREATE OR ALTER PROCEDURE usp_GetTownsStartingWith(@input NVARCHAR(20))
AS
BEGIN
	SELECT t.[Name]
	FROM Towns AS t
	WHERE SUBSTRING(t.[Name], 1, LEN(@input)) = @input
END

EXECUTE usp_GetTownsStartingWith 'b'

/*** Problem 4. Employees from Town ***/
GO

CREATE OR ALTER PROC usp_GetEmployeesFromTown(@townName NVARCHAR(32))
AS
BEGIN
	SELECT e.FirstName AS [First Name],
		   e.LastName AS [Last Name]
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.[Name] = @townName
END

EXECUTE usp_GetEmployeesFromTown 'Sofia'

/*** Problem 5. Salary Level Function ***/
GO

CREATE FUNCTION ufn_GetSalaryLevel(@salaryInput DECIMAL(18,4))
RETURNS NVARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel NVARCHAR(7);
	IF(@salaryInput < 30000)
	BEGIN
		SET @salaryLevel = 'Low'
	END
	ELSE IF(@salaryInput <= 50000)
	BEGIN
		SET @salaryLevel = 'Average'
	END
	ELSE
		SET @salaryLevel = 'High'

	RETURN @salaryLevel
END

SELECT e.Salary,
	   [dbo].ufn_GetSalaryLevel(e.Salary) AS [Salary Level]
FROM Employees AS e

--SECOND VARIANT 
GO

CREATE FUNCTION ufn_GetSalaryLevel(@salaryInput DECIMAL(18,4))
RETURNS NVARCHAR(7)
AS
BEGIN
	RETURN
	CASE 
		WHEN @salaryInput < 30000 THEN 'Low'
		WHEN @salaryInput <= 50000 THEN 'Average'
		ELSE 'High'
	END
END

/*** Problem 6. Employees by Salary Level ***/
GO

CREATE PROCEDURE usp_EmployeesBySalaryLevel(@salaryLevel NVARCHAR(32))
AS
BEGIN
	SELECT e.FirstName AS [First Name],
		   e.LastName AS [Last Name]
	FROM Employees AS e
	WHERE [dbo].ufn_GetSalaryLevel(e.Salary) = @salaryLevel
END

EXEC [dbo].usp_EmployeesBySalaryLevel 'High'

/*** Problem 7. Define Function ***/
GO

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(32), @word NVARCHAR(32))
RETURNS BIT
AS
BEGIN
	DECLARE @currentIndex INT = 1;
	WHILE(@currentIndex <= LEN(@word))
		BEGIN
			DECLARE @currentLetter CHAR = SUBSTRING(@word, @currentIndex, 1);
			IF(CHARINDEX(@currentLetter, @setOfLetters) <= 0)
			BEGIN
				RETURN 0;
			END
			
			SET @currentIndex += 1
		END
	RETURN 1;
END

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia') AS [Result]

/*** Problem 8. * Delete Employees and Departments ***/

/*** Problem 9. Find Full Name***/
USE Bank

CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT CONCAT(ah.FirstName, ' ', ah.LastName) AS [Full Name]
	FROM AccountHolders AS ah
END

EXECUTE usp_GetHoldersFullName

/*** Problem 10. People with Balance Higher Than ***/
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan(@inputValue DECIMAL(15, 2))
AS
BEGIN
	SELECT ah.FirstName AS [First Name],
		   ah.LastName AS [Last Name]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	GROUP BY ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @inputValue
END

EXEC usp_GetHoldersWithBalanceHigherThan 10000

/*** Problem 11. Future Value Function ***/
GO

CREATE FUNCTION ufn_CalculateFutureValue(@sum DECIMAL(15, 2), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS NVARCHAR(30)
AS
BEGIN
	DECLARE @futureValue DECIMAL(15, 4) =
	@sum * (POWER(1 + @yearlyInterestRate, @numberOfYears));

	RETURN STR(@futureValue, 12, 4)
END

DROP FUNCTION [dbo].ufn_CalculateFutureValue

SELECT [dbo].ufn_CalculateFutureValue(1000, 0.1, 5) AS [Future Value]

/*** Problem 12. Calculating Interest ***/

