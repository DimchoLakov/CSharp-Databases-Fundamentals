/*** Problem 1.	Records’ Count ***/
SELECT COUNT(*) AS [Count]
FROM WizzardDeposits

/*** Problem 2.	Longest Magic Wand ***/
SELECT TOP (1) MagicWandSize AS [LongestMagicWandSize]
FROM WizzardDeposits
ORDER BY MagicWandSize DESC

/*** Problem 3.	Longest Magic Wand per Deposit Groups ***/
SELECT w.DepositGroup, MAX(w.MagicWandSize) AS [LongestMagicWand]
FROM WizzardDeposits AS w
GROUP BY w.DepositGroup

/*** Problem 4.	* Smallest Deposit Group per Magic Wand Size ***/
SELECT TOP (2) w.DepositGroup
FROM WizzardDeposits AS w
GROUP BY w.DepositGroup
ORDER BY AVG(w.MagicWandSize) ASC

/*** Problem 5.	Deposits Sum ***/
SELECT w.DepositGroup, SUM(w.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS w
GROUP BY w.DepositGroup

/*** Problem 6.	Deposits Sum for Ollivander Family ***/
SELECT w.DepositGroup, SUM(w.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS w
WHERE w.MagicWandCreator = 'Ollivander family'
GROUP BY w.DepositGroup

/*** Problem 7.	Deposits Filter ***/
SELECT *
FROM (SELECT w.DepositGroup, SUM(w.DepositAmount) AS [TotalSum]
FROM WizzardDeposits AS w
WHERE w.MagicWandCreator = 'Ollivander family'
GROUP BY w.DepositGroup
) AS w1
WHERE w1.TotalSum < 150000
ORDER BY w1.TotalSum DESC

/*** Problem 8.	 Deposit Charge ***/
SELECT w.DepositGroup, w.MagicWandCreator, MIN(w.DepositCharge) AS [MinDepositCharge]
FROM WizzardDeposits AS w
GROUP BY w.DepositGroup, w.MagicWandCreator
ORDER BY w.MagicWandCreator ASC, w.DepositGroup ASC

/*** Problem 9.	Age Groups ***/
SELECT 
		(CASE
			WHEN w.Age BETWEEN 0 AND 10 THEN '[0-10]'
			WHEN w.Age BETWEEN 11 AND 20 THEN '[11-20]'
			WHEN w.Age BETWEEN 21 AND 30 THEN '[21-30]'
			WHEN w.Age BETWEEN 31 AND 40 THEN '[31-40]'
			WHEN w.Age BETWEEN 41 AND 50 THEN '[41-50]'
			WHEN w.Age BETWEEN 51 AND 60 THEN '[51-60]'
			WHEN w.Age > 60 THEN '[61+]'
		END) AS [AgeGroup],
		COUNT(*) AS [WizardCount]
FROM WizzardDeposits AS w
GROUP BY 
		(CASE
			WHEN w.Age BETWEEN 0 AND 10 THEN '[0-10]'
			WHEN w.Age BETWEEN 11 AND 20 THEN '[11-20]'
			WHEN w.Age BETWEEN 21 AND 30 THEN '[21-30]'
			WHEN w.Age BETWEEN 31 AND 40 THEN '[31-40]'
			WHEN w.Age BETWEEN 41 AND 50 THEN '[41-50]'
			WHEN w.Age BETWEEN 51 AND 60 THEN '[51-60]'
			WHEN w.Age > 60 THEN '[61+]'
		END)

/*** Problem 10. First Letter ***/
SELECT LEFT(w.FirstName, 1) AS [First Letter]
FROM WizzardDeposits AS w
WHERE w.DepositGroup = 'Troll Chest'
GROUP BY LEFT(w.FirstName, 1)
ORDER BY [First Letter] ASC

/*** Problem 11. Average Interest ***/
SELECT w.DepositGroup, w.IsDepositExpired, AVG(w.DepositInterest) AS [AverageInterest]
FROM WizzardDeposits AS w
WHERE w.DepositStartDate > CONVERT(date, '01/01/1985', 103)
GROUP BY w.DepositGroup, w.IsDepositExpired
ORDER BY w.DepositGroup DESC, w.IsDepositExpired ASC

/*** Problem 12. * Rich Wizard, Poor Wizard ***/
SELECT SUM(Result.Diff) AS [SumDifference]
FROM (SELECT w.DepositAmount - LEAD(w.DepositAmount, 1) OVER (ORDER BY Id) AS Diff
FROM WizzardDeposits AS w) AS Result

--SELECT SUM(Result.Diff) AS [SumDifference]
--FROM (SELECT DepositAmount - 
--		(SELECT DepositAmount FROM WizzardDeposits WHERE Id = w.Id + 1) AS Diff
--FROM WizzardDeposits AS w) AS Result


/*** Problem 13. Departments Total Salaries ***/
USE SoftUni

SELECT e.DepartmentID, SUM(e.Salary) AS [TotalSalary]
FROM Employees AS e
GROUP BY e.DepartmentID
ORDER BY DepartmentID

/*** Problem 14. Employees Minimum Salaries ***/
SELECT e.DepartmentID, MIN(e.Salary) AS [MinimumSalary]
FROM Employees AS e
WHERE e.DepartmentID IN(2, 5, 7) AND YEAR(e.HireDate) >= 2000
GROUP BY e.DepartmentID

/*** Problem 15. Employees Average Salaries ***/
SELECT *
INTO Rich_Employees
FROM Employees AS e
WHERE e.Salary > 30000

DELETE
FROM Rich_Employees
WHERE ManagerID = 42

UPDATE Rich_Employees
SET Salary += 5000
WHERE DepartmentID = 1

SELECT re.DepartmentID, AVG(re.Salary)
FROM Rich_Employees AS re
GROUP BY re.DepartmentID

/*** Problem 16. Employees Maximum Salaries ***/
SELECT *
FROM (SELECT e.DepartmentID, MAX(e.Salary) AS [MaxSalary]
FROM Employees AS e
GROUP BY e.DepartmentID) AS Result
WHERE Result.MaxSalary NOT BETWEEN 30000 AND 70000

/*** Problem 17. Employees Count Salaries ***/
SELECT COUNT(*) AS [Count]
FROM Employees AS e
WHERE e.ManagerID IS NULL

/*** Problem 18. *3rd Highest Salary ***/
SELECT DISTINCT e.DepartmentID, e.Salary
FROM (SELECT DepartmentID, Salary, DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS [SalaryRank]
FROM Employees) AS e
WHERE SalaryRank = 3

/*** Problem 19. **Salary Challenge ***/
SELECT TOP (10) FirstName, LastName, DepartmentID
FROM Employees AS e
WHERE Salary > (SELECT AVG(Salary) FROM Employees WHERE e.DepartmentID = DepartmentID)