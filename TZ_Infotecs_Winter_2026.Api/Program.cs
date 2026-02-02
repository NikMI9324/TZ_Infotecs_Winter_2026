using Microsoft.EntityFrameworkCore;
using TZ_Infotecs_Winter_2026.Application.Interfaces;
using TZ_Infotecs_Winter_2026.Application.Services;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;
using TZ_Infotecs_Winter_2026.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("ConnextionString__DefaultConnection");
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MyAppContext>(options =>
{
    //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString,
        m => m.MigrationsAssembly("TZ_Infotecs_Winter_2026.Infrastructure"));
});


builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<ICsvFileReader, CsvFileReader>();
builder.Services.AddScoped<IResultFiltrationService, ResultFiltrationService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyAppContext>();
    var maxTries = 10;
    for(int i =0; i < maxTries; i++)
    {
        try
        {
            context.Database.Migrate();
        }
        catch {
            if(i == maxTries - 1)
                break;
            await Task.Delay(5000);
        }
    }
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

