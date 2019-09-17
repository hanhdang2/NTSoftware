
using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class DetailUserRepository : NTRepository<DetailUser, int>, IDetailUserRepository
    {
        private AppDbContext _appContext;
        public DetailUserRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
