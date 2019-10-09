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
        #region GET

        ProjectDetailViewModel GetById(int id, int companyId);
        PagedResult<ProjectViewModel> GetAllPaging(int page, int pageSize,int companyId, string description);

        #endregion GET

        #region POST

        Project Add(ProjectDetailViewModel vm);

        #endregion POST

        #region PUT

        void Update(ProjectDetailViewModel Vm);

        #endregion PUT

        #region DELETE

        void Delete(int id);

        #endregion DELETE
    }
}
