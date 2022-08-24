using BancoOnBoarding.API.Middlewares;
using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository;
using BancoOnBoarding.Repository.Interfaces;
using BancoOnBoarding.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

// Repositories
builder.Services.AddScoped<IAsignacionClienteRepository, AsignacionClienteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPatioRepository, PatioRepository>();
builder.Services.AddScoped<IEjecutivoRepository, EjecutivoRepository>();
builder.Services.AddScoped<ISolicitudCreditoRepository, SolicitudCreditoRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();

// services
builder.Services.AddScoped<IAsignacionClienteService, AsignacionClienteService>();
builder.Services.AddScoped<ISolicitudCreditoService, SolicitudCreditoService>();
builder.Services.AddScoped<IPatioService, PatioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<ICargaDatosEjecutivoService, CargaDatosEjecutivoService>();
builder.Services.AddScoped<ICargaDatosClienteService, CargaDatosClienteService>();
builder.Services.AddScoped<ICargaDatosMarcaService, CargaDatosMarcaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
