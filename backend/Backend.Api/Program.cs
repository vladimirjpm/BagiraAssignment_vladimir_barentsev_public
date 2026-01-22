using Backend.Infrastructure.Interfaces;
using Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Scenario Builder API",
        Version = "v1",
        Description = "API for managing training scenarios and entities"
    });
});

// Register repositories
// TODO(candidate): Replace with your chosen data storage implementation (Database, In-Memory, etc.)
// TODO(candidate): Create Application layer with business logic services
builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();
builder.Services.AddScoped<IEntityRepository, EntityRepository>();

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

app.Run();
