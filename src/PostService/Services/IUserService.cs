using System.Threading.Tasks;
using PostService.Entities;

namespace PostService.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}
