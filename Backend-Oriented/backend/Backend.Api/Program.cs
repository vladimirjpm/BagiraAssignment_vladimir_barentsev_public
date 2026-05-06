var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO(candidate): Configure CORS to allow requests from your frontend origin
// Example:
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//         policy.WithOrigins("http://localhost:5173")
//               .AllowAnyHeader()
//               .AllowAnyMethod());
// });

// TODO(candidate): Register your repositories and services here
// Example:
// builder.Services.AddScoped<IScenarioRepository, ScenarioRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO(candidate): Add app.UseCors("AllowFrontend") here if you configured CORS above

app.UseAuthorization();

app.MapControllers();

app.Run();
