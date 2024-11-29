using System;
using UMS.Interface;
using UMS.Models;

namespace UMS.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> login(string username, string password)
        {
            var validUser = _context.Users.Where(x => x.email == username)
                .Where(y => y.password == password).FirstOrDefault();
            if (validUser?.email != null && validUser.password != null)
            {
                await loginDetails(username);
                return true;
            }
            else
            {
                return false;
            }
        }

        //Method to insert the login details at the time of login to maintain login history
        public async Task<bool> loginDetails(string username)
        {
            try
            {
                //Adding data in login table
                //When the user logins the table will log the login time and the user email
                Login loginData = new Login()
                {
                    email = username,
                    loginTime = DateTime.Now
                };
                _context.Login.Add(loginData);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _context.Dispose();
            }
        }
    }
}
