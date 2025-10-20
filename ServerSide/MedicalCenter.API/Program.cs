using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MedicalCenter.API.Middlewares;
using MedicalCenter.BLL.Auth;
using MedicalCenter.BLL.Interfaces;
using MedicalCenter.BLL.Mapping;
using MedicalCenter.BLL.Services;
using MedicalCenter.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MedicalCenter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==============================
            // 1️⃣ Database Configuration
            // ==============================
            builder.Services.AddDbContext<MedicalCenterDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ==============================
            // 2️⃣ CORS Configuration (Important for production)
            // ==============================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // ==============================
            // 3️⃣ JSON + ENUM Settings
            // ==============================
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false)
                    );
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            // ==============================
            // 4️⃣ AutoMapper + Services
            // ==============================
            builder.Services.AddAutoMapper(typeof(UserMap).Assembly);
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAutoMapper(typeof(PatientMap).Assembly);
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddAutoMapper(typeof(DoctorMap).Assembly);
            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddAutoMapper(typeof(appointmentMap).Assembly);
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            // ==============================
            // 5️⃣ JWT Configuration
            // ==============================
            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings!.Secret);

            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // no delay in token expiration
                };
            });

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // ==============================
            // 6️⃣ Swagger Configuration (with JWT support)
            // ==============================
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedicalCenter API", Version = "v1" });

                // Enable JWT authentication in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token. Example: Bearer eyJhbGciOiJIUzI1NiIs..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ==============================
            // 7️⃣ Build the App
            // ==============================
            var app = builder.Build();

            // ==============================
            // 8️⃣ Middleware Pipeline
            // ==============================

            // Enable Swagger in both Development and Production
            // Remove "|| app.Environment.IsProduction()" if you want Swagger only in Development
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable CORS (must be before Authentication)
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            // Enable Authentication + Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}