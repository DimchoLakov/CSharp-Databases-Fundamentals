using System;
using System.Data.SqlClient;
using System.Text;

namespace _03.MinionNames
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            Console.Write("Please enter villain Id: ");
            int villainId = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                string villainNameQuery =
                                          @"SELECT v.[Name]
                                            FROM Villains AS v
                                            WHERE Id = @villainId";

                string minionNamesQuery =
                    @"SELECT m.[Name] AS [Minion Name],
	                         m.Age AS [Minion Age]
	                         FROM Villains AS v
                      JOIN MinionsVillains AS mv ON mv.VillainId  = v.Id
                      JOIN Minions AS m ON m.Id = mv.MinionId
                      WHERE v.Id = @villainId
                      ORDER BY m.[Name] ASC";

                string villainName = GetVillainName(villainNameQuery, connection, villainId);
                string minionsNames = GetMinionsNames(minionNamesQuery, connection, villainId);
                minionsNames = string.IsNullOrWhiteSpace(minionsNames) ? "no minions" : minionsNames;
                
                Console.WriteLine($"Villain: {villainName}\n{minionsNames}");

                connection.Close();
            }
        }

        private static string GetVillainName(string queryStr, SqlConnection connection, int villainId)
        {
            string villainName = string.Empty;

            var getVillainNameCommand = new SqlCommand(queryStr, connection);
            getVillainNameCommand.Parameters.AddWithValue("@villainId", villainId);

            using (getVillainNameCommand)
            {
                var reader = getVillainNameCommand.ExecuteReader();

                using (reader)
                {
                    int counter = 1;
                    while (reader.Read())
                    {
                        villainName = reader[0].ToString();
                    }
                }
            }

            return $"{villainName}: ";
        }

        private static string GetMinionsNames(string queryStr, SqlConnection connection, int villainId)
        {
            StringBuilder sb = new StringBuilder();

            var getMinionsCommand = new SqlCommand(queryStr, connection);
            getMinionsCommand.Parameters.AddWithValue("@villainId", villainId);

            using (getMinionsCommand)
            {
                var reader = getMinionsCommand.ExecuteReader();

                using (reader)
                {
                    int counter = 1;
                    while (reader.Read())
                    {
                        sb.AppendLine($"{counter}. {reader[0]} {reader[1]}");
                        counter++;
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}
