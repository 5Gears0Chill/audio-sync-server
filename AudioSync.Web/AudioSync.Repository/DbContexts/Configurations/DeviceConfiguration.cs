using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AudioSync.Core.DataAccess.Entities;

namespace AudioSync.Repository.DbContexts.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> entity)
        {
            entity.ToTable("Device", "dbo");

            entity.Property(e => e.Id).HasColumnName("Id").UseIdentityColumn().ValueGeneratedOnAdd();
            entity.Property(e => e.DeviceId)
                .IsRequired()
                .HasColumnName("DeviceId");
        }
    }
}
