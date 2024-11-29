using UMS.Models;

namespace UMS.Interface
{
    public interface IUserRepository
    {
        Task<bool> login(string username, string password);
    }
}
