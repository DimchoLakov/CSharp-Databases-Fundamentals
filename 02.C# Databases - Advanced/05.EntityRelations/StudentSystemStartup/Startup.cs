using P01_StudentSystem.Data;

namespace StudentSystemStartup
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new StudentSystemContext())
            {
                DatabaseInitializer.InitialSeed(dbContext);
            }
        }
    }
}
