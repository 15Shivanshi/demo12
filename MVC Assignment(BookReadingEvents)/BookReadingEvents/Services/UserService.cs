using DataAccess;
using DataAccess.DatabaseModels;
using Services.Models;
using Sevices.Infrastructure;

namespace Services
{
    public class UserService : IUserService
    {
        //TO CREATE A NEW USER
        public bool Create(User user)
        {
            UserDataAccess userDataAccess = new UserDataAccess();

            if (userDataAccess.Find(user.EmailID) != null)
            {
                return false; //id already registered
            }

            UserEntity newUserEntity = new UserEntity
            {
                EmailID = user.EmailID,
                Password = user.Password,
                FullName = user.FullName
            };

            return userDataAccess.Create(newUserEntity); //return bool

        }

        // TO AUTHENTICATE THE USER
        public bool IsValid(User user)
        {
            //check id and password are correct
            UserDataAccess userDataAccess = new UserDataAccess();

            UserEntity userEntity = new UserEntity
            {
                EmailID = user.EmailID,
                Password = user.Password,
                FullName = user.FullName
            };

            if (!userDataAccess.IsValid(userEntity))
            {
                return false;
            }

            return true;
        }
    }
}
