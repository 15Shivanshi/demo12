using DataAccess.DatabaseModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess
{
    public class EventDbContext : DbContext
    {
        public EventDbContext() : base("EventDbContext")
        {
        }

        public DbSet<BookReadingEventEntity> BookReadingEvents { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
