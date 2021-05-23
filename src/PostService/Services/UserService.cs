using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Entities;

namespace PostService.Services
{
    public class UserService : IUserService
    {
        private readonly PostServiceContext _dbContext;

        public UserService(PostServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
           _dbContext.Add(user);
           await _dbContext.SaveChangesAsync();
           return await _dbContext.User.FirstOrDefaultAsync(x => x.ID == user.ID);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.User.FirstOrDefaultAsync(x => x.ID == user.ID);
        }
    }
}
