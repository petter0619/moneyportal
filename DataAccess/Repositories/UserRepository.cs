using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public int AddNewUser(string authId)
        {
            //AppUser entityEntry = _context.AppUsers.Add(new AppUser() { AuthId = authId }).Entity;
            
            _context.AppUsers.Add(new AppUser() { AuthId = authId });
            var addedUsers = _context.SaveChanges();

            return addedUsers;
        }

        public AppUser GetUserByAuthId(string authId)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.AuthId == authId);
            return user;
        }
    }
}
