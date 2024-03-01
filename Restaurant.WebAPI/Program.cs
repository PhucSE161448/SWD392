using Microsoft.OpenApi.Models;
using Restaurant.Application.Commons;
using Restaurant.Infrastructure;
using Restaurant.WebAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// parse the configuration in appsettings
var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddInfrastructureService(configuration.DatabaseConnection);
builder.Services.AddWebAPIService();
builder.Services.AddSingleton(configuration);
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.JWTSection.Issuer,
            ValidAudience = configuration.JWTSection.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTSection.SecretKey)),
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
    });

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});


builder.Services.AddSingleton(configuration);
var app = builder.Build();
app.UseCors(options =>
options.WithOrigins("http://localhost:3000")
.AllowAnyMethod()
.AllowAnyHeader()
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","MixFood V1"),
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
public partial class Program { }
