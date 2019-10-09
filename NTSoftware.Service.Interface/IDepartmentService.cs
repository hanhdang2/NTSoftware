using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service.Interface
{
    public interface IDepartmentService
    {
        #region GET
        DetailDepartmentViewModel GetById(int id, int companyId);
        PagedResult<DepartmentViewModel> GetAllPaging(int page, int pageSize, int companyId);


        #endregion GET

        #region POST

        Task<Department> Add(DetailDepartmentViewModel vm);

        #endregion POST

        #region PUT
        Task Update(DetailDepartmentViewModel vm);

        #endregion PUT

        #region DELETE

        void Delete(int id);

        #endregion DELETE
    }
}
