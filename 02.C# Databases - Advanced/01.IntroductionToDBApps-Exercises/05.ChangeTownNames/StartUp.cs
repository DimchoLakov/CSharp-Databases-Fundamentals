using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTownNames
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            string countryName = Console.ReadLine();

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                int countryId = GetCountryId(countryName, connection);

                if (countryId == 0)
                {
                    Console.WriteLine("No town names were affected.");

                    connection.Close();

                    Environment.Exit(0);
                }

                int affectedRows = ChangeTownsNamesToUppercase(countryId, connection);
                List<string> modifiedTownNames = GetModifiedTownNames(countryId, connection);

                Console.WriteLine($"{affectedRows} town names were affected.");
                Console.WriteLine($"[{string.Join(", ", modifiedTownNames)}]");
                
                connection.Close();
            }
        }

        private static List<string> GetModifiedTownNames(int countryId, SqlConnection connection)
        {
            List<string> townNames = new List<string>();

            string townsQuery =
                @"SELECT t.[Name]
                  FROM Towns AS t
                  JOIN Countries AS c ON c.Id = t.CountryCode
                  WHERE c.Id = @countryId";

            using (SqlCommand command = new SqlCommand(townsQuery, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        townNames.Add(reader[0].ToString());
                    }
                }
            }

            return townNames;
        }

        private static int ChangeTownsNamesToUppercase(int countryId, SqlConnection connection)
        {
            string updateQuery =
                @"UPDATE Towns
	              SET [Name] = UPPER([Name])
	              WHERE CountryCode = @countryId";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                return (int)command.ExecuteNonQuery();
            }
        }

        private static int GetCountryId(string countryName, SqlConnection connection)
        {
            string query =
                @"SELECT TOP (1) c.Id
                  FROM Towns AS t
                  JOIN Countries AS c ON c.Id = t.CountryCode
                  WHERE c.[Name] = @countryName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);
                var countryId = command.ExecuteScalar();
                if (countryId == null)
                {
                    return 0;
                }

                return (int)countryId;
            }
        }
    }
}
