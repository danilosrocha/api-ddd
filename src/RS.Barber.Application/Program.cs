using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RS.Barber.Application.Token;
using RS.Barber.Domain.Entities;
using RS.Barber.Domain.Interfaces;
using RS.Barber.Domain.Validators;
using RS.Barber.Infra.Data.Contexts;
using RS.Barber.Infra.Data.Repositories;
using RS.Barber.Service;
using RS.Barber.Service.Erros;
using RS.Barber.Utils.Mapings;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Conexão com banco:

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BarbeariaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BarbeariaContext>()
    .AddDefaultTokenProviders()
    .AddUserValidator<CustomPhoneNumberValidator<Usuario>>();

// 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper:

var config = new MapperConfiguration(config =>
{
    config.AddProfile<ClienteInputMap>();
    config.AddProfile<UsuarioInputMap>();
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

//

// Injeção de dependência:

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IClienteService, ClienteService>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<CustomUserService, CustomUserService>();

builder.Services.AddTransient<ClienteErrosService, ClienteErrosService>();

//

// JWT Token

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

//

// Validacao no Swagger:

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Auth using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


//

var configs = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var devClient = "";

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins(devClient));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
