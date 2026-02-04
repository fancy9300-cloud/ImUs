using ImUs.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImUs.Infrastructure.Data;

public class ImUsDbContext : IdentityDbContext<IdentityUser>
{
    public ImUsDbContext(DbContextOptions<ImUsDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tenant>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Name).HasMaxLength(200).IsRequired();
            e.Property(t => t.Slug).HasMaxLength(100).IsRequired();
            e.HasIndex(t => t.Slug).IsUnique();
        });

        builder.Entity<Store>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Name).HasMaxLength(200).IsRequired();
            e.HasOne(s => s.Tenant).WithMany(t => t.Stores).HasForeignKey(s => s.TenantId);
        });

        builder.Entity<Zone>(e =>
        {
            e.HasKey(z => z.Id);
            e.Property(z => z.Name).HasMaxLength(100).IsRequired();
            e.HasOne(z => z.Store).WithMany(s => s.Zones).HasForeignKey(z => z.StoreId);
        });

        builder.Entity<Device>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Name).HasMaxLength(200).IsRequired();
            e.HasOne(d => d.Store).WithMany(s => s.Devices).HasForeignKey(d => d.StoreId);
            e.HasOne(d => d.Zone).WithMany(z => z.Devices).HasForeignKey(d => d.ZoneId);
        });

        builder.Entity<Product>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(200).IsRequired();
            e.Property(p => p.Sku).HasMaxLength(50);
            e.HasOne(p => p.Zone).WithMany(z => z.Products).HasForeignKey(p => p.ZoneId);
        });

        builder.Entity<Alert>(e =>
        {
            e.HasKey(a => a.Id);
            e.HasOne(a => a.Store).WithMany().HasForeignKey(a => a.StoreId);
            e.HasOne(a => a.Device).WithMany().HasForeignKey(a => a.DeviceId);
        });

        builder.Entity<SensorReading>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.Device).WithMany(d => d.Readings).HasForeignKey(r => r.DeviceId);
        });
    }
}
