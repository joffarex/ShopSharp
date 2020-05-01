using System.Threading.Tasks;

namespace ShopSharp.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task<bool> CreateManagerUser(string username, string password);
    }
}