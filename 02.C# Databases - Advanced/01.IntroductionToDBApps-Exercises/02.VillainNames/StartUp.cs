using System;
using System.Data.SqlClient;

namespace _02.VillainNames
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                string villainsMinionsQuery =
                    @"SELECT v.[Name],
                             COUNT(mv.MinionId) AS MinionsCount
                      FROM Villains AS v
                      JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
                      JOIN Minions AS m ON m.Id = mv.MinionId
                      GROUP BY v.[Name]
                      HAVING COUNT(mv.MinionId) > 3
                      ORDER BY MinionsCount DESC";

                var command = new SqlCommand(villainsMinionsQuery, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string villainName = reader.GetValue(0).ToString();
                    int minionsCount = int.Parse(reader.GetValue(1).ToString());

                    Console.WriteLine($"{villainName} - {minionsCount}");
                }

                connection.Close();
            }
        }
    }
}
