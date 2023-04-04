using Microsoft.EntityFrameworkCore;

namespace URLShortener.DAL.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) 
        { 
        }
        public DbSet<Url> Url { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "Username1",
                    Password = "Password1",
                    Email = "Email1",
                    Role = "Administrator"
                },
                new User
                {
                    Id = 2,
                    Username = "Username2",
                    Password = "Password2",
                    Email = "Email2",
                    Role = "User"
                }
            );
            modelBuilder.Entity<Url>().HasData(
                new Url
                {
                    Id = 1,
                    ShortenedUrl = "ShortenedUrl1",
                    OriginalUrl = "OriginalUrl1",
                    UserId = 1,
                },
                new Url
                {
                    Id = 2,
                    ShortenedUrl = "ShortenedUrl2",
                    OriginalUrl = "OriginalUrl2",
                    UserId = 2,

                }
            );

        }
    }
}
