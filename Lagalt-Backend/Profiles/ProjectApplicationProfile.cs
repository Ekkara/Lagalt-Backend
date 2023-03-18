using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.ProjectApplication;

namespace Lagalt_Backend.Profiles
{
    public class ProjectApplicationProfile : Profile
    {
        public ProjectApplicationProfile() {
            CreateMap<CreateProjectApplicationDTO, ProjectApplication>();

            CreateMap<ProjectApplication, ReadProjectApplicationAdminDTO>();
        }
    }
}
