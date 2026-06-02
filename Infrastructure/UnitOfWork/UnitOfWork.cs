using Domain.Model;
using Domain.Repository;
using Infrastructure.Context;
using Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly MainContext _context;
        public UnitOfWork(MainContext context)
        {
            _context = context;
        }

        private IGenericRepository<User>? _users;
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
