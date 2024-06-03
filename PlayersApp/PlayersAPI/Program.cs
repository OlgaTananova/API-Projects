using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlayersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Scoped service 
builder.Services.AddScoped<IPlayerService, PlayerService>();
// add database context
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=Players.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed the database with the data from the csv file

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var environment = services.GetRequiredService<IWebHostEnvironment>();

    // Ensure database is created
    context.Database.EnsureCreated();

    // Import data if the database is empty
    if (!context.Players.Any())
    {
        SeedData.Initialize(context, "Player.csv");
    }
}

app.Run();
