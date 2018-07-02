using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07.PrintAllMinionNames
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
                
                List<string> minionsNames = GetMinionsNames(connection);
                
                PrintMinionsNames(minionsNames);
                
                connection.Close();
            }
        }

        private static void PrintMinionsNames(List<string> minionsNames)
        {
            for (int first = 0, last = minionsNames.Count - 1; first <= last; first++, last--)
            {
                Console.WriteLine(minionsNames[first]);

                if (first != last)
                {
                    Console.WriteLine(minionsNames[last]);
                }
            }
        }

        private static List<string> GetMinionsNames(SqlConnection connection)
        {
            List<string> minionsNames = new List<string>();

            string query =
                @"SELECT m.[Name]
                  FROM Minions AS m";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionsNames.Add(reader[0].ToString());
                    }
                }
            }

            return minionsNames;
        }
    }
}
