using Lagalt_Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();
builder.Services.AddDbContext<LagaltDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*using Lagalt_Backend.Models;

using var lagaltDbContext = new LagaltDbContext();

var FirstTestUser = new User { UserName = "Maddie", Hidden = false };
var SecondTestUser = new User { UserName = "Davis", Hidden = false };
var FirstTestProject = new Project { ProjectName = "My New Game", ProjectCategoryId = 1, ProjectCategoryName = "Games" };
var SecondTestProject = new Project { ProjectName = "My New Song", ProjectCategoryId = 2, ProjectCategoryName = "Music" };

lagaltDbContext.Users.Add(FirstTestUser);
lagaltDbContext.Users.Add(SecondTestUser);
lagaltDbContext.Projects.Add(FirstTestProject);
lagaltDbContext.Projects.Add(SecondTestProject);

lagaltDbContext.SaveChanges();*/
