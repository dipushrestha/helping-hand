using System;
using System.Threading.Tasks;

using helping_hand.Models;
using helping_hand.Data.IRepository;

namespace helping_hand.Data.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IBaseRepository<Request> _requests;
        private IBaseRepository<HelpRequest> _helpRequests;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<Request> Requests => _requests ??= new BaseRepository<Request>(_dbContext);
        public IBaseRepository<HelpRequest> HelpRequests => _helpRequests ??= new BaseRepository<HelpRequest>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
