using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Mappings;
using MovieHub.API.Middleware;
using MovieHub.API.Repositories;
using MovieHub.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Services
// Register controllers
builder.Services.AddControllers();

// Register DbContext with SQL Server
builder.Services.AddDbContext<MovieHubDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register all the service class
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IBranchHallService, BranchHallService>();
builder.Services.AddScoped<IBranchHallSeatService, BranchHallSeatService>();
builder.Services.AddScoped<IHallShowTimeService, HallShowTimeService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieShowTimeService, MovieShowTimeService>();
builder.Services.AddScoped<
    IMovieHallShowTimeService,
    MovieHallShowTimeService
>();

//Register all the repository class
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IHallRepository, HallRepositoriy>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IShowTimeRepository, ShowTimeRepository>();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
#endregion

app.MapControllers();

app.Run();
