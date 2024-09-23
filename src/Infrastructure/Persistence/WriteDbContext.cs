using Core.ApplicationAggregate;
using Core.Common;
using Core.ServerAggregate;
using Core.ServerAggregate.Entites;
using Core.ServerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public sealed class WriteDbContext : DbContext
    {
        internal DbSet<Server> Servers { get; set; }
        internal DbSet<Application> Applications { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>(server =>
            {
                server.HasKey(x => x.Id);

                server.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                server.Property(x => x.IPAddress)
                    .HasConversion(
                        ip => ip.Value,
                        ipString => new ServerIPAddress(ipString)
                    )
                    .HasMaxLength(45)
                    .IsRequired();

                server.ComplexProperty(x => x.ServerSpecifications, specs =>
                {
                    specs.Property(s => s.CPU).HasColumnName("specs_cpu").IsRequired();
                    specs.Property(s => s.RAMGigagabytes).HasColumnName("specs_ram").IsRequired();
                    specs.Property(s => s.DiskSizeGigabytes).HasColumnName("specs_disk").IsRequired();
                });

                server.HasMany(x => x.Secrets)
                    .WithOne();

                modelBuilder.Entity<ServerSecret>(secret =>
                {
                    secret.HasKey(x => x.Id);
                    secret.Property(x => x.Id).ValueGeneratedNever();
                    secret.Property(x => x.SecretName)
                        .IsRequired();
                    secret.Property(x => x.SecretValue)
                        .IsRequired();

                    secret.Property<byte>("IsActive")
                    .HasColumnType("BIT")
                    .HasDefaultValue(true)
                    .IsRequired();

                    secret.Property<DateTime>("CreatedDate")
                        .HasDefaultValue(DateTime.UtcNow)
                        .IsRequired();

                    secret.Property<DateTime>("ModifiedDate")
                        .HasDefaultValue(DateTime.UtcNow)
                        .IsRequired();
                });

                server.Navigation(x => x.Secrets)
                    .AutoInclude();

                server.Property<byte>("IsActive")
                    .HasColumnType("BIT")
                    .HasDefaultValue(true)
                    .IsRequired();

                server.Property<DateTime>("CreatedDate")
                    .HasDefaultValue(DateTime.UtcNow)
                    .IsRequired();

                server.Property<DateTime>("ModifiedDate")
                    .HasDefaultValue(DateTime.UtcNow)
                    .IsRequired();

                server.HasQueryFilter(x => EF.Property<byte>(x, "IsActive") == 1);
                modelBuilder.Entity<ServerSecret>().HasQueryFilter(x => EF.Property<byte>(x, "IsActive") == 1);
            });

            modelBuilder.Entity<Application>(app =>
            {
                app.HasKey(a => a.Id);
                app.Property(a => a.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(100);
                app.Property(a => a.Port)
                    .IsRequired();
                app.Property(a => a.CodeRepository)
                    .IsRequired()
                    .HasMaxLength(255);

                app.HasOne<Server>()
                    .WithMany()
                    .HasForeignKey(a => a.ServerId)
                    .OnDelete(DeleteBehavior.NoAction);

                app.Property<byte>("IsActive")
                    .HasColumnType("BIT")
                    .HasDefaultValue(true)
                    .IsRequired();

                app.Property<DateTime>("CreatedDate")
                    .HasDefaultValue(DateTime.UtcNow)
                    .IsRequired();

                app.Property<DateTime>("ModifiedDate")
                    .HasDefaultValue(DateTime.UtcNow)
                    .IsRequired();

                app.HasQueryFilter(a => EF.Property<byte>(a, "IsActive") == 1);
            });
        }

        public static void SeedData(WriteDbContext context)
        {
            if (!context.Servers.Any())
            {
                var secrets = new List<ServerSecret>()
                {
                    new ServerSecret("Test", "HGkwefn378")
                };

                var servers = new List<Server>()
                {
                    new Server("Server1", new ServerIPAddress("192.168.1.1"), new ServerSpecifications("i5", 16, 256), secrets),
                    new Server("Server2", new ServerIPAddress("192.168.1.2"), new ServerSpecifications("i5", 16, 256), new List<ServerSecret>())
                };

                var applications = new List<Application>()
                {
                    new Application(servers[0].Id, "Test App", 8080, "https://github.com/githubtraining/hellogitworld"),
                    new Application(null, "Standalone App", 443, "https://github.com/docker-library/hello-world")
                };

                context.Servers.AddRange(servers);
                context.Applications.AddRange(applications);

                context.SaveChanges();

                var serversRes = context.Servers.ToList();
                Console.WriteLine($"Servers in database: {serversRes.Count}");
            }
        }

        private void HandleSaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries<Entity>())
            {
                if(entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;
                    entry.Property("IsActive").IsModified = true;
                    entry.Property("ModifiedDate").IsModified = true;
                    entry.CurrentValues["IsActive"] = (byte)0;
                    entry.CurrentValues["ModifiedDate"] = DateTime.UtcNow;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["ModifiedDate"] = DateTime.UtcNow;
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
