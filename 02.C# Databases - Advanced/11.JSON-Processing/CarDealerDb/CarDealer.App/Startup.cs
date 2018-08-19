namespace CarDealer.App
{
    public class Startup
    {
        public static void Main()
        {
            var jsonProcessor = new JsonProcessor();
            jsonProcessor.MigrateDatabase();
            jsonProcessor.ImportData();
            jsonProcessor.ExportData();
        }
    }
}
