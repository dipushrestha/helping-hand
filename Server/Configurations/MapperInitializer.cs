using AutoMapper;

using helping_hand.Models;
using helping_hand.Models.Dto;

namespace helping_hand.Server.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApiUser, RegisterDto>().ReverseMap();
        }
    }
}
