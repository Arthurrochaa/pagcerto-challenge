using api.Infrastructure.Data;
using api.Models.RepositoryModel.TransactionRepositories;
using api.Models.ServiceModel.TransactionServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));
services.AddControllers();

services.AddTransient<ITransactionService, TransactionService>();
services.AddTransient<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
