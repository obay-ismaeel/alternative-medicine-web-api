using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
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

Directory.CreateDirectory(basePath);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

var context = app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>();
DbSeeder.CreateAndSeedDb(context!);

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
