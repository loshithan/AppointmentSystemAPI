using System.Text;
using AppointmentSystem.Infrastructure;
using AppointmentSystem.Infrastructure.Infrastructure.Seeding;
using AppointmentSystem.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);


// // Add services to the container.
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
// builder.Services.AddScoped<IProfessionalAvailabilityRepository, ProfessionalAvailabilityRepository>();

builder.Services.AddInfrastructureServices(builder.Configuration); // Infrastructure Layer


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//register mediatorR
// Register MediatR with the Application assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(GetAllAppointments.Query).Assembly, // Application layer
    typeof(Program).Assembly // API layer (if needed)
));
// 🔹 Register DbContext (EF Core)
builder.Services.AddDbContext<AppointmentSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// resigter Cors
builder.Services.AddCors(options =>
{
        options.AddPolicy(name: "AllowedOriginsPolicy",
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
    // options.AddPolicy("AllowedOriginsPolicy", policy =>
    // {
    //     policy.WithOrigins("https://localhost:7071") // Change this to your Blazor Server URL
    //           .AllowAnyHeader()
    //           .AllowAnyMethod()
    //           .AllowCredentials(); // Needed for authentication
    // });
});

builder.Services.AddControllers();

//fluent validation config
builder.Services.AddValidatorsFromAssemblyContaining<GetAllAppointmentsValidator>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]??"YourSuperSecretKeyHere_1234567890");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Seed data during application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var env = services.GetRequiredService<IHostEnvironment>();
    try
    {
        await Seed.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// app.UseCors("AllowedOriginsPolicy");

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();

