using System;
using System.Threading.Tasks;

namespace DDNet.Application.Interfaces
{
    public interface IAuthDispatcher
    {
        Task DispatchPin(string emailAddress, Guid pin);
    }
}
