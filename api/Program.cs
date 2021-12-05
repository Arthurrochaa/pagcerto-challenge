using api.Infrastructure.Data;
using api.Models.RepositoryModel.TransactionRepositories;
using api.Models.ServiceModel.TransactionServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));
services
    .AddControllers().AddJsonOptions(c => {
        c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

services.AddTransient<ITransactionService, TransactionService>();
services.AddTransient<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
