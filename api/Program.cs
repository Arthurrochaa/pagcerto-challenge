using api.Infrastructure.Data;
using api.Models.RepositoryModel.AdvanceRequestRepositories;
using api.Models.RepositoryModel.TransactionRepositories;
using api.Models.ServiceModel.AdvanceRequestServices;
using api.Models.ServiceModel.TransactionServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

services.AddControllers().AddJsonOptions(c => {
        c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Desafio Pagcerto",
            Version = "v1",
            Description = "API REST desenvolvida com o ASP.NET 6 + SQL Server + EF Core",
            Contact = new OpenApiContact
            {
                Name = "Arthur Rocha",
                Url = new Uri("https://github.com/Arthurrochaa"),
                Email = "arthurrochafts@gmail.com"
            }
        });
});

services.AddTransient<ITransactionService, TransactionService>();
services.AddTransient<ITransactionRepository, TransactionRepository>();
services.AddTransient<IAdvanceRequestRepository, AdvanceRequestRepository>();
services.AddTransient<IAdvanceRequestService, AdvanceRequestService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Desafio Pagcerto");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
