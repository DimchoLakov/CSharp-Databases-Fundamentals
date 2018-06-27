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
GO

CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT CONCAT(ah.FirstName, ' ', ah.LastName) AS [Full Name]
	FROM AccountHolders AS ah
END

EXECUTE usp_GetHoldersFullName

/*** Problem 10. People with Balance Higher Than ***/
GO
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

SELECT [dbo].ufn_CalculateFutureValue(1000, 0.1, 5) AS [Future Value]

/*** Problem 12. Calculating Interest ***/
GO

CREATE PROC usp_CalculateFutureValueForAccount(@accountId INT, @interestRate DECIMAL(15, 2))
AS
BEGIN
	SELECT TOP (1) ah.Id AS [Account Id],
		   ah.FirstName AS [First Name],
		   ah.LastName AS [Last Name],
		   a.Balance AS [Current Balance],
		   [dbo].ufn_CalculateFutureValue(a.Balance, @interestRate, 5) AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	WHERE ah.Id = @accountId
END

EXEC usp_CalculateFutureValueForAccount 7, 0.1

/*** Part 3. Queries for Diablo Database 
	 Problem 13. *Scalar Function: Cash in User Games Odd Rows 
																***/
CREATE FUNCTION ufn_CashInUserGames(@gameName NVARCHAR(64))
RETURNS @CashInUserGames TABLE
(
	SumCash DECIMAL(15, 2) NOT NULL
)
AS
BEGIN
	
	RETURN
	
END

GO

CREATE FUNCTION ufn_CashInUsersGames(@gameName NVARCHAR(64))
RETURNS TABLE
AS
	RETURN(
			SELECT SUM(GCR.Cash) AS [SumCash]
			FROM 
			(
				SELECT g.Id,
					   ug.Cash,
					   ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS [Row Number]
				FROM Games AS g
				JOIN UsersGames AS ug ON ug.GameId = g.Id
				WHERE g.[Name] = @gameName
			) AS GCR
			WHERE GCR.[Row Number] % 2 = 1
		 )
GO

SELECT *
FROM [dbo].ufn_CashInUsersGames('Love in a mist')


/*** Section II. Triggers and Transactions 
	 Part 1. Queries for Bank Database
	 Problem 14. Create Table Logs
									***/

USE Bank

CREATE TABLE Logs
(
	LogId INT IDENTITY NOT NULL,
	AccountId INT NOT NULL,
	OldSum DECIMAL(15, 2) NOT NULL,
	NewSum DECIMAL(15, 2) NOT NULL
)

ALTER TABLE Logs
	ADD PRIMARY KEY (LogId),
	FOREIGN KEY (AccountId) REFERENCES Accounts(Id)

GO

CREATE TRIGGER tr_LogsUpdateTrigger ON [dbo].Accounts
FOR UPDATE
AS
BEGIN
	DECLARE @accountId INT = (
		SELECT Id FROM inserted);

	DECLARE @oldSum DECIMAL(15, 2) = (
		SELECT Balance FROM deleted);

	DECLARE @newSum DECIMAL(15, 2) = (
		SELECT Balance FROM inserted);

	INSERT INTO Logs(AccountId, OldSum, NewSum)
	VALUES
	(@accountId, @oldSum, @newSum)
END

SELECT *
FROM Logs

UPDATE Accounts
	SET Balance = 100
	WHERE Id = 1

/*** Problem 15. Create Table Emails ***/
CREATE TABLE NotificationEmails
(
	Id INT IDENTITY NOT NULL,
	Recipient INT NOT NULL,
	[Subject] NVARCHAR(64) NOT NULL,
	Body NVARCHAR(64) NOT NULL
)

ALTER TABLE NotificationEmails
	ADD PRIMARY KEY (Id),
		FOREIGN KEY (Recipient) REFERENCES Accounts(Id)

GO

CREATE TRIGGER tr_Emails_To_Logs ON [dbo].Logs
FOR INSERT
AS
BEGIN
	DECLARE @recipientId INT = (
		SELECT AccountId FROM inserted);

	DECLARE @subject NVARCHAR(64) = CONCAT('Balance change for account:', ' ', @recipientId);
	
	DECLARE @oldAmount DECIMAL(15, 2) = (
		SELECT OldSum FROM inserted);

	DECLARE @newAmount DECIMAL(15, 2) = (
		SELECT NewSum FROM inserted);

	DECLARE @body NVARCHAR(64) = CONCAT('On', ' ', GETDATE(), ' ' +  'your balance was changed from ', @oldAmount, ' to ', @newAmount);
	
	INSERT INTO NotificationEmails(Recipient, [Subject], Body)
	VALUES
	(@recipientId, @subject, @body)
END

UPDATE Accounts
	SET Balance = 5002
	WHERE Id = 1

SELECT *
FROM Logs

SELECT *
FROM NotificationEmails
/*** Problem 16. Deposit Money ***/
GO

CREATE PROCEDURE usp_DepositMoney (@accountId INT, @moneyAmount DECIMAL(20, 4))
AS
BEGIN
	IF(@moneyAmount > 0)
	BEGIN
		UPDATE Accounts
			SET Balance += @moneyAmount
			WHERE Id = @accountId
	END

	--IF(@@ROWCOUNT <> 1)
	--BEGIN
	--	RAISERROR('Invalid account', 16, 1);
	--	ROLLBACK;
	--	RETURN;
	--END
END

EXEC usp_DepositMoney 3, 12.43245656

SELECT *
FROM Accounts
WHERE Id = 3

/*** Problem 17. Withdraw Money ***/
GO

CREATE PROCEDURE usp_WithdrawMoney(@accountId INT, @moneyAccount DECIMAL(20, 4))
AS
BEGIN
	IF(@moneyAccount > 0)
	BEGIN
		UPDATE Accounts
			SET Balance -= @moneyAccount
			WHERE Id = @accountId		
	END
END

EXEC usp_WithdrawMoney 5, 25

SELECT *
FROM Accounts
WHERE Id = 5

/*** Problem 18. Money Transfer ***/
GO

CREATE PROCEDURE usp_TransferMoney(@senderId INT, @receiverId INT, @amount DECIMAL(20, 4))
AS
BEGIN
	EXEC usp_WithdrawMoney @senderId, @amount
	EXEC usp_DepositMoney @receiverId, @amount
END

EXEC usp_TransferMoney 5, 1, 5000

SELECT *
FROM Accounts
WHERE Id IN(5, 1)

/*** Part 2. Queries for Diablo Database
	 Problem 19. Trigger 
						 ***/
GO

CREATE TRIGGER tr_Users_Items_Restricting ON [dbo].UserGameItems
INSTEAD OF UPDATE
AS
BEGIN
	--1.Start
	DECLARE @userCurrentLevel INT = 
		(
		SELECT [Level]
		FROM UsersGames
		WHERE Id = (
			SELECT UserGameId
			FROM inserted
				   )
		)
		
	DECLARE @itemMinLevel INT =
		(
			SELECT MinLevel
			FROM Items
			WHERE Id = (
				SELECT ItemId
				FROM inserted
					   )
		)

		IF(@itemMinLevel > @userCurrentLevel)
		BEGIN
			RAISERROR('Your current level is too low!', 16, 1);
		END

	INSERT INTO UserGameItems(ItemId, UserGameId)
	VALUES
	(
	  (SELECT ItemId
	  FROM inserted),
	   
	  (SELECT UserGameId
	  FROM inserted)
	)
	--1.End

	--2.Start
	UPDATE ug
		SET ug.Cash += 50000
	FROM Games AS g
	JOIN UsersGames AS ug ON ug.GameId = g.Id
	JOIN Users AS u ON u.Id = ug.UserId
	WHERE u.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
					 AND g.[Name] = 'Bali'
	--2.End
	
	--3.Start
	DECLARE @itemIdFirst INT = 251
	WHILE(@itemIdFirst <= 299)
	BEGIN
			INSERT INTO UserGameItems(ItemId, UserGameId)
			SELECT @itemIdFirst, ugi.UserGameId 
			FROM Games AS g
			JOIN UsersGames AS ug ON ug.GameId = g.Id
			JOIN Users AS u ON u.Id = ug.UserId
			JOIN UserGameItems AS ugi ON ugi.UserGameId = u.Id
			WHERE u.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
							 AND g.[Name] = 'Bali'
		SET @itemIdFirst += 1
	END

	DECLARE @itemIdSecond INT = 501
	WHILE(@itemIdSecond <= 539)
	BEGIN
			INSERT INTO UserGameItems(ItemId, UserGameId)
			SELECT @itemIdSecond, ugi.UserGameId 
			FROM Games AS g
			JOIN UsersGames AS ug ON ug.GameId = g.Id
			JOIN Users AS u ON u.Id = ug.UserId
			JOIN UserGameItems AS ugi ON ugi.UserGameId = u.Id
			WHERE u.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
							 AND g.[Name] = 'Bali'
		SET @itemIdSecond += 1
	END
	
	--3.End

	--4.Start

	SELECT u.Username, g.[Name], ug.Cash, i.[Name]
	FROM UsersGames AS ug
	JOIN Games AS g ON g.Id = ug.GameId
	JOIN Users AS u ON u.Id = ug.UserId
	JOIN UserGameItems AS ugi ON ugi.UserGameId = u.Id
	JOIN Items AS i ON i.Id = ugi.ItemId
	WHERE g.[Name] = 'Bali'
	ORDER BY u.Username ASC, i.[Name] ASC

	--4.End
END 

SELECT *
FROM UsersGames

SELECT DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE 
     TABLE_NAME = 'Items' AND 
     COLUMN_NAME = 'MinLevel'

/*** 20. ***/ 
--TODO!
/*** 21. ***/ 
--TODO!
/*** 22. ***/ 
--TODO!