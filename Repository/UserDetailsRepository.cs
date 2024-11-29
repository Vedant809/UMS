using UMS.Models;
using UMS.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace UMS.Repository
{
    public class UserDetailsRepository:IUserDetailsRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserDetailsRepository> _logger;
        public UserDetailsRepository(AppDbContext context,ILogger<UserDetailsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Get the list of Active Users
        public List<UserDetails> GetAll()
        {
            try
            {
                bool isActive = true;
                var users = _context.UserDetails.ToList();
                var activeUsers = users.Where(u => u.IsActive == isActive).ToList();
                _logger.LogInformation($"Users =====>{users.Count}");
                _logger.LogDebug($"Active Users Count============>{activeUsers.Count}");
                return activeUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Delete Method
        //Soft Delete the User
        //Change the IsActive state of the user to false

        public async Task<bool> deleteUser(List<int> users)
        {
            try
            {
                foreach(var user in users)
                {
                    //Used AsNoTracking to avoid conflict 
                    var activeUsers = _context.UserDetails.AsNoTracking().Where(u => u.UserId == user).FirstOrDefault();
                    //activeUsers.IsActive = false;

                    UserDetails details = new UserDetails()
                    {
                        UserId = user,
                        FirstName = activeUsers.FirstName,
                        LastName = activeUsers.LastName,
                        Email = activeUsers.Email,
                        DOB = activeUsers.DOB,
                        Roles = activeUsers.Roles,
                        IsActive = false,   //Set the IsActive Flag to false
                        UserCode = activeUsers.UserCode,
                        Picture = activeUsers.Picture
                    };
                    _context.UserDetails.Update(details);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                _context.Dispose();
            }
        }

        //Create a new user

        public async Task<int> createUser(UserDetails userDetails)
        {
            try
            {
                UserDetails user = new UserDetails()
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Email = userDetails.Email,
                    DOB = userDetails.DOB,
                    Roles = userDetails.Roles,
                    IsActive = true,
                    UserCode = Guid.NewGuid().ToString(),
                    Picture = new byte[] { 0x01, 0x02, 0xFF, 0xAB, 0x45 }
                };
                _context.UserDetails.Add(user);
                await _context.SaveChangesAsync();
                var id = _context.UserDetails.Single(x => x.FirstName == userDetails.FirstName).UserId;
                return id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //Update the user
        public async Task<bool> updateUser(UserDetails userDetails)
        {
            try
            {
                //var data = _context.UserDetails.AsNoTracking().Single(x=>x.Email == userDetails.Email);
                UserDetails user = new UserDetails()
                {
                    UserId = userDetails.UserId,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Email = userDetails.Email,
                    DOB = userDetails.DOB,
                    Roles = userDetails.Roles,
                    IsActive = true,
                    UserCode = Guid.NewGuid().ToString()
                };
                _context.UserDetails.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Search functionality

        public List<UserDetails> getUserByFirstName(string? firstName)
        {
            try
            {
                var oldUser = new List<UserDetails>();
                if (firstName != null)
                {
                    oldUser = _context.UserDetails.Where(x => x.FirstName == firstName).ToList();
                }
                return oldUser;

            }

            catch(Exception ex)
            {
                throw ex;
            }

        }

    }
}
