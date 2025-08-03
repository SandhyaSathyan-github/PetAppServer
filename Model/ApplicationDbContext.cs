using Microsoft.EntityFrameworkCore;

namespace PetAppServer.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<SuccessStory> SuccessStories { get; set; }
        public DbSet<LostPet> LostPets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.PetImages)
                .WithOne(i => i.Pet)
                .HasForeignKey(i => i.PetId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pets)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SuccessStories)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.LostPets)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);
        }
    }
}
