using ImUs.Application.Services;
using ImUs.Domain.Entities;
using ImUs.Domain.Interfaces;
using ImUs.Infrastructure.Data;
using ImUs.Infrastructure.Repositories;
using ImUs.Infrastructure.Services;
using ImUs.Web.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ImUsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=imus.db"));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ImUsDbContext>();

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Application Services
builder.Services.AddScoped<IAlertService, AlertAppService>();
builder.Services.AddScoped<IDeviceService, DeviceAppService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<IoTIngestionService>();
builder.Services.AddScoped<AlertAppService>();
builder.Services.AddScoped<DeviceAppService>();
builder.Services.AddScoped<ISmartSearchService, SmartSearchService>();

// Blazor + API
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ImUs API", Version = "v1" });
});

var app = builder.Build();

// Seed data
await SeedData.InitializeAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImUs API v1"));
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<AlertHub>("/hubs/alerts");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
