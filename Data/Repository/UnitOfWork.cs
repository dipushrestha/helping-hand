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
        private IBaseRepository<Urgency> _urgencies;
        private IBaseRepository<HelpService> _helpServices;
        private IBaseRepository<Notice> _notices;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<Request> Requests => _requests ??= new BaseRepository<Request>(_dbContext);
        public IBaseRepository<HelpRequest> HelpRequests => _helpRequests ??= new BaseRepository<HelpRequest>(_dbContext);
        public IBaseRepository<Urgency> Urgencies => _urgencies ??= new BaseRepository<Urgency>(_dbContext);
        public IBaseRepository<HelpService> HelpServices => _helpServices ??= new BaseRepository<HelpService>(_dbContext);
        public IBaseRepository<Notice> Notices => _notices ??= new BaseRepository<Notice>(_dbContext);

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
