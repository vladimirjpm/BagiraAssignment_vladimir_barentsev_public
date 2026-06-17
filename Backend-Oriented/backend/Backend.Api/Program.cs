using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Api.Middleware;
using Backend.Application.Common;
using Backend.Application.Entities;
using Backend.Application.Scenarios;
using Backend.Application.Search;
using Backend.Infrastructure;
using Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Shared JSON options (camelCase + string enums) reused by the exception middleware
// so error bodies serialize identically to controller responses.
var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
jsonOptions.Converters.Add(new JsonStringEnumConverter());
builder.Services.AddSingleton(jsonOptions);

// String enums are contract-critical: the frontend sends/expects "Soldier", "Friendly".
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Unify model-validation 400s into the ErrorResponse shape (frontend reads error.message).
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .ToDictionary(
                e => e.Key,
                e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

        var body = new ErrorResponse("Validation failed.", StatusCodes.Status400BadRequest, errors);
        return new BadRequestObjectResult(body);
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS origins come from configuration/env (Cors:AllowedOrigins, e.g. Cors__AllowedOrigins__0).
// In dev/docker the frontend is same-origin (vite proxy / nginx), so this is a
// safety net for cross-origin setups rather than the primary wiring.
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                  ?? new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IScenarioService, ScenarioService>();
builder.Services.AddScoped<IEntityService, EntityService>();
builder.Services.AddScoped<ISearchService, SearchService>();

var app = builder.Build();

// Auto-migrate when using Postgres; no-op for in-memory.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<AppDbContext>();
    if (db is not null)
        await db.Database.MigrateAsync();
}

// Wrap the whole pipeline so domain exceptions become uniform error responses.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Intentionally no UseHttpsRedirection: inside the container there is no TLS cert,
// and the 307 redirects confuse the SPA/proxy. TLS terminates at a real proxy in prod.

app.UseCors("frontend");

app.MapControllers();

app.Run();
