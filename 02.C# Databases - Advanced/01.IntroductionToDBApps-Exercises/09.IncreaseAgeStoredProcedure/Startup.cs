using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _09.IncreaseAgeStoredProcedure
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            int minionId = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                IncreaseMinionsAge(connection, minionId);

                PrintMinions(connection, minionId);

                connection.Close();
            }
        }

        private static void PrintMinions(SqlConnection connection, int minionId)
        {
            List<string> minions = new List<string>();

            string procedureName =
                @"SELECT m.[Name], m.Age
                  FROM Minions AS m
                  WHERE m.Id = @minionId";

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minions.Add($"{reader[0]} - {reader[1]} years old");
                    }
                }

                Console.WriteLine(string.Join(Environment.NewLine, minions));
            }
        }

        private static void IncreaseMinionsAge(SqlConnection connection, int minionId)
        {
            string procedureName = @"usp_GetOlder";

            using (SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.AddWithValue("@minionId", minionId);

                command.ExecuteNonQuery();
            }
        }
    }
}
