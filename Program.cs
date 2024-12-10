using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("GoldenRaspberryAwards"));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddTransient<AwardService>();
builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<DataSeeder>();
    seeder.Seed(@"C:\projects\GoldenRaspberryAwardsAPI\DataBase\movielist.csv");

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
