using System;
using System.Threading.Tasks;
using CrispArchitecture.Application.Interfaces;

namespace CrispArchitecture.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ITestRepository _testRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITestRepository TestRepository
        {
            get { return _testRepository ??= new TestRepository(_context); }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}