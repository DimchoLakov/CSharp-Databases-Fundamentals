namespace PhotoShare.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;
    using Configuration;

    public class PhotoShareContext : DbContext
    { 
        public PhotoShareContext() { }

	    public PhotoShareContext(DbContextOptions<PhotoShareContext> options)
		    : base(options)
	    {
	    }
        
        public virtual DbSet<User> Users { get; set; }   
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<AlbumRole> AlbumRoles { get; set; }
        public virtual DbSet<Town> Towns { get; set; }	
	    public virtual DbSet<AlbumTag> AlbumTags { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfig());
            modelBuilder.ApplyConfiguration(new AlbumRoleConfig());
            modelBuilder.ApplyConfiguration(new AlbumTagConfig());
            modelBuilder.ApplyConfiguration(new FriendshipConfig());
            modelBuilder.ApplyConfiguration(new PictureConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new TownConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}