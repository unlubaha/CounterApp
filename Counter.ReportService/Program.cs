using Counter.ReportService.Services;
using Counter.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//if you need different database for every service
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        b => b.MigrationsAssembly("Counter.ReportService")));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql());

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();