using Services.Infrastructure;
using Sevices.Infrastructure;

namespace Services
{
    public class ServiceFactory
    {
        public IEventService GetEventService()
        {
            return new EventService();
        }

        public IUserService GetUserService()
        {
            return new UserService();
        }
    }
}
