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

/*** Problem 6.	Employees Hired After ***/
SELECT e.FirstName, e.LastName, e.HireDate, d.[Name]
FROM Employees AS e
JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE d.[Name] IN ('Sales', 'Finance') AND e.HireDate > '1/1/1999'
ORDER BY e.HireDate ASC

/*** Problem 7.	Employees with Project ***/
SELECT TOP (5) e.EmployeeID, e.FirstName, p.[Name]
FROM Employees AS e
JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > CONVERT(datetime2, '13/08/2002', 103) AND p.EndDate IS NULL
ORDER BY e.EmployeeID ASC

/*** Problem 8.	Employee 24 ***/
SELECT e.EmployeeID, e.FirstName, 
	CASE
		WHEN p.StartDate >= '2005' THEN NULL
		ELSE p.[Name]
	END AS [ProjectName]
FROM Employees AS e
INNER JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24

/*** Problem 9.	Employee Manager ***/
SELECT e1.EmployeeID, e1.FirstName, e1.ManagerID, e2.FirstName
FROM Employees AS e1
JOIN Employees AS e2
ON e1.ManagerID = e2.EmployeeID
WHERE e1.ManagerID IN (3, 7)
ORDER BY e1.EmployeeID ASC

/*** Problem 10. Employee Summary ***/
SELECT TOP (50) e1.EmployeeID, 
	   e1.FirstName + ' ' + e1.LastName AS [EmployeeName], 
	   e2.FirstName + ' ' + e2.LastName AS [ManagerName],
	   d.[Name] AS [DepartmentName]
FROM Employees AS e1
JOIN Employees AS e2
ON e1.ManagerID = e2.EmployeeID
JOIN Departments AS d
ON e1.DepartmentID = d.DepartmentID
ORDER BY e1.EmployeeID ASC
