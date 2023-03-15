using Lagalt_Backend.Models.Main;

namespace Lagalt_Backend.Models
{
    public class SeedData
    {
        public static IEnumerable<User> GetUser()
        {
            return new List<User>()
        {

            new User
            {
                Id = 1,
                KeycloakId = "keycloak1",
                UserName = "user1",
                Picture = "Pict1.png",
                Status= "Junior Dep1",
                Description = "I'm user 1",
                IsProfileHidden = false,
                Skills = new List<Skill>(),
                ProjectApplications = new List<ProjectApplication>()
            },
            new User
            {
                Id = 2,
                KeycloakId = "keycloak2",
                UserName = "user2",
                 Picture = "Pict2.png",
                Status= "Junior Dep2",
                Description = "I'm user 2",
                IsProfileHidden = false,
                Skills = new List<Skill>(),
                ProjectApplications = new List<ProjectApplication>()
            },
             new User
            {
                Id = 3,
                KeycloakId = "keycloak3",
                UserName = "user3",
                Picture = "Pict3.png",
                Status= "Junior Dep3",
                Description = "I'm user 3",
                IsProfileHidden = true,
                Skills = new List<Skill>(),
                ProjectApplications = new List<ProjectApplication>()
            }
        };
        }

        public static IEnumerable<Skill> GetSkill()
        {
            return new List<Skill>()
        {

            new Skill
            {
                Id = 1,
                SkillName = "C#"
            },
            new Skill
            {
                Id = 2,
                SkillName = "JavaScript"
            },
             new Skill
             {
                 Id = 3,
                 SkillName = "Python"
             },
              new Skill
              {
                  Id = 4,
                  SkillName = "JavaScript"
              }
        };
        }

        public static IEnumerable<Project> GetProject()
        {
            return new List<Project>()
        {
            new Project
            {
               Id = 1,
                Title = "Project 1",
                Description = "This is project 1",
                ProjectType = "Web Application",
                GitHubLink = "https://github.com/project1",
                Skills = new List<Skill>(),
                ProjectApplications = new List<ProjectApplication>(),
                Messages = new List<Message>(),
                UserId = 1
            },
            new Project
            {
                Id = 2,
                Title = "Project 2",
                Description = "This is project 2",
                ProjectType = "Mobile Application",
                GitHubLink = null,
                Skills = new List<Skill>(),
                ProjectApplications = new List<ProjectApplication>(),
                Messages = new List<Message>(),
                UserId = 2
            }
        };
        }


        public static IEnumerable<ProjectApplication> GetProjectApplication()
        {
            return new List<ProjectApplication>()
        {
            new ProjectApplication
            {
                Id = 1,
                ProjectId = 1,
                UserId = 1,
                Status = "Pending"
            },
            new ProjectApplication
            {
                Id = 2,
                ProjectId = 2,
                UserId = 2,
                Status = "Accepted"
            }
        };
        }

        public static IEnumerable<Message> GetMessage()
        {
            return new List<Message>()
        {
            new Message
            {
                MessageId = 1,
                ProjectId = 1,
                SenderId = 1,
                MessageText = "Hi, I'm interested in your project"
            },
            new Message
            {
               MessageId = 2,
                ProjectId = 2,
                SenderId = 2,
                MessageText = "This project looks great, can I join?"
            }
        };

        }
    }
}
