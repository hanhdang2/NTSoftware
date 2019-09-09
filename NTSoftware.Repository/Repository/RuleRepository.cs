using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;

using System.Text;

namespace NTSoftware.Repository.Repository
{
   public class RuleRepository : NTRepository<Rule,int>, IRuleRepository
    {
        private AppDbContext _appContext;
        public RuleRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
