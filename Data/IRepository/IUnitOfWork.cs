using helping_hand.Models;

using System;
using System.Threading.Tasks;

namespace helping_hand.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Request> Requests { get; }
        IBaseRepository<HelpRequest> HelpRequests { get; }
        IBaseRepository<Urgency> Urgencies { get; }
        IBaseRepository<HelpService> HelpServices { get; }
        IBaseRepository<Notice> Notices { get; }

        Task Save();
    }
}
