using DataAccess.DatabaseModels;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess
{
    public class UserDataAccess
    {
        private EventDbContext context = new EventDbContext();
        
        // THIS METHOD IS USED TO CREATE A NEW USER 
        public bool Create(UserEntity newUser)
        {
            newUser.Password = HashPassword(newUser.Password);
            try
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            catch(SqlException e)
            {
                System.Diagnostics.Debug.WriteLine("DATABASE ERROR : " + e.Message);
                return false;
            }

        }

        // THIS METHOD IS USED TO AUTHENTICATE USER BY MATCHING PASSWORD HASH VALUES
        public bool IsValid(UserEntity user)
        {
            UserEntity GetUser = context.Users.Find(user.EmailID);

            var hashedPassword = HashPassword(user.Password);
            if(GetUser!=null)
            {
                if (GetUser.Password == hashedPassword)
                    return true;
            }
            return false;
        }

        //THIS METHOD IS USED TO FIND A USER BY ID
        public UserEntity Find(string userEmailID)
        {
            return context.Users.Find(userEmailID);
        }

        //THIS METHOD IS USED TO GENERATE HASH VALUES FOR USER PASSWORD USING 
        // SHA256 ALGORITHM
        public static string HashPassword(string password, string algorithm = "sha256")
        {
            return Hash(Encoding.UTF8.GetBytes(password), algorithm);
        }

        //THIS METHOD IS USED BY HASHPASSWORD METHOD TO CREATE HASH OF PASSWORDS.
        public static string Hash(byte[] input, string algorithm = "sha256")
        {
            using (var hashAlgorithm = HashAlgorithm.Create(algorithm))
            {
                return Convert.ToBase64String(hashAlgorithm.ComputeHash(input));
            }
        }
    }
    
}
