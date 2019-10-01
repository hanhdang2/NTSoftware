using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Service.Interface
{
    public interface IDepartmentService
    {
        #region GET
        GenericResult GetById(int id);
        PagedResult<DepartmentViewModel> GetAllPaging(int page, int pageSize);


        #endregion GET

        #region POST

        GenericResult Add(DepartmentViewModel vm);

        #endregion POST

        #region PUT
        GenericResult Update(DepartmentViewModel vm);

        #endregion PUT

        #region DELETE

        GenericResult Delete(int id);

        #endregion DELETE
    }
}
