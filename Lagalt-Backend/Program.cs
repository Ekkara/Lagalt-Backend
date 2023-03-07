using Lagalt_Backend.Models;

using var lagaltDbContext = new LagaltDbContext();

var FirstTestUser = new User { UserName = "Maddie", Hidden = true };
var SecondTestUser = new User { UserName = "Davis", Hidden = false };
var FirstTestProject = new Project { ProjectName = "My New Game", ProjectCategoryId = 1, ProjectCategoryName = "Games" };
var SecondTestProject = new Project { ProjectName = "My New Song", ProjectCategoryId = 2, ProjectCategoryName = "Music" };

lagaltDbContext.Users.Add(FirstTestUser);
lagaltDbContext.Users.Add(SecondTestUser);
lagaltDbContext.Projects.Add(FirstTestProject);
lagaltDbContext.Projects.Add(SecondTestProject);

lagaltDbContext.SaveChanges();
