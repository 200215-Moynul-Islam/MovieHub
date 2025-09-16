using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Services
// Register controllers
builder.Services.AddControllers();

// Register DbContext with SQL Server
builder.Services.AddDbContext<MovieHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#endregion

app.MapControllers();

app.Run();
