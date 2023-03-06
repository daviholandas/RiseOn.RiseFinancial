using RiseOn.RiseFinancial.Application;
using RiseOn.RiseFinancial.Infrastructure;
using RiseOn.RiseFinancial.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationIoC(builder.Configuration);
builder.Services.AddInfrastructureDataServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddExpenseEndpoints();

app.Run();

