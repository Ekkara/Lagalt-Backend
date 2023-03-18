using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.User;

namespace Lagalt_Backend.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile() { 
        CreateMap<User, ReadUserNamesDTO>();
        }
    }
}
