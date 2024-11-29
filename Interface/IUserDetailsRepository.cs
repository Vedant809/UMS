using UMS.Models;

namespace UMS.Interface
{
    public interface IUserDetailsRepository
    {
        public List<UserDetails> GetAll();
        public Task<bool> deleteUser(List<int> users);

        public Task<int> createUser(UserDetails userDetails);

        public Task<bool> updateUser(UserDetails userDetails);
        public List<UserDetails> getUserByFirstName(string? firstName);
    }
}
