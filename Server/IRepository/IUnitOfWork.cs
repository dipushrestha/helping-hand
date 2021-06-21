using helping_hand.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helping_hand.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Request> Requests { get; }
        Task Save();
    }
}
