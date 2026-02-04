using ImUs.Domain.Entities;
using ImUs.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImUs.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ImUsDbContext>();
        await context.Database.EnsureCreatedAsync();

        if (await context.Tenants.AnyAsync()) return;

        var tenant = new Tenant { Name = "Conad", Slug = "conad" };
        context.Tenants.Add(tenant);
        await context.SaveChangesAsync();

        var store = new Store
        {
            Name = "Conad Cesena Centro",
            Address = "Via Emilia 123",
            City = "Cesena",
            TenantId = tenant.Id
        };
        context.Stores.Add(store);
        await context.SaveChangesAsync();

        var zones = new[]
        {
            new Zone { Name = "Reparto Frigo", Description = "Latticini e surgelati", StoreId = store.Id },
            new Zone { Name = "Corsia Snack", Description = "Snack e bevande", StoreId = store.Id },
            new Zone { Name = "Ortofrutta", Description = "Frutta e verdura", StoreId = store.Id }
        };
        context.Zones.AddRange(zones);
        await context.SaveChangesAsync();

        var devices = new[]
        {
            new Device
            {
                Name = "CAM-01 Ingresso",
                Type = DeviceType.Camera,
                Manufacturer = "Axis Communications",
                SerialNumber = "AXIS-001",
                IsOnline = true,
                LastSeen = DateTime.UtcNow,
                StoreId = store.Id,
                ZoneId = null
            },
            new Device
            {
                Name = "SENSOR-FRIGO-01",
                Type = DeviceType.Sensor,
                Manufacturer = "Sensirion",
                SerialNumber = "SENS-001",
                IsOnline = true,
                LastSeen = DateTime.UtcNow,
                StoreId = store.Id,
                ZoneId = zones[0].Id
            },
            new Device
            {
                Name = "ROBOT-PULIZIA-01",
                Type = DeviceType.Robot,
                Manufacturer = "Pudu Robotics",
                SerialNumber = "PUDU-001",
                IsOnline = false,
                LastSeen = DateTime.UtcNow.AddHours(-2),
                StoreId = store.Id,
                ZoneId = null
            },
            new Device
            {
                Name = "ESL-CORSIA-02",
                Type = DeviceType.ElectronicLabel,
                Manufacturer = "VusionGroup",
                SerialNumber = "VUS-001",
                IsOnline = true,
                LastSeen = DateTime.UtcNow,
                StoreId = store.Id,
                ZoneId = zones[1].Id
            }
        };
        context.Devices.AddRange(devices);
        await context.SaveChangesAsync();

        // Seed Identity roles
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["SuperAdmin", "TenantAdmin", "Developer", "HardwarePartner"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Seed a default admin user
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        const string adminEmail = "admin@imus.io";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "SuperAdmin");
        }
    }
}
