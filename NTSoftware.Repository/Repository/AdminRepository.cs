using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class AdminRepository :NTRepository<Admin,int> ,IAdminRepository
    {
        private AppDbContext _appContext;
        public AdminRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
