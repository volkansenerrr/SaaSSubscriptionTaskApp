using Microsoft.EntityFrameworkCore;
using SaaSSubscriptionApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // 🔥 BURASI SEED DATA (EKLEDİĞİM KISIM)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubscriptionPlan>().HasData(
            new SubscriptionPlan { Id = 1, Name = "Standart", Price = 100 },
            new SubscriptionPlan { Id = 2, Name = "Premium", Price = 200 }
        );

        modelBuilder.Entity<Feature>().HasData(
            new Feature { Id = 1, Name = "Ek Kullanıcı", Price = 20 },
            new Feature { Id = 2, Name = "Ek Depolama", Price = 50 }
        );
    }
}