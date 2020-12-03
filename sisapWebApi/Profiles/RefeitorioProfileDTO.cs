using AutoMapper;
using sisapWebApi.DTO;
using sisapWebApi.Models.SQLServer;

namespace sisapWebApi.Profiles
{
    public class RefeitorioProfileDTO : Profile
    {
        public RefeitorioProfileDTO()
        {
            CreateMap<Refeitorio, RefeitorioDTO>();
        }
    }
}
