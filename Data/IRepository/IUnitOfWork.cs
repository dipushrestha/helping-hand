using helping_hand.Model;

using System;
using System.Threading.Tasks;

namespace helping_hand.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Request> Requests { get; }
        Task Save();
    }
}
