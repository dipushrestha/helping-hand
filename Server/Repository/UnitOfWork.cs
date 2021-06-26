using helping_hand.Models;
using helping_hand.Server.Data;
using helping_hand.Server.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helping_hand.Server.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private IBaseRepository<Request> _requests;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IBaseRepository<Request> Requests => _requests ??= new BaseRepository<Request>(_databaseContext);

        public void Dispose()
        {
            _databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
