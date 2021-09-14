using onlineShop.Entity;
using onlineShop.Model;
using System.Threading.Tasks;

namespace onlineShop.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(UserEntity user, string password);
        Task<ServiceResponse<string>> Login(string Name, string password);
        Task<bool> UserExits(string Name);
    }
}