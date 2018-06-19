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

/*** Problem 11. Min Average Salary ***/
SELECT MIN(avgsbd.AverageSalary) AS [MinAverageSalary]
FROM (
	SELECT AVG(e.Salary) AS [AverageSalary]
	FROM Employees AS e
	GROUP BY e.DepartmentID) AS [avgsbd]

/*** Problem 12. Highest Peaks in Bulgaria ***/
USE [Geography]

SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
JOIN Mountains AS m ON mc.MountainId = m.Id
JOIN Peaks AS p ON m.Id = p.MountainId
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC

/*** Problem 13. Count Mountain Ranges ***/
SELECT c.CountryCode, COUNT(m.MountainRange) AS [MountainRanges]
FROM Countries AS c
JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
JOIN Mountains AS m ON mc.MountainId = m.Id
GROUP BY c.CountryCode
HAVING c.CountryCode IN ('BG', 'RU', 'US')

/*** Problem 14. Countries with Rivers ***/
SELECT TOP (5) c.CountryName, r.RiverName
FROM Countries AS c
LEFT OUTER JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
LEFT OUTER JOIN Rivers AS r ON cr.RiverId = r.Id
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName ASC

/*** Problem 15. *Continents and Currencies ***/      -- TODO !!!!!!!!!


/*** Problem 16. Countries without any Mountains ***/
SELECT COUNT(c.CountryCode) AS [CountryCode]
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL

/*** Problem 17. Highest Peak and Longest River by Country ***/
SELECT TOP (5) peaks.CountryName,
	   peaks.Elevation AS [HighestPeakElevation],
	   rivers.[Length] AS [LongestRiverLength]
FROM 
(
	SELECT c.CountryName,
	   c.CountryCode, 
	   DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS DescendingElevationRank,
	   p.Elevation
FROM Countries AS c
	FULL OUTER JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	FULL OUTER JOIN Mountains AS m ON mc.MountainId = m.Id
	FULL OUTER JOIN Peaks AS p ON m.Id = p.MountainId
) AS peaks
FULL OUTER JOIN
(
	SELECT c.CountryName, 
		   c.CountryCode,
		   DENSE_RANK() OVER(PARTITION BY c.CountryCode ORDER BY r.[Length] DESC) AS DescendingRiverLengthRank,
		   r.[Length]
	FROM Countries AS c
	FULL OUTER JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
	FULL OUTER JOIN Rivers AS r ON cr.RiverId = r.Id
) AS rivers
ON peaks.CountryCode = rivers.CountryCode
WHERE peaks.DescendingElevationRank = 1
	  AND rivers.DescendingRiverLengthRank = 1
	  AND (peaks.Elevation IS NOT NULL OR rivers.[Length] IS NOT NULL)
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, CountryName ASC

--SECOND VARIANT

SELECT TOP (5) c.CountryName, MAX(p.Elevation) AS [HighestPeakElevation], MAX(r.Length) AS [LongestRiverLength]
FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName ASC