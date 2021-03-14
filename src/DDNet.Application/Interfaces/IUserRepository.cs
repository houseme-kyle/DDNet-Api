using DDNet.Application.Entities;
using System.Threading.Tasks;

namespace DDNet.Application.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<bool> Exists(string emailHash);
        Task<User> Find(string emailHash);
    }
}
