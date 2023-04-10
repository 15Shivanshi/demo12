using Services.Models;

namespace Sevices.Infrastructure
{
    public interface IUserService
    {
        bool Create(User user);
        //bool Exists(string emailID);
        bool IsValid(User user);

    }
}
