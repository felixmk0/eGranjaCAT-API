using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nastrafarmapi.Entities;
using nastrafarmapi.Entities.Moviments;

namespace nastrafarmapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Entrada> Entrades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lot>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserGuid) 
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Lot>()
                .HasOne(l => l.Farm)
                .WithMany()
                .HasForeignKey(l => l.FarmId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Entrada>()
                .HasOne(l => l.Farm)
                .WithMany()
                .HasForeignKey(l => l.FarmId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entrada>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserGuid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Lot)
                .WithMany()
                .HasForeignKey(e => e.LotId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
