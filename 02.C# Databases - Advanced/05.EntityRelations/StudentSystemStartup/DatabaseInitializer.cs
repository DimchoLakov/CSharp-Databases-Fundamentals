using Microsoft.EntityFrameworkCore;

namespace StudentSystemStartup
{
    public class DatabaseInitializer
    {
        public static void InitialSeed(DbContext dbContext)
        {
            StudentsSeed(dbContext);

            CoursesSeed(dbContext);

            HomeworkSeed(dbContext);

            ResourcesSeed(dbContext);
        }

        private static void ResourcesSeed(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        private static void HomeworkSeed(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        private static void CoursesSeed(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        private static void StudentsSeed(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
