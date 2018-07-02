namespace Configuration
{
    public class Configuration
    {
		// Please add your Server name in the connection strings below.
		
        public const string ConnectionString =
                                              @"Server=" +
                                              @"Database=MinionsDB;" +
                                              @"Integrated Security=True";
        public const string FirstTimeConnectionString =
                                                       @"Server=" +
                                                       @"Integrated Security=True";
    }
}
