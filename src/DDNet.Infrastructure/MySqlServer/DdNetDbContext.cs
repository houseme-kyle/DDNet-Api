using DDNet.Infrastructure.SqlServer.Models;
using Microsoft.EntityFrameworkCore;
namespace DDNet.Infrastructure.SqlServer
{
    public class DdNetDbContext : DbContext
    {
        public DdNetDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserTable> Users { get; set; }
        public DbSet<UserRoleTable> UserRoles { get; set; }
        public DbSet<PinTable> Pins { get; set; }
        public DbSet<ClanTable> Clans { get; set; }
        public DbSet<ServerTable> Servers { get; set; }
        public DbSet<MapTable> Maps { get; set; }
        public DbSet<DifficultyTierTable> DifficultyTiers { get; set; }
        public DbSet<RaceTable> Races { get; set; }
        public DbSet<RaceCheckpointTable> RaceCheckpoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>().ToTable("User");
            modelBuilder.Entity<UserRoleTable>().ToTable("UserRole");
            modelBuilder.Entity<ClanTable>().ToTable("Clan");
            modelBuilder.Entity<ServerTable>().ToTable("Server");
            modelBuilder.Entity<MapTable>().ToTable("Map");
            modelBuilder.Entity<DifficultyTierTable>().ToTable("DifficultyTier");
            modelBuilder.Entity<RaceTable>().ToTable("Race");
            modelBuilder.Entity<RaceCheckpointTable>().ToTable("RaceCheckpoint");
        }
    }
}
