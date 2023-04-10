using AutoMapper;
using DataAccess.DatabaseModels;
using DataAccess.HelperModels;
using Services.Models;

namespace Services.Infrastructure
{
    public class AutomapperWebProfile
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BookReadingEventEntity, BookReadingEvent>();
            cfg.CreateMap<BookReadingEvent, BookReadingEventEntity>();
            cfg.CreateMap<UserEntity, User>();
            cfg.CreateMap<User, UserEntity>();
            cfg.CreateMap<CommentEntity, Comment>();
            cfg.CreateMap<Comment, CommentEntity>();
            cfg.CreateMap<ShowEventEntity, ShowEvent>();
            cfg.CreateMap<ShowEvent, ShowEventEntity>();
        });

        public static IMapper Mapper = config.CreateMapper();
    }
}
