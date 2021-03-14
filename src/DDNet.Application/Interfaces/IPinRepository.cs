using DDNet.Application.Entities;
using System.Threading.Tasks;

namespace DDNet.Application.Interfaces
{
    public interface IPinRepository:IBaseRepository<Pin>
    {
        Task<bool> Exists(string pinLookup);
        Task<Pin> Find(string pinLookup);
    }
}
