using AutoMapper;
using GameHub.Common.Entities;
using GameHub.Common.Models.RequestModels.GameEvent;

namespace GameHub.Common.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<GameEvent, RequestCreateEvent>();
            CreateMap<RequestCreateEvent, GameEvent>();
        }
    }
}
