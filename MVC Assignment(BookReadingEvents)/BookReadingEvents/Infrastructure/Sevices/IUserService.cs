using Infrastructure.Models;

namespace Infrastructure.Sevices
{
    public interface IUserService
    {
        bool Create(User user);
        //bool Exists(string emailID);
        bool IsValid(User user);

    }
}
