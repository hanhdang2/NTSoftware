using NTSoftware.Core.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository
{
    public class NTUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public NTUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
