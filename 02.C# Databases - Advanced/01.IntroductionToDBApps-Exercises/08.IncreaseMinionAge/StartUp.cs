using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08.IncreaseMinionAge
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            int[] ids = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                UpdateMinionsAge(ids, connection);

                PrintMinions(connection);

                connection.Close();
            }
        }

        private static void PrintMinions(SqlConnection connection)
        {
            List<string> minionsNames = new List<string>();

            string query =
                @"SELECT m.[Name], m.Age
                  FROM Minions AS m";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionsNames.Add(reader[0].ToString() + " " + reader[1].ToString());
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, minionsNames));
        }

        private static void UpdateMinionsAge(int[] ids, SqlConnection connection)
        {
            string updateAgeQuery =
                @"UPDATE Minions
                  SET
                  Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name)-1)),
	              Age += 1
	              WHERE Id IN(@minionsIds)";
            updateAgeQuery = updateAgeQuery.Replace("@minionsIds", string.Join(",  ", ids));

            using (SqlCommand command = new SqlCommand(updateAgeQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
