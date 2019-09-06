using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
   public interface IProjectService
    {
       
        ProjectViewModel GetById(int id);
        List<ProjectViewModel> GetAll();
        PagedResult<ProjectViewModel> GetAllPaging(int page, int pageSize);
        Project Add(ProjectViewModel vm);
    }
}
