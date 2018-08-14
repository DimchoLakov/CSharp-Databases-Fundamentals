namespace PhotoShare.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Contracts;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly PhotoShareContext _dbContext;

        public DatabaseInitializerService(PhotoShareContext context)
        {
            this._dbContext = context;
        }

        public void InitializeDatabase()
        {
            this._dbContext.Database.Migrate();
        }
    }
}
