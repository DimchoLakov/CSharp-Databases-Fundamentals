using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class Startup
    {
        public static void Main()
        {
            using (var dbContext = new StudentSystemContext())
            {

            }
        }
    }
}
