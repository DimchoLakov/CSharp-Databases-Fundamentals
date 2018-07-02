using System;
using System.Data.SqlClient;

namespace _04.AddMinion
{
    using Configuration;

    public class Startup
    {
        public static void Main()
        {
            string[] minionInfo =
                Console.ReadLine().Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] villainInput =
                Console.ReadLine().Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string minionName = minionInfo[1];
            int minionAge = int.Parse(minionInfo[2]);
            string minionTownName = minionInfo[3];
            string villainName = villainInput[1];

            var connection = new SqlConnection(Configuration.ConnectionString);

            using (connection)
            {
                connection.Open();

                int townId = GetTownId(minionTownName, connection); // with transaction(if it fails, the Rollback method will execute)
                int villainId = GetVillainId(villainName, connection); //without transacton
                int minionId = GetMinionId(minionName, minionAge, townId, connection); //without transacton

                TrySetMinionToVillain(villainId, minionId, connection, villainName, minionName); //without transacton

                string finalMessage =
                    "If no messages are diplayed above then all the information you've put is already in the system.";
                Console.WriteLine();
                Console.WriteLine(new string('-', finalMessage.Length));
                Console.WriteLine(finalMessage);
            }
        }

        private static void TrySetMinionToVillain(int villainId, int minionId, SqlConnection connection, string villainName, string minionName)
        {
            string query =
                @"SELECT *
                  FROM MinionsVillains AS mv
                  WHERE mv.MinionId = @minionId AND
              	  mv.VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                if (command.ExecuteScalar() == null)
                {
                    SetMinionToVillain(villainId, minionId, connection);
                    Console.WriteLine($"Successfully added {minionName} to be a minion of {villainName}");
                }
            }
        }

        private static void SetMinionToVillain(int villainId, int minionId, SqlConnection connection)
        {
            string query =
                @"INSERT INTO MinionsVillains(MinionId, VillainId)
                  VALUES
                  (
                  	 @minionId, @villainId
                  )";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }

        private static int GetMinionId(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            string getMinionIdQuery =
                @"SELECT Id
                  FROM Minions AS m
                  WHERE m.[Name] = @minionName AND
              	  m.Age = @minionAge AND
              	  m.TownId = @townId";

            using (SqlCommand command = new SqlCommand(getMinionIdQuery, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@minionAge", minionAge);
                command.Parameters.AddWithValue("@townId", townId);

                if (command.ExecuteScalar() == null)
                {
                    CreateMinionInDB(minionName, minionAge, townId, connection);
                }

                return (int)command.ExecuteScalar();
            }
        }

        private static int GetVillainId(string villainName, SqlConnection connection)
        {
            string getVillainIdQuery =
                @"SELECT Id
                  FROM Villains AS v
                  WHERE v.[Name] = @villainName";

            using (SqlCommand command = new SqlCommand(getVillainIdQuery, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                if (command.ExecuteScalar() == null)
                {
                    CreateVillainInDB(villainName, connection);
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                return (int)command.ExecuteScalar();
            }
        }

        private static void CreateMinionInDB(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            string insertMinionQuery =
                @"INSERT INTO Minions([Name], Age, TownId)
                  VALUES
                  (
                      @minionName, @minionAge, @townId
                  )";

            using (SqlCommand command = new SqlCommand(insertMinionQuery, connection))
            {
                command.Parameters.AddWithValue("@minionName", minionName);
                command.Parameters.AddWithValue("@minionAge", minionAge);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
        }

        private static int GetTownId(string minionTownName, SqlConnection connection)
        {
            string townIdQuery =
                @"SELECT Id FROM Towns WHERE [Name] = @townName";

            using (SqlCommand command = new SqlCommand(townIdQuery, connection))
            {
                SqlTransaction transaction;

                transaction = connection.BeginTransaction("Minions Transaction");
                
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.Parameters.AddWithValue("@townName", minionTownName);
                    if (command.ExecuteScalar() == null)
                    {
                        CreateTownInDB(minionTownName, connection);
                        Console.WriteLine($"Town {minionTownName} was added to the database.");
                    }

                    transaction.Commit();

                    return (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }

            return -1;
        }

        private static void CreateVillainInDB(string villainName, SqlConnection connection)
        {
            string insertVillainQuery =
                @"INSERT INTO Villains([Name], EvilnessFactorId)
                  VALUES
                  (
                      @villainName, 4
                  )";

            using (SqlCommand command = new SqlCommand(insertVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainName", villainName);
                command.ExecuteNonQuery();
            }
        }

        private static void CreateTownInDB(string minionTownName, SqlConnection connection)
        {
            string insertTownQuery =
                @"INSERT INTO Towns([Name], CountryCode)
                  VALUES
                  (
                      @townName, 1
                  )";

            using (SqlCommand command = new SqlCommand(insertTownQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", minionTownName);
                command.ExecuteNonQuery();
            }
        }
    }
}
