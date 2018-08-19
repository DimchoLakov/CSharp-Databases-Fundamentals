namespace ProductShop.App
{
    public class StartUp
    {
        public static void Main()
        {
            var jsonProcessor = new JsonProcessor();
            jsonProcessor.InitializeDatabase();
            jsonProcessor.ImportData();
            jsonProcessor.ExportData();
        }
    }
}
