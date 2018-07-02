using System.Data.SqlClient;

namespace _01.InitialSetup
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            var connection = new SqlConnection(Configuration.FirstTimeConnectionString);

            using (connection)
            {
                connection.Open();

                string createDatabaseString = "CREATE DATABASE MinionsDB";
                ExecuteNonQueryCommand(createDatabaseString, connection);

                connection.ChangeDatabase("MinionsDB");

                string createTableCountries =
                    "CREATE TABLE Countries" +
                    "(Id INT PRIMARY KEY IDENTITY," +
                    "[Name] VARCHAR(50))";
                string createTableMinions =
                    "CREATE TABLE Minions" +
                    "(Id INT PRIMARY KEY IDENTITY," +
                    "[Name] VARCHAR(30)," +
                    "Age INT," +
                    "TownId INT FOREIGN KEY REFERENCES Towns(Id))";
                string createTableTowns =
                    "CREATE TABLE Towns" +
                    "(Id INT PRIMARY KEY IDENTITY," +
                    "[Name] VARCHAR(50), " +
                    "CountryCode INT FOREIGN KEY REFERENCES Countries(Id))";
                string createTableEvilnessFactor =
                    "CREATE TABLE EvilnessFactors" +
                    "(Id INT PRIMARY KEY IDENTITY, " +
                    "[Name] VARCHAR(50))";
                string createTableVillains =
                    "CREATE TABLE Villains" +
                    "(Id INT PRIMARY KEY IDENTITY," +
                    "[Name] VARCHAR(50), " +
                    "EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";
                string createTableMinionVillains =
                    "CREATE TABLE MinionsVillains" +
                    "(MinionId INT FOREIGN KEY REFERENCES Minions(Id)," +
                    "VillainId INT FOREIGN KEY REFERENCES Villains(Id)," +
                    "CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";

                ExecuteNonQueryCommand(createTableCountries, connection);
                ExecuteNonQueryCommand(createTableTowns, connection);
                ExecuteNonQueryCommand(createTableMinions, connection);
                ExecuteNonQueryCommand(createTableEvilnessFactor, connection);
                ExecuteNonQueryCommand(createTableVillains, connection);
                ExecuteNonQueryCommand(createTableMinionVillains, connection);

                string insertIntoCountries =
                    "INSERT INTO Countries([Name])" +
                    "VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";
                string insertIntoTowns =
                    "INSERT INTO Towns ([Name], CountryCode)" +
                    "VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";
                string insertIntoMinions =
                    "INSERT INTO Minions ([Name],Age, TownId)" +
                    "VALUES ('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";
                string insertIntoEvilnessFactor =
                    "INSERT INTO EvilnessFactors ([Name])" +
                    "VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";
                string insertIntoVillains =
                    "INSERT INTO Villains ([Name], EvilnessFactorId)" +
                    "VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";
                string insertIntoMinionsVillains =
                    "INSERT INTO MinionsVillains (MinionId, VillainId)" +
                    "VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                ExecuteNonQueryCommand(insertIntoCountries, connection);
                ExecuteNonQueryCommand(insertIntoTowns, connection);
                ExecuteNonQueryCommand(insertIntoMinions, connection);
                ExecuteNonQueryCommand(insertIntoEvilnessFactor, connection);
                ExecuteNonQueryCommand(insertIntoVillains, connection);
                ExecuteNonQueryCommand(insertIntoMinionsVillains, connection);
                
                connection.Close();
            }
        }

        private static void ExecuteNonQueryCommand(string createDatabaseString, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(createDatabaseString, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
