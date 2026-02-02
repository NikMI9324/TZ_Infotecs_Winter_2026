using Microsoft.EntityFrameworkCore;
using TZ_Infotecs_Winter_2026.Application.Interfaces;
using TZ_Infotecs_Winter_2026.Application.Services;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;
using TZ_Infotecs_Winter_2026.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MyAppContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString,
        m => m.MigrationsAssembly("TZ_Infotecs_Winter_2026.Infrastructure"));
});


builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<ICsvFileReader, CsvFileReader>();
builder.Services.AddScoped<IResultFiltrationService, ResultFiltrationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

