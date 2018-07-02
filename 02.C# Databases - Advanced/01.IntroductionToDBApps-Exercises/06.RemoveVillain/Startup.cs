using System;
using System.Data.SqlClient;

namespace _06.RemoveVillain
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            int inputVillainId = int.Parse(Console.ReadLine());

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction("Delete villain.");

                try
                {
                    int villainId = GetVillainId(inputVillainId, connection, transaction);
                    if (villainId == -1)
                    {
                        Console.WriteLine("No such villain was found.");

                        Environment.Exit(0);
                    }

                    string releasedMinions = ReleasesMinions(villainId, connection, transaction);
                    string deletedVillain = DeleteVillain(villainId, connection, transaction);

                    Console.WriteLine(deletedVillain);
                    Console.WriteLine(releasedMinions);
                }
                catch (SqlException e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e.Message);
                }

                transaction.Commit();

                connection.Close();
            }
        }

        private static string DeleteVillain(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string getVillainNameQuery =
                @"SELECT [Name]
                  FROM Villains
                  WHERE Id = @villainId";
            
            string deleteFromVillainsQuery =
                @"DELETE FROM Villains
                  WHERE Id = @villainId";

            string villainName = string.Empty;

            using (SqlCommand command = new SqlCommand(getVillainNameQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                villainName = (string)command.ExecuteScalar();
            }

            using (SqlCommand command = new SqlCommand(deleteFromVillainsQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }

            return $"{villainName} was deleted.";
        }

        private static string ReleasesMinions(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string releaseQuery =
                @"DELETE FROM MinionsVillains
                  WHERE VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(releaseQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);

                int releasedMinionsCount = command.ExecuteNonQuery();
                return $"{releasedMinionsCount} minions were released.";
            }
        }

        private static int GetVillainId(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string getVillainQuery =
                @"SELECT v.Id
                  FROM Villains AS v
                  WHERE v.Id = @villainId";

            using (SqlCommand command = new SqlCommand(getVillainQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);

                var result = command.ExecuteScalar();
                if (result == null)
                {
                    return -1;
                }

                return (int)result;
            }
        }
    }
}
