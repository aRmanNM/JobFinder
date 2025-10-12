using System.Text;
using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Parsers;
using JobFinder.Server.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<ApplicationDbContext>(conf =>
    conf.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<IParserFactory, ParserFactory>();

builder.Services.AddScoped<WebHelper>();

builder.Services.AddScoped<IParser, FakeParser>();

builder.Services.AddScoped<IParser, JobinjaParser>();
builder.Services.AddScoped<IParser, QueraParser>();
builder.Services.AddScoped<IParser, JobvisionParser>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUserHelper>();

builder.Services.AddHttpClient("Default", opt =>
{
    opt.Timeout = TimeSpan.FromSeconds(5);
    opt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:128.0) Gecko/20100101 Firefox/128.0");
});

builder.Services.AddIdentityCore<AppUser>(conf =>
{
    conf.Password.RequireDigit = false;
    conf.Password.RequireLowercase = false;
    conf.Password.RequireNonAlphanumeric = false;
    conf.Password.RequireUppercase = false;
    conf.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new Exception("jwt key not set"))),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateIssuer = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateAudience = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(conf =>
{
    conf.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
