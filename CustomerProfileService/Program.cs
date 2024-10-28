using CustomerOperations.Application.Service;
using CustomerOperations.Domain;
using CustomerOperations.Infrastructure.EF;
using CustomerProfileService.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedOperations.Application.Service;
using SharedOperations.Domain.Validator;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("App", "CustomerProfileService")
    .WriteTo.Seq(builder.Configuration["SeqUrl"])
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();

builder.Services.AddScoped<ICustomerService, DefaultCustomerService>();

builder.Services.AddTransient<IPasswordValidator, DefaultPasswordValidator>();
builder.Services.AddTransient<IPhoneNumberValidator, DefaultPhoneNumberValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
