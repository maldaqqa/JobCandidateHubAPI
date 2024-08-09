using JobCandidateHubAPI.Data;
using JobCandidateHubAPI.Middlewares;
using JobCandidateHubAPI.Repositories;
using JobCandidateHubAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Get the Database type and the corresponding Connection String to use the appropriate DB Provider.
var databaseSettings = builder.Configuration.GetSection("DatabaseSettings");
var dbType = databaseSettings["DatabaseType"];

if (string.IsNullOrWhiteSpace(dbType))
{
    throw new InvalidOperationException("DatabaseType is not configured in the appsettings.json.");
}

var connectionString = databaseSettings.GetSection("ConnectionStrings")[dbType];

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException($"Connection string for database type '{dbType}' is not configured in the appsettings.json.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    switch (dbType)
    {
        case "SQLite":
            options.UseSqlite(connectionString);
            break;
        case "MSSQL":
            options.UseSqlServer(connectionString);
            break;
        case "MySQL":
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            break;
        case "PostgreSQL":
            options.UseNpgsql(connectionString);
            break;
        case "Oracle":
            options.UseOracle(connectionString);
            break;
        case "InMemory":
            options.UseInMemoryDatabase(connectionString);
            break;
        default:
            throw new InvalidOperationException($"Unsupported database type: {dbType}");
    }
});


builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();
//Make sure DB is created 
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.EnsureCreated();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
