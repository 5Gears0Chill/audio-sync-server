using Microsoft.EntityFrameworkCore;
using AudioSync.Core.DataAccess.Entities;
using AudioSync.Repository.DbContexts.Configurations;

namespace AudioSync.Repository.DbContexts
{
    public class AudioSyncDbContext : BaseDbContext
    {
        public AudioSyncDbContext(DbContextOptions<AudioSyncDbContext> options) : base(options) { }
        public virtual DbSet<Device> Devices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //link relational entities
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        }
    }
}
