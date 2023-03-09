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
                UserId = 1,
                UserName = "JohnDoe",
                Email = "johndoe@example.com",
                Password = "John1234",
                IsProfileHidden = false
            },
            new User
            {
                UserId = 2,
                UserName = "JaneDoe",
                Email = "janedoe@example.com",
                Password = "Jan12345",
                IsProfileHidden = true
            }
        };
        }

        public static IEnumerable<Skill> GetSkill()
        {
            return new List<Skill>()
        {
            new Skill
            {
                SkillId = 1,
                SkillName = "C#"
            },
            new Skill
            {
                SkillId = 2,
                SkillName = "JavaScript"
            },
             new Skill
             {
                 SkillId = 3,
                 SkillName = "Python"
             },
              new Skill
              {
                  SkillId = 4,
                  SkillName = "JavaScript"
              }
        };
        }

        public static IEnumerable<UserSkill> GetUserSkill()
        {
            return new List<UserSkill>()
        {
            new UserSkill
            {
                UserId = 1,
                SkillId = 1
            },
            new UserSkill
            {
                UserId = 2,
                SkillId = 2
            }
        };
        }

        public static IEnumerable<Project> GetProject()
        {
            return new List<Project>()
        {
            new Project
            {
                ProjectId = 1,
                Title = "Project 1",
                Description = "Description of project 1",
                ProjectType = "Web application",
                GitHubLink = "https://github.com/...",
                GitLabLink = "https://gitlab.com/..."
            },
            new Project
            {
                ProjectId = 2,
                Title = "Project 2",
                Description = "Description of project 2",
                ProjectType = "Desktop application",
                GitHubLink = "https://github.com/...",
                GitLabLink = "https://gitlab.com/..."
            }
        };
        }

        public static IEnumerable<ProjectSkill> GetProjectSkill()
        {
            return new List<ProjectSkill>()
        {
            new ProjectSkill
            {
                ProjectId = 1,
                SkillSetId = 1
            },
            new ProjectSkill
            {
                ProjectId = 2,
                SkillSetId = 2
            }
        };
        }

        public static IEnumerable<ProjectApplication> GetProjectApplication()
        {
            return new List<ProjectApplication>()
        {
            new ProjectApplication
            {
                ApplicationId = 1,
                ProjectId = 1,
                UserId = 1,
                Status = "Pending"
            },
            new ProjectApplication
            {
                ApplicationId = 2,
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
                MessageText = "Hi Jane, can you help me with this project?"
            },
            new Message
            {
                MessageId = 2,
                ProjectId = 2,
                SenderId = 2,            
                MessageText = "Sure John, what do you need help with?"
            }
        };
        }
    }
}
