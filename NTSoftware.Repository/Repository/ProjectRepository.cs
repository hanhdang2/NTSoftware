using NTSoftware.Core.Models.Models;
using NTSoftware.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Repository.Repository
{
    public class ProjectRepository :NTRepository<Project,int>,IProjectRepository
    {
        private AppDbContext _appContext;
        public ProjectRepository(AppDbContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
