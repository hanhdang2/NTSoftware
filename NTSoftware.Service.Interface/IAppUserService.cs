using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service.Interface
{
    public interface IAppUserService
    {
        #region GET
        Task<AppUserViewModel> GetById(string id);
        Task<AppUserViewModel> GetByUserName(string userName);
        PagedResult<AppUserViewModel> GetAllPaging(int page, int pageSize);

        #endregion GET

        #region POST

        Task<AppUserViewModel> AddAsync(AppUserViewModel Vm);

        #endregion POST

        #region PUT

        Task UpdateAsync(AppUserViewModel userVm);

        #endregion PUT

        #region DELETE

        Task DeleteUser(string id);

        #endregion DELETE

        #region OTHER_METHOD

        Task RemoveDepartment(int departmentId);
        Task AddDepartment(List<AppUserViewModel> lstVm);

        #endregion OTHER_METHOD
    }
}
