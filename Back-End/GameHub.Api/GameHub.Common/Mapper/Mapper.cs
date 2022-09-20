using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels;

namespace GameHub.Common.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<GameEvent, RequestEvent>();
            CreateMap<RequestEvent, GameEvent>();
        }
    }
}
