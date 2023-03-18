using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.DTO.Message;

namespace Lagalt_Backend.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile() {
            CreateMap<CreateMessageDTO, Message>();
            CreateMap<Message, ReadMessageDTO>();
        }
    }
}
