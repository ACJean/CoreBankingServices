using AccountOperations.Application;
using AccountOperations.Domain;
using AccountOperations.Infrastructure.EF;
using AccountTransactionService.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedOperations.Domain.Services;
using SharedOperations.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
builder.Services.AddTransient<ICustomerResources, HttpCustomerResources>();

builder.Services.AddScoped<IAccountService, DefaultAccountService>();
builder.Services.AddScoped<IMovementsService, DefaultMovementsService>();
builder.Services.AddScoped<IReportService, DefaultReportService>();

// Configuración de clientes http a Microservicios
builder.Services.AddHttpClient("CustomerService", client =>
 {
     client.BaseAddress = new Uri(builder.Configuration["InternalServices:CustomerService"]);
     client.Timeout = TimeSpan.FromSeconds(30);
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
