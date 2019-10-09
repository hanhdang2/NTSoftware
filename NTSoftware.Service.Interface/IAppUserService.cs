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
        Task<GenericResult> GetById(string id);
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
    }
}
