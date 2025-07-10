using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// initialize database connection 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>( options => 
{ 
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(
            connectionString
        )
    ); 
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IFileComparerService, FileComparerService>();

var basePath = Environment.GetEnvironmentVariable("RAILWAY_VOLUME_MOUNT_PATH")
               ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images");

// Ensure directory exists
Directory.CreateDirectory(basePath);

// ... write image


var app = builder.Build();

// seed database
var context = app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
DbSeeder.CreateAndSeedDb(context!);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
