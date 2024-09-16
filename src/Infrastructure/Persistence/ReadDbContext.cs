using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<ServerDto> Servers { get; set; }
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServerDto>(server =>
            {
                server.ToTable("Servers");
                server.HasKey(s => s.Id);
                server.ComplexProperty(x => x.ServerSpecifications, specs =>
                {
                    specs.Property(s => s.CPU).HasColumnName("specs_cpu").IsRequired();
                    specs.Property(s => s.RAMGigagabytes).HasColumnName("specs_ram").IsRequired();
                    specs.Property(s => s.DiskSizeGigabytes).HasColumnName("specs_disk").IsRequired();
                });

                server.HasMany(x => x.Secrets)
                    .WithOne()
                    .HasForeignKey("ServerId");

                modelBuilder.Entity<ServerSecretDto>(secret =>
                {
                    secret.ToTable("ServerSecret");
                    secret.HasKey(s => s.Id);
                    secret.Property<byte>("IsActive")
                    .HasColumnType("BIT")
                    .HasDefaultValue(true)
                    .IsRequired();
                });

                server.Navigation(x => x.Secrets)
                   .AutoInclude();

                server.Property<byte>("IsActive")
                    .HasColumnType("BIT")
                    .HasDefaultValue(true)
                    .IsRequired();

                server.HasQueryFilter(x => EF.Property<byte>(x, "IsActive") == 1);
                modelBuilder.Entity<ServerSecretDto>().HasQueryFilter(x => EF.Property<byte>(x, "IsActive") == 1);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("Read only Context");
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("Read only Context");
        }
    }
}
