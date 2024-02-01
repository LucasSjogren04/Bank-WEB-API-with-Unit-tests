using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UPG3ButWithTests.Repository;
using UPG3ButWithTests.Repository.Interfaces;
using UPG3ButWithTests.Services;
using UPG3ButWithTests.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

string jwtSecretKey = builder.Configuration.GetSection("Jwt:SecretKey").Value;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    ConfigureJwtBearerOptions(options, jwtSecretKey);
});

AddCustomServices(builder.Services);

ConfigureSwagger(builder.Services);

var app = builder.Build();

var env = app.Services.GetRequiredService<IHostEnvironment>();

ConfigureMiddlewarePipeline(app, env);

app.Run();


void ConfigureJwtBearerOptions(JwtBearerOptions options, string secretKey)
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "http://localhost:5212/",
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5212/",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.FromMinutes(1),
    };
}

void AddCustomServices(IServiceCollection services)
{
    builder.Services.AddSingleton<IDBContext, DBContext>();

    services.AddScoped<ILoginService, LoginService>();
    services.AddScoped<ILoginRepo, LoginRepo>();

    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<ICustomerRepo, CustomerRepo>();


    services.AddControllers();
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
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
                new List<string>()
            }
        });
    });
}

void ConfigureMiddlewarePipeline(WebApplication app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Upgift3");
        });
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();
    app.UseAuthorization();
    //app.UseMiddleware<RoleAuthorizationMiddleware>();
    app.MapControllers();
}
