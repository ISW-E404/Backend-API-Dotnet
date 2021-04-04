using System;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
