/*** Problem 1.	Employee Address ***/
USE SoftUni

SELECT TOP (5) e.EmployeeID, e.JobTitle, a.AddressID, a.AddressText
FROM Employees AS e
JOIN Addresses AS a
ON e.AddressID = a.AddressID
ORDER BY a.AddressID ASC

/*** Problem 2.	Addresses with Towns ***/
SELECT TOP (50) e.FirstName, e.LastName, t.[Name] AS [Town], a.AddressText
FROM Employees AS e
JOIN Addresses AS a
ON e.AddressID = a.AddressID
JOIN Towns AS t
ON a.TownID = t.TownID
ORDER BY e.FirstName ASC, e.LastName ASC

/*** Problem 3.	Sales Employee ***/
SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name] AS [DepartmentName]
FROM Employees AS e
JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE d.[Name] = 'Sales'
ORDER BY e.EmployeeID ASC

/*** Problem 4.	Employee Departments ***/
SELECT TOP (5) e.EmployeeID, e.FirstName, e.Salary, d.[Name]
FROM Employees AS e
JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID ASC

/*** Problem 5.	Employees Without Project ***/
SELECT TOP (3) emps.EmployeeID, emps.FirstName
FROM Employees AS emps
WHERE emps.EmployeeID NOT IN
(
	SELECT ep.EmployeeID
	FROM EmployeesProjects AS ep
)
ORDER BY emps.EmployeeID ASC
-- SLOWER ^

SELECT TOP (3) e.EmployeeID, e.FirstName
FROM Employees AS e
LEFT OUTER JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
WHERE ep.EmployeeID IS NULL
-- FASTER ^