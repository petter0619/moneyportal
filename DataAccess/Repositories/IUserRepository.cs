using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        public int AddNewUser(string authId);
        public AppUser GetUserByAuthId(string authId);
    }
}
