using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.Project;

namespace Lagalt_Backend.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() {
            CreateMap<Project, CreateProjectDTO>()
                   .ReverseMap();


            const int MAX_DESC_LENGTH = 100;

            CreateMap<Project, GetProjectForMainDTO>()
                .ForMember(dest => dest.Description, opt => {
                    opt.MapFrom(src => src.Description.Length > MAX_DESC_LENGTH ?
                    src.Description.Substring(0, MAX_DESC_LENGTH - 3).TrimEnd() + "..." :
                    src.Description);
                });

                CreateMap<Project, GetProjectDetails>()
                       .ReverseMap();

            CreateMap<Project, EditProjectDTO>()
                     .ReverseMap();
        }
    }
}
