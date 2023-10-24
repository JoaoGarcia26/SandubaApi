using Microsoft.EntityFrameworkCore;
using SandubaApi.Context;
using SandubaApi.Extensions;
using SandubaApi.Repository.Implementations;
using SandubaApi.Repository.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependencia no padrão UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

////Sistema de logs customizados 
///
/// (Sistema legado, substituido pela biblioteca NLog)
/// 
//builder.Services.AddScoped<LogsFilter>();
//builder.Logging.AddProvider(new CustomLoggingProvider(new CustomLoggingProviderConfiguration
//{
//    LogLevel = LogLevel.Information
//}));

//Captura a ConnectionString dentro do arquivo appsettings.json
string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Adicionando o DbConext e configurando o provedor mysql com a ConnectionString
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
