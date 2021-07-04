using helping_hand.Models;

using System;
using System.Threading.Tasks;

namespace helping_hand.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Request> Requests { get; }
        IBaseRepository<HelpRequest> HelpRequests { get; }

        Task Save();
    }
}
