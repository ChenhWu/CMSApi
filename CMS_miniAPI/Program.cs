using Microsoft.EntityFrameworkCore;
using CMS_miniAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using CMS_miniAPI.Hubs;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var mpolicy = "_myAllowSpecificOrigins";
// 跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: mpolicy,
        policy => { policy.WithOrigins("http://localhost:5173")
            .AllowCredentials().AllowAnyHeader().AllowAnyMethod(); });
});

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option=>option.JsonSerializerOptions.ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var tConnectString = "server=localhost,1433;uid=sa;pwd=Mssql@dmin;database=CMS_test;TrustServerCertificate=true;Encrypt=true";
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
builder.Services.AddDbContext<CMSContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(tConnectString).UseLoggerFactory(loggerFactory));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using(var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<CMSContext>();
        db.Database.Migrate();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(mpolicy);
}

app.UseAuthorization();
app.MapControllers();
app.MapHub<SignalHub>("/signalHub");

app.Run();

