using Lagalt_Backend.Models;
using Lagalt_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



// Configure authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://lemur-3.cloud-iam.com/auth/realms/projectlagaltfullstack",
            ValidAudience = "account",
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var keyuri = "https://lemur-3.cloud-iam.com/auth/realms/projectlagaltfullstack/protocol/openid-connect/certs";
                var response = client.GetAsync(keyuri).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var keys = new JsonWebKeySet(responseString);
                return keys.Keys;
            }
        };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IProjectService), typeof(ProjectService));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<LagaltDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProjectService, ProjectService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Lagalt Network API",
        Description = "Get information about users, posts, groups and events",
        Contact = new OpenApiContact
        {
            Name = "Github repo",
            Url = new Uri("https://github.com/TintinPetersson/AlumniNetworkAPI"),
        }
    });
    options.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Cors stay open, but needs to be authorized by keycloaktoken to access endpoints

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();